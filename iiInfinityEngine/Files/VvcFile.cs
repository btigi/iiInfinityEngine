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

        public VvcFile()
        {
            DisplayFlags = new DisplayFlags();
            ColourFlags = new ColourFlags();
            SequenceFlags = new SequenceFlags();
            PositionFlags = new PositionFlags();
        }

        public string Animation { get; set; }
        public string Animation2 { get; set; }
        public DisplayFlags DisplayFlags { get; set; }
        public ColourFlags ColourFlags { get; set; }
        public Int32 Unused001c { get; set; }
        public SequenceFlags SequenceFlags { get; set; }
        public Int32 Unused0024 { get; set; }
        public Int32 XPosition { get; set; }
        public Int32 YPosition { get; set; }
        public Int32 UseOrientation { get; set; }
        public Int32 FrameRate { get; set; }
        public Int32 OrientationCount { get; set; }
        public Int32 BaseOrientation { get; set; }
        public PositionFlags PositionFlags { get; set; }
        public string BitmapPalette { get; set; }
        public Int32 ZPosition { get; set; }
        public Int32 CentreX { get; set; }
        public Int32 CentreY { get; set; }
        public Int32 LightingBrightness { get; set; }
        public Int32 Duration { get; set; }
        public string InternalName { get; set; }
        public Int32 Bam1Sequence { get; set; }
        public Int32 Bam2Sequence { get; set; }
        public Int32 CurrentAnimationSequence { get; set; }
        public Int32 UseContinuousSequence { get; set; }
        public string Wav1 { get; set; }
        public string Wav2 { get; set; }
        public string AlphaBlendingAnimation { get; set; }
        public Int32 Bam3Sequence { get; set; }
        public string Wav3 { get; set; }
        public byte[] Unused009c { get; set; }
    }

    [Serializable]
    public class DisplayFlags
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
    public class ColourFlags
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
    public class SequenceFlags
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
    public class PositionFlags
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