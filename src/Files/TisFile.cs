using System;

namespace ii.InfinityEngine.Files
{
    [Serializable]
    public class TisFile : IEFile
    {
        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private IEFileType fileType = IEFileType.Tis;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public Int32 TileCount { get; set; }
        public Int32 TileLength { get; set; }
        public Int32 TileDimension { get; set; }
    }
}