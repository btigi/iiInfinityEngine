using System;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;

namespace iiInfinityEngine.Core.Writers
{
    class EffFileBinaryWriter : IEffFileWriter
    {
        public BackupManager BackupManger { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (!(file is EffFile))
                throw new ArgumentException("File is not a valid eff file");

            var effFile = file as EffFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(effFile) == effFile.Checksum))
                return false;

            EffHeaderBinary header = new EffHeaderBinary();

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