using System.IO;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;
using System;

namespace iiInfinityEngine.Core.Writers
{
    public class DFileWriter : IDFileWriter
    {
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (!(file is DlgFile))
                throw new ArgumentException("File is not a valid D file");

            var dlg = file as DlgFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(dlg) == dlg.Checksum))
                return false;

            if (BackupManger != null)
            {
                BackupManger.BackupFile(file, file.Filename, file.FileType, this);
            }

            var str = String.Empty;
            foreach (var state in dlg.states)
            {
                str += String.Format("IF {0}~{1}~ THEN BEGIN {2}{3}", state.Weight != 0 ? String.Format("WEIGHT #{0}{1}", state.Weight, System.Environment.NewLine) : String.Empty, state.Trigger, state.StateNumber, System.Environment.NewLine);
                str += String.Format("  SAY #{0} /* ~{1}~{2} */{3}", state.ResponseText.Strref, state.ResponseText.Text, !String.IsNullOrEmpty(state.ResponseText.Sound) ? String.Format(" [{0}]", state.ResponseText.Sound) : String.Empty, System.Environment.NewLine);
                foreach (var transition in state.transitions)
                {
                    str += String.Format("  IF ~{0}~ THEN {1}{2}{3}{4}",
                        transition.Trigger,
                        transition.HasText ? String.Format("REPLY #{0} /* ~{1}~{2} */ ", transition.TransitionText.Strref, transition.TransitionText.Text, !String.IsNullOrEmpty(transition.TransitionText.Sound) ? String.Format(" [{0}]", transition.TransitionText.Sound) : String.Empty) : String.Empty,
                        transition.HasAction ? String.Format("DO ~{0}~ ", transition.Action) : String.Empty,
                        !transition.TerminateDialog ? transition.Dialog.ToLower() == Path.ChangeExtension(dlg.Filename.ToLower(), null) ? String.Format("GOTO {0}", transition.NextState) : String.Format("EXTERN ~{0}~ {1}", transition.Dialog, transition.NextState) : "EXIT",
                        System.Environment.NewLine);
                }
                str += "END";
                str += System.Environment.NewLine;
                str += System.Environment.NewLine;
            }
            File.WriteAllText(filename, str);

            return true;
        }
    }
}