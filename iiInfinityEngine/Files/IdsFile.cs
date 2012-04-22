using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class IdsFile : IEFile
    {
        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private IEFileType fileType = IEFileType.Ids;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public string contents;

        public void Append(string line)
        {
            contents = contents.Trim() + System.Environment.NewLine;
            contents += line;
        }

        public bool AppendUnless(string line)
        {
            Regex r = new Regex(@"\s"); // Remove whitespace
            var tempFile = r.Replace(contents, String.Empty).ToLower();
            var tempLine = r.Replace(line, String.Empty).ToLower();

            if (tempFile.Contains(tempLine))
            {
                return false;
            }

            contents = contents.Trim() + System.Environment.NewLine;
            contents += line;
            return true;
        }

        public void Remove(string line)
        {
            contents = contents.Replace(line, String.Empty);
        }
    }
}