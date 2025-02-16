using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;

namespace iiInfinityEngine.Core.Writers
{
    public class ProFileBinaryWriter : IProFileWriter
    {
        public BackupManager BackupManger { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (!(file is ProFile))
                throw new ArgumentException("File is not a valid creature file");

            var proFile = file as ProFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(proFile) == proFile.Checksum))
                return false;

            ProHeaderBinary header = new ProHeaderBinary();

            header.ProjectileFlags = proFile.ProjectileFlags.EnableBAMColouring ? header.ProjectileFlags | Common.Bit0 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.EnableSmoke ? header.ProjectileFlags | Common.Bit1 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.ColouredSmoke ? header.ProjectileFlags | Common.Bit2 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.EnableAreaLighting ? header.ProjectileFlags | Common.Bit3 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.EnableAreaHeight ? header.ProjectileFlags | Common.Bit4 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.EnableShadow ? header.ProjectileFlags | Common.Bit5 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.EnableLightSpot ? header.ProjectileFlags | Common.Bit6 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.EnableBrightenFlags ? header.ProjectileFlags | Common.Bit7 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.LowLevelBrighten ? header.ProjectileFlags | Common.Bit8 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.HighLevelBrighten ? header.ProjectileFlags | Common.Bit9 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit10 ? header.ProjectileFlags | Common.Bit10 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit11 ? header.ProjectileFlags | Common.Bit11 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit12 ? header.ProjectileFlags | Common.Bit12 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit13 ? header.ProjectileFlags | Common.Bit13 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit14 ? header.ProjectileFlags | Common.Bit14 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit15 ? header.ProjectileFlags | Common.Bit15 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit16 ? header.ProjectileFlags | Common.Bit16 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit17 ? header.ProjectileFlags | Common.Bit17 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit18 ? header.ProjectileFlags | Common.Bit18 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit19 ? header.ProjectileFlags | Common.Bit19 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit20 ? header.ProjectileFlags | Common.Bit20 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit20 ? header.ProjectileFlags | Common.Bit21 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit22 ? header.ProjectileFlags | Common.Bit22 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit23 ? header.ProjectileFlags | Common.Bit23 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit24 ? header.ProjectileFlags | Common.Bit24 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit25 ? header.ProjectileFlags | Common.Bit25 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit26 ? header.ProjectileFlags | Common.Bit26 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit27 ? header.ProjectileFlags | Common.Bit27 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit28 ? header.ProjectileFlags | Common.Bit28 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit29 ? header.ProjectileFlags | Common.Bit29 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit30 ? header.ProjectileFlags | Common.Bit30 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Bit31 ? header.ProjectileFlags | Common.Bit31 : header.ProjectileFlags;
            header.SparkingFlags = proFile.SparkingFlags.ShowSparkle ? header.SparkingFlags | Common.Bit0 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.UseZCoordinate ? header.SparkingFlags | Common.Bit1 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.LoopFireSound ? header.SparkingFlags | Common.Bit2 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.LoopImpactSound ? header.SparkingFlags | Common.Bit3 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.DoNotAffectDirectTarget ? header.SparkingFlags | Common.Bit4 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.DrawBelowAnimateObjects ? header.SparkingFlags | Common.Bit5 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.AllowSavingGame ? header.SparkingFlags | Common.Bit6 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.LoopSpreadAnimation ? header.SparkingFlags | Common.Bit7 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit8 ? header.SparkingFlags | Common.Bit8 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit9 ? header.SparkingFlags | Common.Bit9 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit10 ? header.SparkingFlags | Common.Bit10 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit11 ? header.SparkingFlags | Common.Bit11 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit12 ? header.SparkingFlags | Common.Bit12 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit13 ? header.SparkingFlags | Common.Bit13 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit14 ? header.SparkingFlags | Common.Bit14 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit15 ? header.SparkingFlags | Common.Bit15 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit16 ? header.SparkingFlags | Common.Bit16 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit17 ? header.SparkingFlags | Common.Bit17 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit18 ? header.SparkingFlags | Common.Bit18 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit19 ? header.SparkingFlags | Common.Bit19 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit20 ? header.SparkingFlags | Common.Bit20 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit21 ? header.SparkingFlags | Common.Bit21 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit22 ? header.SparkingFlags | Common.Bit22 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit23 ? header.SparkingFlags | Common.Bit23 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit24 ? header.SparkingFlags | Common.Bit24 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit25 ? header.SparkingFlags | Common.Bit25 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit26 ? header.SparkingFlags | Common.Bit26 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit27 ? header.SparkingFlags | Common.Bit27 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit28 ? header.SparkingFlags | Common.Bit28 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit29 ? header.SparkingFlags | Common.Bit29 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit30 ? header.SparkingFlags | Common.Bit30 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.Bit31 ? header.SparkingFlags | Common.Bit31 : header.SparkingFlags;
            header.ftype = new array4() { character1 = 'P', character2 = 'R', character3 = 'O', character4 = ' ' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = '.', character4 = '0' };
            header.ImpactWav = proFile.ImpactWav;
            header.FaceTargetGranularity = (byte)proFile.FaceTargetGranularity;
            header.LightSpotHeight = proFile.LightSpotHeight;
            header.LightSpotIntensity = proFile.LightSpotIntensity;
            header.LightSpotWidth = proFile.LightSpotWidth;
            header.Palette = proFile.Palette;
            header.ProjectileAnimation = proFile.ProjectileAnimation;
            header.ProjectileColour1 = proFile.ProjectileColour1;
            header.ProjectileColour2 = proFile.ProjectileColour2;
            header.ProjectileColour3 = proFile.ProjectileColour3;
            header.ProjectileColour4 = proFile.ProjectileColour4;
            header.ProjectileColour5 = proFile.ProjectileColour5;
            header.ProjectileColour6 = proFile.ProjectileColour6;
            header.ProjectileColour7 = proFile.ProjectileColour7;
            header.ProjectileNumberBAMSequence = proFile.ProjectileNumberBAMSequence;
            header.ProjectileSmokeAnimation = proFile.ProjectileSmokeAnimation;
            header.ProjectileSpeed = proFile.ProjectileSpeed;
            header.ProjectileType = (short)proFile.ProjectileType;
            header.ShadowAnimation = proFile.ShadowAnimation;
            header.ShadowNumberBAMSequence = proFile.ShadowNumberBAMSequence;
            header.SmokeColour1 = proFile.SmokeColour1;
            header.SmokeColour2 = proFile.SmokeColour2;
            header.SmokeColour3 = proFile.SmokeColour3;
            header.SmokeColour4 = proFile.SmokeColour4;
            header.SmokeColour5 = proFile.SmokeColour5;
            header.SmokeColour6 = proFile.SmokeColour6;
            header.SmokeColour7 = proFile.SmokeColour7;
            header.SmokePuffPeriod = proFile.SmokePuffPeriod;
            header.SparkColour = (short)proFile.SparkColour;
            header.TrailingAnimation1 = proFile.TrailingAnimation1;
            header.TrailingAnimation2 = proFile.TrailingAnimation2;
            header.TrailingAnimation3 = proFile.TrailingAnimation3;
            header.TrailingBAMSequence1 = proFile.TrailingBAMSequence1;
            header.TrailingBAMSequence2 = proFile.TrailingBAMSequence2;
            header.TrailingBAMSequence3 = proFile.TrailingBAMSequence3;
            header.SourceAnimationBam = proFile.SourceAnimationBam;
            header.FireWav = proFile.FireWav;
            header.Unused = proFile.Unused;
            header.Unused2 = proFile.Unused2;

