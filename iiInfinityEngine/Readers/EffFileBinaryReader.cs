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
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var f = Read(fs);
                f.Filename = Path.GetFileName(filename);
                return f;
            }
        }

        public EffFile Read(Stream s)
        {
            using (BinaryReader br = new BinaryReader(s))
            {
                var effFile = ParseFile(br);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                effFile.OriginalFile = ParseFile(br);
                return effFile;
            }
        }

        private EffFile ParseFile(BinaryReader br)
        {
            var header = (EffHeaderBinary)Common.ReadStruct(br, typeof(EffHeaderBinary));

            EffFile effFile = new EffFile();
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
            effFile.Resistance = header.Resistance;
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
            effFile.Projectile = header.Projectile;
            effFile.ParentResourceSlot = header.ParentResourceSlot;
            effFile.Variable = header.Variable;
            effFile.CasterLevel = header.CasterLevel;
            effFile.Unknown5 = header.Unknown5;
            effFile.SecondaryType = header.SecondaryType;
            effFile.TimeApplied = header.TimeApplied;
            effFile.Unknownd4_1 = header.Unknownd4_1;
            effFile.Unknownd4_2 = header.Unknownd4_2;
            effFile.Unknownd4_3 = header.Unknownd4_3;
            effFile.Unknownd4_4 = header.Unknownd4_4;
            effFile.Unknownd4_5 = header.Unknownd4_5;
            effFile.Unknownd4_6 = header.Unknownd4_6;
            effFile.Unknownd4_7 = header.Unknownd4_7;
            effFile.Unknownd4_8 = header.Unknownd4_8;
            effFile.Unknownd4_9 = header.Unknownd4_9;
            effFile.Unknownd4_10 = header.Unknownd4_10;
            effFile.Unknownd4_11 = header.Unknownd4_11;
            effFile.Unknownd4_12 = header.Unknownd4_12;
            effFile.Unknownd4_13 = header.Unknownd4_13;
            effFile.Unknownd4_14 = header.Unknownd4_14;
            effFile.Unknownd4_15 = header.Unknownd4_15;

            effFile.Checksum = HashGenerator.GenerateKey(effFile);
            return effFile;
        }
    }
}