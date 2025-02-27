using System.IO;
using WindowsFormsApplication1.Binary;
using WindowsFormsApplication1.Files;

namespace WindowsFormsApplication1.Writers
{
    class EffBinaryWriter
    {
        public void Write(string filename, EffFile effFile)
        {
            EffHeader header = new EffHeader();

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
            header.ParentResource = effFile.ParentResource;
            header.ParentResourceSlot = effFile.ParentResourceSlot;
            header.Power = effFile.Power;
            header.PrimaryType = effFile.PrimaryType;
            header.Probability1 = effFile.Probability1;
            header.Probability2 = effFile.Probability2;
            header.Projectile = effFile.Projectile;
            header.Resistance = effFile.Resistance;
            header.Resource = effFile.Resource;
            header.Resource2 = effFile.Resource2;
            header.Resource3 = effFile.Resource3;
            header.ResourceTypeFromParent = effFile.ResourceTypeFromParent;
            header.SavingThrowBonus = effFile.SavingThrowBonus;
            header.SavingThrowType = effFile.SavingThrowType;
            header.SecondaryType = effFile.SecondaryType;
            header.SetLocalVariableIfNonExistant = effFile.SetLocalVariableIfNonExistant;
            header.TargetType = effFile.TargetType;
            header.TargetXCoordinate = effFile.TargetXCoordinate;
            header.TargetYCoordinate = effFile.TargetYCoordinate;
            header.TimingMode = effFile.TimingMode;
            header.Unknown = effFile.Unknown;
            header.Unknown2 = effFile.Unknown2;
            header.Unknown3 = effFile.Unknown3;
            header.Unknown4 = effFile.Unknown4;
            header.Unknown5 = effFile.Unknown5;
            header.Unknownd4_1 = effFile.Unknownd4_1;
            header.Unknownd4_2 = effFile.Unknownd4_2;
            header.Unknownd4_3 = effFile.Unknownd4_3;
            header.Unknownd4_4 = effFile.Unknownd4_4;
            header.Unknownd4_5 = effFile.Unknownd4_5;
            header.Unknownd4_6 = effFile.Unknownd4_6;
            header.Unknownd4_7 = effFile.Unknownd4_7;
            header.Unknownd4_8 = effFile.Unknownd4_8;
            header.Unknownd4_9 = effFile.Unknownd4_9;
            header.Unknownd4_10 = effFile.Unknownd4_10;
            header.Unknownd4_11 = effFile.Unknownd4_11;
            header.Unknownd4_12 = effFile.Unknownd4_12;
            header.Unknownd4_13 = effFile.Unknownd4_13;
            header.Unknownd4_14 = effFile.Unknownd4_14;
            header.Unknownd4_15 = effFile.Unknownd4_15;
            header.Variable = effFile.Variable;

            using (MemoryStream s = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(s))
                {
                    var headerAsBytes = Common.WriteStruct(header);

                    bw.Write(headerAsBytes);

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