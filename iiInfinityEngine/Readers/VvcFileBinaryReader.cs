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
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public VvcFile Read(Stream s)
        {
            using var br = new BinaryReader(s);
            var vvcFile = ParseFile(br);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            vvcFile.OriginalFile = ParseFile(br);
            return vvcFile;
        }

        private VvcFile ParseFile(BinaryReader br)
        {
            var header = (VvcHeaderBinary)Common.ReadStruct(br, typeof(VvcHeaderBinary));

            var vvcFile = new VvcFile();

            vvcFile.AlphaBlendingAnimation = header.AlphaBlendingAnimation;
            vvcFile.Animation = header.Animation;
            vvcFile.Animation2 = header.Animation2;
            vvcFile.Bam1Sequence = header.Bam1Sequence;
            vvcFile.Bam2Sequence = header.Bam2Sequence;
            vvcFile.Bam3Sequence = header.Bam3Sequence;
            vvcFile.BaseOrientation = header.BaseOrientation;
            vvcFile.BitmapPalette = header.BitmapPalette;
            vvcFile.CentreX = header.CentreX;
            vvcFile.CentreY = header.CentreY;
            vvcFile.ColourFlags.NotLightSource = (header.ColourFlags & Common.Bit0) != 0;
            vvcFile.ColourFlags.LightSource = (header.ColourFlags & Common.Bit1) != 0;
            vvcFile.ColourFlags.InternalBrightness = (header.ColourFlags & Common.Bit2) != 0;
            vvcFile.ColourFlags.Timestopped = (header.ColourFlags & Common.Bit3) != 0;
            vvcFile.ColourFlags.Bit4 = (header.ColourFlags & Common.Bit4) != 0;
            vvcFile.ColourFlags.InternalGamma = (header.ColourFlags & Common.Bit5) != 0;
            vvcFile.ColourFlags.NonReservedPalette = (header.ColourFlags & Common.Bit6) != 0;
            vvcFile.ColourFlags.FullPalette = (header.ColourFlags & Common.Bit7) != 0;
            vvcFile.ColourFlags.Blend = (header.ColourFlags & Common.Bit8) != 0;
            vvcFile.ColourFlags.Sepia = (header.ColourFlags & Common.Bit9) != 0;
            vvcFile.ColourFlags.Bit10 = (header.ColourFlags & Common.Bit10) != 0;
            vvcFile.ColourFlags.Bit11 = (header.ColourFlags & Common.Bit11) != 0;
            vvcFile.ColourFlags.Bit12 = (header.ColourFlags & Common.Bit12) != 0;
            vvcFile.ColourFlags.Bit13 = (header.ColourFlags & Common.Bit13) != 0;
            vvcFile.ColourFlags.Bit14 = (header.ColourFlags & Common.Bit14) != 0;
            vvcFile.ColourFlags.Bit15 = (header.ColourFlags & Common.Bit15) != 0;
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
            vvcFile.DisplayFlags.Bit14 = (header.DisplayFlags & Common.Bit14) != 0;
            vvcFile.DisplayFlags.Scale = (header.DisplayFlags & Common.Bit15) != 0;
            vvcFile.Duration = header.Duration;
            vvcFile.FrameRate = header.FrameRate;
            vvcFile.InternalName = header.InternalName;
            vvcFile.LightingBrightness = header.LightingBrightness;
            vvcFile.OrientationCount = header.OrientationCount;
            vvcFile.PositionFlags.OrbitTarget = (header.PositionFlags & Common.Bit0) != 0;
            vvcFile.PositionFlags.RelativeToTarget = (header.PositionFlags & Common.Bit1) != 0;
            vvcFile.PositionFlags.Bit2 = (header.PositionFlags & Common.Bit2) != 0;
            vvcFile.PositionFlags.IgnoreOrientation = (header.PositionFlags & Common.Bit3) != 0;           
            vvcFile.PositionFlags.Bit4 = (header.PositionFlags & Common.Bit4) != 0;
            vvcFile.PositionFlags.Bit5 = (header.PositionFlags & Common.Bit5) != 0;
            vvcFile.PositionFlags.Bit6 = (header.PositionFlags & Common.Bit6) != 0;
            vvcFile.PositionFlags.Bit7 = (header.PositionFlags & Common.Bit7) != 0;
            vvcFile.PositionFlags.Bit8 = (header.PositionFlags & Common.Bit8) != 0;
            vvcFile.PositionFlags.Bit9 = (header.PositionFlags & Common.Bit9) != 0;
            vvcFile.PositionFlags.Bit10 = (header.PositionFlags & Common.Bit10) != 0;
            vvcFile.PositionFlags.Bit11 = (header.PositionFlags & Common.Bit11) != 0;
            vvcFile.PositionFlags.Bit12 = (header.PositionFlags & Common.Bit12) != 0;
            vvcFile.PositionFlags.Bit13 = (header.PositionFlags & Common.Bit13) != 0;
            vvcFile.PositionFlags.Bit14 = (header.PositionFlags & Common.Bit14) != 0;
            vvcFile.PositionFlags.Bit15 = (header.PositionFlags & Common.Bit15) != 0;
            vvcFile.PositionFlags.Bit16 = (header.PositionFlags & Common.Bit16) != 0;
            vvcFile.PositionFlags.Bit17 = (header.PositionFlags & Common.Bit17) != 0;
            vvcFile.PositionFlags.Bit18 = (header.PositionFlags & Common.Bit18) != 0;
            vvcFile.PositionFlags.Bit19 = (header.PositionFlags & Common.Bit19) != 0;
            vvcFile.PositionFlags.Bit20 = (header.PositionFlags & Common.Bit20) != 0;
            vvcFile.PositionFlags.Bit21 = (header.PositionFlags & Common.Bit21) != 0;
            vvcFile.PositionFlags.Bit22 = (header.PositionFlags & Common.Bit22) != 0;
            vvcFile.PositionFlags.Bit23 = (header.PositionFlags & Common.Bit23) != 0;
            vvcFile.PositionFlags.Bit24 = (header.PositionFlags & Common.Bit24) != 0;
            vvcFile.PositionFlags.Bit25 = (header.PositionFlags & Common.Bit25) != 0;
            vvcFile.PositionFlags.Bit26 = (header.PositionFlags & Common.Bit26) != 0;
            vvcFile.PositionFlags.Bit27 = (header.PositionFlags & Common.Bit27) != 0;
            vvcFile.PositionFlags.Bit28 = (header.PositionFlags & Common.Bit28) != 0;
            vvcFile.PositionFlags.Bit29 = (header.PositionFlags & Common.Bit29) != 0;
            vvcFile.PositionFlags.Bit30 = (header.PositionFlags & Common.Bit30) != 0;
            vvcFile.PositionFlags.Bit31 = (header.PositionFlags & Common.Bit31) != 0;
            vvcFile.SequenceFlags.Looping = (header.SequenceFlags & Common.Bit0) != 0;
            vvcFile.SequenceFlags.SpecialLighting = (header.SequenceFlags & Common.Bit1) != 0;
            vvcFile.SequenceFlags.ModifyForHeight = (header.SequenceFlags & Common.Bit2) != 0;
            vvcFile.SequenceFlags.DrawAnimation = (header.SequenceFlags & Common.Bit3) != 0;
            vvcFile.SequenceFlags.CustomPalette = (header.SequenceFlags & Common.Bit4) != 0;
            vvcFile.SequenceFlags.Purgeable = (header.SequenceFlags & Common.Bit5) != 0;
            vvcFile.SequenceFlags.NotCoveredByWallgroups = (header.SequenceFlags & Common.Bit6) != 0;
            vvcFile.SequenceFlags.MidLevelBrighten = (header.SequenceFlags & Common.Bit7) != 0;
            vvcFile.SequenceFlags.HighLevelBrighten = (header.SequenceFlags & Common.Bit8) != 0;
            vvcFile.SequenceFlags.Bit9 = (header.SequenceFlags & Common.Bit9) != 0;
            vvcFile.SequenceFlags.Bit10 = (header.SequenceFlags & Common.Bit10) != 0;
            vvcFile.SequenceFlags.Bit11 = (header.SequenceFlags & Common.Bit11) != 0;
            vvcFile.SequenceFlags.Bit12 = (header.SequenceFlags & Common.Bit12) != 0;
            vvcFile.SequenceFlags.Bit13 = (header.SequenceFlags & Common.Bit13) != 0;
            vvcFile.SequenceFlags.Bit14 = (header.SequenceFlags & Common.Bit14) != 0;
            vvcFile.SequenceFlags.Bit15 = (header.SequenceFlags & Common.Bit15) != 0;
            vvcFile.SequenceFlags.Bit16 = (header.SequenceFlags & Common.Bit16) != 0;
            vvcFile.SequenceFlags.Bit17 = (header.SequenceFlags & Common.Bit17) != 0;
            vvcFile.SequenceFlags.Bit18 = (header.SequenceFlags & Common.Bit18) != 0;
            vvcFile.SequenceFlags.Bit19 = (header.SequenceFlags & Common.Bit19) != 0;
            vvcFile.SequenceFlags.Bit20 = (header.SequenceFlags & Common.Bit20) != 0;
            vvcFile.SequenceFlags.Bit21 = (header.SequenceFlags & Common.Bit21) != 0;
            vvcFile.SequenceFlags.Bit22 = (header.SequenceFlags & Common.Bit22) != 0;
            vvcFile.SequenceFlags.Bit23 = (header.SequenceFlags & Common.Bit23) != 0;
            vvcFile.SequenceFlags.Bit24 = (header.SequenceFlags & Common.Bit24) != 0;
            vvcFile.SequenceFlags.Bit25 = (header.SequenceFlags & Common.Bit25) != 0;
            vvcFile.SequenceFlags.Bit26 = (header.SequenceFlags & Common.Bit26) != 0;
            vvcFile.SequenceFlags.Bit27 = (header.SequenceFlags & Common.Bit27) != 0;
            vvcFile.SequenceFlags.Bit28 = (header.SequenceFlags & Common.Bit28) != 0;
            vvcFile.SequenceFlags.Bit29 = (header.SequenceFlags & Common.Bit29) != 0;
            vvcFile.SequenceFlags.Bit30 = (header.SequenceFlags & Common.Bit30) != 0;
            vvcFile.SequenceFlags.Bit31 = (header.SequenceFlags & Common.Bit31) != 0;
            vvcFile.Unused001c = header.Unused001c;
            vvcFile.Unused0024 = header.Unused0024;
            vvcFile.Unused009c = header.Unused009c;
            vvcFile.UseContinuousSequence = header.UseContinuousSequence;
            vvcFile.UseOrientation = header.UseOrientation;
            vvcFile.Wav1 = header.Wav1;
            vvcFile.Wav2 = header.Wav2;
            vvcFile.Wav3 = header.Wav3;
            vvcFile.XPosition = header.XPosition;
            vvcFile.YPosition = header.YPosition;
            vvcFile.ZPosition = header.ZPosition;

            vvcFile.Checksum = HashGenerator.GenerateKey(vvcFile);
            return vvcFile;
        }
    }
}