using System;
using System.IO;
using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Writers.Interfaces;

namespace ii.InfinityEngine.Writers
{
    public class WfxFileBinaryWriter : IWfxFileWriter
    {
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not WfxFile)
                throw new ArgumentException("File is not a valid wfx file");

            var wfxFile = file as WfxFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(wfxFile) == wfxFile.Checksum))
                return false;

            var header = new WfxHeaderBinary();

            header.Flags = wfxFile.Flags.CutsceneVolumeEnabled ? header.Flags | Common.Bit0 : header.Flags;
            header.Flags = wfxFile.Flags.CustomSRCurveRadiusEnabled ? header.Flags | Common.Bit1 : header.Flags;
            header.Flags = wfxFile.Flags.RandomFrequencyVariationEnabled ? header.Flags | Common.Bit2 : header.Flags;
            header.Flags = wfxFile.Flags.CutsceneVolumeEnabled ? header.Flags | Common.Bit3 : header.Flags;
            header.Flags = wfxFile.Flags.EnvironmentalAudioEnabled ? header.Flags | Common.Bit4 : header.Flags;
            header.Flags = wfxFile.Flags.Bit5 ? header.Flags | Common.Bit5 : header.Flags;
            header.Flags = wfxFile.Flags.Bit6 ? header.Flags | Common.Bit6 : header.Flags;
            header.Flags = wfxFile.Flags.Bit7 ? header.Flags | Common.Bit7 : header.Flags;
            header.Flags = wfxFile.Flags.Bit8 ? header.Flags | Common.Bit8 : header.Flags;
            header.Flags = wfxFile.Flags.Bit9 ? header.Flags | Common.Bit9 : header.Flags;
            header.Flags = wfxFile.Flags.Bit10 ? header.Flags | Common.Bit10 : header.Flags;
            header.Flags = wfxFile.Flags.Bit11 ? header.Flags | Common.Bit11 : header.Flags;
            header.Flags = wfxFile.Flags.Bit12 ? header.Flags | Common.Bit12 : header.Flags;
            header.Flags = wfxFile.Flags.Bit13 ? header.Flags | Common.Bit13 : header.Flags;
            header.Flags = wfxFile.Flags.Bit14 ? header.Flags | Common.Bit14 : header.Flags;
            header.Flags = wfxFile.Flags.Bit15 ? header.Flags | Common.Bit15 : header.Flags;
            header.Flags = wfxFile.Flags.Bit16 ? header.Flags | Common.Bit16 : header.Flags;
            header.Flags = wfxFile.Flags.Bit17 ? header.Flags | Common.Bit17 : header.Flags;
            header.Flags = wfxFile.Flags.Bit18 ? header.Flags | Common.Bit18 : header.Flags;
            header.Flags = wfxFile.Flags.Bit19 ? header.Flags | Common.Bit19 : header.Flags;
            header.Flags = wfxFile.Flags.Bit20 ? header.Flags | Common.Bit20 : header.Flags;
            header.Flags = wfxFile.Flags.Bit21 ? header.Flags | Common.Bit21 : header.Flags;
            header.Flags = wfxFile.Flags.Bit22 ? header.Flags | Common.Bit22 : header.Flags;
            header.Flags = wfxFile.Flags.Bit23 ? header.Flags | Common.Bit23 : header.Flags;
            header.Flags = wfxFile.Flags.Bit24 ? header.Flags | Common.Bit24 : header.Flags;
            header.Flags = wfxFile.Flags.Bit25 ? header.Flags | Common.Bit25 : header.Flags;
            header.Flags = wfxFile.Flags.Bit26 ? header.Flags | Common.Bit26 : header.Flags;
            header.Flags = wfxFile.Flags.Bit27 ? header.Flags | Common.Bit27 : header.Flags;
            header.Flags = wfxFile.Flags.Bit28 ? header.Flags | Common.Bit28 : header.Flags;
            header.Flags = wfxFile.Flags.Bit29 ? header.Flags | Common.Bit29 : header.Flags;
            header.Flags = wfxFile.Flags.Bit30 ? header.Flags | Common.Bit30 : header.Flags;
            header.Flags = wfxFile.Flags.Bit31 ? header.Flags | Common.Bit31 : header.Flags;

            header.ftype = new array4() { character1 = 'W', character2 = 'F', character3 = 'X', character4 = ' ' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = '.', character4 = '0' };
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