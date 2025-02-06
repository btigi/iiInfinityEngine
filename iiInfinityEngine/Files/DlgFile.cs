using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class DlgFile : IEFile
    {
        public List<State2> states = new List<State2>();

        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private IEFileType fileType = IEFileType.Dlg;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public HeaderFlags Flags;
    }

    [Serializable]
    public class State2
    {
        public Int32 Weight;
        public Int32 StateNumber;
        public string Trigger;
        public IEString ResponseText;
        public string SymbolicName;
        public List<Transition2> transitions = new List<Transition2>();
    }

    [Serializable]
    public class Transition2
    {
        public string Trigger;
        public IEString TransitionText;
        public IEString JournalText;
        public string Dialog;
        public Int32 NextState;
        public string NextStateSymbolicName;
        public string Action;
        public bool HasText { get; set; }
        public bool HasTrigger { get; set; }
        public bool HasAction { get; set; }
        public bool TerminateDialog { get; set; }
        public bool HasJouranl { get; set; }
        public bool Unknown { get; set; }
        public bool AddQuestJournalEntry { get; set; }
        public bool RemoveQuestJournalEntry { get; set; }
        public bool AddQuestCompleteJournalEntry { get; set; }
    }

    [Serializable]
    public struct HeaderFlags
    {
        public bool Enemy { get; set; }
        public bool EscapeArea { get; set; }
        public bool Nothing { get; set; }
        public bool Bit03 { get; set; }
        public bool Bit04 { get; set; }
        public bool Bit05 { get; set; }
        public bool Bit06 { get; set; }
        public bool Bit07 { get; set; }
        public bool Bit08 { get; set; }
        public bool Bit09 { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
        public bool Bit16 { get; set; }
        public bool Bit17 { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool Bit20 { get; set; }
        public bool Bit21 { get; set; }
        public bool Bit22 { get; set; }
        public bool Bit23 { get; set; }
        public bool Bit24 { get; set; }
        public bool Bit25 { get; set; }
        public bool Bit26 { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool Bit31 { get; set; }
    }
}