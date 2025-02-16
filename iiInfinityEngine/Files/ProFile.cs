using System;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class ProFile : IEFile
    {
        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private readonly IEFileType fileType = IEFileType.Pro;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public ProjectileType ProjectileType;
        public Int16 ProjectileSpeed;
        public SparkingFlags SparkingFlags;
        public array8 FireWav;
        public array8 ImpactWav;
        public array8 SourceAnimationBam;
        public SparkColour SparkColour;        
        public array214 Unused; //TODO:PRO
        public ProjectileFlags ProjectileFlags;
        public array8 ProjectileAnimation;
        public array8 ShadowAnimation;
        public byte ProjectileNumberBAMSequence;
        public byte ShadowNumberBAMSequence;
        public Int16 LightSpotIntensity;
        public Int16 LightSpotWidth;
        public Int16 LightSpotHeight;
        public array8 Palette;
        public byte ProjectileColour1;
        public byte ProjectileColour2;
        public byte ProjectileColour3;
        public byte ProjectileColour4;
        public byte ProjectileColour5;
        public byte ProjectileColour6;
        public byte ProjectileColour7;
        public byte SmokePuffPeriod;
        public byte SmokeColour1;
        public byte SmokeColour2;
        public byte SmokeColour3;
        public byte SmokeColour4;
        public byte SmokeColour5;
        public byte SmokeColour6;
        public byte SmokeColour7;
        public FaceTargetGranularity FaceTargetGranularity;
        public Int16 ProjectileSmokeAnimation;// (animate.ids) 
        public array8 TrailingAnimation1;
        public array8 TrailingAnimation2;
        public array8 TrailingAnimation3;
        public Int16 TrailingBAMSequence1;
        public Int16 TrailingBAMSequence2;
        public Int16 TrailingBAMSequence3;
        public array172 Unused2; //TODO:PRO

        public ProExtendedHeader ExtendedHeader;

        public ProFile()
        {
            ExtendedHeader = new ProExtendedHeader();
        }
    }

    [Serializable]
    public class ProExtendedHeader
    {
        public AreaProjectileFlags AreaProjectileFlags;
        public Int16 RayCount { get; set; }
        public Int16 TriggerRadius { get; set; }// (divide by approx 8.5 to receive radius in feet)
        public Int16 AreaOfEffect { get; set; }// (divide by approx 8.5 to receive radius in feet)
        public array8 ExplosionSound { get; set; }
        public Int16 ExplosionDelay { get; set; }
        public Int16 FragmentAnimation { get; set; }// (animate.ids)
        public Int16 SecondaryProjectile { get; set; }// (projectl.ids-1)
        public byte TriggerCount { get; set; }// (used if bits 8 and 9 at 0x200 are not set)
        public byte ExplosionEffect { get; set; }// (fireball.ids)
        public byte ExplosionColour { get; set; }
        public byte Unused219 { get; set; }
        public Int16 ExplosionProjectile { get; set; }// (projectl.ids), played on affected creatures
        public array8 ExplosionAnimation { get; set; } // (VVC)
        public Int16 ConeWidth { get; set; }// (1 to 359)
        public array218 Unused2; //TODO:PRO
    }

    [Serializable]
    public enum ProjectileType
    {
        NoProjectile = 0,
        NoBam = 1,
        SingleTarget = 2,
        AreaOfEffect = 3
    }

    [Serializable]
    public struct SparkingFlags
    {
        public bool ShowSparkle { get; set; }
        public bool UseZCoordinate { get; set; }
        public bool LoopFireSound { get; set; }
        public bool LoopImpactSound { get; set; }
        public bool DoNotAffectDirectTarget { get; set; }
        public bool DrawBelowAnimateObjects { get; set; }
        public bool AllowSavingGame { get; set; }
        public bool LoopSpreadAnimation { get; set; }
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

    [Serializable]
    public enum SparkColour
    {
        Black = 1,
        Blue = 2,
        Chromatic = 3,
        Gold = 4,
        Green = 5,
        Purple = 6,
        Red = 7,
        White = 8,
        Ice = 9,
        Stone = 10,
        Magenta = 11,
        Orange = 12
    }

    [Serializable]
    public struct ProjectileFlags
    {
        public bool EnableBAMColouring { get; set; }//(palette at 0x11c)
        public bool EnableSmoke { get; set; }
        public bool ColouredSmoke { get; set; }
        public bool EnableAreaLighting { get; set; }
        public bool EnableAreaHeight { get; set; }
        public bool EnableShadow { get; set; }
        public bool EnableLightSpot { get; set; }
        public bool EnableBrightenFlags { get; set; }
        public bool LowLevelBrighten { get; set; }
        public bool HighLevelBrighten { get; set; }
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
    public enum FaceTargetGranularity
    {
        DoNotMirror = 0,
        DoNotFaceTarget = 1,
        MirroredEasternDirectionsReducedGranularity = 5,
        MirroredEasternDirectionsFullGranularity = 9,
        NotMirroredNotReduced = 16,
    }

    [Serializable]
    public struct AreaProjectileFlags
    {
        public bool TrapVisible { get; set; }
        public bool TriggeredByInanimateObjects { get; set; }
        public bool TriggeredOnCondition { get; set; }
        public bool TriggerDelayed { get; set; }
        public bool UseSecondaryProjectile { get; set; }
        public bool UseFragmentGraphics { get; set; }
        public bool TargetSelection1 { get; set; }
        public bool TargetSelection2 { get; set; }
        public bool MageLevelDuration { get; set; }
        public bool ClericLevelDuration { get; set; }
        public bool UseVVC { get; set; }
        public bool ConeShape { get; set; }
        public bool IgnoreLos { get; set; }
        public bool DelayedExplosion { get; set; } //  (req. bit 0) 
        public bool SkipFirstCondition { get; set; }
        public bool SingleTarget { get; set; }
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