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
        public array8 TravelWav;
        public array8 ArrivalWav;
        public array8 TravelBAM;
        public Int16 SparkColour;
        public array214 Unused;
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
        public array172 Unused2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ProExtendedHeaderBinary
    {
        public Int32 AreaProjectileFlags;
        public Int16 TriggerRadius;// (divide by approx 8.5 to receive radius in feet)
        public Int16 AreaOfEffect;// (divide by approx 8.5 to receive radius in feet)
        public array8 TriggerSound;
        public Int16 ExplosionDelay;
        public Int16 FragmentAnimation;// (animate.ids)
        public Int16 SecondaryProjectile;// (projectl.ids-1)
        public byte TriggerCount;// (used if bits 8 and 9 at 0x200 are not set)
        public byte ExplosionAnimation;// (fireball.ids)
        public byte ExplosionColour;
        public byte Unused;
        public Int16 ExplosionProjectile;// (projectl.ids), played on affected creatures
        public array8 ExplosionAnimationAnimation; // (VVC)
        public Int16 ConeWidth;// (1 to 359)
        public array218 Unused2;
    }
}