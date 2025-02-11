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
        private readonly IEFileType fileType = IEFileType.Tlk;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public Int16 LangugeId;
        public List<StringEntry> Strings = [];
    }

    [Serializable]
    public class StringEntry
    {
        public string Text;
        public StringEntryType Flags;
        public string Sound;
        public Int32 VolumeVariance;
        public Int32 PitchVariance;
        public Int32 Strref;
    }
}