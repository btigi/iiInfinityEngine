using System.IO;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Readers.Interfaces;

namespace iiInfinityEngine.Core.Readers
{
    public class IdsFileReader : IIdsFileReader
    {
        public IdsFile Read(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var f = Read(fs);
                f.Filename = Path.GetFileName(filename);
                return f;
            }
        }

        public IdsFile Read(Stream s)
        {
            using (StreamReader rdr = new StreamReader(s))
            {
                var file = Parse(rdr);
                s.Seek(0, SeekOrigin.Begin);
                file.OriginalFile = Parse(rdr);
                return file;
            }
        }

        private IdsFile Parse(StreamReader rdr)
        {
            if (rdr.BaseStream.ReadByte() == 0xff && rdr.BaseStream.ReadByte() == 0xff)
            {
                byte[] xorKey = {0x88, 0xa8, 0x8f, 0xba, 0x8a, 0xd3, 0xb9, 0xf5, 0xed, 0xb1, 0xcf, 0xea, 0xaa, 0xe4, 0xb5, 0xfb,
                                 0xeb, 0x82, 0xf9, 0x90, 0xca, 0xc9, 0xb5, 0xe7, 0xdc, 0x8e, 0xb7, 0xac, 0xee, 0xf7, 0xe0, 0xca,
                                 0x8e, 0xea, 0xca, 0x80, 0xce, 0xc5, 0xad, 0xb7, 0xc4, 0xd0, 0x84, 0x93, 0xd5, 0xf0, 0xeb, 0xc8,
                                 0xb4, 0x9d, 0xcc, 0xaf, 0xa5, 0x95, 0xba, 0x99, 0x87, 0xd2, 0x9d, 0xe3, 0x91, 0xba, 0x90, 0xca};

                const int FileMarkerPrefixLength = 2; // 0xff 0xff

                char[] chArray = new char[rdr.BaseStream.Length - FileMarkerPrefixLength];
                for (int i = 0; i < rdr.BaseStream.Length - FileMarkerPrefixLength; i++)
                {
                    chArray[i] = (char)(rdr.BaseStream.ReadByte() ^ xorKey[i % 64]);
                }

                var file = new IdsFile();
                file.contents = new string(chArray);
                file.Checksum = MD5HashGenerator.GenerateKey(file);
                return file;
            }
            else
            {
                rdr.BaseStream.Position = 0;

                string str = rdr.ReadToEnd();
                var file = new IdsFile();
                file.contents = str;
                file.Checksum = MD5HashGenerator.GenerateKey(file);
                return file;
            }
        }
    }
}