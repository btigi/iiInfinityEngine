using System;

namespace ii.InfinityEngine.Files
{
    [Serializable]
    public class VefFile : IEFile
    {
        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private readonly IEFileType fileType = IEFileType.Vvc;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        VefBody VefComponent1 { get; set; }
        VefBody VefComponent2 { get; set; }
    }

    [Serializable]
    public class VefBody
    {
        public int TicksUntilStart { get; set; }
        public int Unused4 { get; set; }
        public int TicksUntilLoop { get; set; }
        public int ResourceType { get; set; }
        public array8 ResRef { get; set; }
        public int ContinuousCycles { get; set; }
        public array124 Unused1c { get; set; }
    }
}