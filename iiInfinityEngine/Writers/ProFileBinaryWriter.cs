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
            header.ExtendedFlags = proFile.ExtendedFlags.BounceFromWalls ? header.ExtendedFlags | Common.Bit0 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.PassTarget ? header.ExtendedFlags | Common.Bit1 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.DrawCenterVvcOnce ? header.ExtendedFlags | Common.Bit2 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.HitImmediately ? header.ExtendedFlags | Common.Bit3 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.FaceTarget ? header.ExtendedFlags | Common.Bit4 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.CurvedTarget ? header.ExtendedFlags | Common.Bit5 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.StartRandomFrame ? header.ExtendedFlags | Common.Bit6 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.Pillar ? header.ExtendedFlags | Common.Bit7 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.SemiTransparentTrailPuffVef ? header.ExtendedFlags | Common.Bit8 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.TintedTrailPuffVef ? header.ExtendedFlags | Common.Bit9 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.MultipleProjectiles ? header.ExtendedFlags | Common.Bit10 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.DefaultSpellOnMissed ? header.ExtendedFlags | Common.Bit11 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.FallingPath ? header.ExtendedFlags | Common.Bit12 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.Comet ? header.ExtendedFlags | Common.Bit13 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.LinedUpAreaOfEffect ? header.ExtendedFlags | Common.Bit14 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.RectangularAreaOfEffect ? header.ExtendedFlags | Common.Bit15 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.DrawBehindTarget ? header.ExtendedFlags | Common.Bit16 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.CastingGlowEffect ? header.ExtendedFlags | Common.Bit17 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.TravelDoor ? header.ExtendedFlags | Common.Bit18 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.StopFadeAfterHit ? header.ExtendedFlags | Common.Bit19 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.DisplayMessage ? header.ExtendedFlags | Common.Bit20 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.RandomPath ? header.ExtendedFlags | Common.Bit21 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.StartRandomSequence ? header.ExtendedFlags | Common.Bit22 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.ColourPulseOnHit ? header.ExtendedFlags | Common.Bit23 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.TouchProjectile ? header.ExtendedFlags | Common.Bit24 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.NegateFirstCreatureTarget ? header.ExtendedFlags | Common.Bit25 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.NegateSecondCreatureTarget ? header.ExtendedFlags | Common.Bit26 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.UseEitherIds ? header.ExtendedFlags | Common.Bit27 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.DelayedPayload ? header.ExtendedFlags | Common.Bit28 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.LimitedPathCount ? header.ExtendedFlags | Common.Bit29 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.IwdStyleCheck ? header.ExtendedFlags | Common.Bit30 : header.ExtendedFlags;
            header.ExtendedFlags = proFile.ExtendedFlags.CasterAffected ? header.ExtendedFlags | Common.Bit31 : header.ExtendedFlags;
            header.ftype = new array4() { character1 = 'P', character2 = 'R', character3 = 'O', character4 = ' ' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = '.', character4 = '0' };
            header.AngleIncreaseMaximum = proFile.AngleIncreaseMaximum;
            header.AngleIncreaseMinimum = proFile.AngleIncreaseMinimum;
            header.ColourSpeed = proFile.ColourSpeed;
            header.CreatureType1 = proFile.CreatureType1;
            header.CreatureType2 = proFile.CreatureType2;
            header.CreatureValue1 = proFile.CreatureValue1;
            header.CreatureValue2 = proFile.CreatureValue2;
            header.CurveMaximum = proFile.CurveMaximum;
            header.CurveMinimum = proFile.CurveMinimum;
            header.DefaultSpell = proFile.DefaultSpell;
            header.DisplayedMessage = proFile.DisplayedMessage;
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
            header.ProjectileWidth = proFile.ProjectileWidth;
            header.ProjectileSmokeAnimation = proFile.ProjectileSmokeAnimation;
            header.ProjectileSpeed = proFile.ProjectileSpeed;
            header.ProjectileType = (short)proFile.ProjectileType;
            header.PuffFlags = proFile.PuffFlags.PuffAtTarget ? header.PuffFlags | Common.Bit0 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.PuffAtSource ? header.PuffFlags | Common.Bit1 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit2 ? header.PuffFlags | Common.Bit2 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit3 ? header.PuffFlags | Common.Bit3 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit4 ? header.PuffFlags | Common.Bit4 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit5 ? header.PuffFlags | Common.Bit5 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit6 ? header.PuffFlags | Common.Bit6 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit7 ? header.PuffFlags | Common.Bit7 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit8 ? header.PuffFlags | Common.Bit8 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit9 ? header.PuffFlags | Common.Bit9 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit10 ? header.PuffFlags | Common.Bit10 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit11 ? header.PuffFlags | Common.Bit11 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit12 ? header.PuffFlags | Common.Bit12 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit13 ? header.PuffFlags | Common.Bit13 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit14 ? header.PuffFlags | Common.Bit14 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit15 ? header.PuffFlags | Common.Bit15 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit16 ? header.PuffFlags | Common.Bit16 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit17 ? header.PuffFlags | Common.Bit17 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit18 ? header.PuffFlags | Common.Bit18 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit19 ? header.PuffFlags | Common.Bit19 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit20 ? header.PuffFlags | Common.Bit20 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit21 ? header.PuffFlags | Common.Bit21 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit22 ? header.PuffFlags | Common.Bit22 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit23 ? header.PuffFlags | Common.Bit23 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit24 ? header.PuffFlags | Common.Bit24 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit25 ? header.PuffFlags | Common.Bit25 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit26 ? header.PuffFlags | Common.Bit26 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit27 ? header.PuffFlags | Common.Bit27 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit28 ? header.PuffFlags | Common.Bit28 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit29 ? header.PuffFlags | Common.Bit29 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit30 ? header.PuffFlags | Common.Bit30 : header.PuffFlags;
            header.PuffFlags = proFile.PuffFlags.Bit31 ? header.PuffFlags | Common.Bit31 : header.PuffFlags;
            header.PulseColour = proFile.PulseColour;
            header.RadiusMaximum = proFile.RadiusMaximum;
            header.RadiusMinumum = proFile.RadiusMinumum;
            header.ScreenShakeAmount = proFile.ScreenShakeAmount;
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
            header.SuccessSpell = proFile.SuccessSpell;
            header.Thac0Bonus = proFile.Thac0Bonus;
            header.Thac0BonusNonActor = proFile.Thac0BonusNonActor;
            header.TrailingAnimation1 = proFile.TrailingAnimation1;
            header.TrailingAnimation2 = proFile.TrailingAnimation2;
            header.TrailingAnimation3 = proFile.TrailingAnimation3;
            header.TrailingBAMSequence1 = proFile.TrailingBAMSequence1;
            header.TrailingBAMSequence2 = proFile.TrailingBAMSequence2;
            header.TrailingBAMSequence3 = proFile.TrailingBAMSequence3;
            header.SourceAnimationBam = proFile.SourceAnimationBam;
            header.FireWav = proFile.FireWav;
            header.Unused64 = proFile.Unused64;
            header.Unused158 = proFile.Unused158;

            ProExtendedHeaderBinary extendedHeader = new ProExtendedHeaderBinary();
            extendedHeader.AnimationGranularity = proFile.ExtendedHeader.AnimationGranularity;
            extendedHeader.AnimationGranularityDivider = proFile.ExtendedHeader.AnimationGranularityDivider;
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
            extendedHeader.AreaSound = proFile.ExtendedHeader.AreaSound;
            extendedHeader.ConeWidth = proFile.ExtendedHeader.ConeWidth;
            extendedHeader.DiceSize = proFile.ExtendedHeader.DiceSize;
            extendedHeader.DiceThrown = proFile.ExtendedHeader.DiceThrown;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.PalettedRing ? extendedHeader.EnhancedExtendedFlags | Common.Bit0 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.RandomSpeed ? extendedHeader.EnhancedExtendedFlags | Common.Bit1 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.StartScattered ? extendedHeader.EnhancedExtendedFlags | Common.Bit2 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.PalettedCenter ? extendedHeader.EnhancedExtendedFlags | Common.Bit3 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.RepeatScattering ? extendedHeader.EnhancedExtendedFlags | Common.Bit4 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.PalettedAnimation ? extendedHeader.EnhancedExtendedFlags | Common.Bit5 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit6 ? extendedHeader.EnhancedExtendedFlags | Common.Bit6 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit7 ? extendedHeader.EnhancedExtendedFlags | Common.Bit7 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit8 ? extendedHeader.EnhancedExtendedFlags | Common.Bit8 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.OrientedFireballPuffs ? extendedHeader.EnhancedExtendedFlags | Common.Bit9 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.UseHitDiceLookup ? extendedHeader.EnhancedExtendedFlags | Common.Bit10 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit11 ? extendedHeader.EnhancedExtendedFlags | Common.Bit11 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit12 ? extendedHeader.EnhancedExtendedFlags | Common.Bit12 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.BlendAreaRingAnimation ? extendedHeader.EnhancedExtendedFlags | Common.Bit13 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.GlowAreaRingAnimation ? extendedHeader.EnhancedExtendedFlags | Common.Bit14 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.HitPointLimit ? extendedHeader.EnhancedExtendedFlags | Common.Bit15 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit16 ? extendedHeader.EnhancedExtendedFlags | Common.Bit16 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit17 ? extendedHeader.EnhancedExtendedFlags | Common.Bit17 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit18 ? extendedHeader.EnhancedExtendedFlags | Common.Bit18 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit19 ? extendedHeader.EnhancedExtendedFlags | Common.Bit19 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit20 ? extendedHeader.EnhancedExtendedFlags | Common.Bit20 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit21 ? extendedHeader.EnhancedExtendedFlags | Common.Bit21 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit22 ? extendedHeader.EnhancedExtendedFlags | Common.Bit22 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit23 ? extendedHeader.EnhancedExtendedFlags | Common.Bit23 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit24 ? extendedHeader.EnhancedExtendedFlags | Common.Bit24 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit25 ? extendedHeader.EnhancedExtendedFlags | Common.Bit25 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit26 ? extendedHeader.EnhancedExtendedFlags | Common.Bit26 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit27 ? extendedHeader.EnhancedExtendedFlags | Common.Bit27 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit28 ? extendedHeader.EnhancedExtendedFlags | Common.Bit28 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit29 ? extendedHeader.EnhancedExtendedFlags | Common.Bit29 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit30 ? extendedHeader.EnhancedExtendedFlags | Common.Bit30 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.EnhancedExtendedFlags = proFile.ExtendedHeader.EnhancedExtendedFlags.Bit31 ? extendedHeader.EnhancedExtendedFlags | Common.Bit31 : extendedHeader.EnhancedExtendedFlags;
            extendedHeader.ExplosionEffect = proFile.ExtendedHeader.ExplosionEffect;
            extendedHeader.ExplosionAnimation = proFile.ExtendedHeader.ExplosionAnimation;
            extendedHeader.ExplosionColour = proFile.ExtendedHeader.ExplosionColour;
            extendedHeader.ExplosionDelay = proFile.ExtendedHeader.ExplosionDelay;
            extendedHeader.ExplosionProjectile = proFile.ExtendedHeader.ExplosionProjectile;
            extendedHeader.FragmentAnimation = proFile.ExtendedHeader.FragmentAnimation;
            extendedHeader.RayCount = proFile.ExtendedHeader.RayCount;
            extendedHeader.RingAnimation = proFile.ExtendedHeader.RingAnimation;
            extendedHeader.RotateRaysClockwise = proFile.ExtendedHeader.RotateRaysClockwise;
            extendedHeader.SecondaryProjectile = proFile.ExtendedHeader.SecondaryProjectile;
            extendedHeader.SpreadAnimation = proFile.ExtendedHeader.SpreadAnimation;
            extendedHeader.TriggerCount = proFile.ExtendedHeader.TriggerCount;
            extendedHeader.TriggerRadius = proFile.ExtendedHeader.TriggerRadius;
            extendedHeader.ExplosionSound = proFile.ExtendedHeader.ExplosionSound;
            extendedHeader.Unused219 = proFile.ExtendedHeader.Unused219;
            extendedHeader.Unused24c = proFile.ExtendedHeader.Unused24c;

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