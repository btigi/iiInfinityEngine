using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace iiInfinityEngine.Core.Writers
{
    public class DlgFileBinaryWriter : IDlgFileWriter
    {
        const int HeaderSize = 52;
        const int StateSize = 16;
        const int StateTriggerSize = 8;
        const int TransitionSize = 32;
        const int TransitionTriggerSize = 8;
        const int ActionSize = 8;

        public TlkFile TlkFile { get; set; }
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not DlgFile)
                throw new ArgumentException("File is not a valid dlg file");

            var dlgFile = file as DlgFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(dlgFile) == dlgFile.Checksum))
                return false;

            List<StateBinary> states = [];
            List<TransitionBinary> transitions = [];
            List<StateTriggerBinary> stateTriggers = [];
            List<TransitionTriggerBinary> transitionTriggers = [];
            List<ActionTableBinary> actions = [];
            List<string> stateTriggerText = [];
            List<int> stateTriggerTextAssociatedData = [];
            List<string> transitionTriggerText = [];
            List<int> transitionTriggerTextAssociatedData = [];
            List<string> actionText = [];
            List<int> actionTextAssociatedData = [];

            //TODO: Resolve symbolic links

            foreach (var state in dlgFile.states)
            {
                foreach (var transition in state.transitions)
                {
                    if (transition.HasAction)
                    {
                        var offset = 0;
                        for (int i = 0; i < actionText.Count; i++)
                        {
                            offset += actionText[i].Length;
                        }
                        actionTextAssociatedData.Add(offset);
                         
                        ActionTableBinary action = new ActionTableBinary();
                        action.ActionTableStringLength = transition.Action.Length;
                        action.ActionTableStringOffset = 0;
                        actions.Add(action);
                        actionText.Add(transition.Action);
                    }

                    if (transition.HasTrigger)
                    {
                        var offset = 0;
                        for (int i = 0; i < transitionTriggerText.Count; i++)
                        {
                            offset += transitionTriggerText[i].Length;
                        }
                        transitionTriggerTextAssociatedData.Add(offset);

                        var transitionTrigger = new TransitionTriggerBinary();
                        transitionTrigger.TransitionTriggerStringLength = transition.Trigger.Length;
                        transitionTrigger.TransitionTriggerStringOffset = 0;
                        transitionTriggers.Add(transitionTrigger);
                        transitionTriggerText.Add(transition.Trigger);
                    }

                    var transitionBinary = new TransitionBinary();
                    transitionBinary.ActionIndex = transition.HasAction ? actions.Count - 1 : -1;
                    transitionBinary.Dialog = transition.Dialog;
                    transitionBinary.Flags = transition.HasText ? transitionBinary.Flags | Common.Bit0 : transitionBinary.Flags;
                    transitionBinary.Flags = transition.HasTrigger ? transitionBinary.Flags | Common.Bit1 : transitionBinary.Flags;
                    transitionBinary.Flags = transition.HasAction ? transitionBinary.Flags | Common.Bit2 : transitionBinary.Flags;
                    transitionBinary.Flags = transition.TerminateDialog ? transitionBinary.Flags | Common.Bit3 : transitionBinary.Flags;
                    transitionBinary.Flags = transition.HasJouranl ? transitionBinary.Flags | Common.Bit4 : transitionBinary.Flags;
                    transitionBinary.Flags = transition.Interrupt ? transitionBinary.Flags | Common.Bit5 : transitionBinary.Flags;
                    transitionBinary.Flags = transition.RemoveQuestJournalEntry ? transitionBinary.Flags | Common.Bit6 : transitionBinary.Flags;
                    transitionBinary.Flags = transition.AddQuestCompleteJournalEntry ? transitionBinary.Flags | Common.Bit7 : transitionBinary.Flags;
                    //TODO: bits 8 through 31
                    transitionBinary.JournalText = Common.WriteString(transition.JournalText, TlkFile);
                    transitionBinary.NextState = transition.NextState;
                    transitionBinary.TransitionText = Common.WriteString(transition.TransitionText, TlkFile);
                    transitionBinary.TransitionTrigger = transition.HasTrigger ? transitionTriggers.Count - 1 : -1;
                    transitions.Add(transitionBinary);
                }

                if (!String.IsNullOrEmpty(state.Trigger))
                {
                    var offset = 0;
                    for (int i = 0; i < stateTriggerText.Count; i++)
                    {
                        offset += stateTriggerText[i].Length;
                    }
                    stateTriggerTextAssociatedData.Add(offset);

                    var stateTrigger = new StateTriggerBinary();
                    stateTrigger.StateTriggerStringLength = state.Trigger.Length;
                    stateTrigger.StateTriggerStringOffset = 0;
                    stateTriggers.Add(stateTrigger);
                    stateTriggerText.Add(state.Trigger);
                }

                var stateBinary = new StateBinary();
                stateBinary.ResponseText = Common.WriteString(state.ResponseText, TlkFile);
                stateBinary.TransitionCount = state.transitions.Count;
                stateBinary.TransitionIndex = state.transitions.Count > 0 ? transitions.Count - state.transitions.Count : -1;
                stateBinary.TriggerIndex = !String.IsNullOrEmpty(state.Trigger) ? stateTriggers.Count - 1 : -1;
                states.Add(stateBinary);
            }

            var header = new DlgHeaderBinary();
            header.Flags = dlgFile.Flags.Enemy ? header.Flags | Common.Bit0 : header.Flags;
            header.Flags = dlgFile.Flags.EscapeArea ? header.Flags | Common.Bit1 : header.Flags;
            header.Flags = dlgFile.Flags.Nothing ? header.Flags | Common.Bit2 : header.Flags;
            header.Flags = dlgFile.Flags.Bit03 ? header.Flags | Common.Bit3 : header.Flags;
            header.Flags = dlgFile.Flags.Bit04 ? header.Flags | Common.Bit4 : header.Flags;
            header.Flags = dlgFile.Flags.Bit05 ? header.Flags | Common.Bit5 : header.Flags;
            header.Flags = dlgFile.Flags.Bit06 ? header.Flags | Common.Bit6 : header.Flags;
            header.Flags = dlgFile.Flags.Bit07 ? header.Flags | Common.Bit7 : header.Flags;
            header.Flags = dlgFile.Flags.Bit08 ? header.Flags | Common.Bit8 : header.Flags;
            header.Flags = dlgFile.Flags.Bit09 ? header.Flags | Common.Bit9 : header.Flags;
            header.Flags = dlgFile.Flags.Bit10 ? header.Flags | Common.Bit10 : header.Flags;
            header.Flags = dlgFile.Flags.Bit11 ? header.Flags | Common.Bit11 : header.Flags;
            header.Flags = dlgFile.Flags.Bit12 ? header.Flags | Common.Bit12 : header.Flags;
            header.Flags = dlgFile.Flags.Bit13 ? header.Flags | Common.Bit13 : header.Flags;
            header.Flags = dlgFile.Flags.Bit14 ? header.Flags | Common.Bit14 : header.Flags;
            header.Flags = dlgFile.Flags.Bit15 ? header.Flags | Common.Bit15 : header.Flags;
            header.Flags = dlgFile.Flags.Bit16 ? header.Flags | Common.Bit16 : header.Flags;
            header.Flags = dlgFile.Flags.Bit17 ? header.Flags | Common.Bit17 : header.Flags;
            header.Flags = dlgFile.Flags.Bit18 ? header.Flags | Common.Bit18 : header.Flags;
            header.Flags = dlgFile.Flags.Bit19 ? header.Flags | Common.Bit19 : header.Flags;
            header.Flags = dlgFile.Flags.Bit20 ? header.Flags | Common.Bit20 : header.Flags;
            header.Flags = dlgFile.Flags.Bit21 ? header.Flags | Common.Bit21 : header.Flags;
            header.Flags = dlgFile.Flags.Bit22 ? header.Flags | Common.Bit22 : header.Flags;
            header.Flags = dlgFile.Flags.Bit23 ? header.Flags | Common.Bit23 : header.Flags;
            header.Flags = dlgFile.Flags.Bit24 ? header.Flags | Common.Bit24 : header.Flags;
            header.Flags = dlgFile.Flags.Bit25 ? header.Flags | Common.Bit25 : header.Flags;
            header.Flags = dlgFile.Flags.Bit26 ? header.Flags | Common.Bit26 : header.Flags;
            header.Flags = dlgFile.Flags.Bit27 ? header.Flags | Common.Bit27 : header.Flags;
            header.Flags = dlgFile.Flags.Bit28 ? header.Flags | Common.Bit28 : header.Flags;
            header.Flags = dlgFile.Flags.Bit29 ? header.Flags | Common.Bit29 : header.Flags;
            header.Flags = dlgFile.Flags.Bit30 ? header.Flags | Common.Bit30 : header.Flags;
            header.Flags = dlgFile.Flags.Bit31 ? header.Flags | Common.Bit31 : header.Flags;
            header.ftype = new array4() { character1 = 'D', character2 = 'L', character3 = 'G', character4 = ' ' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = '.', character4 = '0' };
            header.StateCount = states.Count;
            header.StateOffset = HeaderSize;
            header.TransitionCount = transitions.Count;
            header.TransitionOffset = HeaderSize + (states.Count * StateSize);
            header.StateTriggerOffset = HeaderSize + (states.Count * StateSize) + (transitions.Count * TransitionSize);
            header.StateTriggerCount = stateTriggers.Count;
            header.TransitionTriggerOffset = HeaderSize + (states.Count * StateSize) + (transitions.Count * TransitionSize) + (stateTriggers.Count * StateTriggerSize);
            header.TransitionTriggerCount = transitionTriggers.Count;
            header.ActionTableOffset = HeaderSize + (states.Count * StateSize) + (transitions.Count * TransitionSize) + (stateTriggers.Count * StateTriggerSize) + (transitionTriggers.Count * TransitionTriggerSize);
            header.ActionTableCount = actions.Count;

            using var s = new MemoryStream();
            using var bw = new BinaryWriter(s);
            var headerAsBytes = Common.WriteStruct(header);

            bw.Write(headerAsBytes);

            foreach (var state in states)
            {
                var stateAsBytes = Common.WriteStruct(state);
                bw.Write(stateAsBytes);
            }

            foreach (var transition in transitions)
            {
                var transitionAsBytes = Common.WriteStruct(transition);
                bw.Write(transitionAsBytes);
            }

            for (int i = 0; i < stateTriggers.Count; i++)
            {
                var stateTrigger = stateTriggers[i];
                stateTrigger.StateTriggerStringOffset = header.ActionTableOffset + (actions.Count * ActionSize) + stateTriggerTextAssociatedData[i];
                var statetriggerAsBytes = Common.WriteStruct(stateTrigger);
                bw.Write(statetriggerAsBytes);
            }

            for (int i = 0; i < transitionTriggers.Count; i++)
            {
                var transitionTrigger = transitionTriggers[i];
                transitionTrigger.TransitionTriggerStringOffset = header.ActionTableOffset + (actions.Count * ActionSize) + (stateTriggers.Count * StateTriggerSize) + transitionTriggerTextAssociatedData[i];
                var transitionTriggerAsBytes = Common.WriteStruct(transitionTrigger);
                bw.Write(transitionTriggerAsBytes);
            }

            for (int i = 0; i < actions.Count; i++)
            {
                var action = actions[i];
                action.ActionTableStringOffset = header.ActionTableOffset + (actions.Count * ActionSize) + (stateTriggers.Count * StateTriggerSize) + (transitionTriggers.Count * TransitionTriggerSize) + actionTextAssociatedData[i];
                var actionAsBytes = Common.WriteStruct(action);
                bw.Write(actionAsBytes);
            }

            var encoding = new UTF8Encoding();
            foreach (var str in stateTriggerText)
            {
                bw.Write(encoding.GetBytes(str));
            }

            foreach (var str in transitionTriggerText)
            {
                bw.Write(encoding.GetBytes(str));
            }

            foreach (var str in actionText)
            {
                bw.Write(encoding.GetBytes(str));
            }

            if (BackupManger != null)
            {
                BackupManger.BackupFile(file, file.Filename, file.FileType, this);
            }

            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            bw.BaseStream.Position = 0;
            bw.BaseStream.CopyTo(fs);
            fs.Flush(flushToDisk: true);
            return true;
        }
    }
}