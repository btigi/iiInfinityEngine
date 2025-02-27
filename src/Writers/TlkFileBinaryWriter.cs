using System;
using System.Collections.Generic;
using System.IO;
using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Writers.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace ii.InfinityEngine.Writers
{
    public class TlkFileBinaryWriter : ITlkFileWriter
    {
        const int HeaderSize = 18;
        const int StringInfoSize = 26;

        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not TlkFile)
                throw new ArgumentException("File is not a valid tlk file");

            var tlkFile = file as TlkFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(tlkFile) == tlkFile.Checksum))
                return false;

            var stringEntries = new List<TlkEntryBinary>();
            var strings = new List<String>();

            var header = new TlkHeaderBinary();
            header.ftype = new array4() { character1 = 'T', character2 = 'L', character3 = 'K', character4 = ' ' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = ' ', character4 = ' ' };
            header.LanguageId = tlkFile.LangugeId;
            header.StringCount = tlkFile.Strings.Count;
            header.StringOffset = HeaderSize + (StringInfoSize * tlkFile.Strings.Count);

            foreach (var stringInfo in tlkFile.Strings)
            {
                var stringInfoBinary = new TlkEntryBinary();
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.HasText ? stringInfoBinary.Flags | Common.Bit0 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.HasSound ? stringInfoBinary.Flags | Common.Bit1 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.HasToken ? stringInfoBinary.Flags | Common.Bit2 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.Bit3 ? stringInfoBinary.Flags | Common.Bit3 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.Bit4 ? stringInfoBinary.Flags | Common.Bit4 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.Bit5 ? stringInfoBinary.Flags | Common.Bit5 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.Bit6 ? stringInfoBinary.Flags | Common.Bit6 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.Bit7 ? stringInfoBinary.Flags | Common.Bit7 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.Bit8 ? stringInfoBinary.Flags | Common.Bit8 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.Bit9 ? stringInfoBinary.Flags | Common.Bit9 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.Bit10 ? stringInfoBinary.Flags | Common.Bit10 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.Bit11 ? stringInfoBinary.Flags | Common.Bit11 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.Bit12 ? stringInfoBinary.Flags | Common.Bit12 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.Bit13 ? stringInfoBinary.Flags | Common.Bit13 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.Bit14 ? stringInfoBinary.Flags | Common.Bit14 : stringInfoBinary.Flags);
                stringInfoBinary.Flags = (Int16)(stringInfo.Flags.Bit15 ? stringInfoBinary.Flags | Common.Bit15 : stringInfoBinary.Flags);
                stringInfoBinary.PitchVariance = stringInfo.PitchVariance;
                stringInfoBinary.Sound = stringInfo.Sound;
                stringInfoBinary.StringIndex = strings.Count;
                stringInfoBinary.StringLength = stringInfo.Text.Length;
                stringInfoBinary.VolumeVariance = stringInfo.VolumeVariance;
                stringEntries.Add(stringInfoBinary);
                strings.Add(stringInfo.Text);
            }

            using MemoryStream s = new MemoryStream();
            using BinaryWriter bw = new BinaryWriter(s);
            var headerAsBytes = Common.WriteStruct(header);

            bw.Write(headerAsBytes);

            foreach (var stringEntry in stringEntries)
            {
                var stringEntryAsBytes = Common.WriteStruct(stringEntry);
                bw.Write(stringEntryAsBytes);
            }

            foreach (var text in strings)
            {
                bw.Write(text);
            }

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            using FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            bw.BaseStream.Position = 0;
            bw.BaseStream.CopyTo(fs);
            fs.Flush(flushToDisk: true);
            return true;
        }
    }
}