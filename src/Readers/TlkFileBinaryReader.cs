using System;
using System.Buffers.Binary;
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
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 1 << 16, FileOptions.SequentialScan);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public TlkFile Read(Stream s)
        {
            using var br = new BinaryReader(s);
            return ParseFile(br);
        }

        private TlkFile ParseFile(BinaryReader br)
        {
            var header = (TlkHeaderBinary)Common.ReadStruct(br, typeof(TlkHeaderBinary));

            const int entrySize = 26;
            br.BaseStream.Seek(18, SeekOrigin.Begin);
            var entriesBlock = br.ReadBytes(header.StringCount * entrySize);

            var totalStringBytes = 0;
            for (var i = 0; i < header.StringCount; i++)
            {
                totalStringBytes += BinaryPrimitives.ReadInt32LittleEndian(entriesBlock.AsSpan(i * entrySize + 22));
            }

            br.BaseStream.Seek(header.StringOffset, SeekOrigin.Begin);
            var stringData = br.ReadBytes(totalStringBytes);

            var tlk = new TlkFile();
            tlk.LangugeId = header.LanguageId;
            tlk.Strings.Capacity = header.StringCount;

            int stringDataOffset = 0;
            for (int i = 0; i < header.StringCount; i++)
            {
                int entryOffset = i * entrySize;
                var flags = BinaryPrimitives.ReadInt16LittleEndian(entriesBlock.AsSpan(entryOffset));
                var sound = new array8(entriesBlock.AsSpan(entryOffset + 2, 8));
                var volumeVariance = BinaryPrimitives.ReadInt32LittleEndian(entriesBlock.AsSpan(entryOffset + 10));
                var pitchVariance = BinaryPrimitives.ReadInt32LittleEndian(entriesBlock.AsSpan(entryOffset + 14));
                var stringLength = BinaryPrimitives.ReadInt32LittleEndian(entriesBlock.AsSpan(entryOffset + 22));

                var text = stringLength > 0
                    ? Encoding.UTF8.GetString(stringData, stringDataOffset, stringLength)
                    : string.Empty;
                stringDataOffset += stringLength;

                var stringInfo = new StringEntry();
                stringInfo.Strref = i;
                stringInfo.Flags.HasText = (flags & Common.Bit0) != 0;
                stringInfo.Flags.HasSound = (flags & Common.Bit1) != 0;
                stringInfo.Flags.HasToken = (flags & Common.Bit2) != 0;
                stringInfo.Flags.Bit3 = (flags & Common.Bit3) != 0;
                stringInfo.Flags.Bit4 = (flags & Common.Bit4) != 0;
                stringInfo.Flags.Bit5 = (flags & Common.Bit5) != 0;
                stringInfo.Flags.Bit6 = (flags & Common.Bit6) != 0;
                stringInfo.Flags.Bit7 = (flags & Common.Bit7) != 0;
                stringInfo.Flags.Bit8 = (flags & Common.Bit8) != 0;
                stringInfo.Flags.Bit9 = (flags & Common.Bit9) != 0;
                stringInfo.Flags.Bit10 = (flags & Common.Bit10) != 0;
                stringInfo.Flags.Bit11 = (flags & Common.Bit11) != 0;
                stringInfo.Flags.Bit12 = (flags & Common.Bit12) != 0;
                stringInfo.Flags.Bit13 = (flags & Common.Bit13) != 0;
                stringInfo.Flags.Bit14 = (flags & Common.Bit14) != 0;
                stringInfo.Flags.Bit15 = (flags & Common.Bit15) != 0;
                stringInfo.PitchVariance = pitchVariance;
                stringInfo.Sound = sound;
                stringInfo.Text = text;
                stringInfo.VolumeVariance = volumeVariance;
                tlk.Strings.Add(stringInfo);
            }

            tlk.Checksum = HashGenerator.GenerateKey(tlk);
            return tlk;
        }
    }
}