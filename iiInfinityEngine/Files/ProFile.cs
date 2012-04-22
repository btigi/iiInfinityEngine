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
        private IEFileType fileType = IEFileType.Pro;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public ProjectileType ProjectileType;
        public Int16 ProjectileSpeed;
        public SparkingFlags SparkingFlags;
        public array8 TravelWav;
        public array8 ArrivalWav;
        public array8 TravelBAM;
        public SparkColour SparkColour;
        public array214 Unused;
        public ProjectileFlags ProjectileFlags;
        public string ProjectileAnimation;
        public string ShadowAnimation;
        public byte ProjectileNumberBAMSequence;
        public byte ShadowNumberBAMSequence;
        public Int16 LightSpotIntensity;
        public Int16 LightSpotWidth;
        public Int16 LightSpotHeight;
        public string Palette;
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
        public string TrailingAnimation1;
        public string TrailingAnimation2;
        public string TrailingAnimation3;
        public Int16 TrailingBAMSequence1;
        public Int16 TrailingBAMSequence2;
        public Int16 TrailingBAMSequence3;
        public array172 Unused2;

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
        public Int16 TriggerRadius { get; set; }// (divide by approx 8.5 to receive radius in feet)
        public Int16 AreaOfEffect { get; set; }// (divide by approx 8.5 to receive radius in feet)
        public array8 TriggerSound { get; set; }
        public Int16 ExplosionDelay { get; set; }
        public Int16 FragmentAnimation { get; set; }// (animate.ids)
        public Int16 SecondaryProjectile { get; set; }// (projectl.ids-1)
        public byte TriggerCount { get; set; }// (used if bits 8 and 9 at 0x200 are not set)
        public byte ExplosionAnimation { get; set; }// (fireball.ids)
        public byte ExplosionColour { get; set; }
        public byte Unused { get; set; }
        public Int16 ExplosionProjectile { get; set; }// (projectl.ids), played on affected creatures
        public array8 ExplosionAnimationAnimation { get; set; } // (VVC)
        public Int16 ConeWidth { get; set; }// (1 to 359)
        public array218 Unused2;
    }

    [Serializable]
    public enum ProjectileType
    {
        NoBam = 1,
        SingleTarget = 2,
        AreaOfEffect = 3
    }

    [Serializable]
    public struct SparkingFlags
    {
        public bool ShowSparkle { get; set; }
        public bool UseZCoordinate { get; set; }
        public bool LoopTravelSound { get; set; }
        public bool LoopArrivalSound { get; set; }
        public bool DoNotAffectDirectTarget { get; set; }
        public bool DrawBelowAnimateObjects { get; set; }
    }

    [Serializable]
    public enum SparkColour
    {
        Crashe = 0,
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
        public bool Unused { get; set; }
        public bool EnableAreaLighting { get; set; }
        public bool EnableAreaHeight { get; set; }
        public bool EnableShadow { get; set; }
        public bool EnableLightSpot { get; set; }
        public bool EnableBrightenFlags { get; set; }
        public bool LowLevelBrighten { get; set; }
        public bool HighLevelBrighten { get; set; }
    }

    [Serializable]
    public enum FaceTargetGranularity
    {
        DoNotFaceTarget = 1,
        MirroredEasternDirectionsReducedGranularity = 5,
        MirroredEasternDirectionsFullGranularity = 9,
        NotMirroredNotReduced = 16,
    }

    [Serializable]
    public struct AreaProjectileFlags
    {
        public bool ProjectileRemainsVisibleAtDestination { get; set; }
        public bool TriggeredByInanimateObjects { get; set; }
        public bool TriggeredOnCondition { get; set; }
        public bool TriggerDuringDelay { get; set; }
        public bool UseSecondaryProjectile { get; set; }
        public bool UseFragmentGraphics { get; set; }
        public bool TargetSelection1 { get; set; }
        public bool TargetSelection2 { get; set; }
        public bool NumberOfTriggersEqualToCastMageLevel { get; set; }
        public bool NumberOfTriggersEqualToCastClericLevel { get; set; }
        public bool UseVVC { get; set; }
        public bool ConeShape { get; set; }
        public bool AffectObjectsThroughWallMountainBuildingAndOffArea { get; set; }
        public bool CheckTriggersFromAnimationFrame30 { get; set; } //  (req. bit 0) 
        public bool DelayedExplosion { get; set; }
        public bool AffectOnlyOneTarget { get; set; }

        //*Target selection is relative to caster:
        //00 Affect all
        //01 Affect only enemies
        //10 Affect all
        //11 Affect only allies
    }
}