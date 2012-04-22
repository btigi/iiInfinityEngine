using System;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace iiInfinityEngine.Core.Writers
{
    public class WfxFileBinaryWriter : IWfxFileWriter
    {
        const int HeaderSize = 114;
        const int ExtendedHeaderSize = 56;
        const int FeatureBlockSize = 48;
        public BackupManager BackupManger { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (!(file is WfxFile))
                throw new ArgumentException("File is not a valid creature file");

            var wfxFile = file as WfxFile;

            if (!(forceSave) && (MD5HashGenerator.GenerateKey(wfxFile) == wfxFile.Checksum))
                return false;

            WfxHeaderBinary header = new WfxHeaderBinary();

            header.Flags = wfxFile.Flags.CutsceneVolumeEnabled ? header.Flags | Common.Bit0 : header.Flags;
            header.Flags = wfxFile.Flags.CustomSRCurveRadiusEnabled ? header.Flags | Common.Bit1 : header.Flags;
            header.Flags = wfxFile.Flags.RandomFrequencyVariationEnabled ? header.Flags | Common.Bit2 : header.Flags;
            header.Flags = wfxFile.Flags.CutsceneVolumeEnabled ? header.Flags | Common.Bit3 : header.Flags;
            header.Flags = wfxFile.Flags.EnvironmentalAudioEnabled ? header.Flags | Common.Bit4 : header.Flags;

            header.ftype = new array4() { character1 = 'W', character2 = 'F', character3 = 'X', character4 = ' ' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = '.', character4 = '0' };
            header.RandomFrequencyVariation = wfxFile.RandomFrequencyVariation;
            header.RandomVolumeVariation = wfxFile.RandomVolumeVariation;
            header.SrCurveRadius = wfxFile.SrCurveRadius;
            header.Unused = wfxFile.Unused0018;

            using (MemoryStream s = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(s))
                {
                    var headerAsBytes = Common.WriteStruct(header);

                    bw.Write(headerAsBytes);

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