using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Readers.Interfaces;

namespace iiInfinityEngine.Core.Readers
{
    public class WfxFileBinaryReader : IWfxFileReader
    {
        public WfxFile Read(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var f = Read(fs);
                f.Filename = Path.GetFileName(filename);
                return f;
            }
        }

        public WfxFile Read(Stream s)
        {
            using (BinaryReader br = new BinaryReader(s))
            {
                var wfxFile = ParseFile(br);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                wfxFile.OriginalFile = ParseFile(br);
                return wfxFile;
            }
        }

        private WfxFile ParseFile(BinaryReader br)
        {
            var header = (WfxHeaderBinary)Common.ReadStruct(br, typeof(WfxHeaderBinary));

            var wfxFile = new WfxFile();

            wfxFile.SrCurveRadius = header.SrCurveRadius;
            wfxFile.Flags.CustomSRCurveRadiusEnabled = (header.Flags & Common.Bit0) != 0;
            wfxFile.Flags.CutsceneVolumeEnabled = (header.Flags & Common.Bit1) != 0;
            wfxFile.Flags.EnvironmentalAudioEnabled = (header.Flags & Common.Bit2) != 0;
            wfxFile.Flags.RandomFrequencyVariationEnabled = (header.Flags & Common.Bit3) != 0;
            wfxFile.Flags.RandomVolumeVariationEnabled = (header.Flags & Common.Bit4) != 0;
            wfxFile.RandomFrequencyVariation = header.RandomFrequencyVariation;
            wfxFile.RandomVolumeVariation = header.RandomVolumeVariation;
            wfxFile.Unused0018 = header.Unused;

            wfxFile.Checksum = MD5HashGenerator.GenerateKey(wfxFile);
            return wfxFile;
        }
    }
}