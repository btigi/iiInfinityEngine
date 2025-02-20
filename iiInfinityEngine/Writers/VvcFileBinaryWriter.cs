using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using System;
using iiInfinityEngine.Core.Writers.Interfaces;
using System.Linq;

namespace iiInfinityEngine.Core.Writers
{
    public class VvcFileBinaryWriter : IVvcFileWriter
    {
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not VvcFile)
                throw new ArgumentException("File is not a valid vvc file");

            var vvcFile = file as VvcFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(vvcFile) == vvcFile.Checksum))
                return false;

            var header = new VvcHeaderBinary();

            header.ftype = ['V', 'V', 'C', ' '];
            header.fversion = ['V', '1', '.', '0'];

            header.AlphaBlendingAnimation = vvcFile.AlphaBlendingAnimation.ToArray();
            header.Animation = vvcFile.Animation.ToArray();
            header.Animation2 = vvcFile.Animation2.ToArray();
            header.Bam1Sequence = vvcFile.Bam1Sequence;
            header.Bam2Sequence = vvcFile.Bam2Sequence;
            header.Bam3Sequence = vvcFile.Bam3Sequence;
            header.BaseOrientation = vvcFile.BaseOrientation;
            header.BitmapPalette = vvcFile.BitmapPalette.ToArray();
            header.CentreX = vvcFile.CentreX;
            header.CentreY = vvcFile.CentreY;
            header.ColourFlags = vvcFile.ColourFlags.NotLightSource ? Convert.ToUInt16(header.ColourFlags | Common.Bit0) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.LightSource ? Convert.ToUInt16(header.ColourFlags | Common.Bit1) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.InternalBrightness ? Convert.ToUInt16(header.ColourFlags | Common.Bit2) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Timestopped ? Convert.ToUInt16(header.ColourFlags | Common.Bit3) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Bit4 ? Convert.ToUInt16(header.ColourFlags | Common.Bit4) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.InternalGamma ? Convert.ToUInt16(header.ColourFlags | Common.Bit5) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.NonReservedPalette ? Convert.ToUInt16(header.ColourFlags | Common.Bit6) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.FullPalette ? Convert.ToUInt16(header.ColourFlags | Common.Bit7) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Blend ? Convert.ToUInt16(header.ColourFlags | Common.Bit8) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Sepia ? Convert.ToUInt16(header.ColourFlags | Common.Bit9) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Bit10 ? Convert.ToUInt16(header.ColourFlags | Common.Bit10) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Bit11 ? Convert.ToUInt16(header.ColourFlags | Common.Bit11) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Bit12 ? Convert.ToUInt16(header.ColourFlags | Common.Bit12) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Bit13 ? Convert.ToUInt16(header.ColourFlags | Common.Bit13) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Bit14 ? Convert.ToUInt16(header.ColourFlags | Common.Bit14) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Bit15 ? Convert.ToUInt16(header.ColourFlags | Common.Bit15) : header.ColourFlags;
            header.CurrentAnimationSequence = vvcFile.CurrentAnimationSequence;
            header.DisplayFlags = vvcFile.DisplayFlags.Transparent ? Convert.ToUInt16(header.DisplayFlags | Common.Bit0) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Translucent ? Convert.ToUInt16(header.DisplayFlags | Common.Bit1) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.TranslucentShadow ? Convert.ToUInt16(header.DisplayFlags | Common.Bit2) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Blended ? Convert.ToUInt16(header.DisplayFlags | Common.Bit3) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.MirrorXAxis ? Convert.ToUInt16(header.DisplayFlags | Common.Bit4) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.MirrorYAxis ? Convert.ToUInt16(header.DisplayFlags | Common.Bit5) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Clipped ? Convert.ToUInt16(header.DisplayFlags | Common.Bit6) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.CopyFromBack ? Convert.ToUInt16(header.DisplayFlags | Common.Bit7) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.ClearFill ? Convert.ToUInt16(header.DisplayFlags | Common.Bit8) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Blend3D ? Convert.ToUInt16(header.DisplayFlags | Common.Bit9) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.NotCoveredByWall ? Convert.ToUInt16(header.DisplayFlags | Common.Bit10) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.PersistThroughTimestop ? Convert.ToUInt16(header.DisplayFlags | Common.Bit11) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.IgnoreDreamPalette ? Convert.ToUInt16(header.DisplayFlags | Common.Bit12) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Blend2D ? Convert.ToUInt16(header.DisplayFlags | Common.Bit13) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Bit14 ? Convert.ToUInt16(header.DisplayFlags | Common.Bit14) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Scale ? Convert.ToUInt16(header.DisplayFlags | Common.Bit15) : header.DisplayFlags;
            header.Duration = vvcFile.Duration;
            header.FrameRate = vvcFile.FrameRate;
            header.InternalName = vvcFile.InternalName.ToArray();
            header.LightingBrightness = vvcFile.LightingBrightness;
            header.LightingBrightness = vvcFile.LightingBrightness;
            header.OrientationCount = vvcFile.OrientationCount;
            header.PositionFlags = vvcFile.PositionFlags.OrbitTarget ? Convert.ToInt16(header.PositionFlags | Common.Bit0) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.RelativeToTarget ? Convert.ToInt16(header.PositionFlags | Common.Bit1) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit2 ? Convert.ToInt16(header.PositionFlags | Common.Bit2) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.IgnoreOrientation ? Convert.ToInt16(header.PositionFlags | Common.Bit3) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit4 ? Convert.ToInt16(header.PositionFlags | Common.Bit4) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit5 ? Convert.ToInt16(header.PositionFlags | Common.Bit5) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit6 ? Convert.ToInt16(header.PositionFlags | Common.Bit6) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit7 ? Convert.ToInt16(header.PositionFlags | Common.Bit7) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit8 ? Convert.ToInt16(header.PositionFlags | Common.Bit8) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit9 ? Convert.ToInt16(header.PositionFlags | Common.Bit9) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit10 ? Convert.ToInt16(header.PositionFlags | Common.Bit10) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit11 ? Convert.ToInt16(header.PositionFlags | Common.Bit11) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit12 ? Convert.ToInt16(header.PositionFlags | Common.Bit12) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit13 ? Convert.ToInt16(header.PositionFlags | Common.Bit13) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit14 ? Convert.ToInt16(header.PositionFlags | Common.Bit14) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit15 ? Convert.ToInt16(header.PositionFlags | Common.Bit15) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit16 ? Convert.ToInt16(header.PositionFlags | Common.Bit16) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit17 ? Convert.ToInt16(header.PositionFlags | Common.Bit17) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit18 ? Convert.ToInt16(header.PositionFlags | Common.Bit18) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit19 ? Convert.ToInt16(header.PositionFlags | Common.Bit19) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit20 ? Convert.ToInt16(header.PositionFlags | Common.Bit20) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit21 ? Convert.ToInt16(header.PositionFlags | Common.Bit21) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit22 ? Convert.ToInt16(header.PositionFlags | Common.Bit22) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit23 ? Convert.ToInt16(header.PositionFlags | Common.Bit23) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit24 ? Convert.ToInt16(header.PositionFlags | Common.Bit24) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit25 ? Convert.ToInt16(header.PositionFlags | Common.Bit25) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit26 ? Convert.ToInt16(header.PositionFlags | Common.Bit26) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit27 ? Convert.ToInt16(header.PositionFlags | Common.Bit27) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit28 ? Convert.ToInt16(header.PositionFlags | Common.Bit28) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit29 ? Convert.ToInt16(header.PositionFlags | Common.Bit29) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit30 ? Convert.ToInt16(header.PositionFlags | Common.Bit30) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Bit31 ? Convert.ToInt16(header.PositionFlags | Common.Bit31) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Looping ? Convert.ToInt16(header.PositionFlags | Common.Bit0) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.SpecialLighting ? Convert.ToInt16(header.PositionFlags | Common.Bit1) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.ModifyForHeight ? Convert.ToInt16(header.PositionFlags | Common.Bit2) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.DrawAnimation ? Convert.ToInt16(header.PositionFlags | Common.Bit3) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.CustomPalette ? Convert.ToInt16(header.PositionFlags | Common.Bit4) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Purgeable ? Convert.ToInt16(header.PositionFlags | Common.Bit5) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.NotCoveredByWallgroups ? Convert.ToInt16(header.PositionFlags | Common.Bit6) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.MidLevelBrighten ? Convert.ToInt16(header.PositionFlags | Common.Bit7) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.HighLevelBrighten ? Convert.ToInt16(header.PositionFlags | Common.Bit8) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit9 ? Convert.ToInt16(header.PositionFlags | Common.Bit9) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit10 ? Convert.ToInt16(header.PositionFlags | Common.Bit10) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit11 ? Convert.ToInt16(header.PositionFlags | Common.Bit11) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit12 ? Convert.ToInt16(header.PositionFlags | Common.Bit12) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit13 ? Convert.ToInt16(header.PositionFlags | Common.Bit13) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit14 ? Convert.ToInt16(header.PositionFlags | Common.Bit14) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit15 ? Convert.ToInt16(header.PositionFlags | Common.Bit15) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit16 ? Convert.ToInt16(header.PositionFlags | Common.Bit16) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit17 ? Convert.ToInt16(header.PositionFlags | Common.Bit17) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit18 ? Convert.ToInt16(header.PositionFlags | Common.Bit18) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit19 ? Convert.ToInt16(header.PositionFlags | Common.Bit19) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit20 ? Convert.ToInt16(header.PositionFlags | Common.Bit20) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit21 ? Convert.ToInt16(header.PositionFlags | Common.Bit21) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit22 ? Convert.ToInt16(header.PositionFlags | Common.Bit22) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit23 ? Convert.ToInt16(header.PositionFlags | Common.Bit23) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit24 ? Convert.ToInt16(header.PositionFlags | Common.Bit24) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit25 ? Convert.ToInt16(header.PositionFlags | Common.Bit25) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit26 ? Convert.ToInt16(header.PositionFlags | Common.Bit26) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit27 ? Convert.ToInt16(header.PositionFlags | Common.Bit27) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit28 ? Convert.ToInt16(header.PositionFlags | Common.Bit28) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit29 ? Convert.ToInt16(header.PositionFlags | Common.Bit29) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit30 ? Convert.ToInt16(header.PositionFlags | Common.Bit30) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Bit31 ? Convert.ToInt16(header.PositionFlags | Common.Bit31) : header.PositionFlags;
            header.Unused = vvcFile.Unused001c;
            header.Unused2 = vvcFile.Unused0024;
            header.Unused3 = vvcFile.Unused009c;
            header.UseContinuousSequence = vvcFile.UseContinuousSequence;
            header.UseOrientation = vvcFile.UseOrientation;
            header.Wav1 = vvcFile.Wav1.ToArray();
            header.Wav2 = vvcFile.Wav2.ToArray();
            header.Wav3 = vvcFile.Wav3.ToArray();
            header.XPosition = vvcFile.XPosition;
            header.YPosition = vvcFile.YPosition;
            header.ZPosition = vvcFile.ZPosition;

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