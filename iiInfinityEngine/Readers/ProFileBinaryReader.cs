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
            proFile.ImpactWav = header.ImpactWav;
            proFile.FaceTargetGranularity = (FaceTargetGranularity)header.FaceTargetGranularity;
            proFile.LightSpotHeight = header.LightSpotHeight;
            proFile.LightSpotIntensity = header.LightSpotIntensity;
            proFile.LightSpotWidth = header.LightSpotWidth;
            proFile.Palette = header.Palette;
            proFile.ProjectileAnimation = header.ProjectileAnimation;
            proFile.ProjectileColour1 = header.ProjectileColour1;
            proFile.ProjectileColour2 = header.ProjectileColour2;
            proFile.ProjectileColour3 = header.ProjectileColour3;
            proFile.ProjectileColour4 = header.ProjectileColour4;
            proFile.ProjectileColour5 = header.ProjectileColour5;
            proFile.ProjectileColour6 = header.ProjectileColour6;
            proFile.ProjectileColour7 = header.ProjectileColour7;
            proFile.ProjectileFlags.EnableBAMColouring = (header.ProjectileFlags & Common.Bit0) != 0;
            proFile.ProjectileFlags.EnableSmoke = (header.ProjectileFlags & Common.Bit1) != 0;
            proFile.ProjectileFlags.ColouredSmoke = (header.ProjectileFlags & Common.Bit2) != 0;
            proFile.ProjectileFlags.EnableAreaLighting = (header.ProjectileFlags & Common.Bit3) != 0;
            proFile.ProjectileFlags.EnableAreaHeight = (header.ProjectileFlags & Common.Bit4) != 0;
            proFile.ProjectileFlags.EnableShadow = (header.ProjectileFlags & Common.Bit5) != 0;
            proFile.ProjectileFlags.EnableLightSpot = (header.ProjectileFlags & Common.Bit6) != 0;
            proFile.ProjectileFlags.EnableBrightenFlags = (header.ProjectileFlags & Common.Bit7) != 0;
            proFile.ProjectileFlags.LowLevelBrighten = (header.ProjectileFlags & Common.Bit8) != 0;
            proFile.ProjectileFlags.HighLevelBrighten = (header.ProjectileFlags & Common.Bit9) != 0;
            proFile.ProjectileFlags.Bit10 = (header.ProjectileFlags & Common.Bit10) != 0;
            proFile.ProjectileFlags.Bit11 = (header.ProjectileFlags & Common.Bit11) != 0;
            proFile.ProjectileFlags.Bit12 = (header.ProjectileFlags & Common.Bit12) != 0;
            proFile.ProjectileFlags.Bit13 = (header.ProjectileFlags & Common.Bit13) != 0;
            proFile.ProjectileFlags.Bit14 = (header.ProjectileFlags & Common.Bit14) != 0;
            proFile.ProjectileFlags.Bit15 = (header.ProjectileFlags & Common.Bit15) != 0;
            proFile.ProjectileFlags.Bit16 = (header.ProjectileFlags & Common.Bit16) != 0;
            proFile.ProjectileFlags.Bit17 = (header.ProjectileFlags & Common.Bit17) != 0;
            proFile.ProjectileFlags.Bit18 = (header.ProjectileFlags & Common.Bit18) != 0;
            proFile.ProjectileFlags.Bit19 = (header.ProjectileFlags & Common.Bit19) != 0;
            proFile.ProjectileFlags.Bit20 = (header.ProjectileFlags & Common.Bit20) != 0;
            proFile.ProjectileFlags.Bit21 = (header.ProjectileFlags & Common.Bit21) != 0;
            proFile.ProjectileFlags.Bit22 = (header.ProjectileFlags & Common.Bit22) != 0;
            proFile.ProjectileFlags.Bit23 = (header.ProjectileFlags & Common.Bit23) != 0;
            proFile.ProjectileFlags.Bit24 = (header.ProjectileFlags & Common.Bit24) != 0;
            proFile.ProjectileFlags.Bit25 = (header.ProjectileFlags & Common.Bit25) != 0;
            proFile.ProjectileFlags.Bit26 = (header.ProjectileFlags & Common.Bit26) != 0;
            proFile.ProjectileFlags.Bit27 = (header.ProjectileFlags & Common.Bit27) != 0;
            proFile.ProjectileFlags.Bit28 = (header.ProjectileFlags & Common.Bit28) != 0;
            proFile.ProjectileFlags.Bit29 = (header.ProjectileFlags & Common.Bit29) != 0;
            proFile.ProjectileFlags.Bit30 = (header.ProjectileFlags & Common.Bit30) != 0;
            proFile.ProjectileFlags.Bit31 = (header.ProjectileFlags & Common.Bit31) != 0;
            proFile.ProjectileNumberBAMSequence = header.ProjectileNumberBAMSequence;
            proFile.ProjectileSmokeAnimation = header.ProjectileSmokeAnimation;
            proFile.ProjectileSpeed = header.ProjectileSpeed;
            proFile.ProjectileType = (ProjectileType)header.ProjectileType;
            proFile.ShadowAnimation = header.ShadowAnimation;
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
            proFile.SparkingFlags.LoopFireSound = (header.ProjectileFlags & Common.Bit2) != 0;
            proFile.SparkingFlags.LoopImpactSound = (header.ProjectileFlags & Common.Bit3) != 0;
            proFile.SparkingFlags.DoNotAffectDirectTarget = (header.ProjectileFlags & Common.Bit4) != 0;
            proFile.SparkingFlags.DrawBelowAnimateObjects = (header.ProjectileFlags & Common.Bit5) != 0;
            proFile.SparkingFlags.AllowSavingGame = (header.ProjectileFlags & Common.Bit6) != 0;
            proFile.SparkingFlags.LoopSpreadAnimation = (header.ProjectileFlags & Common.Bit7) != 0;
            proFile.SparkingFlags.Bit8 = (header.ProjectileFlags & Common.Bit8) != 0;
            proFile.SparkingFlags.Bit9 = (header.ProjectileFlags & Common.Bit9) != 0;
            proFile.SparkingFlags.Bit10 = (header.ProjectileFlags & Common.Bit10) != 0;
            proFile.SparkingFlags.Bit11 = (header.ProjectileFlags & Common.Bit11) != 0;
            proFile.SparkingFlags.Bit12 = (header.ProjectileFlags & Common.Bit12) != 0;
            proFile.SparkingFlags.Bit13 = (header.ProjectileFlags & Common.Bit13) != 0;
            proFile.SparkingFlags.Bit14 = (header.ProjectileFlags & Common.Bit14) != 0;
            proFile.SparkingFlags.Bit15 = (header.ProjectileFlags & Common.Bit15) != 0;
            proFile.SparkingFlags.Bit16 = (header.ProjectileFlags & Common.Bit16) != 0;
            proFile.SparkingFlags.Bit17 = (header.ProjectileFlags & Common.Bit17) != 0;
            proFile.SparkingFlags.Bit18 = (header.ProjectileFlags & Common.Bit18) != 0;
            proFile.SparkingFlags.Bit19 = (header.ProjectileFlags & Common.Bit19) != 0;
            proFile.SparkingFlags.Bit20 = (header.ProjectileFlags & Common.Bit20) != 0;
            proFile.SparkingFlags.Bit21 = (header.ProjectileFlags & Common.Bit21) != 0;
            proFile.SparkingFlags.Bit22 = (header.ProjectileFlags & Common.Bit22) != 0;
            proFile.SparkingFlags.Bit23 = (header.ProjectileFlags & Common.Bit23) != 0;
            proFile.SparkingFlags.Bit24 = (header.ProjectileFlags & Common.Bit24) != 0;
            proFile.SparkingFlags.Bit25 = (header.ProjectileFlags & Common.Bit25) != 0;
            proFile.SparkingFlags.Bit26 = (header.ProjectileFlags & Common.Bit26) != 0;
            proFile.SparkingFlags.Bit27 = (header.ProjectileFlags & Common.Bit27) != 0;
            proFile.SparkingFlags.Bit28 = (header.ProjectileFlags & Common.Bit28) != 0;
            proFile.SparkingFlags.Bit29 = (header.ProjectileFlags & Common.Bit29) != 0;
            proFile.SparkingFlags.Bit30 = (header.ProjectileFlags & Common.Bit30) != 0;
            proFile.SparkingFlags.Bit31 = (header.ProjectileFlags & Common.Bit31) != 0;
            proFile.TrailingAnimation1 = header.TrailingAnimation1;
            proFile.TrailingAnimation2 = header.TrailingAnimation2;
            proFile.TrailingAnimation3 = header.TrailingAnimation3;
            proFile.TrailingBAMSequence1 = header.TrailingBAMSequence1;
            proFile.TrailingBAMSequence2 = header.TrailingBAMSequence2;
            proFile.TrailingBAMSequence3 = header.TrailingBAMSequence3;
            proFile.SourceAnimationBam = header.ImpactWav;
            proFile.FireWav = header.ImpactWav;

            if (header.ProjectileType == (int)ProjectileType.AreaOfEffect)
            {
                var extendedHeader = (ProExtendedHeaderBinary)Common.ReadStruct(br, typeof(ProExtendedHeaderBinary));
                proFile.ExtendedHeader.AreaOfEffect = extendedHeader.AreaOfEffect;

                proFile.ExtendedHeader.AreaProjectileFlags.TrapVisible = (header.ProjectileFlags & Common.Bit0) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.TriggeredByInanimateObjects = (header.ProjectileFlags & Common.Bit1) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.TriggeredOnCondition = (header.ProjectileFlags & Common.Bit2) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.TriggerDelayed = (header.ProjectileFlags & Common.Bit3) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.UseSecondaryProjectile = (header.ProjectileFlags & Common.Bit4) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.UseFragmentGraphics = (header.ProjectileFlags & Common.Bit5) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.TargetSelection1 = (header.ProjectileFlags & Common.Bit6) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.TargetSelection2 = (header.ProjectileFlags & Common.Bit7) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.MageLevelDuration = (header.ProjectileFlags & Common.Bit8) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.ClericLevelDuration= (header.ProjectileFlags & Common.Bit9) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.UseVVC = (header.ProjectileFlags & Common.Bit10) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.ConeShape = (header.ProjectileFlags & Common.Bit11) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.IgnoreLos = (header.ProjectileFlags & Common.Bit12) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.DelayedExplosion = (header.ProjectileFlags & Common.Bit13) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.SkipFirstCondition = (header.ProjectileFlags & Common.Bit14) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.SingleTarget = (header.ProjectileFlags & Common.Bit15) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit16 = (header.ProjectileFlags & Common.Bit16) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit17 = (header.ProjectileFlags & Common.Bit17) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit18 = (header.ProjectileFlags & Common.Bit18) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit19 = (header.ProjectileFlags & Common.Bit19) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit20 = (header.ProjectileFlags & Common.Bit20) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit21 = (header.ProjectileFlags & Common.Bit21) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit22 = (header.ProjectileFlags & Common.Bit22) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit23 = (header.ProjectileFlags & Common.Bit23) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit24 = (header.ProjectileFlags & Common.Bit24) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit25 = (header.ProjectileFlags & Common.Bit25) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit26 = (header.ProjectileFlags & Common.Bit26) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit27 = (header.ProjectileFlags & Common.Bit27) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit28 = (header.ProjectileFlags & Common.Bit28) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit29 = (header.ProjectileFlags & Common.Bit29) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit30 = (header.ProjectileFlags & Common.Bit30) != 0;
                proFile.ExtendedHeader.AreaProjectileFlags.Bit31 = (header.ProjectileFlags & Common.Bit31) != 0;
                proFile.ExtendedHeader.ConeWidth = extendedHeader.ConeWidth;
                proFile.ExtendedHeader.ExplosionEffect = extendedHeader.ExplosionEffect;
                proFile.ExtendedHeader.ExplosionAnimation = extendedHeader.ExplosionAnimation;
                proFile.ExtendedHeader.ExplosionColour = extendedHeader.ExplosionColour;
                proFile.ExtendedHeader.ExplosionDelay = extendedHeader.ExplosionDelay;
                proFile.ExtendedHeader.ExplosionProjectile = extendedHeader.ExplosionProjectile;
                proFile.ExtendedHeader.FragmentAnimation = extendedHeader.FragmentAnimation;
                proFile.ExtendedHeader.RayCount = extendedHeader.RayCount;
                proFile.ExtendedHeader.SecondaryProjectile = extendedHeader.SecondaryProjectile;
                proFile.ExtendedHeader.TriggerCount = extendedHeader.TriggerCount;
                proFile.ExtendedHeader.TriggerRadius = extendedHeader.TriggerRadius;
                proFile.ExtendedHeader.ExplosionSound = extendedHeader.ExplosionSound;
                proFile.ExtendedHeader.Unused219 = extendedHeader.Unused219;
                proFile.ExtendedHeader.Unused2 = extendedHeader.Unused2;
            }

            proFile.Checksum = HashGenerator.GenerateKey(proFile);
            return proFile;
        }
    }
}