using System;
using System.Drawing;

namespace ii.InfinityEngine.Files
{
    [Serializable]
    public class MosFile : IEFile
    {
        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private IEFileType fileType = IEFileType.Mos;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public Int16 Columns { get; set; }
        public Int16 Rows { get; set; }
        public Int32 BlockSize { get; set; }
        public Image Image { get; set; }
    }
}