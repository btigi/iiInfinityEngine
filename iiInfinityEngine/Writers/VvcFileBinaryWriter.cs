using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using System;
using iiInfinityEngine.Core.Writers.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace iiInfinityEngine.Core.Writers
{
    public class VvcFileBinaryWriter : IVvcFileWriter
    {
        public BackupManager BackupManger { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (!(file is VvcFile))
                throw new ArgumentException("File is not a valid creature file");

            var vvcFile = file as VvcFile;

            if (!(forceSave) && (MD5HashGenerator.GenerateKey(vvcFile) == vvcFile.Checksum))
                return false;

            VvcHeaderBinary header = new VvcHeaderBinary();

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
            header.ColourFlags = vvcFile.ColourFlags.NotLightSource ? Convert.ToUInt16(header.ColourFlags | Common.Bit0) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.LightSource ? Convert.ToUInt16(header.ColourFlags | Common.Bit1) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.InternalBrightness ? Convert.ToUInt16(header.ColourFlags | Common.Bit2) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Timestopped ? Convert.ToUInt16(header.ColourFlags | Common.Bit3) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused ? Convert.ToUInt16(header.ColourFlags | Common.Bit4) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.InternalGamma ? Convert.ToUInt16(header.ColourFlags | Common.Bit5) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.NonReservedPalette ? Convert.ToUInt16(header.ColourFlags | Common.Bit6) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.FullPalette ? Convert.ToUInt16(header.ColourFlags | Common.Bit7) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused ? Convert.ToUInt16(header.ColourFlags | Common.Bit8) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Sepia ? Convert.ToUInt16(header.ColourFlags | Common.Bit9) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused2 ? Convert.ToUInt16(header.ColourFlags | Common.Bit10) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused3 ? Convert.ToUInt16(header.ColourFlags | Common.Bit11) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused4 ? Convert.ToUInt16(header.ColourFlags | Common.Bit12) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused5 ? Convert.ToUInt16(header.ColourFlags | Common.Bit13) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused6 ? Convert.ToUInt16(header.ColourFlags | Common.Bit14) : header.ColourFlags;
            header.ColourFlags = vvcFile.ColourFlags.Unused7 ? Convert.ToUInt16(header.ColourFlags | Common.Bit15) : header.ColourFlags;
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
            header.DisplayFlags = vvcFile.DisplayFlags.Unused1 ? Convert.ToUInt16(header.DisplayFlags | Common.Bit14) : header.DisplayFlags;
            header.DisplayFlags = vvcFile.DisplayFlags.Unused2 ? Convert.ToUInt16(header.DisplayFlags | Common.Bit15) : header.DisplayFlags;
            header.Duration = vvcFile.Duration;
            header.FrameRate = vvcFile.FrameRate;
            header.InternalName = new array8(vvcFile.InternalName);
            header.LightingBrightness = vvcFile.LightingBrightness;
            header.LightingBrightness = vvcFile.LightingBrightness;
            header.OrientationCount = vvcFile.OrientationCount;
            header.PositionFlags = vvcFile.PositionFlags.OrbitTarget ? Convert.ToInt16(header.PositionFlags | Common.Bit0) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.RelativeToTarget ? Convert.ToInt16(header.PositionFlags | Common.Bit1) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.Unused ? Convert.ToInt16(header.PositionFlags | Common.Bit2) : header.PositionFlags;
            header.PositionFlags = vvcFile.PositionFlags.IgnoreOrientation ? Convert.ToInt16(header.PositionFlags | Common.Bit3) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Looping ? Convert.ToInt16(header.PositionFlags | Common.Bit0) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.SpecialLighting ? Convert.ToInt16(header.PositionFlags | Common.Bit1) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.ModifyForHeight ? Convert.ToInt16(header.PositionFlags | Common.Bit2) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.DrawAnimation ? Convert.ToInt16(header.PositionFlags | Common.Bit3) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.CustomPalette ? Convert.ToInt16(header.PositionFlags | Common.Bit4) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.Purgeable ? Convert.ToInt16(header.PositionFlags | Common.Bit5) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.NotCoveredByWallgroups ? Convert.ToInt16(header.PositionFlags | Common.Bit6) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.MidLevelBrighten ? Convert.ToInt16(header.PositionFlags | Common.Bit7) : header.PositionFlags;
            header.SequenceFlags = vvcFile.SequenceFlags.HighLevelBrighten ? Convert.ToInt16(header.PositionFlags | Common.Bit8) : header.PositionFlags;
            header.Unused = vvcFile.Unused001c;
            header.Unused2 = vvcFile.Unused0024;
            header.Unused3 = vvcFile.Unused009c;
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