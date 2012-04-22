using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class DimensionalArrayFile : IEFile
    {
        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private IEFileType fileType = IEFileType.DimensionalArray;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public string contents;
    }
}