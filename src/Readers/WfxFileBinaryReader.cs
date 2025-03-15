using System.IO;
using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Readers.Interfaces;

namespace ii.InfinityEngine.Readers
{
    public class WfxFileBinaryReader : IWfxFileReader
    {
        public WfxFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public WfxFile Read(Stream s)
        {
            using var br = new BinaryReader(s);
            var wfxFile = ParseFile(br);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            wfxFile.OriginalFile = ParseFile(br);
            return wfxFile;
        }

        private WfxFile ParseFile(BinaryReader br)
        {
            var header = (WfxHeaderBinary)Common.ReadStruct(br, typeof(WfxHeaderBinary));

            var wfxFile = new WfxFile();

            wfxFile.SrCurveRadius = header.SrCurveRadius;
            wfxFile.Flags.CutsceneVolumeEnabled = (header.Flags & Common.Bit0) != 0;
            wfxFile.Flags.CustomSRCurveRadiusEnabled = (header.Flags & Common.Bit1) != 0;
            wfxFile.Flags.RandomFrequencyVariationEnabled = (header.Flags & Common.Bit2) != 0;
            wfxFile.Flags.RandomVolumeVariationEnabled = (header.Flags & Common.Bit3) != 0;
            wfxFile.Flags.EnvironmentalAudioEnabled = (header.Flags & Common.Bit4) != 0;
            wfxFile.Flags.Bit5 = (header.Flags & Common.Bit5) != 0;
            wfxFile.Flags.Bit6 = (header.Flags & Common.Bit6) != 0;
            wfxFile.Flags.Bit7 = (header.Flags & Common.Bit7) != 0;
            wfxFile.Flags.Bit8 = (header.Flags & Common.Bit8) != 0;
            wfxFile.Flags.Bit9 = (header.Flags & Common.Bit9) != 0;
            wfxFile.Flags.Bit10 = (header.Flags & Common.Bit10) != 0;
            wfxFile.Flags.Bit11 = (header.Flags & Common.Bit11) != 0;
            wfxFile.Flags.Bit12 = (header.Flags & Common.Bit12) != 0;
            wfxFile.Flags.Bit13 = (header.Flags & Common.Bit13) != 0;
            wfxFile.Flags.Bit14 = (header.Flags & Common.Bit14) != 0;
            wfxFile.Flags.Bit15 = (header.Flags & Common.Bit15) != 0;
            wfxFile.Flags.Bit16 = (header.Flags & Common.Bit16) != 0;
            wfxFile.Flags.Bit17 = (header.Flags & Common.Bit17) != 0;
            wfxFile.Flags.Bit18 = (header.Flags & Common.Bit18) != 0;
            wfxFile.Flags.Bit19 = (header.Flags & Common.Bit19) != 0;
            wfxFile.Flags.Bit20 = (header.Flags & Common.Bit20) != 0;
            wfxFile.Flags.Bit21 = (header.Flags & Common.Bit21) != 0;
            wfxFile.Flags.Bit22 = (header.Flags & Common.Bit22) != 0;
            wfxFile.Flags.Bit23 = (header.Flags & Common.Bit23) != 0;
            wfxFile.Flags.Bit24 = (header.Flags & Common.Bit24) != 0;
            wfxFile.Flags.Bit25 = (header.Flags & Common.Bit25) != 0;
            wfxFile.Flags.Bit26 = (header.Flags & Common.Bit26) != 0;
            wfxFile.Flags.Bit27 = (header.Flags & Common.Bit27) != 0;
            wfxFile.Flags.Bit28 = (header.Flags & Common.Bit28) != 0;
            wfxFile.Flags.Bit29 = (header.Flags & Common.Bit29) != 0;
            wfxFile.Flags.Bit30 = (header.Flags & Common.Bit30) != 0;
            wfxFile.Flags.Bit31 = (header.Flags & Common.Bit31) != 0;
            wfxFile.RandomFrequencyVariation = header.RandomFrequencyVariation;
            wfxFile.RandomVolumeVariation = header.RandomVolumeVariation;
            wfxFile.Unused0018 = header.Unused;

            wfxFile.Checksum = HashGenerator.GenerateKey(wfxFile);
            return wfxFile;
        }
    }
}