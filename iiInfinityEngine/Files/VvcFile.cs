using System;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class VvcFile : IEFile
    {
        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private readonly IEFileType fileType = IEFileType.Vvc;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public string Animation;
        public string Animation2;
        public DisplayFlags DisplayFlags;
        public ColourFlags ColourFlags;
        public Int32 Unused001c;
        public SequenceFlags SequenceFlags;
        public Int32 Unused0024;
        public Int32 XPosition;
        public Int32 YPosition;
        public Int32 UseOrientation;
        public Int32 FrameRate;
        public Int32 OrientationCount;
        public Int32 BaseOrientation;
        public PositionFlags PositionFlags;
        public string BitmapPalette;
        public Int32 ZPosition;
        public Int32 CentreX;
        public Int32 CentreY;
        public Int32 LightingBrightness;
        public Int32 Duration;
        public string InternalName;
        public Int32 Bam1Sequence;
        public Int32 Bam2Sequence;
        public Int32 CurrentAnimationSequence;
        public Int32 UseContinuousSequence;
        public string Wav1;
        public string Wav2;
        public string AlphaBlendingAnimation;
        public Int32 Bam3Sequence;
        public string Wav3;
        public byte[] Unused009c;
    }

    [Serializable]
    public struct DisplayFlags
    {
        public bool Transparent { get; set; }
        public bool Translucent { get; set; }
        public bool TranslucentShadow { get; set; }
        public bool Blended { get; set; }
        public bool MirrorXAxis { get; set; }
        public bool MirrorYAxis { get; set; }
        public bool Clipped { get; set; }
        public bool CopyFromBack { get; set; }
        public bool ClearFill { get; set; }
        public bool Blend3D { get; set; }
        public bool NotCoveredByWall { get; set; }
        public bool PersistThroughTimestop { get; set; }
        public bool IgnoreDreamPalette { get; set; }
        public bool Blend2D { get; set; }
        public bool Bit14 { get; set; }
        public bool Scale { get; set; }
    }

    [Serializable]
    public struct ColourFlags
    {
        public bool NotLightSource { get; set; }
        public bool LightSource { get; set; }
        public bool InternalBrightness { get; set; }
        public bool Timestopped { get; set; }
        public bool Bit4 { get; set; }
        public bool InternalGamma { get; set; }
        public bool NonReservedPalette { get; set; }
        public bool FullPalette { get; set; }
        public bool Blend { get; set; }
        public bool Sepia { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
    }

    [Serializable]
    public struct SequenceFlags
    {
        public bool Looping { get; set; }
        public bool SpecialLighting { get; set; }
        public bool ModifyForHeight { get; set; }
        public bool DrawAnimation { get; set; }
        public bool CustomPalette { get; set; }
        public bool Purgeable { get; set; }
        public bool NotCoveredByWallgroups { get; set; }
        public bool MidLevelBrighten { get; set; }
        public bool HighLevelBrighten { get; set; }
        public bool Bit9 { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
        public bool Bit16 { get; set; }
        public bool Bit17 { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool Bit20 { get; set; }
        public bool Bit21 { get; set; }
        public bool Bit22 { get; set; }
        public bool Bit23 { get; set; }
        public bool Bit24 { get; set; }
        public bool Bit25 { get; set; }
        public bool Bit26 { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool Bit31 { get; set; }
    }

    [Serializable]
    public struct PositionFlags
    {
        public bool OrbitTarget { get; set; }
        public bool RelativeToTarget { get; set; }
        public bool Bit2 { get; set; }
        public bool IgnoreOrientation { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool Bit9 { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
        public bool Bit16 { get; set; }
        public bool Bit17 { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool Bit20 { get; set; }
        public bool Bit21 { get; set; }
        public bool Bit22 { get; set; }
        public bool Bit23 { get; set; }
        public bool Bit24 { get; set; }
        public bool Bit25 { get; set; }
        public bool Bit26 { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool Bit31 { get; set; }
    }
}