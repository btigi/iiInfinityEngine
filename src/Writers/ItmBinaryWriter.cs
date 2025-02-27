using System;
using System.Collections.Generic;
using System.IO;
using WindowsFormsApplication1.Binary;
using WindowsFormsApplication1.Files;

namespace WindowsFormsApplication1.Writers
{
    class ItmBinaryWriter
    {
        const int HeaderSize = 114;
        const int ExtendedHeaderSize = 56;
        const int FeatureBlockSize = 48;

        List<ItmExtendedHeader> itmExtendedHeaders = new List<ItmExtendedHeader>();
        List<ItmFeatureBlock> itmFeatureBlocks = new List<ItmFeatureBlock>();

        public void Write(string filename, ItmFile itmFile)
        {
            foreach (var featureBlock in itmFile.itmFeatureBlocks)
            {
                ItmFeatureBlock featureBlockBinary = new ItmFeatureBlock();
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
                featureBlockBinary.TimingMode = featureBlock.TimingMode;
                featureBlockBinary.Unknown = featureBlock.Unknown;
                itmFeatureBlocks.Add(featureBlockBinary);
            }

            foreach (var extendedHeader in itmFile.itmExtendedHeader)
            {
                ItmExtendedHeader extendedHeaderBinary = new ItmExtendedHeader();
                extendedHeaderBinary.AlternaticeDamageBonus = extendedHeader.AlternaticeDamageBonus;
                extendedHeaderBinary.AlternaticeDiceSides = extendedHeader.AlternaticeDiceSides;
                extendedHeaderBinary.AlternaticeDiceThrown = extendedHeader.AlternaticeDiceThrown;
                extendedHeaderBinary.AttackType = extendedHeader.AttackType;
                extendedHeaderBinary.ChargeDepletionBehaviour = extendedHeader.ChargeDepletionBehaviour;
                extendedHeaderBinary.Charges = extendedHeader.Charges;
                extendedHeaderBinary.DamageBonus = extendedHeader.DamageBonus;
                extendedHeaderBinary.DamageType = extendedHeader.DamageType;
                extendedHeaderBinary.DiceSides = extendedHeader.DiceSides;
                extendedHeaderBinary.DiceThrown = extendedHeader.DiceThrown;
                extendedHeaderBinary.Flags = extendedHeader.Flags;
                extendedHeaderBinary.IdentificationRequirement = extendedHeader.IdentificationRequirement;
                extendedHeaderBinary.IsBowArrow = extendedHeader.IsBowArrow;
                extendedHeaderBinary.IsCrossbowBolt = extendedHeader.IsCrossbowBolt;
                extendedHeaderBinary.IsMiscProjectile = extendedHeader.IsMiscProjectile;
                extendedHeaderBinary.Location = extendedHeader.Location;
                extendedHeaderBinary.MeleeAnimation1 = extendedHeader.MeleeAnimation1;
                extendedHeaderBinary.MeleeAnimation2 = extendedHeader.MeleeAnimation2;
                extendedHeaderBinary.MeleeAnimation3 = extendedHeader.MeleeAnimation3;
                extendedHeaderBinary.PrimaryType = extendedHeader.PrimaryType;
                extendedHeaderBinary.ProjectileAnimation = extendedHeader.ProjectileAnimation;
                extendedHeaderBinary.ProjectileType = extendedHeader.ProjectileType;
                extendedHeaderBinary.Range = extendedHeader.Range;
                extendedHeaderBinary.SecondaryType = extendedHeader.SecondaryType;
                extendedHeaderBinary.Speed = extendedHeader.Speed;
                extendedHeaderBinary.TargetCount = extendedHeader.TargetCount;
                extendedHeaderBinary.TargetType = extendedHeader.TargetType;
                extendedHeaderBinary.Thac0Bonus = extendedHeader.Thac0Bonus;
                extendedHeaderBinary.Unknown = extendedHeader.Unknown;
                extendedHeaderBinary.UseIcon = extendedHeader.UseIcon;
                extendedHeaderBinary.FeatureBlockCount = extendedHeader.FeatureBlockCount;
                extendedHeaderBinary.FeatureBlockOffset = Convert.ToInt16(extendedHeader.itmFeatureBlocks.Count);

                foreach (var featureBlock in extendedHeader.itmFeatureBlocks)
                {
                    ItmFeatureBlock featureBlockBinary = new ItmFeatureBlock();
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
                    featureBlockBinary.TimingMode = featureBlock.TimingMode;
                    featureBlockBinary.Unknown = featureBlock.Unknown;
                    itmFeatureBlocks.Add(featureBlockBinary);
                }

                itmExtendedHeaders.Add(extendedHeaderBinary);
            }

            ItmHeader header = new ItmHeader();

            header.Flags = itmFile.Flags.CriticalItem ? header.Flags | 1 : header.Flags;
            header.Flags = itmFile.Flags.TwoHanded ? header.Flags | 2 : header.Flags;
            header.Flags = itmFile.Flags.Movable ? header.Flags | 4 : header.Flags;
            header.Flags = itmFile.Flags.Displayable ? header.Flags | 8 : header.Flags;
            header.Flags = itmFile.Flags.Cursed ? header.Flags | 16 : header.Flags;
            header.Flags = itmFile.Flags.Unknown5 ? header.Flags | 32 : header.Flags;
            header.Flags = itmFile.Flags.Magical ? header.Flags | 64 : header.Flags;
            header.Flags = itmFile.Flags.Bow ? header.Flags | 128 : header.Flags;

            header.Flags = itmFile.Flags.Silver ? header.Flags | 256 : header.Flags;
            header.Flags = itmFile.Flags.ColdIron ? header.Flags | 512 : header.Flags;
            header.Flags = itmFile.Flags.Stolen ? header.Flags | 1024 : header.Flags;
            header.Flags = itmFile.Flags.Conversable ? header.Flags | 2048 : header.Flags;
            header.Flags = itmFile.Flags.Unknown12 ? header.Flags | 4096 : header.Flags;
            header.Flags = itmFile.Flags.Unknown13 ? header.Flags | 8192 : header.Flags;
            header.Flags = itmFile.Flags.Unknown14 ? header.Flags | 16384 : header.Flags;
            header.Flags = itmFile.Flags.Unknown15 ? header.Flags | 32768 : header.Flags;

            header.Flags = itmFile.Flags.Unknown16 ? header.Flags | 65536 : header.Flags;
            header.Flags = itmFile.Flags.Unknown17 ? header.Flags | 131072 : header.Flags;
            header.Flags = itmFile.Flags.Unknown18 ? header.Flags | 262144 : header.Flags;
            header.Flags = itmFile.Flags.Unknown19 ? header.Flags | 524288 : header.Flags;
            header.Flags = itmFile.Flags.Unknown20 ? header.Flags | 1048576 : header.Flags;
            header.Flags = itmFile.Flags.Unknown21 ? header.Flags | 2097152 : header.Flags;
            header.Flags = itmFile.Flags.Unknown22 ? header.Flags | 4194304 : header.Flags;
            header.Flags = itmFile.Flags.Unknown23 ? header.Flags | 8388608 : header.Flags;

            header.Flags = itmFile.Flags.Unknown24 ? header.Flags | 16777216 : header.Flags;
            header.Flags = itmFile.Flags.Unknown25 ? header.Flags | 33554432 : header.Flags;
            header.Flags = itmFile.Flags.Unknown26 ? header.Flags | 67108864 : header.Flags;
            header.Flags = itmFile.Flags.Unknown27 ? header.Flags | 134217728 : header.Flags;
            header.Flags = itmFile.Flags.Unknown28 ? header.Flags | 268435456 : header.Flags;
            header.Flags = itmFile.Flags.Unknown29 ? header.Flags | 536870912 : header.Flags;
            header.Flags = itmFile.Flags.Unknown30 ? header.Flags | 1073741824 : header.Flags;
            //header.Flags = itmFile.Flags.Unknown31 ? header.Flags | 2147483648 : header.Flags;

            header.ftype = new array4() { character1 = 'I', character2 = 'T', character3 = 'M', character4 = ' ' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = ' ', character4 = ' ' };
            header.Animation = new array2() { character1 = itmFile.Animation[0], character2 = itmFile.Animation[1] };
            header.DescriptionIcon = new array8(itmFile.DescriptionIcon);
            header.Enchantment = itmFile.Enchantment;

            header.ExtendedHeaderCount = Convert.ToInt16(itmExtendedHeaders.Count);
            header.ExtendedHeaderOffset = HeaderSize;
            header.FeatureBlockEquippingCount = Convert.ToInt16(itmFile.itmFeatureBlocks.Count);
            header.FeatureBlockEquippingIndex = 0;
            header.FeatureBlockOffset = HeaderSize + (ExtendedHeaderSize * itmExtendedHeaders.Count);

            header.GroundIcon = new array8(itmFile.GroundIcon);
            header.IdentifiedDescription = itmFile.IdentifiedDescription;
            header.IdentifiedName = itmFile.IdentifiedName;
            header.InventoryIcon = new array8(itmFile.InventoryIcon);
            header.ItemType = Convert.ToInt16(itmFile.ItemType);
            header.KitUsability1 = itmFile.KitUsability1;
            header.KitUsability2 = itmFile.KitUsability2;
            header.KitUsability3 = itmFile.KitUsability3;
            header.KitUsability4 = itmFile.KitUsability4;
            header.LoreToIdentify = itmFile.LoreToIdentify;
            header.MinimumCharisma = itmFile.MinimumCharisma;
            header.MinimumConstitution = itmFile.MinimumConstitution;
            header.MinimumDexterity = itmFile.MinimumDexterity;
            header.MinimumIntelligence = itmFile.MinimumIntelligence;
            header.MinimumLevel = itmFile.MinimumLevel;
            header.MinimumStrength = itmFile.MinimumStrength;
            header.MinimumStrengthBonus = itmFile.MinimumStrengthBonus;
            header.MinimumWisdom = itmFile.MinimumWisdom;
            header.Price = itmFile.Price;
            header.Proficiency = itmFile.Proficiency;
            header.ReplacementItem = new array8(itmFile.ReplacementItem);
            header.StackAmount = itmFile.StackAmount;
            header.UnidentifiedDescription = itmFile.UnidentifiedDescription;
            header.UnidentifiedName = itmFile.UnidentifiedName;
            header.Unknown = itmFile.Unknown;
            header.Unknown2 = itmFile.Unknown2;
            header.Unknown3 = itmFile.Unknown3;
            header.Usability1 = itmFile.Usability1;
            header.Usability2 = itmFile.Usability2;
            header.Usability3 = itmFile.Usability3;
            header.Weight = itmFile.Weight;

            using (MemoryStream s = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(s))
                {
                    var headerAsBytes = Common.WriteStruct(header);

                    bw.Write(headerAsBytes);

                    foreach (var extendedHeader in itmExtendedHeaders)
                    {
                        var extendedHeaderAsBytes = Common.WriteStruct(extendedHeader);
                        bw.Write(extendedHeaderAsBytes);
                    }

                    foreach (var featureBlock in itmFeatureBlocks)
                    {
                        var featureBlockAsBytes = Common.WriteStruct(featureBlock);
                        bw.Write(featureBlockAsBytes);
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