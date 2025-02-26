using System.IO;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Readers.Interfaces;

namespace iiInfinityEngine.Core.Readers
{
    public class LuaFileReader : ILuaFileReader
    {
        public LuaFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public LuaFile Read(Stream s)
        {
            using var rdr = new StreamReader(s);
            var file = Parse(rdr);
            rdr.BaseStream.Seek(0, SeekOrigin.Begin);
            file.OriginalFile = Parse(rdr);
            return file;
        }

        private LuaFile Parse(StreamReader rdr)
        {
            var str = rdr.ReadToEnd();
            var file = new LuaFile();
            file.Contents = str;
            file.Checksum = HashGenerator.GenerateKey(file);
            return file;
        }
    }
}