using System;
using System.Collections.Generic;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace iiInfinityEngine.Core.Writers
{
    public class SplFileBinaryWriter : ISplFileWriter
    {
        const int HeaderSize = 114;
        const int ExtendedHeaderSize = 56;
        const int FeatureBlockSize = 48;

        public TlkFile TlkFile { get; set; }
        public BackupManager BackupManger { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (!(file is SplFile))
                throw new ArgumentException("File is not a valid creature file");

            var splFile = file as SplFile;

            if (!(forceSave) && (MD5HashGenerator.GenerateKey(splFile) == splFile.Checksum))
                return false;

            List<SplExtendedHeaderBinary> splExtendedHeaders = new List<SplExtendedHeaderBinary>();
            List<SplFeatureBlockBinary> splFeatureBlocks = new List<SplFeatureBlockBinary>();

            foreach (var featureBlock in splFile.splFeatureBlocks)
            {
                SplFeatureBlockBinary featureBlockBinary = new SplFeatureBlockBinary();
                featureBlockBinary.DiceSides = featureBlock.DiceSides;
                featureBlockBinary.DiceThrown = featureBlock.DiceThrown;
                featureBlockBinary.Duration = featureBlock.Duration;
                featureBlockBinary.Opcode = featureBlock.Opcode;
                featureBlockBinary.Parameter1 = featureBlock.Parameter1;
                featureBlockBinary.Parameter2 = featureBlock.Parameter2;
                featureBlockBinary.Power = featureBlock.Power;
                featureBlockBinary.Probability1 = featureBlock.Probability1;
                featureBlockBinary.Probability2 = featureBlock.Probability2;
                featureBlockBinary.Resistance = featureBlock.Resistance;
                featureBlockBinary.Resource = featureBlock.Resource;
                featureBlockBinary.SavingThrowBonus = featureBlock.SavingThrowBonus;
                featureBlockBinary.SavingThrowType = featureBlock.SavingThrowType;
                featureBlockBinary.TargetType = featureBlock.TargetType;
                featureBlockBinary.TimingMode = Convert.ToByte(featureBlock.TimingMode);
                featureBlockBinary.Unknown = featureBlock.Unknown;
                splFeatureBlocks.Add(featureBlockBinary);
            }

            foreach (var extendedHeader in splFile.splExtendedHeader)
            {
                SplExtendedHeaderBinary extendedHeaderBinary = new SplExtendedHeaderBinary();
                extendedHeaderBinary.CastingTime = extendedHeader.CastingTime;
                extendedHeaderBinary.ChargeDepletionBehaviour = extendedHeader.ChargeDepletionBehaviour;
                extendedHeaderBinary.Charges = extendedHeader.Charges;
                extendedHeaderBinary.DamageType = extendedHeader.DamageType;
                extendedHeaderBinary.DiceSides = extendedHeader.DiceSides;
                extendedHeaderBinary.DiceThrown = extendedHeader.DiceThrown;
                extendedHeaderBinary.LevelRequired = extendedHeader.LevelRequired;
                extendedHeaderBinary.Location = extendedHeader.Location;
                extendedHeaderBinary.MemorisedIcon = extendedHeader.MemorisedIcon;
                extendedHeaderBinary.ProjectileAnimation = extendedHeader.ProjectileAnimation;
                extendedHeaderBinary.Range = extendedHeader.Range;
                extendedHeaderBinary.SpellForm = extendedHeader.SpellForm;
                extendedHeaderBinary.TargetCount = extendedHeader.TargetCount;
                extendedHeaderBinary.TargetType = extendedHeader.TargetType;
                extendedHeaderBinary.Unknown = extendedHeader.Unknown;
                extendedHeaderBinary.Unknown2 = extendedHeader.Unknown2;
                extendedHeaderBinary.FeatureBlockCount = extendedHeader.FeatureBlockCount;
                extendedHeaderBinary.FeatureBlockOffset = Convert.ToInt16(extendedHeader.splFeatureBlocks.Count);

                foreach (var featureBlock in extendedHeader.splFeatureBlocks)
                {
                    SplFeatureBlockBinary featureBlockBinary = new SplFeatureBlockBinary();
                    featureBlockBinary.DiceSides = featureBlock.DiceSides;
                    featureBlockBinary.DiceThrown = featureBlock.DiceThrown;
                    featureBlockBinary.Duration = featureBlock.Duration;
                    featureBlockBinary.Opcode = featureBlock.Opcode;
                    featureBlockBinary.Parameter1 = featureBlock.Parameter1;
                    featureBlockBinary.Parameter2 = featureBlock.Parameter2;
                    featureBlockBinary.Power = featureBlock.Power;
                    featureBlockBinary.Probability1 = featureBlock.Probability1;
                    featureBlockBinary.Probability2 = featureBlock.Probability2;
                    featureBlockBinary.Resistance = featureBlock.Resistance;
                    featureBlockBinary.Resource = featureBlock.Resource;
                    featureBlockBinary.SavingThrowBonus = featureBlock.SavingThrowBonus;
                    featureBlockBinary.SavingThrowType = featureBlock.SavingThrowType;
                    featureBlockBinary.TargetType = featureBlock.TargetType;
                    featureBlockBinary.TimingMode = Convert.ToByte(featureBlock.TimingMode);
                    featureBlockBinary.Unknown = featureBlock.Unknown;
                    splFeatureBlocks.Add(featureBlockBinary);
                }

                splExtendedHeaders.Add(extendedHeaderBinary);
            }

            SplHeaderBinary header = new SplHeaderBinary();

            header.Flags = splFile.Flags.Byte1Bit0 ? header.Flags | 1 : header.Flags;
            header.Flags = splFile.Flags.Byte1Bit1 ? header.Flags | 2 : header.Flags;
            header.Flags = splFile.Flags.Byte1Bit2 ? header.Flags | 4 : header.Flags;
            header.Flags = splFile.Flags.Byte1Bit3 ? header.Flags | 8 : header.Flags;
            header.Flags = splFile.Flags.Byte1Bit4 ? header.Flags | 16 : header.Flags;
            header.Flags = splFile.Flags.Byte1Bit5 ? header.Flags | 32 : header.Flags;
            header.Flags = splFile.Flags.Byte1Bit6 ? header.Flags | 64 : header.Flags;
            header.Flags = splFile.Flags.Byte1Bit7 ? header.Flags | 128 : header.Flags;

            header.Flags = splFile.Flags.Byte2Bit0 ? header.Flags | 256 : header.Flags;
            header.Flags = splFile.Flags.Byte2Bit1 ? header.Flags | 512 : header.Flags;
            header.Flags = splFile.Flags.Byte2Bit2 ? header.Flags | 1024 : header.Flags;
            header.Flags = splFile.Flags.Byte2Bit3 ? header.Flags | 2048 : header.Flags;
            header.Flags = splFile.Flags.Byte2Bit4 ? header.Flags | 4096 : header.Flags;
            header.Flags = splFile.Flags.Byte2Bit5 ? header.Flags | 8192 : header.Flags;
            header.Flags = splFile.Flags.Byte2Bit6 ? header.Flags | 16384 : header.Flags;
            header.Flags = splFile.Flags.Byte2Bit7 ? header.Flags | 32768 : header.Flags;

            header.Flags = splFile.Flags.Byte3Bit0 ? header.Flags | 65536 : header.Flags;
            header.Flags = splFile.Flags.Byte3Bit1 ? header.Flags | 131072 : header.Flags;
            header.Flags = splFile.Flags.Byte3Bit2 ? header.Flags | 262144 : header.Flags;
            header.Flags = splFile.Flags.Byte3Bit3 ? header.Flags | 524288 : header.Flags;
            header.Flags = splFile.Flags.Byte3Bit4 ? header.Flags | 1048576 : header.Flags;
            header.Flags = splFile.Flags.Byte3Bit5 ? header.Flags | 2097152 : header.Flags;
            header.Flags = splFile.Flags.Byte3Bit6 ? header.Flags | 4194304 : header.Flags;
            header.Flags = splFile.Flags.Byte3Bit7 ? header.Flags | 8388608 : header.Flags;

            header.Flags = splFile.Flags.Byte4Bit0 ? header.Flags | 16777216 : header.Flags;
            header.Flags = splFile.Flags.Byte4Bit1 ? header.Flags | 33554432 : header.Flags;
            header.Flags = splFile.Flags.Byte4Bit2 ? header.Flags | 67108864 : header.Flags;
            header.Flags = splFile.Flags.Byte4Bit3 ? header.Flags | 134217728 : header.Flags;
            header.Flags = splFile.Flags.Byte4Bit4 ? header.Flags | 268435456 : header.Flags;
            header.Flags = splFile.Flags.Byte4Bit5 ? header.Flags | 536870912 : header.Flags;
            header.Flags = splFile.Flags.Byte4Bit6 ? header.Flags | 1073741824 : header.Flags;
            //header.Flags = splFile.Flags.Byte4Bit7 ? header.Flags | 2147483648 : header.Flags;

            header.ftype = new array4() { character1 = 'S', character2 = 'P', character3 = 'L', character4 = ' ' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = ' ', character4 = ' ' };
            header.CastinGraphic = splFile.CastingGraphic;
            header.CompletionSound = splFile.CompletionSound;
            header.ExclusionFlags = splFile.ExclusionFlags;

            header.ExtendedHeaderCount = Convert.ToInt16(splExtendedHeaders.Count);
            header.ExtendedHeaderOffset = HeaderSize;
            header.FeatureBlockCastingCount = Convert.ToInt16(splFile.splFeatureBlocks.Count);
            header.FeatureBlockCastingIndex = 0;
            header.FeatureBlockOffset = HeaderSize + (ExtendedHeaderSize * splExtendedHeaders.Count);
            header.IdentifiedDescription = Common.WriteString(splFile.IdentifiedDescription, TlkFile);
            header.IdentifiedName = Common.WriteString(splFile.IdentifiedName, TlkFile);
            header.PrimaryType = splFile.PrimaryType;
            header.SecondaryType = splFile.SecondaryType;
            header.SpellBookIcon = splFile.SpellBookIcon;
            header.SpellLevel = splFile.SpellLevel;
            header.SpellType = Convert.ToInt16(splFile.SpellType);
            header.UnidentifiedDescription = Common.WriteString(splFile.UnidentifiedDescription, TlkFile);
            header.UnidentifiedName = Common.WriteString(splFile.UnidentifiedName, TlkFile);
            header.Unknown = splFile.Unknown;
            header.Unknown2 = splFile.Unknown2;
            header.Unknown3 = splFile.Unknown3;
            header.Unknown4 = splFile.Unknown4;
            header.Unknown5 = splFile.Unknown5;
            header.Unknown6 = splFile.Unknown6;
            header.Unknown7 = splFile.Unknown7;
            header.Unknown8 = splFile.Unknown8;
            header.Unknown9 = splFile.Unknown9;
            header.Unknown10 = splFile.Unknown10;
            header.Unknown11 = splFile.Unknown11;

            using (MemoryStream s = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(s))
                {
                    var headerAsBytes = Common.WriteStruct(header);

                    bw.Write(headerAsBytes);

                    foreach (var extendedHeader in splExtendedHeaders)
                    {
                        var extendedHeaderAsBytes = Common.WriteStruct(extendedHeader);
                        bw.Write(extendedHeaderAsBytes);
                    }

                    foreach (var featureBlock in splFeatureBlocks)
                    {
                        var featureBlockAsBytes = Common.WriteStruct(featureBlock);
                        bw.Write(featureBlockAsBytes);
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