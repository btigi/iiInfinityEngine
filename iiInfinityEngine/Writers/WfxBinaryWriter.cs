using System.IO;
using WindowsFormsApplication1.Files;
using WindowsFormsApplication1.Readers;

namespace WindowsFormsApplication1.Writers
{
    class WfxBinaryWriter
    {
        const int HeaderSize = 114;
        const int ExtendedHeaderSize = 56;
        const int FeatureBlockSize = 48;

        public void Write(string filename, WfxFile wfxFile)
        {
            WfxHeader header = new WfxHeader();

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
            header.Unused = wfxFile.Unused;

            using (MemoryStream s = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(s))
                {
                    var headerAsBytes = Common.WriteStruct(header);

                    bw.Write(headerAsBytes);

                    using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    {
                        bw.BaseStream.Position = 0;
                        bw.BaseStream.CopyTo(fs);
                        fs.Flush(flushToDisk: true);
                    }
                }
            }
        }
    }
}