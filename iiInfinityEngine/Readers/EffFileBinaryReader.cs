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
            effFile.TargetType = header.TargetType;
            effFile.Power = header.Power;
            effFile.Parameter1 = header.Parameter1;
            effFile.Parameter2 = header.Parameter2;
            effFile.TimingMode = header.TimingMode;
            effFile.Unknown = header.Unknown;
            effFile.Duration = header.Duration;
            effFile.Probability1 = header.Probability1;
            effFile.Probability2 = header.Probability2;
            effFile.Resource = header.Resource;
            effFile.DiceThrown = header.DiceThrown;
            effFile.DiceSides = header.DiceSides;
            effFile.SavingThrowType = header.SavingThrowType;
            effFile.SavingThrowBonus = header.SavingThrowBonus;
            effFile.SetLocalVariableIfNonExistant = header.SetLocalVariableIfNonExistant;
            effFile.PrimaryType = header.PrimaryType;
            effFile.Unknown2 = header.Unknown2;
            effFile.LowestAffectedLevelFromParent = header.LowestAffectedLevelFromParent;
            effFile.HighestAffectedLevelFromParent = header.HighestAffectedLevelFromParent;
            effFile.Resistance = header.Resistance;
            effFile.Parameter3 = header.Parameter3;
            effFile.Parameter4 = header.Parameter4;
            effFile.Resource2 = header.Resource2;
            effFile.Resource3 = header.Resource3; // VVC aka Parameter 5
            effFile.Unknown3 = header.Unknown3;
            effFile.Unknown4 = header.Unknown4;
            effFile.CasterXCoordinate = header.CasterXCoordinate;
            effFile.CasterYCoordinate = header.CasterYCoordinate;
            effFile.TargetXCoordinate = header.TargetXCoordinate;
            effFile.TargetYCoordinate = header.TargetYCoordinate;
            effFile.ResourceTypeFromParent = header.ResourceTypeFromParent;
            effFile.ParentResource = header.ParentResource;
            effFile.Projectile = header.Projectile;
            effFile.ParentResourceSlot = header.ParentResourceSlot;
            effFile.Variable = header.Variable;
            effFile.CasterLevel = header.CasterLevel;
            effFile.Unknown5 = header.Unknown5;
            effFile.SecondaryType = header.SecondaryType;
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

            effFile.Checksum = MD5HashGenerator.GenerateKey(effFile);
            return effFile;
        }
    }
}