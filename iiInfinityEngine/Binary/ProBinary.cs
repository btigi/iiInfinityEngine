using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ProHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int16 ProjectileType;
        public Int16 ProjectileSpeed;
        public Int32 SparkingFlags;
        public array8 FireWav;
        public array8 ImpactWav;
        public array8 SourceAnimationBam;
        public Int16 SparkColour;
        public Int16 ProjectileWidth;
        public Int32 ExtendedFlags;
        public Int32 DisplayedMessage;
        public Int32 PulseColour;
        public Int16 ColourSpeed;
        public Int16 ScreenShakeAmount;
        public Int16 CreatureValue1;
        public Int16 CreatureType1;
        public Int16 CreatureValue2;
        public Int16 CreatureType2;
        public array8 DefaultSpell;
        public array8 SuccessSpell;
        public Int16 AngleIncreaseMinimum;
        public Int16 AngleIncreaseMaximum;
        public Int16 CurveMinimum;
        public Int16 CurveMaximum;
        public Int16 Thac0Bonus;
        public Int16 Thac0BonusNonActor;
        public Int16 RadiusMinumum;
        public Int16 RadiusMaximum;
        public array156 Unused64;
        public Int32 ProjectileFlags;
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
        public byte FaceTargetGranularity;
        public Int16 ProjectileSmokeAnimation;// (animate.ids) 
        public array8 TrailingAnimation1;
        public array8 TrailingAnimation2;
        public array8 TrailingAnimation3;
        public Int16 TrailingBAMSequence1;
        public Int16 TrailingBAMSequence2;
        public Int16 TrailingBAMSequence3;
        public Int32 PuffFlags;
        public array168 Unused158;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ProExtendedHeaderBinary
    {
        public Int32 AreaProjectileFlags;
        public Int16 RayCount;
        public Int16 TriggerRadius;// (divide by approx 8.5 to receive radius in feet)
        public Int16 AreaOfEffect;// (divide by approx 8.5 to receive radius in feet)
        public array8 ExplosionSound;
        public Int16 ExplosionDelay;
        public Int16 FragmentAnimation;// (animate.ids)
        public Int16 SecondaryProjectile;// (projectl.ids-1)
        public byte TriggerCount;// (used if bits 8 and 9 at 0x200 are not set)
        public byte ExplosionEffect;// (fireball.ids)
        public byte ExplosionColour;
        public byte Unused219;
        public Int16 ExplosionProjectile;// (projectl.ids), played on affected creatures
        public array8 ExplosionAnimation; // (VVC)
        public Int16 ConeWidth;// (1 to 359)
        public Int16 RotateRaysClockwise;
        public array8 SpreadAnimation;
        public array8 RingAnimation;
        public array8 AreaSound;
        public Int32 EnhancedExtendedFlags;
        public Int16 DiceThrown;
        public Int16 DiceSize;
        public Int16 AnimationGranularity;
        public Int16 AnimationGranularityDivider;
        public array180 Unused24c;
    }
}