using System.IO;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Readers.Interfaces;

namespace iiInfinityEngine.Core.Readers
{
    public class MenuFileReader : IMenuFileReader
    {
        public MenuFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public MenuFile Read(Stream s)
        {
            using var rdr = new StreamReader(s);
            var file = Parse(rdr);
            rdr.BaseStream.Seek(0, SeekOrigin.Begin);
            file.OriginalFile = Parse(rdr);
            return file;
        }

        private MenuFile Parse(StreamReader rdr)
        {
            var str = rdr.ReadToEnd();
            var file = new MenuFile();
            file.Contents = str;
            file.Checksum = HashGenerator.GenerateKey(file);
            return file;
        }
    }
}