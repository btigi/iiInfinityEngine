using System;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;

namespace iiInfinityEngine.Core.Writers
{
    public class WfxFileBinaryWriter : IWfxFileWriter
    {
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not WfxFile)
                throw new ArgumentException("File is not a valid creature file");

            var wfxFile = file as WfxFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(wfxFile) == wfxFile.Checksum))
                return false;

            var header = new WfxHeaderBinary();

            header.Flags = wfxFile.Flags.CutsceneVolumeEnabled ? header.Flags | Common.Bit0 : header.Flags;
            header.Flags = wfxFile.Flags.CustomSRCurveRadiusEnabled ? header.Flags | Common.Bit1 : header.Flags;
            header.Flags = wfxFile.Flags.RandomFrequencyVariationEnabled ? header.Flags | Common.Bit2 : header.Flags;
            header.Flags = wfxFile.Flags.CutsceneVolumeEnabled ? header.Flags | Common.Bit3 : header.Flags;
            header.Flags = wfxFile.Flags.EnvironmentalAudioEnabled ? header.Flags | Common.Bit4 : header.Flags;

            header.ftype = ['W', 'F', 'X', ' '];
            header.fversion = ['V', '1', '.', '0'];
            header.RandomFrequencyVariation = wfxFile.RandomFrequencyVariation;
            header.RandomVolumeVariation = wfxFile.RandomVolumeVariation;
            header.SrCurveRadius = wfxFile.SrCurveRadius;
            header.Unused = wfxFile.Unused0018;

            using var s = new MemoryStream();
            using var bw = new BinaryWriter(s);
            var headerAsBytes = Common.WriteStruct(header);

            bw.Write(headerAsBytes);

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            bw.BaseStream.Position = 0;
            bw.BaseStream.CopyTo(fs);
            fs.Flush(flushToDisk: true);
            return true;
        }
    }
}