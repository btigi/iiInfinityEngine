using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Readers.Interfaces;

namespace iiInfinityEngine.Core.Readers
{
    public class EffFileBinaryReader : IEffFileReader
    {
        public EffFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public EffFile Read(Stream s)
        {
            using var br = new BinaryReader(s);
            var effFile = ParseFile(br);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            effFile.OriginalFile = ParseFile(br);
            return effFile;
        }

        private EffFile ParseFile(BinaryReader br)
        {
            var header = (EffHeaderBinary)Common.ReadStruct(br, typeof(EffHeaderBinary));

            var effFile = new EffFile();
            effFile.Opcode = header.Opcode;
            effFile.TargetType = (EffTargetType)header.TargetType;
            effFile.Power = header.Power;
            effFile.Parameter1 = header.Parameter1;
            effFile.Parameter2 = header.Parameter2;
            effFile.TimingMode = (EffTimingMode)header.TimingMode;
            effFile.Unknown26 = header.Unknown26;
            effFile.Duration = header.Duration;
            effFile.Probability1 = header.Probability1;
            effFile.Probability2 = header.Probability2;
            effFile.Resource = header.Resource;
            effFile.DiceThrown = header.DiceThrown;
            effFile.DiceSides = header.DiceSides;
            effFile.SavingThrowType.Spells = (header.SavingThrowType & Common.Bit0) != 0;
            effFile.SavingThrowType.Breath = (header.SavingThrowType & Common.Bit1) != 0;
            effFile.SavingThrowType.ParalyzePoisonDeath = (header.SavingThrowType & Common.Bit2) != 0;
            effFile.SavingThrowType.Wands = (header.SavingThrowType & Common.Bit3) != 0;
            effFile.SavingThrowType.PetrifyPolymorph = (header.SavingThrowType & Common.Bit4) != 0;
            effFile.SavingThrowType.Bit5 = (header.SavingThrowType & Common.Bit5) != 0;
            effFile.SavingThrowType.Bit6 = (header.SavingThrowType & Common.Bit6) != 0;
            effFile.SavingThrowType.Bit7 = (header.SavingThrowType & Common.Bit7) != 0;
            effFile.SavingThrowType.Bit8 = (header.SavingThrowType & Common.Bit8) != 0;
            effFile.SavingThrowType.Bit9 = (header.SavingThrowType & Common.Bit9) != 0;
            effFile.SavingThrowType.IgnorePrimaryTarget = (header.SavingThrowType & Common.Bit10) != 0;
            effFile.SavingThrowType.IgnoreSecondaryTarget = (header.SavingThrowType & Common.Bit11) != 0;
            effFile.SavingThrowType.Bit12 = (header.SavingThrowType & Common.Bit12) != 0;
            effFile.SavingThrowType.Bit13 = (header.SavingThrowType & Common.Bit13) != 0;
            effFile.SavingThrowType.Bit14 = (header.SavingThrowType & Common.Bit14) != 0;
            effFile.SavingThrowType.Bit15 = (header.SavingThrowType & Common.Bit15) != 0;
            effFile.SavingThrowType.Bit16 = (header.SavingThrowType & Common.Bit16) != 0;
            effFile.SavingThrowType.Bit17 = (header.SavingThrowType & Common.Bit17) != 0;
            effFile.SavingThrowType.Bit18 = (header.SavingThrowType & Common.Bit18) != 0;
            effFile.SavingThrowType.Bit19 = (header.SavingThrowType & Common.Bit19) != 0;
            effFile.SavingThrowType.Bit20 = (header.SavingThrowType & Common.Bit20) != 0;
            effFile.SavingThrowType.Bit21 = (header.SavingThrowType & Common.Bit21) != 0;
            effFile.SavingThrowType.Bit22 = (header.SavingThrowType & Common.Bit22) != 0;
            effFile.SavingThrowType.Bit23 = (header.SavingThrowType & Common.Bit23) != 0;
            effFile.SavingThrowType.BypassMirrorImage = (header.SavingThrowType & Common.Bit24) != 0;
            effFile.SavingThrowType.IgnoreDifficulty = (header.SavingThrowType & Common.Bit25) != 0;
            effFile.SavingThrowType.Bit26 = (header.SavingThrowType & Common.Bit26) != 0;
            effFile.SavingThrowType.Bit27 = (header.SavingThrowType & Common.Bit27) != 0;
            effFile.SavingThrowType.Bit28 = (header.SavingThrowType & Common.Bit28) != 0;
            effFile.SavingThrowType.Bit29 = (header.SavingThrowType & Common.Bit29) != 0;
            effFile.SavingThrowType.Bit30 = (header.SavingThrowType & Common.Bit30) != 0;
            effFile.SavingThrowType.Bit31 = (header.SavingThrowType & Common.Bit31) != 0;
            effFile.SavingThrowBonus = header.SavingThrowBonus;
            effFile.Special = header.Special;
            effFile.PrimaryType = header.PrimaryType;
            effFile.Unknown50 = header.Unknown50;
            effFile.LowestAffectedLevelFromParent = header.LowestAffectedLevelFromParent;
            effFile.HighestAffectedLevelFromParent = header.HighestAffectedLevelFromParent;
            effFile.Resistance.DispellableAffectedByMagicResistance = (header.Resistance & Common.Bit0) != 0;
            effFile.Resistance.IgnoreMagicResistance = (header.Resistance & Common.Bit1) != 0;
            effFile.Resistance.SelfTargetted = (header.Resistance & Common.Bit2) != 0;
            effFile.Resistance.Bit3 = (header.Resistance & Common.Bit3) != 0;
            effFile.Resistance.Bit4 = (header.Resistance & Common.Bit4) != 0;
            effFile.Resistance.Bit5 = (header.Resistance & Common.Bit5) != 0;
            effFile.Resistance.Bit6 = (header.Resistance & Common.Bit6) != 0;
            effFile.Resistance.Bit7 = (header.Resistance & Common.Bit7) != 0;
            effFile.Resistance.Bit8 = (header.Resistance & Common.Bit8) != 0;
            effFile.Resistance.Bit9 = (header.Resistance & Common.Bit9) != 0;
            effFile.Resistance.Bit10 = (header.Resistance & Common.Bit10) != 0;
            effFile.Resistance.Bit11 = (header.Resistance & Common.Bit11) != 0;
            effFile.Resistance.Bit12 = (header.Resistance & Common.Bit12) != 0;
            effFile.Resistance.Bit13 = (header.Resistance & Common.Bit13) != 0;
            effFile.Resistance.Bit14 = (header.Resistance & Common.Bit14) != 0;
            effFile.Resistance.Bit15 = (header.Resistance & Common.Bit15) != 0;
            effFile.Resistance.Bit16 = (header.Resistance & Common.Bit16) != 0;
            effFile.Resistance.Bit17 = (header.Resistance & Common.Bit17) != 0;
            effFile.Resistance.Bit18 = (header.Resistance & Common.Bit18) != 0;
            effFile.Resistance.Bit19 = (header.Resistance & Common.Bit19) != 0;
            effFile.Resistance.Bit20 = (header.Resistance & Common.Bit20) != 0;
            effFile.Resistance.Bit21 = (header.Resistance & Common.Bit21) != 0;
            effFile.Resistance.Bit22 = (header.Resistance & Common.Bit22) != 0;
            effFile.Resistance.Bit23 = (header.Resistance & Common.Bit23) != 0;
            effFile.Resistance.Bit24 = (header.Resistance & Common.Bit24) != 0;
            effFile.Resistance.Bit25 = (header.Resistance & Common.Bit25) != 0;
            effFile.Resistance.Bit26 = (header.Resistance & Common.Bit26) != 0;
            effFile.Resistance.Bit27 = (header.Resistance & Common.Bit27) != 0;
            effFile.Resistance.Bit28 = (header.Resistance & Common.Bit28) != 0;
            effFile.Resistance.Bit29 = (header.Resistance & Common.Bit29) != 0;
            effFile.Resistance.Bit30 = (header.Resistance & Common.Bit30) != 0;
            effFile.Resistance.EffectAppliedByItem = (header.Resistance & Common.Bit31) != 0;
            effFile.Parameter3 = header.Parameter3;
            effFile.Parameter4 = header.Parameter4;
            effFile.Parameter5 = header.Parameter5;
            effFile.Resource2 = header.Resource2;
            effFile.Resource3 = header.Resource3;
            effFile.CasterXCoordinate = header.CasterXCoordinate;
            effFile.CasterYCoordinate = header.CasterYCoordinate;
            effFile.TargetXCoordinate = header.TargetXCoordinate;
            effFile.TargetYCoordinate = header.TargetYCoordinate;
            effFile.ResourceTypeFromParent = (ResourceTypeFromParent)header.ResourceTypeFromParent;
            effFile.ParentResource = header.ParentResource;
            effFile.FlagsFromParent.Bit0 = (header.FlagsFromParent & Common.Bit0) != 0;
            effFile.FlagsFromParent.Bit1 = (header.FlagsFromParent & Common.Bit1) != 0;
            effFile.FlagsFromParent.Bit2 = (header.FlagsFromParent & Common.Bit2) != 0;
            effFile.FlagsFromParent.Bit3 = (header.FlagsFromParent & Common.Bit3) != 0;
            effFile.FlagsFromParent.Bit4 = (header.FlagsFromParent & Common.Bit4) != 0;
            effFile.FlagsFromParent.Bit5 = (header.FlagsFromParent & Common.Bit5) != 0;
            effFile.FlagsFromParent.Bit6 = (header.FlagsFromParent & Common.Bit6) != 0;
            effFile.FlagsFromParent.Bit7 = (header.FlagsFromParent & Common.Bit7) != 0;
            effFile.FlagsFromParent.Bit8 = (header.FlagsFromParent & Common.Bit8) != 0;
            effFile.FlagsFromParent.BreaksSanctuaryInvisibility = (header.FlagsFromParent & Common.Bit9) != 0;
            effFile.FlagsFromParent.Hostile = (header.FlagsFromParent & Common.Bit10) != 0;
            effFile.FlagsFromParent.NoLOSRequired = (header.FlagsFromParent & Common.Bit11) != 0;
            effFile.FlagsFromParent.AllowSpotting = (header.FlagsFromParent & Common.Bit12) != 0;
            effFile.FlagsFromParent.OutdoorsOnly = (header.FlagsFromParent & Common.Bit13) != 0;
            effFile.FlagsFromParent.IgnoreWildSurgeDeadMagic = (header.FlagsFromParent & Common.Bit14) != 0;
            effFile.FlagsFromParent.IgnoreWildSurge = (header.FlagsFromParent & Common.Bit15) != 0;
            effFile.FlagsFromParent.NonCombatAbility = (header.FlagsFromParent & Common.Bit16) != 0;
            effFile.FlagsFromParent.Bit17 = (header.FlagsFromParent & Common.Bit17) != 0;
            effFile.FlagsFromParent.Bit18 = (header.FlagsFromParent & Common.Bit18) != 0;
            effFile.FlagsFromParent.Bit19 = (header.FlagsFromParent & Common.Bit19) != 0;
            effFile.FlagsFromParent.Bit20 = (header.FlagsFromParent & Common.Bit20) != 0;
            effFile.FlagsFromParent.Bit21 = (header.FlagsFromParent & Common.Bit21) != 0;
            effFile.FlagsFromParent.Bit22 = (header.FlagsFromParent & Common.Bit22) != 0;
            effFile.FlagsFromParent.Bit23 = (header.FlagsFromParent & Common.Bit23) != 0;
            effFile.FlagsFromParent.CanTargetInvisible = (header.FlagsFromParent & Common.Bit24) != 0;
            effFile.FlagsFromParent.CastableWhenSilenced = (header.FlagsFromParent & Common.Bit25) != 0;
            effFile.FlagsFromParent.Bit26 = (header.FlagsFromParent & Common.Bit26) != 0;
            effFile.FlagsFromParent.Bit27 = (header.FlagsFromParent & Common.Bit27) != 0;
            effFile.FlagsFromParent.Bit28 = (header.FlagsFromParent & Common.Bit28) != 0;
            effFile.FlagsFromParent.Bit29 = (header.FlagsFromParent & Common.Bit29) != 0;
            effFile.FlagsFromParent.Bit30 = (header.FlagsFromParent & Common.Bit30) != 0;
            effFile.FlagsFromParent.Bit31 = (header.FlagsFromParent & Common.Bit31) != 0;
            effFile.Projectile = header.Projectile;
            effFile.ParentResourceSlot = header.ParentResourceSlot;
            effFile.Variable = header.Variable;
            effFile.CasterLevel = header.CasterLevel;
            effFile.FirstApply = header.FirstApply;
            effFile.SecondaryType = header.SecondaryType;
            effFile.TimeApplied = header.TimeApplied;
            effFile.Unknownd4 = header.Unknownd4;
            effFile.Unknownd8 = header.Unknownd8;
            effFile.Unknowndc = header.Unknowndc;
            effFile.Unknowne0 = header.Unknowne0;
            effFile.Unknowne4 = header.Unknowne4;
            effFile.Unknowne8 = header.Unknowne8;
            effFile.Unknownec = header.Unknownec;
            effFile.Unknownf0 = header.Unknownf0;
            effFile.Unknownf4 = header.Unknownf4;
            effFile.Unknownf8 = header.Unknownf8;
            effFile.Unknownfc = header.Unknownfc;
            effFile.Unknownd100 = header.Unknownd100;
            effFile.Unknownd104 = header.Unknownd104;
            effFile.Unknownd108 = header.Unknownd108;
            effFile.Unknownd10c = header.Unknownd10c;

            effFile.Checksum = HashGenerator.GenerateKey(effFile);
            return effFile;
        }
    }
}