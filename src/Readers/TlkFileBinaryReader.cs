using System.Collections.Generic;
using System.IO;
using System.Text;
using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Readers.Interfaces;

namespace ii.InfinityEngine.Readers
{
    public class TlkFileBinaryReader : ITlkFileReader
    {
        public TlkFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public TlkFile Read(Stream s)
        {
            using var br = new BinaryReader(s);
            var tlkFile = ParseFile(br);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            tlkFile.OriginalFile = ParseFile(br);
            return tlkFile;
        }

        private TlkFile ParseFile(BinaryReader br)
        {
            var header = (TlkHeaderBinary)Common.ReadStruct(br, typeof(TlkHeaderBinary));

            var stringDataEntries = new List<TlkEntryBinary>();
            var stringEntries = new List<string>();

            br.BaseStream.Seek(18, SeekOrigin.Begin);
            for (int i = 0; i < header.StringCount; i++)
            {
                var stringDataEntry = (TlkEntryBinary)Common.ReadStruct(br, typeof(TlkEntryBinary));
                stringDataEntries.Add(stringDataEntry);
            }

            br.BaseStream.Seek(header.StringOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.StringCount; i++)
            {
                var stringEntryBytes = br.ReadBytes(stringDataEntries[i].StringLength);
                var stringEntry = Encoding.UTF8.GetString(stringEntryBytes);
                stringEntries.Add(new string(stringEntry));
            }

            var tlk = new TlkFile();
            tlk.LangugeId = header.LanguageId;

            int stringIndex = 0;
            foreach (var data in stringDataEntries)
            {
                var stringInfo = new StringEntry();
                stringInfo.Strref = stringIndex;
                stringInfo.Flags.HasText = (data.Flags & Common.Bit0) != 0;
                stringInfo.Flags.HasSound = (data.Flags & Common.Bit1) != 0;
                stringInfo.Flags.HasToken = (data.Flags & Common.Bit2) != 0;
                stringInfo.Flags.Bit3 = (data.Flags & Common.Bit3) != 0;
                stringInfo.Flags.Bit4 = (data.Flags & Common.Bit4) != 0;
                stringInfo.Flags.Bit5 = (data.Flags & Common.Bit5) != 0;
                stringInfo.Flags.Bit6 = (data.Flags & Common.Bit6) != 0;
                stringInfo.Flags.Bit7 = (data.Flags & Common.Bit7) != 0;
                stringInfo.Flags.Bit8 = (data.Flags & Common.Bit8) != 0;
                stringInfo.Flags.Bit9 = (data.Flags & Common.Bit9) != 0;
                stringInfo.Flags.Bit10 = (data.Flags & Common.Bit10) != 0;
                stringInfo.Flags.Bit11 = (data.Flags & Common.Bit11) != 0;
                stringInfo.Flags.Bit12 = (data.Flags & Common.Bit12) != 0;
                stringInfo.Flags.Bit13 = (data.Flags & Common.Bit13) != 0;
                stringInfo.Flags.Bit14 = (data.Flags & Common.Bit14) != 0;
                stringInfo.Flags.Bit15 = (data.Flags & Common.Bit15) != 0;
                stringInfo.PitchVariance = data.PitchVariance;
                stringInfo.Sound = data.Sound;
                stringInfo.Text = stringEntries[stringIndex];
                stringInfo.VolumeVariance = data.VolumeVariance;
                tlk.Strings.Add(stringInfo);
                stringIndex++;
            }

            tlk.Checksum = HashGenerator.GenerateKey(tlk);
            return tlk;
        }
    }
}