using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers
{
    public class ProFileBinaryReader : IProFileReader
    {
        public ProFile Read(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var f = Read(fs);
                f.Filename = Path.GetFileName(filename);
                return f;
            }
        }

        public ProFile Read(Stream s)
        {
            using (BinaryReader br = new BinaryReader(s))
            {
                var proFile = ParseFile(br);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                proFile.OriginalFile = ParseFile(br);
                return proFile;
            }
        }

        private ProFile ParseFile(BinaryReader br)
        {
            var header = (ProHeaderBinary)Common.ReadStruct(br, typeof(ProHeaderBinary));

            ProFile proFile = new ProFile();
            proFile.ArrivalWav = header.ArrivalWav;
            proFile.FaceTargetGranularity = (FaceTargetGranularity)header.FaceTargetGranularity;
            proFile.LightSpotHeight = header.LightSpotHeight;
            proFile.LightSpotIntensity = header.LightSpotIntensity;
            proFile.LightSpotWidth = header.LightSpotWidth;
            proFile.Palette = header.Palette.ToString();
            proFile.ProjectileAnimation = header.ProjectileAnimation.ToString();
            proFile.ProjectileColour1 = header.ProjectileColour1;
            proFile.ProjectileColour2 = header.ProjectileColour2;
            proFile.ProjectileColour3 = header.ProjectileColour3;
            proFile.ProjectileColour4 = header.ProjectileColour4;
            proFile.ProjectileColour5 = header.ProjectileColour5;
            proFile.ProjectileColour6 = header.ProjectileColour6;
            proFile.ProjectileColour7 = header.ProjectileColour7;
            proFile.ProjectileFlags.EnableBAMColouring = (header.ProjectileFlags & Common.Bit0) != 0;
            proFile.ProjectileFlags.EnableSmoke = (header.ProjectileFlags & Common.Bit1) != 0;
            proFile.ProjectileFlags.Unused = (header.ProjectileFlags & Common.Bit2) != 0;
            proFile.ProjectileFlags.EnableAreaLighting = (header.ProjectileFlags & Common.Bit3) != 0;
            proFile.ProjectileFlags.EnableAreaHeight = (header.ProjectileFlags & Common.Bit4) != 0;
            proFile.ProjectileFlags.EnableShadow = (header.ProjectileFlags & Common.Bit5) != 0;
            proFile.ProjectileFlags.EnableLightSpot = (header.ProjectileFlags & Common.Bit6) != 0;
            proFile.ProjectileFlags.EnableBrightenFlags = (header.ProjectileFlags & Common.Bit7) != 0;
            proFile.ProjectileFlags.LowLevelBrighten = (header.ProjectileFlags & Common.Bit8) != 0;
            proFile.ProjectileFlags.HighLevelBrighten = (header.ProjectileFlags & Common.Bit9) != 0;
            proFile.ProjectileNumberBAMSequence = header.ProjectileNumberBAMSequence;
            proFile.ProjectileSmokeAnimation = header.ProjectileSmokeAnimation;
            proFile.ProjectileSpeed = header.ProjectileSpeed;
            proFile.ProjectileType = (ProjectileType)header.ProjectileType;
            proFile.ShadowAnimation = header.ShadowAnimation.ToString();
            proFile.ShadowNumberBAMSequence = header.ShadowNumberBAMSequence;
            proFile.SmokeColour1 = header.SmokeColour1;
            proFile.SmokeColour2 = header.SmokeColour2;
            proFile.SmokeColour3 = header.SmokeColour3;
            proFile.SmokeColour4 = header.SmokeColour4;
            proFile.SmokeColour5 = header.SmokeColour5;
            proFile.SmokeColour6 = header.SmokeColour6;
            proFile.SmokeColour7 = header.SmokeColour7;
            proFile.SmokePuffPeriod = header.SmokePuffPeriod;
            proFile.SparkColour = (SparkColour)header.SparkColour;
            proFile.SparkingFlags.ShowSparkle = (header.ProjectileFlags & Common.Bit0) != 0;
            proFile.SparkingFlags.UseZCoordinate = (header.ProjectileFlags & Common.Bit1) != 0;
            proFile.SparkingFlags.LoopTravelSound = (header.ProjectileFlags & Common.Bit2) != 0;
            proFile.SparkingFlags.LoopArrivalSound = (header.ProjectileFlags & Common.Bit3) != 0;
            proFile.SparkingFlags.DoNotAffectDirectTarget = (header.ProjectileFlags & Common.Bit4) != 0;
            proFile.SparkingFlags.DrawBelowAnimateObjects = (header.ProjectileFlags & Common.Bit5) != 0;
            proFile.TrailingAnimation1 = header.TrailingAnimation1.ToString();
            proFile.TrailingAnimation2 = header.TrailingAnimation2.ToString();
            proFile.TrailingAnimation3 = header.TrailingAnimation3.ToString();
            proFile.TrailingBAMSequence1 = header.TrailingBAMSequence1;
            proFile.TrailingBAMSequence2 = header.TrailingBAMSequence2;
            proFile.TrailingBAMSequence3 = header.TrailingBAMSequence3;
            proFile.TravelBAM = header.ArrivalWav;
            proFile.TravelWav = header.ArrivalWav;

            if (header.ProjectileType == (int)ProjectileType.AreaOfEffect)
            {
                var extendedHeader = (ProExtendedHeaderBinary)Common.ReadStruct(br, typeof(ProExtendedHeaderBinary));
                proFile.ExtendedHeader.AreaOfEffect = extendedHeader.AreaOfEffect;

                proFile.ExtendedHeader.AreaProjectileFlags.ProjectileRemainsVisibleAtDestination = (header.ProjectileFlags & Common.Bit0) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.TriggeredByInanimateObjects = (header.ProjectileFlags & Common.Bit1) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.TriggeredOnCondition = (header.ProjectileFlags & Common.Bit2) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.TriggerDuringDelay = (header.ProjectileFlags & Common.Bit3) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.UseSecondaryProjectile = (header.ProjectileFlags & Common.Bit4) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.UseFragmentGraphics = (header.ProjectileFlags & Common.Bit5) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.TargetSelection1 = (header.ProjectileFlags & Common.Bit6) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.TargetSelection2 = (header.ProjectileFlags & Common.Bit7) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.NumberOfTriggersEqualToCastMageLevel = (header.ProjectileFlags & Common.Bit8) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.NumberOfTriggersEqualToCastClericLevel = (header.ProjectileFlags & Common.Bit9) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.UseVVC = (header.ProjectileFlags & Common.Bit10) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.ConeShape = (header.ProjectileFlags & Common.Bit11) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.AffectObjectsThroughWallMountainBuildingAndOffArea = (header.ProjectileFlags & Common.Bit12) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.CheckTriggersFromAnimationFrame30 = (header.ProjectileFlags & Common.Bit13) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.DelayedExplosion = (header.ProjectileFlags & Common.Bit14) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.AffectOnlyOneTarget = (header.ProjectileFlags & Common.Bit15) != 0;

                proFile.ExtendedHeader.ConeWidth = extendedHeader.ConeWidth;
                proFile.ExtendedHeader.ExplosionAnimation = extendedHeader.ExplosionAnimation;
                proFile.ExtendedHeader.ExplosionAnimationAnimation = extendedHeader.ExplosionAnimationAnimation;
                proFile.ExtendedHeader.ExplosionColour = extendedHeader.ExplosionColour;
                proFile.ExtendedHeader.ExplosionDelay = extendedHeader.ExplosionDelay;
                proFile.ExtendedHeader.ExplosionProjectile = extendedHeader.ExplosionProjectile;
                proFile.ExtendedHeader.FragmentAnimation = extendedHeader.FragmentAnimation;
                proFile.ExtendedHeader.SecondaryProjectile = extendedHeader.SecondaryProjectile;
                proFile.ExtendedHeader.TriggerCount = extendedHeader.TriggerCount;
                proFile.ExtendedHeader.TriggerRadius = extendedHeader.TriggerRadius;
                proFile.ExtendedHeader.TriggerSound = extendedHeader.TriggerSound;
                proFile.ExtendedHeader.Unused = extendedHeader.Unused;
                proFile.ExtendedHeader.Unused2 = extendedHeader.Unused2;
            }

            proFile.Checksum = MD5HashGenerator.GenerateKey(proFile);
            return proFile;
        }
    }
}