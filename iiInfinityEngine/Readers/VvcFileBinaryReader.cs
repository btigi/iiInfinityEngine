using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Readers.Interfaces;

namespace iiInfinityEngine.Core.Readers
{
    public class VvcFileBinaryReader : IVvcFileReader
    {
        public VvcFile Read(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var f = Read(fs);
                f.Filename = Path.GetFileName(filename);
                return f;
            }
        }

        public VvcFile Read(Stream s)
        {
            using (BinaryReader br = new BinaryReader(s))
            {
                var vvcFile = ParseFile(br);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                vvcFile.OriginalFile = ParseFile(br);
                return vvcFile;
            }
        }

        private VvcFile ParseFile(BinaryReader br)
        {
            var header = (VvcHeaderBinary)Common.ReadStruct(br, typeof(VvcHeaderBinary));

            VvcFile vvcFile = new VvcFile();

            vvcFile.AlphaBlendingAnimation = header.AlphaBlendingAnimation.ToString();
            vvcFile.Animation = header.Animation.ToString();
            vvcFile.Animation2 = header.Animation2.ToString();
            vvcFile.Bam1Sequence = header.Bam1Sequence;
            vvcFile.Bam2Sequence = header.Bam2Sequence;
            vvcFile.Bam3Sequence = header.Bam3Sequence;
            vvcFile.BaseOrientation = header.BaseOrientation;
            vvcFile.BitmapPalette = header.BitmapPalette.ToString();
            vvcFile.CentreX = header.CentreX;
            vvcFile.CentreY = header.CentreY;
            vvcFile.ColourFlags.NotLightSource = (header.ColourFlags & Common.Bit0) != 0;
            vvcFile.ColourFlags.LightSource = (header.ColourFlags & Common.Bit1) != 0;
            vvcFile.ColourFlags.InternalBrightness = (header.ColourFlags & Common.Bit2) != 0;
            vvcFile.ColourFlags.Timestopped = (header.ColourFlags & Common.Bit3) != 0;
            vvcFile.ColourFlags.Unused1 = (header.ColourFlags & Common.Bit4) != 0;
            vvcFile.ColourFlags.InternalGamma = (header.ColourFlags & Common.Bit5) != 0;
            vvcFile.ColourFlags.NonReservedPalette = (header.ColourFlags & Common.Bit6) != 0;
            vvcFile.ColourFlags.FullPalette = (header.ColourFlags & Common.Bit7) != 0;
            vvcFile.ColourFlags.Unused = (header.ColourFlags & Common.Bit8) != 0;
            vvcFile.ColourFlags.Sepia = (header.ColourFlags & Common.Bit9) != 0;
            vvcFile.ColourFlags.Unused2 = (header.ColourFlags & Common.Bit10) != 0;
            vvcFile.ColourFlags.Unused3 = (header.ColourFlags & Common.Bit11) != 0;
            vvcFile.ColourFlags.Unused4 = (header.ColourFlags & Common.Bit12) != 0;
            vvcFile.ColourFlags.Unused5 = (header.ColourFlags & Common.Bit13) != 0;
            vvcFile.ColourFlags.Unused6 = (header.ColourFlags & Common.Bit14) != 0;
            vvcFile.ColourFlags.Unused7 = (header.ColourFlags & Common.Bit15) != 0;
            vvcFile.CurrentAnimationSequence = header.CurrentAnimationSequence;
            vvcFile.DisplayFlags.Transparent = (header.DisplayFlags & Common.Bit0) != 0;
            vvcFile.DisplayFlags.Translucent = (header.DisplayFlags & Common.Bit1) != 0;
            vvcFile.DisplayFlags.TranslucentShadow = (header.DisplayFlags & Common.Bit2) != 0;
            vvcFile.DisplayFlags.Blended = (header.DisplayFlags & Common.Bit3) != 0;
            vvcFile.DisplayFlags.MirrorXAxis = (header.DisplayFlags & Common.Bit4) != 0;
            vvcFile.DisplayFlags.MirrorYAxis = (header.DisplayFlags & Common.Bit5) != 0;
            vvcFile.DisplayFlags.Clipped = (header.DisplayFlags & Common.Bit6) != 0;
            vvcFile.DisplayFlags.CopyFromBack = (header.DisplayFlags & Common.Bit7) != 0;
            vvcFile.DisplayFlags.ClearFill = (header.DisplayFlags & Common.Bit8) != 0;
            vvcFile.DisplayFlags.Blend3D = (header.DisplayFlags & Common.Bit9) != 0;
            vvcFile.DisplayFlags.NotCoveredByWall = (header.DisplayFlags & Common.Bit10) != 0;
            vvcFile.DisplayFlags.PersistThroughTimestop = (header.DisplayFlags & Common.Bit11) != 0;
            vvcFile.DisplayFlags.IgnoreDreamPalette = (header.DisplayFlags & Common.Bit12) != 0;
            vvcFile.DisplayFlags.Blend2D = (header.DisplayFlags & Common.Bit13) != 0;
            vvcFile.DisplayFlags.Unused1 = (header.DisplayFlags & Common.Bit14) != 0;
            vvcFile.DisplayFlags.Unused2 = (header.DisplayFlags & Common.Bit15) != 0;
            vvcFile.Duration = header.Duration;
            vvcFile.FrameRate = header.FrameRate;
            vvcFile.InternalName = header.InternalName.ToString();
            vvcFile.LightingBrightness = header.LightingBrightness;
            vvcFile.OrientationCount = header.OrientationCount;
            vvcFile.PositionFlags.OrbitTarget = (header.PositionFlags & Common.Bit0) != 0;
            vvcFile.PositionFlags.RelativeToTarget = (header.PositionFlags & Common.Bit1) != 0;
            vvcFile.PositionFlags.Unused = (header.PositionFlags & Common.Bit2) != 0;
            vvcFile.PositionFlags.IgnoreOrientation = (header.PositionFlags & Common.Bit3) != 0;
            vvcFile.SequenceFlags.Looping = (header.SequenceFlags & Common.Bit0) != 0;
            vvcFile.SequenceFlags.SpecialLighting = (header.SequenceFlags & Common.Bit1) != 0;
            vvcFile.SequenceFlags.ModifyForHeight = (header.SequenceFlags & Common.Bit2) != 0;
            vvcFile.SequenceFlags.DrawAnimation = (header.SequenceFlags & Common.Bit3) != 0;
            vvcFile.SequenceFlags.CustomPalette = (header.SequenceFlags & Common.Bit4) != 0;
            vvcFile.SequenceFlags.Purgeable = (header.SequenceFlags & Common.Bit5) != 0;
            vvcFile.SequenceFlags.NotCoveredByWallgroups = (header.SequenceFlags & Common.Bit6) != 0;
            vvcFile.SequenceFlags.MidLevelBrighten = (header.SequenceFlags & Common.Bit7) != 0;
            vvcFile.SequenceFlags.HighLevelBrighten = (header.SequenceFlags & Common.Bit8) != 0;
            vvcFile.Unused001c = header.Unused;
            vvcFile.Unused0024 = header.Unused2;
            vvcFile.Unused009c = header.Unused3;
            vvcFile.UseContinuousSequence = header.UseContinuousSequence;
            vvcFile.UseOrientation = header.UseOrientation;
            vvcFile.Wav1 = header.Wav1.ToString();
            vvcFile.Wav2 = header.Wav2.ToString();
            vvcFile.Wav3 = header.Wav3.ToString();
            vvcFile.XPosition = header.XPosition;
            vvcFile.YPosition = header.YPosition;
            vvcFile.ZPosition = header.ZPosition;

            vvcFile.Checksum = MD5HashGenerator.GenerateKey(vvcFile);
            return vvcFile;
        }
    }
}