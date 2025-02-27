using System;

namespace ii.InfinityEngine.Files
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

        public ProFile()
        {
            PuffFlags = new PuffFlags();
            SparkingFlags = new SparkingFlags();
            ExtendedFlags = new ExtendedFlags();
            ExtendedHeader = new ProExtendedHeader();
            ProjectileFlags = new ProjectileFlags();
        }

        public ProjectileType ProjectileType { get; set; }
        public Int16 ProjectileSpeed { get; set; }
        public SparkingFlags SparkingFlags { get; set; }
        public array8 FireWav { get; set; }
        public array8 ImpactWav { get; set; }
        public array8 SourceAnimationBam { get; set; }
        public SparkColour SparkColour { get; set; }
        public Int16 ProjectileWidth { get; set; }
        public ExtendedFlags ExtendedFlags { get; set; }
        public IEString DisplayedMessage { get; set; }
        public Int32 PulseColour { get; set; }
        public Int16 ColourSpeed { get; set; }
        public Int16 ScreenShakeAmount { get; set; }
        public Int16 CreatureValue1 { get; set; }
        public Int16 CreatureType1 { get; set; }
        public Int16 CreatureValue2 { get; set; }
        public Int16 CreatureType2 { get; set; }
        public array8 DefaultSpell { get; set; }
        public array8 SuccessSpell { get; set; }
        public Int16 AngleIncreaseMinimum { get; set; }
        public Int16 AngleIncreaseMaximum { get; set; }
        public Int16 CurveMinimum { get; set; }
        public Int16 CurveMaximum { get; set; }
        public Int16 Thac0Bonus { get; set; }
        public Int16 Thac0BonusNonActor { get; set; }
        public Int16 RadiusMinumum { get; set; }
        public Int16 RadiusMaximum { get; set; }
        public array156 Unused64 { get; set; }
        public ProjectileFlags ProjectileFlags { get; set; }
        public array8 ProjectileAnimation { get; set; }
        public array8 ShadowAnimation { get; set; }
        public byte ProjectileNumberBAMSequence { get; set; }
        public byte ShadowNumberBAMSequence { get; set; }
        public Int16 LightSpotIntensity { get; set; }
        public Int16 LightSpotWidth { get; set; }
        public Int16 LightSpotHeight { get; set; }
        public array8 Palette { get; set; }
        public byte ProjectileColour1 { get; set; }
        public byte ProjectileColour2 { get; set; }
        public byte ProjectileColour3 { get; set; }
        public byte ProjectileColour4 { get; set; }
        public byte ProjectileColour5 { get; set; }
        public byte ProjectileColour6 { get; set; }
        public byte ProjectileColour7 { get; set; }
        public byte SmokePuffPeriod { get; set; }
        public byte SmokeColour1 { get; set; }
        public byte SmokeColour2 { get; set; }
        public byte SmokeColour3 { get; set; }
        public byte SmokeColour4 { get; set; }
        public byte SmokeColour5 { get; set; }
        public byte SmokeColour6 { get; set; }
        public byte SmokeColour7 { get; set; }
        public FaceTargetGranularity FaceTargetGranularity;
        public Int16 ProjectileSmokeAnimation { get; set; }// (animate.ids) 
        public array8 TrailingAnimation1 { get; set; }
        public array8 TrailingAnimation2 { get; set; }
        public array8 TrailingAnimation3 { get; set; }
        public Int16 TrailingBAMSequence1 { get; set; }
        public Int16 TrailingBAMSequence2 { get; set; }
        public Int16 TrailingBAMSequence3 { get; set; }
        public PuffFlags PuffFlags { get; set; }
        public array168 Unused158 { get; set; }

        public ProExtendedHeader ExtendedHeader { get; set; }
    }

    [Serializable]
    public class ProExtendedHeader
    {
        public ProExtendedHeader()
        {
            AreaProjectileFlags= new AreaProjectileFlags();
            EnhancedExtendedFlags = new EnhancedExtendedFlags();
        }

        public AreaProjectileFlags AreaProjectileFlags { get; set; }
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
        public Int16 RotateRaysClockwise { get; set; }
        public array8 SpreadAnimation { get; set; }
        public array8 RingAnimation { get; set; }
        public array8 AreaSound { get; set; }
        public EnhancedExtendedFlags EnhancedExtendedFlags { get; set; }
        public Int16 DiceThrown { get; set; }
        public Int16 DiceSize { get; set; }
        public Int16 AnimationGranularity { get; set; }
        public Int16 AnimationGranularityDivider { get; set; }
        public array180 Unused24c { get; set; }
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
    public class SparkingFlags
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
    public class ExtendedFlags
    {
        public bool BounceFromWalls { get; set; }
        public bool PassTarget { get; set; }
        public bool DrawCenterVvcOnce { get; set; }
        public bool HitImmediately { get; set; }
        public bool FaceTarget { get; set; }
        public bool CurvedTarget { get; set; }
        public bool StartRandomFrame { get; set; }
        public bool Pillar { get; set; }
        public bool SemiTransparentTrailPuffVef { get; set; }
        public bool TintedTrailPuffVef { get; set; }
        public bool MultipleProjectiles { get; set; }
        public bool DefaultSpellOnMissed { get; set; }
        public bool FallingPath { get; set; }
        public bool Comet { get; set; }
        public bool LinedUpAreaOfEffect { get; set; }
        public bool RectangularAreaOfEffect { get; set; }
        public bool DrawBehindTarget { get; set; }
        public bool CastingGlowEffect { get; set; }
        public bool TravelDoor { get; set; }
        public bool StopFadeAfterHit { get; set; }
        public bool DisplayMessage { get; set; }
        public bool RandomPath { get; set; }
        public bool StartRandomSequence { get; set; }
        public bool ColourPulseOnHit { get; set; }
        public bool TouchProjectile { get; set; }
        public bool NegateFirstCreatureTarget { get; set; }
        public bool NegateSecondCreatureTarget { get; set; }
        public bool UseEitherIds { get; set; }
        public bool DelayedPayload { get; set; }
        public bool LimitedPathCount { get; set; }
        public bool IwdStyleCheck { get; set; }
        public bool CasterAffected { get; set; }
    }

    [Serializable]
    public class EnhancedExtendedFlags
    {
        public bool PalettedRing { get; set; }
        public bool RandomSpeed { get; set; }
        public bool StartScattered { get; set; }
        public bool PalettedCenter { get; set; }
        public bool RepeatScattering { get; set; }
        public bool PalettedAnimation { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool OrientedFireballPuffs { get; set; }
        public bool UseHitDiceLookup { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool BlendAreaRingAnimation { get; set; }
        public bool GlowAreaRingAnimation { get; set; }
        public bool HitPointLimit { get; set; }
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
    public class ProjectileFlags
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
    public class PuffFlags
    {
        public bool PuffAtTarget { get; set; }
        public bool PuffAtSource { get; set; }
        public bool Bit2 { get; set; }
        public bool Bit3 { get; set; }
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
    public class AreaProjectileFlags
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