using System;
using System.IO;
using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Writers.Interfaces;

namespace ii.InfinityEngine.Writers
{
    class EffFileBinaryWriter : IEffFileWriter
    {
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not EffFile)
                throw new ArgumentException("File is not a valid eff file");

            var effFile = file as EffFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(effFile) == effFile.Checksum))
                return false;

            var header = new EffHeaderBinary();

            header.ftype = new array4() { character1 = 'E', character2 = 'F', character3 = 'F', character4 = ' ' };
            header.fversion = new array4() { character1 = 'V', character2 = '2', character3 = '.', character4 = '0' };
            header.ftype2 = new array4() { character1 = 'E', character2 = 'F', character3 = 'F', character4 = ' ' };
            header.fversion2 = new array4() { character1 = 'V', character2 = '2', character3 = '.', character4 = '0' }; ;

            header.CasterLevel = effFile.CasterLevel;
            header.CasterXCoordinate = effFile.CasterXCoordinate;
            header.CasterYCoordinate = effFile.CasterYCoordinate;
            header.DiceSides = effFile.DiceSides;
            header.DiceThrown = effFile.DiceThrown;
            header.Duration = effFile.Duration;
            header.HighestAffectedLevelFromParent = effFile.HighestAffectedLevelFromParent;
            header.LowestAffectedLevelFromParent = effFile.LowestAffectedLevelFromParent;
            header.Opcode = effFile.Opcode;
            header.Parameter1 = effFile.Parameter1;
            header.Parameter2 = effFile.Parameter2;
            header.Parameter3 = effFile.Parameter3;
            header.Parameter4 = effFile.Parameter4;
            header.Parameter5 = effFile.Parameter5;
            header.TimeApplied = effFile.TimeApplied;
            header.ParentResource = effFile.ParentResource;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit0 ? header.FlagsFromParent | Common.Bit0 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit1 ? header.FlagsFromParent | Common.Bit1 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit2 ? header.FlagsFromParent | Common.Bit2 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit3 ? header.FlagsFromParent | Common.Bit0 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit4 ? header.FlagsFromParent | Common.Bit4 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit5 ? header.FlagsFromParent | Common.Bit5 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit6 ? header.FlagsFromParent | Common.Bit6 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit7 ? header.FlagsFromParent | Common.Bit7 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit8 ? header.FlagsFromParent | Common.Bit8 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.BreaksSanctuaryInvisibility ? header.FlagsFromParent | Common.Bit9 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Hostile ? header.FlagsFromParent | Common.Bit10 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.NoLOSRequired ? header.FlagsFromParent | Common.Bit11 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.AllowSpotting ? header.FlagsFromParent | Common.Bit12 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.OutdoorsOnly ? header.FlagsFromParent | Common.Bit13 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.IgnoreWildSurgeDeadMagic ? header.FlagsFromParent | Common.Bit14 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.IgnoreWildSurge ? header.FlagsFromParent | Common.Bit15 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.NonCombatAbility ? header.FlagsFromParent | Common.Bit16 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit17 ? header.FlagsFromParent | Common.Bit17 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit18 ? header.FlagsFromParent | Common.Bit18 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit19 ? header.FlagsFromParent | Common.Bit19 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit20 ? header.FlagsFromParent | Common.Bit20 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit21 ? header.FlagsFromParent | Common.Bit21 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit22 ? header.FlagsFromParent | Common.Bit22 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit23 ? header.FlagsFromParent | Common.Bit23 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.CanTargetInvisible ? header.FlagsFromParent | Common.Bit24 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.CastableWhenSilenced ? header.FlagsFromParent | Common.Bit25 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit26 ? header.FlagsFromParent | Common.Bit26 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit27 ? header.FlagsFromParent | Common.Bit27 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit28 ? header.FlagsFromParent | Common.Bit28 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit29 ? header.FlagsFromParent | Common.Bit29 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit30 ? header.FlagsFromParent | Common.Bit30 : header.FlagsFromParent;
            header.FlagsFromParent = effFile.FlagsFromParent.Bit31 ? header.FlagsFromParent | Common.Bit31 : header.FlagsFromParent;
            header.ParentResourceSlot = effFile.ParentResourceSlot;
            header.Power = effFile.Power;
            header.PrimaryType = effFile.PrimaryType;
            header.Probability1 = effFile.Probability1;
            header.Probability2 = effFile.Probability2;
            header.Projectile = effFile.Projectile;
            header.Resistance = effFile.Resistance.DispellableAffectedByMagicResistance ? header.Resistance | Common.Bit0 : header.Resistance;
            header.Resistance = effFile.Resistance.IgnoreMagicResistance ? header.Resistance | Common.Bit1 : header.Resistance;
            header.Resistance = effFile.Resistance.SelfTargetted ? header.Resistance | Common.Bit2 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit3 ? header.Resistance | Common.Bit0 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit4 ? header.Resistance | Common.Bit4 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit5 ? header.Resistance | Common.Bit5 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit6 ? header.Resistance | Common.Bit6 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit7 ? header.Resistance | Common.Bit7 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit8 ? header.Resistance | Common.Bit8 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit9 ? header.Resistance | Common.Bit9 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit10 ? header.Resistance | Common.Bit10 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit11 ? header.Resistance | Common.Bit11 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit12 ? header.Resistance | Common.Bit12 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit13 ? header.Resistance | Common.Bit13 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit14 ? header.Resistance | Common.Bit14 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit15 ? header.Resistance | Common.Bit15 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit16 ? header.Resistance | Common.Bit16 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit17 ? header.Resistance | Common.Bit17 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit18 ? header.Resistance | Common.Bit18 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit19 ? header.Resistance | Common.Bit19 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit20 ? header.Resistance | Common.Bit20 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit21 ? header.Resistance | Common.Bit21 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit22 ? header.Resistance | Common.Bit22 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit23 ? header.Resistance | Common.Bit23 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit24 ? header.Resistance | Common.Bit24 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit25 ? header.Resistance | Common.Bit25 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit26 ? header.Resistance | Common.Bit26 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit27 ? header.Resistance | Common.Bit27 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit28 ? header.Resistance | Common.Bit28 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit29 ? header.Resistance | Common.Bit29 : header.Resistance;
            header.Resistance = effFile.Resistance.Bit30 ? header.Resistance | Common.Bit30 : header.Resistance;
            header.Resistance = effFile.Resistance.EffectAppliedByItem ? header.Resistance | Common.Bit31 : header.Resistance;
            header.Resource = effFile.Resource;
            header.Resource2 = effFile.Resource2;
            header.Resource3 = effFile.Resource3;
            header.ResourceTypeFromParent = (int)effFile.ResourceTypeFromParent;
            header.SavingThrowBonus = effFile.SavingThrowBonus;
            header.SavingThrowType = effFile.SavingThrowType.Spells ? header.SavingThrowType | Common.Bit0 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Breath ? header.SavingThrowType | Common.Bit1 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.ParalyzePoisonDeath ? header.SavingThrowType | Common.Bit2 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Wands ? header.SavingThrowType | Common.Bit0 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.PetrifyPolymorph ? header.SavingThrowType | Common.Bit4 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit5 ? header.SavingThrowType | Common.Bit5 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit6 ? header.SavingThrowType | Common.Bit6 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit7 ? header.SavingThrowType | Common.Bit7 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit8 ? header.SavingThrowType | Common.Bit8 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit9 ? header.SavingThrowType | Common.Bit9 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.IgnorePrimaryTarget ? header.SavingThrowType | Common.Bit10 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.IgnoreSecondaryTarget ? header.SavingThrowType | Common.Bit11 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit12 ? header.SavingThrowType | Common.Bit12 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit13 ? header.SavingThrowType | Common.Bit13 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit14 ? header.SavingThrowType | Common.Bit14 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit15 ? header.SavingThrowType | Common.Bit15 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit16 ? header.SavingThrowType | Common.Bit16 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit17 ? header.SavingThrowType | Common.Bit17 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit18 ? header.SavingThrowType | Common.Bit18 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit19 ? header.SavingThrowType | Common.Bit19 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit20 ? header.SavingThrowType | Common.Bit20 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit21 ? header.SavingThrowType | Common.Bit21 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit22 ? header.SavingThrowType | Common.Bit22 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit23 ? header.SavingThrowType | Common.Bit23 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.BypassMirrorImage ? header.SavingThrowType | Common.Bit24 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.IgnoreDifficulty ? header.SavingThrowType | Common.Bit25 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit26 ? header.SavingThrowType | Common.Bit26 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit27 ? header.SavingThrowType | Common.Bit27 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit28 ? header.SavingThrowType | Common.Bit28 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit29 ? header.SavingThrowType | Common.Bit29 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit30 ? header.SavingThrowType | Common.Bit30 : header.SavingThrowType;
            header.SavingThrowType = effFile.SavingThrowType.Bit31 ? header.SavingThrowType | Common.Bit31 : header.SavingThrowType;
            header.SecondaryType = effFile.SecondaryType;
            header.Special = effFile.Special;
            header.TargetType = (Int32)effFile.TargetType;
            header.TargetXCoordinate = effFile.TargetXCoordinate;
            header.TargetYCoordinate = effFile.TargetYCoordinate;
            header.TimingMode = (Int16)effFile.TimingMode;
            header.Unknown26 = effFile.Unknown26;
            header.Unknown50 = effFile.Unknown50;
            header.FirstApply = effFile.FirstApply;
            header.Unknownd4 = effFile.Unknownd4;
            header.Unknownd8 = effFile.Unknownd8;
            header.Unknowndc = effFile.Unknowndc;
            header.Unknowne0 = effFile.Unknowne0;
            header.Unknowne4 = effFile.Unknowne4;
            header.Unknowne8 = effFile.Unknowne8;
            header.Unknownec = effFile.Unknownec;
            header.Unknownf0 = effFile.Unknownf0;
            header.Unknownf4 = effFile.Unknownf4;
            header.Unknownf8 = effFile.Unknownf8;
            header.Unknownfc = effFile.Unknownfc;
            header.Unknownd100 = effFile.Unknownd100;
            header.Unknownd104 = effFile.Unknownd104;
            header.Unknownd108 = effFile.Unknownd108;
            header.Unknownd10c = effFile.Unknownd10c;
            header.Variable = effFile.Variable;

            using var s = new MemoryStream();
            using var bw = new BinaryWriter(s);
            var headerAsBytes = Common.WriteStruct(header);

            bw.Write(headerAsBytes);

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            bw.BaseStream.Position = 0;
            bw.BaseStream.CopyTo(fs);
            fs.Flush(flushToDisk: true);
            return true;
        }
    }
}