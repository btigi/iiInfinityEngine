using System.Collections.Generic;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Readers.Interfaces;

namespace iiInfinityEngine.Core.Readers
{
    public class TlkFileBinaryReader : ITlkFileReader
    {
        public TlkFile Read(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var f = Read(fs);
                f.Filename = Path.GetFileName(filename);
                return f;
            }
        }

        public TlkFile Read(Stream s)
        {
            using (BinaryReader br = new BinaryReader(s))
            {
                var tlkFile = ParseFile(br);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                tlkFile.OriginalFile = ParseFile(br);
                return tlkFile;
            }
        }

        private TlkFile ParseFile(BinaryReader br)
        {
            var header = (TlkHeaderBinary)Common.ReadStruct(br, typeof(TlkHeaderBinary));

            List<TlkEntryBinary> stringDataEntries = new List<TlkEntryBinary>();
            List<string> stringEntries = new List<string>();

            br.BaseStream.Seek(18, SeekOrigin.Begin);
            for (int i = 0; i < header.StringCount; i++)
            {
                var stringDataEntry = (TlkEntryBinary)Common.ReadStruct(br, typeof(TlkEntryBinary));
                stringDataEntries.Add(stringDataEntry);
            }

            br.BaseStream.Seek(header.StringOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.StringCount; i++)
            {
                var stringEntry = br.ReadChars(stringDataEntries[i].StringLength);
                stringEntries.Add(new string(stringEntry));
            }

            TlkFile tlk = new TlkFile();
            tlk.LangugeId = header.LanguageId;

            int stringIndex = 0;
            foreach (var data in stringDataEntries)
            {
                var stringInfo = new StringEntry();
                stringInfo.Strref = stringIndex;
                stringInfo.Flags = (StringEntryType)data.Flags;
                stringInfo.PitchVariance = data.PitchVariance;
                stringInfo.Sound = data.Sound.ToString();
                stringInfo.Text = stringEntries[stringIndex];
                stringInfo.VolumeVariance = data.VolumeVariance;
                tlk.Strings.Add(stringInfo);
                stringIndex++;
            }

            tlk.Checksum = MD5HashGenerator.GenerateKey(tlk);
            return tlk;
        }
    }
}