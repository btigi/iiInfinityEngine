using System.IO;
using WindowsFormsApplication1.Binary;
using WindowsFormsApplication1.Files;
using System;

namespace WindowsFormsApplication1.Writers
{
    class VvcBinaryWriter
    {
        public void Write(string filename, VvcFile vvcFile)
        {
            VvcHeader header = new VvcHeader();

            header.ftype = new array4() { character1 = 'V', character2 = 'V', character3 = 'C', character4 = ' ' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = '.', character4 = '0' };

            header.AlphaBlendingAnimation = new array8(vvcFile.AlphaBlendingAnimation);
            header.Animation = new array8(vvcFile.Animation);
            header.Animation2 = new array8(vvcFile.Animation2);
            header.Bam1Sequence = vvcFile.Bam1Sequence;
            header.Bam2Sequence = vvcFile.Bam2Sequence;
            header.Bam3Sequence = vvcFile.Bam3Sequence;
            header.BaseOrientation = vvcFile.BaseOrientation;
            header.BitmapPalette = new array8(vvcFile.BitmapPalette);
            header.CentreX = vvcFile.CentreX;
            header.CentreY = vvcFile.CentreY;
            header.ColourFlags = vvcFile.ColourFlags.NotLightSource ? Convert.ToInt16(header.ColourFlags | Common.Bit0) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.LightSource ? Convert.ToInt16(header.ColourFlags | Common.Bit1) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.InternalBrightness ? Convert.ToInt16(header.ColourFlags | Common.Bit2) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Timestopped ? Convert.ToInt16(header.ColourFlags | Common.Bit3) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused ? Convert.ToInt16(header.ColourFlags | Common.Bit4) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.InternalGamma ? Convert.ToInt16(header.ColourFlags | Common.Bit5) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.NonReservedPalette ? Convert.ToInt16(header.ColourFlags | Common.Bit6) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.FullPalette ? Convert.ToInt16(header.ColourFlags | Common.Bit7) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused ? Convert.ToInt16(header.ColourFlags | Common.Bit8) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Sepia ? Convert.ToInt16(header.ColourFlags | Common.Bit9) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused2 ? Convert.ToInt16(header.ColourFlags | Common.Bit10) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused3 ? Convert.ToInt16(header.ColourFlags | Common.Bit11) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused4 ? Convert.ToInt16(header.ColourFlags | Common.Bit12) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused5 ? Convert.ToInt16(header.ColourFlags | Common.Bit13) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused6 ? Convert.ToInt16(header.ColourFlags | Common.Bit14) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused7 ? Convert.ToInt16(header.ColourFlags | Common.Bit15) : header.ColourFlags;
            header.CurrentAnimationSequence = vvcFile.CurrentAnimationSequence;
            header.DisplayFlags = vvcFile.DisplayFlags.Transparent ? Convert.ToInt16(header.DisplayFlags | Common.Bit0) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Translucent ? Convert.ToInt16(header.DisplayFlags | Common.Bit1) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.TranslucentShadow ? Convert.ToInt16(header.DisplayFlags | Common.Bit2) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Blended ? Convert.ToInt16(header.DisplayFlags | Common.Bit3) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.MirrorXAxis ? Convert.ToInt16(header.DisplayFlags | Common.Bit4) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.MirrorYAxis ? Convert.ToInt16(header.DisplayFlags | Common.Bit5) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Clipped ? Convert.ToInt16(header.DisplayFlags | Common.Bit6) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.CopyFromBack ? Convert.ToInt16(header.DisplayFlags | Common.Bit7) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.ClearFill ? Convert.ToInt16(header.DisplayFlags | Common.Bit8) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Blend3D ? Convert.ToInt16(header.DisplayFlags | Common.Bit9) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.NotCoveredByWall ? Convert.ToInt16(header.DisplayFlags | Common.Bit10) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.PersistThroughTimestop ? Convert.ToInt16(header.DisplayFlags | Common.Bit11) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.IgnoreDreamPalette ? Convert.ToInt16(header.DisplayFlags | Common.Bit12) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Blend2D ? Convert.ToInt16(header.DisplayFlags | Common.Bit13) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Unused1 ? Convert.ToInt16(header.DisplayFlags | Common.Bit14) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Unused2 ? Convert.ToInt16(header.DisplayFlags | Common.Bit15) : header.DisplayFlags;
            header.Duration = vvcFile.Duration;
            header.FrameRate = vvcFile.FrameRate;
            header.InternalName = new array8(vvcFile.InternalName);
            header.LightingBrightness = vvcFile.LightingBrightness;
            header.LightingBrightness = vvcFile.LightingBrightness;
            header.OrientationCount = vvcFile.OrientationCount;
            header.PositionFlags = vvcFile.PositionFlags.OrbitTarget ? Convert.ToInt16(header.DisplayFlags | Common.Bit0) : header.DisplayFlags;
            header.PositionFlags = vvcFile.PositionFlags.RelativeToTarget ? Convert.ToInt16(header.DisplayFlags | Common.Bit1) : header.DisplayFlags;
            header.PositionFlags = vvcFile.PositionFlags.Unused ? Convert.ToInt16(header.DisplayFlags | Common.Bit2) : header.DisplayFlags;
            header.PositionFlags = vvcFile.PositionFlags.IgnoreOrientation ? Convert.ToInt16(header.DisplayFlags | Common.Bit3) : header.DisplayFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Looping ? Convert.ToInt16(header.DisplayFlags | Common.Bit0) : header.DisplayFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.SpecialLighting ? Convert.ToInt16(header.DisplayFlags | Common.Bit1) : header.DisplayFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.ModifyForHeight ? Convert.ToInt16(header.DisplayFlags | Common.Bit2) : header.DisplayFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.DrawAnimation ? Convert.ToInt16(header.DisplayFlags | Common.Bit3) : header.DisplayFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.CustomPalette ? Convert.ToInt16(header.DisplayFlags | Common.Bit4) : header.DisplayFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Purgeable ? Convert.ToInt16(header.DisplayFlags | Common.Bit5) : header.DisplayFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.NotCoveredByWallgroups ? Convert.ToInt16(header.DisplayFlags | Common.Bit6) : header.DisplayFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.MidLevelBrighten ? Convert.ToInt16(header.DisplayFlags | Common.Bit7) : header.DisplayFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.HighLevelBrighten ? Convert.ToInt16(header.DisplayFlags | Common.Bit8) : header.DisplayFlags;
            header.Unused = vvcFile.Unused;
            header.Unused2 = vvcFile.Unused2;
            header.Unused3 = vvcFile.Unused3;
            header.UseContinuousSequence = vvcFile.UseContinuousSequence;
            header.UseOrientation = vvcFile.UseOrientation;
            header.Wav1 = new array8(vvcFile.Wav1);
            header.Wav2 = new array8(vvcFile.Wav2);
            header.Wav3 = new array8(vvcFile.Wav3);
            header.XPosition = vvcFile.XPosition;
            header.YPosition = vvcFile.YPosition;
            header.ZPosition = vvcFile.ZPosition;

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