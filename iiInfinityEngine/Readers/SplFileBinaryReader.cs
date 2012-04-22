using System.Collections.Generic;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Readers.Interfaces;

namespace iiInfinityEngine.Core.Readers
{
    public class SplFileBinaryReader : ISplFileReader
    {
        public TlkFile TlkFile { get; set; }

        public SplFile Read(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var f = Read(fs);
                f.Filename = Path.GetFileName(filename);
                return f;
            }
        }

        public SplFile Read(Stream s)
        {
            using (BinaryReader br = new BinaryReader(s))
            {
                var splFile = ParseFile(br);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                splFile.OriginalFile = ParseFile(br);
                return splFile;
            }
        }

        private SplFile ParseFile(BinaryReader br)
        {
            var header = (SplHeaderBinary)Common.ReadStruct(br, typeof(SplHeaderBinary));

            if (header.ftype.ToString() != "SPL ")
                return new SplFile();

            List<SplExtendedHeaderBinary> splExtendedHeaders = new List<SplExtendedHeaderBinary>();
            List<SplFeatureBlockBinary> splFeatureBlocks = new List<SplFeatureBlockBinary>();

            br.BaseStream.Seek(header.ExtendedHeaderOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.ExtendedHeaderCount; i++)
            {
                var itmExtendedHeader = (SplExtendedHeaderBinary)Common.ReadStruct(br, typeof(SplExtendedHeaderBinary));
                splExtendedHeaders.Add(itmExtendedHeader);
            }

            br.BaseStream.Seek(header.FeatureBlockOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.FeatureBlockCastingCount; i++)
            {
                var splFeatureBlock = (SplFeatureBlockBinary)Common.ReadStruct(br, typeof(SplFeatureBlockBinary));
                splFeatureBlocks.Add(splFeatureBlock);
            }

            SplFile splFile = new SplFile();
            //splFile.ftype 
            //splFile.fversion 
            splFile.UnidentifiedName = Common.ReadString(header.UnidentifiedName, TlkFile);
            splFile.IdentifiedName = Common.ReadString(header.IdentifiedName, TlkFile);
            splFile.CompletionSound = header.CompletionSound;

            splFile.Flags.Byte1Bit0 = (header.Flags & 1) != 0;
            splFile.Flags.Byte1Bit1 = (header.Flags & 2) != 0;
            splFile.Flags.Byte1Bit2 = (header.Flags & 4) != 0;
            splFile.Flags.Byte1Bit3 = (header.Flags & 8) != 0;
            splFile.Flags.Byte1Bit4 = (header.Flags & 16) != 0;
            splFile.Flags.Byte1Bit5 = (header.Flags & 32) != 0;
            splFile.Flags.Byte1Bit6 = (header.Flags & 64) != 0;
            splFile.Flags.Byte1Bit7 = (header.Flags & 128) != 0;
            splFile.Flags.Byte2Bit0 = (header.Flags & 256) != 0;
            splFile.Flags.Byte2Bit1 = (header.Flags & 512) != 0;
            splFile.Flags.Byte2Bit2 = (header.Flags & 1024) != 0;
            splFile.Flags.Byte2Bit3 = (header.Flags & 2048) != 0;
            splFile.Flags.Byte2Bit4 = (header.Flags & 4096) != 0;
            splFile.Flags.Byte2Bit5 = (header.Flags & 8192) != 0;
            splFile.Flags.Byte2Bit6 = (header.Flags & 16384) != 0;
            splFile.Flags.Byte2Bit7 = (header.Flags & 32768) != 0;
            splFile.Flags.Byte3Bit0 = (header.Flags & 65536) != 0;
            splFile.Flags.Byte3Bit1 = (header.Flags & 131072) != 0;
            splFile.Flags.Byte3Bit2 = (header.Flags & 262144) != 0;
            splFile.Flags.Byte3Bit3 = (header.Flags & 524288) != 0;
            splFile.Flags.Byte3Bit4 = (header.Flags & 1048576) != 0;
            splFile.Flags.Byte3Bit5 = (header.Flags & 2097152) != 0;
            splFile.Flags.Byte3Bit6 = (header.Flags & 4194304) != 0;
            splFile.Flags.Byte3Bit7 = (header.Flags & 8388608) != 0;
            splFile.Flags.Byte4Bit0 = (header.Flags & 16777216) != 0;
            splFile.Flags.Byte4Bit1 = (header.Flags & 33554432) != 0;
            splFile.Flags.Byte4Bit2 = (header.Flags & 67108864) != 0;
            splFile.Flags.Byte4Bit3 = (header.Flags & 134217728) != 0;
            splFile.Flags.Byte4Bit4 = (header.Flags & 268435456) != 0;
            splFile.Flags.Byte4Bit5 = (header.Flags & 536870912) != 0;
            splFile.Flags.Byte4Bit6 = (header.Flags & 1073741824) != 0;
            splFile.Flags.Byte4Bit7 = (header.Flags & 2147483648) != 0;

            splFile.SpellType = (SpellType)header.SpellType;
            splFile.ExclusionFlags = header.ExclusionFlags;
            splFile.CastingGraphic = header.CastinGraphic;
            splFile.Unknown = header.Unknown;
            splFile.PrimaryType = header.PrimaryType;
            splFile.SecondaryType = header.SecondaryType;
            splFile.Unknown1 = header.Unknown1;
            splFile.Unknown2 = header.Unknown2;
            splFile.Unknown3 = header.Unknown3;
            splFile.SpellLevel = header.SpellLevel;
            splFile.Unknown4 = header.Unknown4;
            splFile.SpellBookIcon = header.SpellBookIcon;
            splFile.Unknown5 = header.Unknown5;
            splFile.Unknown6 = header.Unknown6;
            splFile.Unknown7 = header.Unknown7;
            splFile.Unknown8 = header.Unknown8;
            splFile.UnidentifiedDescription = Common.ReadString(header.UnidentifiedDescription, TlkFile);
            splFile.IdentifiedDescription = Common.ReadString(header.IdentifiedDescription, TlkFile);
            splFile.Unknown9 = header.Unknown9;
            splFile.Unknown10 = header.Unknown10;
            splFile.Unknown11 = header.Unknown11;

            var totalFeatureBlockCount = 0;

            foreach (var extendedHeader in splExtendedHeaders)
            {
                SplExtendedHeader2 extendedHeader2 = new SplExtendedHeader2();
                extendedHeader2.ChargeDepletionBehaviour = extendedHeader.ChargeDepletionBehaviour;
                extendedHeader2.Charges = extendedHeader.Charges;
                extendedHeader2.CastingTime = extendedHeader.CastingTime;
                extendedHeader2.DamageType = extendedHeader.DamageType;
                extendedHeader2.DiceSides = extendedHeader.DiceSides;
                extendedHeader2.DiceThrown = extendedHeader.DiceThrown;
                extendedHeader2.Enchantment = extendedHeader.Enchantment;
                extendedHeader2.FeatureBlockCount = extendedHeader.FeatureBlockCount;
                extendedHeader2.FeatureBlockOffset = extendedHeader.FeatureBlockOffset;
                extendedHeader2.LevelRequired = extendedHeader.LevelRequired;
                extendedHeader2.Location = extendedHeader.Location;
                extendedHeader2.MemorisedIcon = extendedHeader.MemorisedIcon;
                extendedHeader2.ProjectileAnimation = extendedHeader.ProjectileAnimation;
                extendedHeader2.Range = extendedHeader.Range;
                extendedHeader2.SpellForm = extendedHeader.SpellForm;
                extendedHeader2.TargetCount = extendedHeader.TargetCount;
                extendedHeader2.TargetType = extendedHeader.TargetType;
                extendedHeader2.Unknown = extendedHeader.Unknown;
                extendedHeader2.Unknown2 = extendedHeader.Unknown2;

                br.BaseStream.Seek(header.FeatureBlockOffset + (header.FeatureBlockCastingCount * 48) + (totalFeatureBlockCount * 48), SeekOrigin.Begin);
                for (int i = 0; i < extendedHeader.FeatureBlockCount; i++)
                {
                    var featureBlock = (SplFeatureBlockBinary)Common.ReadStruct(br, typeof(SplFeatureBlockBinary));

                    SplFeatureBlock2 splFeatureBlock2 = new SplFeatureBlock2();
                    splFeatureBlock2.DiceSides = featureBlock.DiceSides;
                    splFeatureBlock2.DiceThrown = featureBlock.DiceThrown;
                    splFeatureBlock2.Duration = featureBlock.Duration;
                    splFeatureBlock2.Opcode = featureBlock.Opcode;
                    splFeatureBlock2.Parameter1 = featureBlock.Parameter1;
                    splFeatureBlock2.Parameter2 = featureBlock.Parameter2;
                    splFeatureBlock2.Power = featureBlock.Power;
                    splFeatureBlock2.Probability1 = featureBlock.Probability1;
                    splFeatureBlock2.Probability2 = featureBlock.Probability2;
                    splFeatureBlock2.Resistance = featureBlock.Resistance;
                    splFeatureBlock2.Resource = featureBlock.Resource;
                    splFeatureBlock2.SavingThrowBonus = featureBlock.SavingThrowBonus;
                    splFeatureBlock2.SavingThrowType = featureBlock.SavingThrowType;
                    splFeatureBlock2.TargetType = featureBlock.TargetType;
                    splFeatureBlock2.TimingMode = (TimingMode)featureBlock.TimingMode;
                    splFeatureBlock2.Unknown = featureBlock.Unknown;

                    extendedHeader2.splFeatureBlocks.Add(splFeatureBlock2);
                    totalFeatureBlockCount++;
                }

                splFile.splExtendedHeader.Add(extendedHeader2);
            }

            foreach (var featureBlock in splFeatureBlocks)
            {
                SplFeatureBlock2 splFeatureBlock2 = new SplFeatureBlock2();
                splFeatureBlock2.DiceSides = featureBlock.DiceSides;
                splFeatureBlock2.DiceThrown = featureBlock.DiceThrown;
                splFeatureBlock2.Duration = featureBlock.Duration;
                splFeatureBlock2.Opcode = featureBlock.Opcode;
                splFeatureBlock2.Parameter1 = featureBlock.Parameter1;
                splFeatureBlock2.Parameter2 = featureBlock.Parameter2;
                splFeatureBlock2.Power = featureBlock.Power;
                splFeatureBlock2.Probability1 = featureBlock.Probability1;
                splFeatureBlock2.Probability2 = featureBlock.Probability2;
                splFeatureBlock2.Resistance = featureBlock.Resistance;
                splFeatureBlock2.Resource = featureBlock.Resource;
                splFeatureBlock2.SavingThrowBonus = featureBlock.SavingThrowBonus;
                splFeatureBlock2.SavingThrowType = featureBlock.SavingThrowType;
                splFeatureBlock2.TargetType = featureBlock.TargetType;
                splFeatureBlock2.TimingMode = (TimingMode)featureBlock.TimingMode;
                splFeatureBlock2.Unknown = featureBlock.Unknown;

                splFile.splFeatureBlocks.Add(splFeatureBlock2);
            }

            splFile.Checksum = MD5HashGenerator.GenerateKey(splFile);
            return splFile;
        }
    }
}