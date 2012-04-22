using System.IO;
using WindowsFormsApplication1.Binary;
using WindowsFormsApplication1.Files;

namespace WindowsFormsApplication1.Writers
{
    class ProWriter
    {
        public void Write(string filename, ProFile proFile)
        {
            ProBinaryHeader header = new ProBinaryHeader();

            header.ProjectileFlags = proFile.ProjectileFlags.EnableBAMColouring ? header.ProjectileFlags | Common.Bit0 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.EnableSmoke ? header.ProjectileFlags | Common.Bit1 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.Unused ? header.ProjectileFlags | Common.Bit2 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.EnableAreaLighting ? header.ProjectileFlags | Common.Bit3 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.EnableAreaHeight ? header.ProjectileFlags | Common.Bit4 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.EnableShadow ? header.ProjectileFlags | Common.Bit5 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.EnableLightSpot ? header.ProjectileFlags | Common.Bit6 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.EnableBrightenFlags ? header.ProjectileFlags | Common.Bit7 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.LowLevelBrighten ? header.ProjectileFlags | Common.Bit8 : header.ProjectileFlags;
            header.ProjectileFlags = proFile.ProjectileFlags.HighLevelBrighten ? header.ProjectileFlags | Common.Bit9 : header.ProjectileFlags;

            header.SparkingFlags = proFile.SparkingFlags.ShowSparkle ? header.SparkingFlags | Common.Bit0 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.UseZCoordinate ? header.SparkingFlags | Common.Bit1 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.LoopTravelSound ? header.SparkingFlags | Common.Bit2 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.LoopArrivalSound ? header.SparkingFlags | Common.Bit3 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.DoNotAffectDirectTarget ? header.SparkingFlags | Common.Bit4 : header.SparkingFlags;
            header.SparkingFlags = proFile.SparkingFlags.DrawBelowAnimateObjects ? header.SparkingFlags | Common.Bit5 : header.SparkingFlags;

            header.ftype = new array4() { character1 = 'P', character2 = 'R', character3 = 'O', character4 = ' ' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = '.', character4 = '0' };
            header.ArrivalWav = proFile.ArrivalWav;
            header.FaceTargetGranularity = (byte)proFile.FaceTargetGranularity;
            header.LightSpotHeight = proFile.LightSpotHeight;
            header.LightSpotIntensity = proFile.LightSpotIntensity;
            header.LightSpotWidth = proFile.LightSpotWidth;
            header.Palette = new array8(proFile.Palette);
            header.ProjectileAnimation = new array8(proFile.ProjectileAnimation);
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
            header.ShadowAnimation = new array8(proFile.ShadowAnimation);
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
            header.TrailingAnimation1 = new array8(proFile.TrailingAnimation1);
            header.TrailingAnimation2 = new array8(proFile.TrailingAnimation2);
            header.TrailingAnimation3 = new array8(proFile.TrailingAnimation3);
            header.TrailingBAMSequence1 = proFile.TrailingBAMSequence1;
            header.TrailingBAMSequence2 = proFile.TrailingBAMSequence2;
            header.TrailingBAMSequence3 = proFile.TrailingBAMSequence3;
            header.TravelBAM = proFile.TravelBAM;
            header.TravelWav = proFile.TravelWav;
            header.Unused = proFile.Unused;
            header.Unused2 = proFile.Unused2;

            ProBinaryExtendedHeader extendedHeader = new ProBinaryExtendedHeader();
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.ProjectileRemainsVisibleAtDestination ? extendedHeader.AreaProjectileFlags | Common.Bit0 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.TriggeredByInanimateObjects ? extendedHeader.AreaProjectileFlags | Common.Bit1 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.TriggeredOnCondition ? extendedHeader.AreaProjectileFlags | Common.Bit2 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.TriggerDuringDelay ? extendedHeader.AreaProjectileFlags | Common.Bit3 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.UseSecondaryProjectile ? extendedHeader.AreaProjectileFlags | Common.Bit4 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.UseFragmentGraphics ? extendedHeader.AreaProjectileFlags | Common.Bit5 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.TargetSelection1 ? extendedHeader.AreaProjectileFlags | Common.Bit6 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.TargetSelection2 ? extendedHeader.AreaProjectileFlags | Common.Bit7 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.NumberOfTriggersEqualToCastMageLevel ? extendedHeader.AreaProjectileFlags | Common.Bit8 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.NumberOfTriggersEqualToCastClericLevel ? extendedHeader.AreaProjectileFlags | Common.Bit9 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.UseVVC ? extendedHeader.AreaProjectileFlags | Common.Bit10 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.ConeShape ? extendedHeader.AreaProjectileFlags | Common.Bit11 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.AffectObjectsThroughWallMountainBuildingAndOffArea ? extendedHeader.AreaProjectileFlags | Common.Bit12 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.CheckTriggersFromAnimationFrame30 ? extendedHeader.AreaProjectileFlags | Common.Bit13 : extendedHeader.AreaProjectileFlags; //  (req. bit 0) 
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.DelayedExplosion ? extendedHeader.AreaProjectileFlags | Common.Bit14 : extendedHeader.AreaProjectileFlags;
            extendedHeader.AreaProjectileFlags = proFile.ExtendedHeader.AreaProjectileFlags.AffectOnlyOneTarget ? extendedHeader.AreaProjectileFlags | Common.Bit15 : extendedHeader.AreaProjectileFlags;

            extendedHeader.AreaOfEffect = proFile.ExtendedHeader.AreaOfEffect;
            extendedHeader.ConeWidth = proFile.ExtendedHeader.ConeWidth;
            extendedHeader.ExplosionAnimation = proFile.ExtendedHeader.ExplosionAnimation;
            extendedHeader.ExplosionAnimationAnimation = proFile.ExtendedHeader.ExplosionAnimationAnimation;
            extendedHeader.ExplosionColour = proFile.ExtendedHeader.ExplosionColour;
            extendedHeader.ExplosionDelay = proFile.ExtendedHeader.ExplosionDelay;
            extendedHeader.ExplosionProjectile = proFile.ExtendedHeader.ExplosionProjectile;
            extendedHeader.FragmentAnimation = proFile.ExtendedHeader.FragmentAnimation;
            extendedHeader.SecondaryProjectile = proFile.ExtendedHeader.SecondaryProjectile;
            extendedHeader.TriggerCount = proFile.ExtendedHeader.TriggerCount;
            extendedHeader.TriggerRadius = proFile.ExtendedHeader.TriggerRadius;
            extendedHeader.TriggerSound = proFile.ExtendedHeader.TriggerSound;
            extendedHeader.Unused = proFile.ExtendedHeader.Unused;
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

                    using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    {
                        bw.BaseStream.Position = 0;
                        bw.BaseStream.CopyTo(fs);
                        fs.Flush(flushToDisk: true);
                    }
                }
            }
        }
    }
}