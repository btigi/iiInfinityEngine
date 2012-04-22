using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class TlkFile : IEFile
    {
        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private IEFileType fileType = IEFileType.Tlk;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public Int16 LangugeId;
        public List<StringEntry> Strings = new List<StringEntry>();
    }

    [Serializable]
    public class StringEntry
    {
        public string Text { get; set; }
        public StringEntryType Flags { get; set; }
        public string Sound { get; set; }
        public Int32 VolumeVariance { get; set; }
        public Int32 PitchVariance { get; set; }
        public Int32 Strref { get; set; }
    }
}