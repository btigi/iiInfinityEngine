using System;
using System.Collections.Generic;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace iiInfinityEngine.Core.Writers
{
    public class TlkFileBinaryWriter : ITlkFileWriter
    {
        const int HeaderSize = 18;
        const int StringInfoSize = 26;

        public BackupManager BackupManger { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (!(file is TlkFile))
                throw new ArgumentException("File is not a valid creature file");

            var tlkFile = file as TlkFile;

            if (!(forceSave) && (MD5HashGenerator.GenerateKey(tlkFile) == tlkFile.Checksum))
                return false;

            List<TlkEntryBinary> stringEntries = new List<TlkEntryBinary>();
            List<String> strings = new List<String>();

            TlkHeaderBinary header = new TlkHeaderBinary();
            header.ftype = new array4() { character1 = 'T', character2 = 'L', character3 = 'K', character4 = ' ' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = ' ', character4 = ' ' };
            header.LanguageId = tlkFile.LangugeId;
            header.StringCount = tlkFile.Strings.Count;
            header.StringOffset = HeaderSize + (StringInfoSize * tlkFile.Strings.Count);

            foreach (var stringInfo in tlkFile.Strings)
            {
                var stringInfoBinary = new TlkEntryBinary();
                stringInfoBinary.Flags = Convert.ToInt16(stringInfo.Flags);
                stringInfoBinary.PitchVariance = stringInfo.PitchVariance;
                stringInfoBinary.Sound = new array8(stringInfo.Sound);
                stringInfoBinary.StringIndex = strings.Count;
                stringInfoBinary.StringLength = stringInfo.Text.Length;
                stringInfoBinary.VolumeVariance = stringInfo.VolumeVariance;
                stringEntries.Add(stringInfoBinary);
                strings.Add(stringInfo.Text);
            }

            using (MemoryStream s = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(s))
                {
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

                    if (BackupManger != null)
                    {
                        BackupManger.BackupFile(file, file.Filename, file.FileType, this);
                    }

                    using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    {
                        bw.BaseStream.Position = 0;
                        bw.BaseStream.CopyTo(fs);
                        fs.Flush(flushToDisk: true);
                    }
                }
            }
            return true;
        }
    }
}