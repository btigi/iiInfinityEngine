using System.IO;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Readers.Interfaces;

namespace iiInfinityEngine.Core.Readers
{
    public class DimensionalArrayFileReader : IDimensionalArrayFileReader
    {
        public DimensionalArrayFile Read(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var f = Read(fs);
                f.Filename = Path.GetFileName(filename);
                return f;
            }
        }

        public DimensionalArrayFile Read(Stream s)
        {
            using (StreamReader rdr = new StreamReader(s))
            {
                var file = Parse(rdr);
                rdr.BaseStream.Seek(0, SeekOrigin.Begin);
                file.OriginalFile = Parse(rdr);
                return file;
            }
        }

        private DimensionalArrayFile Parse(StreamReader rdr)
        {
            string str = rdr.ReadToEnd();
            var file = new DimensionalArrayFile();
            file.contents = str;
            file.Checksum = MD5HashGenerator.GenerateKey(file);
            return file;
        }
    }
}