            ProExtendedHeaderBinary extendedHeader = new ProExtendedHeaderBinary();
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.TrapVisible ? extendedHeader.AreaProjectileFlags | Common.Bit0 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.TriggeredByInanimateObjects ? extendedHeader.AreaProjectileFlags | Common.Bit1 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.TriggeredOnCondition ? extendedHeader.AreaProjectileFlags | Common.Bit2 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.TriggerDelayed ? extendedHeader.AreaProjectileFlags | Common.Bit3 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.UseSecondaryProjectile ? extendedHeader.AreaProjectileFlags | Common.Bit4 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.UseFragmentGraphics ? extendedHeader.AreaProjectileFlags | Common.Bit5 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.TargetSelection1 ? extendedHeader.AreaProjectileFlags | Common.Bit6 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.TargetSelection2 ? extendedHeader.AreaProjectileFlags | Common.Bit7 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.MageLevelDuration ? extendedHeader.AreaProjectileFlags | Common.Bit8 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.ClericLevelDuration ? extendedHeader.AreaProjectileFlags | Common.Bit9 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.UseVVC ? extendedHeader.AreaProjectileFlags | Common.Bit10 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.ConeShape ? extendedHeader.AreaProjectileFlags | Common.Bit11 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.IgnoreLos ? extendedHeader.AreaProjectileFlags | Common.Bit12 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.DelayedExplosion ? extendedHeader.AreaProjectileFlags | Common.Bit13 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.SkipFirstCondition ? extendedHeader.AreaProjectileFlags | Common.Bit14 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.SingleTarget ? extendedHeader.AreaProjectileFlags | Common.Bit15 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit16 ? extendedHeader.AreaProjectileFlags | Common.Bit16 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit17 ? extendedHeader.AreaProjectileFlags | Common.Bit17 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit18 ? extendedHeader.AreaProjectileFlags | Common.Bit18 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit19 ? extendedHeader.AreaProjectileFlags | Common.Bit19 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit20 ? extendedHeader.AreaProjectileFlags | Common.Bit20 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit21 ? extendedHeader.AreaProjectileFlags | Common.Bit21 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit22 ? extendedHeader.AreaProjectileFlags | Common.Bit22 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit23 ? extendedHeader.AreaProjectileFlags | Common.Bit23 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit24 ? extendedHeader.AreaProjectileFlags | Common.Bit24 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit25 ? extendedHeader.AreaProjectileFlags | Common.Bit25 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit26 ? extendedHeader.AreaProjectileFlags | Common.Bit26 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit27 ? extendedHeader.AreaProjectileFlags | Common.Bit27 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit28 ? extendedHeader.AreaProjectileFlags | Common.Bit28 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit29 ? extendedHeader.AreaProjectileFlags | Common.Bit29 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit30 ? extendedHeader.AreaProjectileFlags | Common.Bit30 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.Bit31 ? extendedHeader.AreaProjectileFlags | Common.Bit31 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaOfEffect = proFile.ExtendedHeader.AreaOfEffect;
            extendedHeader.ConeWidth = proFile.ExtendedHeader.ConeWidth;
            extendedHeader.ExplosionEffect = proFile.ExtendedHeader.ExplosionEffect;
            extendedHeader.ExplosionAnimation = proFile.ExtendedHeader.ExplosionAnimation;
            extendedHeader.ExplosionColour = proFile.ExtendedHeader.ExplosionColour;
            extendedHeader.ExplosionDelay = proFile.ExtendedHeader.ExplosionDelay;
            extendedHeader.ExplosionProjectile = proFile.ExtendedHeader.ExplosionProjectile;
            extendedHeader.FragmentAnimation = proFile.ExtendedHeader.FragmentAnimation;
            extendedHeader.RayCount = proFile.ExtendedHeader.RayCount;
            extendedHeader.SecondaryProjectile = proFile.ExtendedHeader.SecondaryProjectile;
            extendedHeader.TriggerCount = proFile.ExtendedHeader.TriggerCount;
            extendedHeader.TriggerRadius = proFile.ExtendedHeader.TriggerRadius;
            extendedHeader.ExplosionSound = proFile.ExtendedHeader.ExplosionSound;
            extendedHeader.Unused219 = proFile.ExtendedHeader.Unused219;
            extendedHeader.Unused2 = proFile.ExtendedHeader.Unused2;

            using (MemoryStream s = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(s))
                {
                    var headerAsBytes = Common.WriteStruct(header);

                    bw.Write(headerAsBytes);

                    if (header.ProjectileType == (int)ProjectileType.AreaOfEffect)
                    {
                        var extendedHeaderAsBytes = Common.WriteStruct(extendedHeader);
                        bw.Write(extendedHeaderAsBytes);
                    }

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