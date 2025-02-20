﻿using System;
using System.Collections.Generic;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;

namespace iiInfinityEngine.Core.Writers
{
    public class ItmFileBinaryWriter : IItmFileWriter
    {
        const int HeaderSize = 114;
        const int ExtendedHeaderSize = 56;
        const int FeatureBlockSize = 48;

        public TlkFile TlkFile { get; set; }
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not ItmFile)
                throw new ArgumentException("File is not a valid itm file");

            var itmFile = file as ItmFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(itmFile) == itmFile.Checksum))
                return false;

            var itmExtendedHeaders = new List<ItmExtendedHeaderBinary>();
            var itmFeatureBlocks = new List<ItmFeatureBlockBinary>();

            foreach (var featureBlock in itmFile.itmFeatureBlocks)
            {
                var featureBlockBinary = new ItmFeatureBlockBinary();
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
                var extendedHeaderBinary = new ItmExtendedHeaderBinary();
                extendedHeaderBinary.AlternaticeDamageBonus = extendedHeader.AlternaticeDamageBonus;
                extendedHeaderBinary.AlternaticeDiceSides = extendedHeader.AlternaticeDiceSides;
                extendedHeaderBinary.AlternaticeDiceThrown = extendedHeader.AlternaticeDiceThrown;
                extendedHeaderBinary.AttackType = (byte)extendedHeader.AttackType;
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
                extendedHeaderBinary.Location = (byte)extendedHeader.Location;
                extendedHeaderBinary.MeleeAnimation1 = extendedHeader.MeleeAnimation1;
                extendedHeaderBinary.MeleeAnimation2 = extendedHeader.MeleeAnimation2;
                extendedHeaderBinary.MeleeAnimation3 = extendedHeader.MeleeAnimation3;
                extendedHeaderBinary.PrimaryType = extendedHeader.PrimaryType;
                extendedHeaderBinary.ProjectileAnimation = extendedHeader.ProjectileAnimation;
                extendedHeaderBinary.LauncherType = (byte)extendedHeader.LauncherType;
                extendedHeaderBinary.Range = extendedHeader.Range;
                extendedHeaderBinary.SecondaryType = extendedHeader.SecondaryType;
                extendedHeaderBinary.Speed = extendedHeader.Speed;
                extendedHeaderBinary.TargetCount = extendedHeader.TargetCount;
                extendedHeaderBinary.TargetType = (byte)extendedHeader.TargetType;
                extendedHeaderBinary.Thac0Bonus = extendedHeader.Thac0Bonus;
                extendedHeaderBinary.UseIcon = extendedHeader.UseIcon;
                extendedHeaderBinary.FeatureBlockCount = extendedHeader.FeatureBlockCount;
                extendedHeaderBinary.FeatureBlockOffset = Convert.ToInt16(extendedHeader.itmFeatureBlocks.Count);

                foreach (var featureBlock in extendedHeader.itmFeatureBlocks)
                {
                    var featureBlockBinary = new ItmFeatureBlockBinary();
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

            var header = new ItmHeaderBinary();

            header.Flags = itmFile.Flags.CriticalItem ? header.Flags | Common.Bit0 : header.Flags;
            header.Flags = itmFile.Flags.TwoHanded ? header.Flags | Common.Bit1 : header.Flags;
            header.Flags = itmFile.Flags.Movable ? header.Flags | Common.Bit2 : header.Flags;
            header.Flags = itmFile.Flags.Displayable ? header.Flags | Common.Bit3 : header.Flags;
            header.Flags = itmFile.Flags.Cursed ? header.Flags | Common.Bit4 : header.Flags;
            header.Flags = itmFile.Flags.Unknown5 ? header.Flags | Common.Bit5 : header.Flags;
            header.Flags = itmFile.Flags.Magical ? header.Flags | Common.Bit6 : header.Flags;
            header.Flags = itmFile.Flags.Bow ? header.Flags | Common.Bit7 : header.Flags;
            header.Flags = itmFile.Flags.Silver ? header.Flags | Common.Bit8 : header.Flags;
            header.Flags = itmFile.Flags.ColdIron ? header.Flags | Common.Bit9 : header.Flags;
            header.Flags = itmFile.Flags.Stolen ? header.Flags | Common.Bit10 : header.Flags;
            header.Flags = itmFile.Flags.Conversable ? header.Flags | Common.Bit11 : header.Flags;
            header.Flags = itmFile.Flags.Unknown12 ? header.Flags | Common.Bit12 : header.Flags;
            header.Flags = itmFile.Flags.Unknown13 ? header.Flags | Common.Bit13 : header.Flags;
            header.Flags = itmFile.Flags.Unknown14 ? header.Flags | Common.Bit14 : header.Flags;
            header.Flags = itmFile.Flags.Unknown15 ? header.Flags | Common.Bit15 : header.Flags;
            header.Flags = itmFile.Flags.Unknown16 ? header.Flags | Common.Bit16 : header.Flags;
            header.Flags = itmFile.Flags.Unknown17 ? header.Flags | Common.Bit17 : header.Flags;
            header.Flags = itmFile.Flags.Unknown18 ? header.Flags | Common.Bit18 : header.Flags;
            header.Flags = itmFile.Flags.Unknown19 ? header.Flags | Common.Bit19 : header.Flags;
            header.Flags = itmFile.Flags.Unknown20 ? header.Flags | Common.Bit20 : header.Flags;
            header.Flags = itmFile.Flags.Unknown21 ? header.Flags | Common.Bit21 : header.Flags;
            header.Flags = itmFile.Flags.Unknown22 ? header.Flags | Common.Bit22 : header.Flags;
            header.Flags = itmFile.Flags.Unknown23 ? header.Flags | Common.Bit23 : header.Flags;
            header.Flags = itmFile.Flags.Unknown24 ? header.Flags | Common.Bit24 : header.Flags;
            header.Flags = itmFile.Flags.Unknown25 ? header.Flags | Common.Bit25 : header.Flags;
            header.Flags = itmFile.Flags.Unknown26 ? header.Flags | Common.Bit26 : header.Flags;
            header.Flags = itmFile.Flags.Unknown27 ? header.Flags | Common.Bit27 : header.Flags;
            header.Flags = itmFile.Flags.Unknown28 ? header.Flags | Common.Bit28 : header.Flags;
            header.Flags = itmFile.Flags.Unknown29 ? header.Flags | Common.Bit29 : header.Flags;
            header.Flags = itmFile.Flags.Unknown30 ? header.Flags | Common.Bit30 : header.Flags;
            header.Flags = itmFile.Flags.Unknown31 ? header.Flags | Common.Bit31 : header.Flags;

            header.ftype = new array4() { character1 = 'I', character2 = 'T', character3 = 'M', character4 = ' ' };
            header.fversion = new array4() { character1 = 'V', character2 = '1', character3 = ' ', character4 = ' ' };
            header.Animation = itmFile.Animation;
            header.DescriptionIcon = itmFile.DescriptionIcon;
            header.Enchantment = itmFile.Enchantment;

            header.ExtendedHeaderCount = Convert.ToInt16(itmExtendedHeaders.Count);
            header.ExtendedHeaderOffset = HeaderSize;
            header.FeatureBlockEquippingCount = Convert.ToInt16(itmFile.itmFeatureBlocks.Count);
            header.FeatureBlockEquippingIndex = 0;
            header.FeatureBlockOffset = HeaderSize + (ExtendedHeaderSize * itmExtendedHeaders.Count);

            header.GroundIcon = itmFile.GroundIcon;
            header.IdentifiedDescription = Common.WriteString(itmFile.IdentifiedDescription, TlkFile);
            header.IdentifiedName = Common.WriteString(itmFile.IdentifiedName, TlkFile);
            header.InventoryIcon = itmFile.InventoryIcon;
            header.ItemType = Convert.ToInt16(itmFile.ItemType);
            header.KitUsability1 = (byte)(itmFile.KitUsability1.ClericOfTalos ? header.KitUsability1 | Common.Bit0 : header.KitUsability1);
            header.KitUsability1 = (byte)(itmFile.KitUsability1.ClericOfHelm ? header.KitUsability1 | Common.Bit1 : header.KitUsability1);
            header.KitUsability1 = (byte)(itmFile.KitUsability1.ClericOfLathander ? header.KitUsability1 | Common.Bit2 : header.KitUsability1);
            header.KitUsability1 = (byte)(itmFile.KitUsability1.TotemicDruid ? header.KitUsability1 | Common.Bit3 : header.KitUsability1);
            header.KitUsability1 = (byte)(itmFile.KitUsability1.ShapeshifterDruid ? header.KitUsability1 | Common.Bit4 : header.KitUsability1);
            header.KitUsability1 = (byte)(itmFile.KitUsability1.AvengerDruid ? header.KitUsability1 | Common.Bit5 : header.KitUsability1);
            header.KitUsability1 = (byte)(itmFile.KitUsability1.Barbarian ? header.KitUsability1 | Common.Bit6 : header.KitUsability1);
            header.KitUsability1 = (byte)(itmFile.KitUsability1.Wildmage ? header.KitUsability1 | Common.Bit7 : header.KitUsability1);
            header.KitUsability2 = (byte)(itmFile.KitUsability2.StalkerRanger ? header.KitUsability2 | Common.Bit0 : header.KitUsability2);
            header.KitUsability2 = (byte)(itmFile.KitUsability2.BeastermasterRanger ? header.KitUsability2 | Common.Bit1 : header.KitUsability2);
            header.KitUsability2 = (byte)(itmFile.KitUsability2.AssassinThief ? header.KitUsability2 | Common.Bit2 : header.KitUsability2);
            header.KitUsability2 = (byte)(itmFile.KitUsability2.BountyHunterThief ? header.KitUsability2 | Common.Bit3 : header.KitUsability2);
            header.KitUsability2 = (byte)(itmFile.KitUsability2.SwashbucklerThief ? header.KitUsability2 | Common.Bit4 : header.KitUsability2);
            header.KitUsability2 = (byte)(itmFile.KitUsability2.BladeBard ? header.KitUsability2 | Common.Bit5 : header.KitUsability2);
            header.KitUsability2 = (byte)(itmFile.KitUsability2.JesterBard ? header.KitUsability2 | Common.Bit6 : header.KitUsability2);
            header.KitUsability2 = (byte)(itmFile.KitUsability2.SkaldBard ? header.KitUsability2 | Common.Bit7 : header.KitUsability2);
            header.KitUsability3 = (byte)(itmFile.KitUsability3.Diviner ? header.KitUsability3 | Common.Bit0 : header.KitUsability3);
            header.KitUsability3 = (byte)(itmFile.KitUsability3.Enchanter ? header.KitUsability3 | Common.Bit1 : header.KitUsability3);
            header.KitUsability3 = (byte)(itmFile.KitUsability3.Illusionist ? header.KitUsability3 | Common.Bit2 : header.KitUsability3);
            header.KitUsability3 = (byte)(itmFile.KitUsability3.Invoker ? header.KitUsability3 | Common.Bit3 : header.KitUsability3);
            header.KitUsability3 = (byte)(itmFile.KitUsability3.Necromancer ? header.KitUsability3 | Common.Bit4 : header.KitUsability3);
            header.KitUsability3 = (byte)(itmFile.KitUsability3.Transmuter ? header.KitUsability3 | Common.Bit5 : header.KitUsability3);
            header.KitUsability3 = (byte)(itmFile.KitUsability3.All ? header.KitUsability3 | Common.Bit6 : header.KitUsability3);
            header.KitUsability3 = (byte)(itmFile.KitUsability3.Ferlain ? header.KitUsability3 | Common.Bit7 : header.KitUsability3);
            header.KitUsability4 = (byte)(itmFile.KitUsability4.BeserkerFighter ? header.KitUsability4 | Common.Bit0 : header.KitUsability4);
            header.KitUsability4 = (byte)(itmFile.KitUsability4.WizardslayerFighter ? header.KitUsability4 | Common.Bit1 : header.KitUsability4);
            header.KitUsability4 = (byte)(itmFile.KitUsability4.KensaiFighter ? header.KitUsability4 | Common.Bit2 : header.KitUsability4);
            header.KitUsability4 = (byte)(itmFile.KitUsability4.CavalierPaladin ? header.KitUsability4 | Common.Bit3 : header.KitUsability4);
            header.KitUsability4 = (byte)(itmFile.KitUsability4.InquisiterPaladin ? header.KitUsability4 | Common.Bit4 : header.KitUsability4);
            header.KitUsability4 = (byte)(itmFile.KitUsability4.UndeadHunterPaladin ? header.KitUsability4 | Common.Bit5 : header.KitUsability4);
            header.KitUsability4 = (byte)(itmFile.KitUsability4.Abjurer ? header.KitUsability4 | Common.Bit6 : header.KitUsability4);
            header.KitUsability4 = (byte)(itmFile.KitUsability4.Conjurer ? header.KitUsability4 | Common.Bit7 : header.KitUsability4);
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
            header.ReplacementItem = itmFile.ReplacementItem;
            header.StackAmount = itmFile.StackAmount;
            header.UnidentifiedDescription = Common.WriteString(itmFile.UnidentifiedDescription, TlkFile);
            header.UnidentifiedName = Common.WriteString(itmFile.UnidentifiedName, TlkFile);

            header.Usability1 = (byte)(itmFile.Usability1.Chaotic_ ? header.Usability1 | Common.Bit0 : header.Usability1);
            header.Usability1 = (byte)(itmFile.Usability1._Evil ? header.Usability1 | Common.Bit1 : header.Usability1);
            header.Usability1 = (byte)(itmFile.Usability1._Good ? header.Usability1 | Common.Bit2 : header.Usability1);
            header.Usability1 = (byte)(itmFile.Usability1._Neutral ? header.Usability1 | Common.Bit3 : header.Usability1);
            header.Usability1 = (byte)(itmFile.Usability1.Lawful_ ? header.Usability1 | Common.Bit4 : header.Usability1);
            header.Usability1 = (byte)(itmFile.Usability1.Neutral_ ? header.Usability1 | Common.Bit5 : header.Usability1);
            header.Usability1 = (byte)(itmFile.Usability1.Bard ? header.Usability1 | Common.Bit6 : header.Usability1);
            header.Usability1 = (byte)(itmFile.Usability1.Cleric ? header.Usability1 | Common.Bit7 : header.Usability1);
            header.Usability2 = (byte)(itmFile.Usability2.ClericMage ? header.Usability2 | Common.Bit0 : header.Usability2);
            header.Usability2 = (byte)(itmFile.Usability2.ClericThief ? header.Usability2 | Common.Bit1 : header.Usability2);
            header.Usability2 = (byte)(itmFile.Usability2.ClericRanger ? header.Usability2 | Common.Bit2 : header.Usability2);
            header.Usability2 = (byte)(itmFile.Usability2.Fighter ? header.Usability2 | Common.Bit3 : header.Usability2);
            header.Usability2 = (byte)(itmFile.Usability2.FighterDruid ? header.Usability2 | Common.Bit4 : header.Usability2);
            header.Usability2 = (byte)(itmFile.Usability2.FighterMage ? header.Usability2 | Common.Bit5 : header.Usability2);
            header.Usability2 = (byte)(itmFile.Usability2.FighterCleric ? header.Usability2 | Common.Bit6 : header.Usability2);
            header.Usability2 = (byte)(itmFile.Usability2.FighterMageCleric ? header.Usability2 | Common.Bit7 : header.Usability2);
            header.Usability3 = (byte)(itmFile.Usability3.FighterMageThief ? header.Usability3 | Common.Bit0 : header.Usability3);
            header.Usability3 = (byte)(itmFile.Usability3.FighterThief ? header.Usability3 | Common.Bit1 : header.Usability3);
            header.Usability3 = (byte)(itmFile.Usability3.Mage ? header.Usability3 | Common.Bit2 : header.Usability3);
            header.Usability3 = (byte)(itmFile.Usability3.MageThief ? header.Usability3 | Common.Bit3 : header.Usability3);
            header.Usability3 = (byte)(itmFile.Usability3.Paladin ? header.Usability3 | Common.Bit4 : header.Usability3);
            header.Usability3 = (byte)(itmFile.Usability3.Ranger ? header.Usability3 | Common.Bit5 : header.Usability3);
            header.Usability3 = (byte)(itmFile.Usability3.Thief ? header.Usability3 | Common.Bit6 : header.Usability3);
            header.Usability3 = (byte)(itmFile.Usability3.Elf ? header.Usability3 | Common.Bit7 : header.Usability3);
            header.Usability4 = (byte)(itmFile.Usability4.Dwarf ? header.Usability4 | Common.Bit0 : header.Usability4);
            header.Usability4 = (byte)(itmFile.Usability4.HalfElf ? header.Usability4 | Common.Bit1 : header.Usability4);
            header.Usability4 = (byte)(itmFile.Usability4.Halfling ? header.Usability4 | Common.Bit2 : header.Usability4);
            header.Usability4 = (byte)(itmFile.Usability4.Human ? header.Usability4 | Common.Bit3 : header.Usability4);
            header.Usability4 = (byte)(itmFile.Usability4.Gnome ? header.Usability4 | Common.Bit4 : header.Usability4);
            header.Usability4 = (byte)(itmFile.Usability4.Monk ? header.Usability4 | Common.Bit5 : header.Usability4);
            header.Usability4 = (byte)(itmFile.Usability4.Druid ? header.Usability4 | Common.Bit6 : header.Usability4);
            header.Usability4 = (byte)(itmFile.Usability4.HalfOrc ? header.Usability4 | Common.Bit7 : header.Usability4);
            header.Weight = itmFile.Weight;

            using var s = new MemoryStream();
            using var bw = new BinaryWriter(s);
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

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            bw.BaseStream.Position = 0;
            bw.BaseStream.CopyTo(fs);
            fs.Flush(flushToDisk: true);
            return true;
        }
    }
}