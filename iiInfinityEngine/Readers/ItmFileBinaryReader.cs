using System.Collections.Generic;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers
{
    public class ItmFileBinaryReader : IItmFileReader
    {
        public TlkFile TlkFile { get; set; }

        public ItmFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public ItmFile Read(Stream s)
        {
            using var br = new BinaryReader(s);
            var itmFile = ParseFile(br);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            itmFile.OriginalFile = ParseFile(br);
            return itmFile;
        }

        private ItmFile ParseFile(BinaryReader br)
        {
            var header = (ItmHeaderBinary)Common.ReadStruct(br, typeof(ItmHeaderBinary));

            if (header.ftype.ToString() != "ITM ")
                return new ItmFile();

            var itmExtendedHeaders = new List<ItmExtendedHeaderBinary>();
            var itmFeatureBlocks = new List<ItmFeatureBlockBinary>();

            br.BaseStream.Seek(header.ExtendedHeaderOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.ExtendedHeaderCount; i++)
            {
                var itmExtendedHeader = (ItmExtendedHeaderBinary)Common.ReadStruct(br, typeof(ItmExtendedHeaderBinary));
                itmExtendedHeaders.Add(itmExtendedHeader);
            }

            br.BaseStream.Seek(header.FeatureBlockOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.FeatureBlockEquippingCount; i++)
            {
                var itmFeatureBlock = (ItmFeatureBlockBinary)Common.ReadStruct(br, typeof(ItmFeatureBlockBinary));
                itmFeatureBlocks.Add(itmFeatureBlock);
            }

            var itmFile = new ItmFile();
            itmFile.UnidentifiedName = Common.ReadString(header.UnidentifiedName, TlkFile);
            itmFile.IdentifiedName = Common.ReadString(header.IdentifiedName, TlkFile);
            itmFile.ReplacementItem = header.ReplacementItem;
            itmFile.Flags.CriticalItem = (header.Flags & Common.Bit0) != 0;
            itmFile.Flags.TwoHanded = (header.Flags & Common.Bit1) != 0;
            itmFile.Flags.Movable = (header.Flags & Common.Bit2) != 0;
            itmFile.Flags.Displayable = (header.Flags & Common.Bit3) != 0;
            itmFile.Flags.Cursed = (header.Flags & Common.Bit4) != 0;
            itmFile.Flags.Bit5 = (header.Flags & Common.Bit5) != 0;
            itmFile.Flags.Magical = (header.Flags & Common.Bit6) != 0;
            itmFile.Flags.Bow = (header.Flags & Common.Bit7) != 0;
            itmFile.Flags.Silver = (header.Flags & Common.Bit8) != 0;
            itmFile.Flags.ColdIron = (header.Flags & Common.Bit9) != 0;
            itmFile.Flags.Stolen = (header.Flags & Common.Bit10) != 0;
            itmFile.Flags.Conversable = (header.Flags & Common.Bit11) != 0;
            itmFile.Flags.Bit12 = (header.Flags & Common.Bit12) != 0;
            itmFile.Flags.Bit13 = (header.Flags & Common.Bit13) != 0;
            itmFile.Flags.Bit14 = (header.Flags & Common.Bit14) != 0;
            itmFile.Flags.Bit15 = (header.Flags & Common.Bit15) != 0;
            itmFile.Flags.Bit16 = (header.Flags & Common.Bit16) != 0;
            itmFile.Flags.Bit17 = (header.Flags & Common.Bit17) != 0;
            itmFile.Flags.Bit18 = (header.Flags & Common.Bit18) != 0;
            itmFile.Flags.Bit19 = (header.Flags & Common.Bit19) != 0;
            itmFile.Flags.Bit20 = (header.Flags & Common.Bit20) != 0;
            itmFile.Flags.Bit21 = (header.Flags & Common.Bit21) != 0;
            itmFile.Flags.Bit22 = (header.Flags & Common.Bit22) != 0;
            itmFile.Flags.Bit23 = (header.Flags & Common.Bit23) != 0;
            itmFile.Flags.Bit24 = (header.Flags & Common.Bit24) != 0;
            itmFile.Flags.Bit25 = (header.Flags & Common.Bit25) != 0;
            itmFile.Flags.Bit26 = (header.Flags & Common.Bit26) != 0;
            itmFile.Flags.Bit27 = (header.Flags & Common.Bit27) != 0;
            itmFile.Flags.Bit28 = (header.Flags & Common.Bit28) != 0;
            itmFile.Flags.Bit29 = (header.Flags & Common.Bit29) != 0;
            itmFile.Flags.Bit30 = (header.Flags & Common.Bit30) != 0;
            itmFile.Flags.Bit31 = (header.Flags & Common.Bit31) != 0;
            itmFile.ItemType = (ItemType)header.ItemType;
            itmFile.Usability1.Chaotic_ = (header.Usability1 & Common.Bit0) != 0;
            itmFile.Usability1._Evil = (header.Usability1 & Common.Bit1) != 0;
            itmFile.Usability1._Good = (header.Usability1 & Common.Bit2) != 0;
            itmFile.Usability1._Neutral = (header.Usability1 & Common.Bit3) != 0;
            itmFile.Usability1.Lawful_ = (header.Usability1 & Common.Bit4) != 0;
            itmFile.Usability1.Neutral_ = (header.Usability1 & Common.Bit5) != 0;
            itmFile.Usability1.Bard = (header.Usability1 & Common.Bit6) != 0;
            itmFile.Usability1.Cleric = (header.Usability1 & Common.Bit7) != 0;
            itmFile.Usability2.ClericMage = (header.Usability2 & Common.Bit0) != 0;
            itmFile.Usability2.ClericThief = (header.Usability2 & Common.Bit1) != 0;
            itmFile.Usability2.ClericRanger = (header.Usability2 & Common.Bit2) != 0;
            itmFile.Usability2.Fighter = (header.Usability2 & Common.Bit3) != 0;
            itmFile.Usability2.FighterDruid = (header.Usability2 & Common.Bit4) != 0;
            itmFile.Usability2.FighterMage = (header.Usability2 & Common.Bit5) != 0;
            itmFile.Usability2.FighterCleric = (header.Usability2 & Common.Bit6) != 0;
            itmFile.Usability2.FighterMageCleric = (header.Usability2 & Common.Bit7) != 0;
            itmFile.Usability3.FighterMageThief = (header.Usability3 & Common.Bit0) != 0;
            itmFile.Usability3.FighterThief = (header.Usability3 & Common.Bit1) != 0;
            itmFile.Usability3.Mage = (header.Usability3 & Common.Bit2) != 0;
            itmFile.Usability3.MageThief = (header.Usability3 & Common.Bit3) != 0;
            itmFile.Usability3.Paladin = (header.Usability3 & Common.Bit4) != 0;
            itmFile.Usability3.Ranger = (header.Usability3 & Common.Bit5) != 0;
            itmFile.Usability3.Thief = (header.Usability3 & Common.Bit6) != 0;
            itmFile.Usability3.Elf = (header.Usability3 & Common.Bit7) != 0;
            itmFile.Usability4.Dwarf = (header.Usability4 & Common.Bit0) != 0;
            itmFile.Usability4.HalfElf = (header.Usability4 & Common.Bit1) != 0;
            itmFile.Usability4.Halfling = (header.Usability4 & Common.Bit2) != 0;
            itmFile.Usability4.Human = (header.Usability4 & Common.Bit3) != 0;
            itmFile.Usability4.Gnome = (header.Usability4 & Common.Bit4) != 0;
            itmFile.Usability4.Monk = (header.Usability4 & Common.Bit5) != 0;
            itmFile.Usability4.Druid = (header.Usability4 & Common.Bit6) != 0;
            itmFile.Usability4.HalfOrc = (header.Usability4 & Common.Bit7) != 0;
            itmFile.Animation = header.Animation;
            itmFile.MinimumLevel = header.MinimumLevel;
            itmFile.MinimumStrength = header.MinimumStrength;
            itmFile.MinimumStrengthBonus = header.MinimumStrengthBonus;
            itmFile.KitUsability1.ClericOfTalos = (header.KitUsability1 & Common.Bit0) != 0;
            itmFile.KitUsability1.ClericOfHelm = (header.KitUsability1 & Common.Bit1) != 0;
            itmFile.KitUsability1.ClericOfLathander = (header.KitUsability1 & Common.Bit2) != 0;
            itmFile.KitUsability1.TotemicDruid = (header.KitUsability1 & Common.Bit3) != 0;
            itmFile.KitUsability1.ShapeshifterDruid = (header.KitUsability1 & Common.Bit4) != 0;
            itmFile.KitUsability1.AvengerDruid = (header.KitUsability1 & Common.Bit5) != 0;
            itmFile.KitUsability1.Barbarian = (header.KitUsability1 & Common.Bit6) != 0;
            itmFile.KitUsability1.Wildmage = (header.KitUsability1 & Common.Bit7) != 0;
            itmFile.KitUsability2.StalkerRanger = (header.KitUsability2 & Common.Bit0) != 0;
            itmFile.KitUsability2.BeastermasterRanger = (header.KitUsability2 & Common.Bit1) != 0;
            itmFile.KitUsability2.AssassinThief = (header.KitUsability2 & Common.Bit2) != 0;
            itmFile.KitUsability2.BountyHunterThief = (header.KitUsability2 & Common.Bit3) != 0;
            itmFile.KitUsability2.SwashbucklerThief = (header.KitUsability2 & Common.Bit4) != 0;
            itmFile.KitUsability2.BladeBard = (header.KitUsability2 & Common.Bit5) != 0;
            itmFile.KitUsability2.JesterBard = (header.KitUsability2 & Common.Bit6) != 0;
            itmFile.KitUsability2.SkaldBard = (header.KitUsability2 & Common.Bit7) != 0;
            itmFile.KitUsability3.Diviner = (header.KitUsability3 & Common.Bit0) != 0;
            itmFile.KitUsability3.Enchanter = (header.KitUsability4 & Common.Bit1) != 0;
            itmFile.KitUsability3.Illusionist = (header.KitUsability3 & Common.Bit2) != 0;
            itmFile.KitUsability3.Invoker = (header.KitUsability3 & Common.Bit3) != 0;
            itmFile.KitUsability3.Necromancer = (header.KitUsability3 & Common.Bit4) != 0;
            itmFile.KitUsability3.Transmuter = (header.KitUsability3 & Common.Bit5) != 0;
            itmFile.KitUsability3.All = (header.KitUsability3 & Common.Bit6) != 0;
            itmFile.KitUsability3.Ferlain = (header.KitUsability3 & Common.Bit7) != 0;
            itmFile.KitUsability4.BeserkerFighter = (header.KitUsability4 & Common.Bit0) != 0;
            itmFile.KitUsability4.WizardslayerFighter = (header.KitUsability4 & Common.Bit1) != 0;
            itmFile.KitUsability4.KensaiFighter = (header.KitUsability4 & Common.Bit2) != 0;
            itmFile.KitUsability4.CavalierPaladin = (header.KitUsability4 & Common.Bit3) != 0;
            itmFile.KitUsability4.InquisiterPaladin = (header.KitUsability4 & Common.Bit4) != 0;
            itmFile.KitUsability4.UndeadHunterPaladin = (header.KitUsability4 & Common.Bit5) != 0;
            itmFile.KitUsability4.Abjurer = (header.KitUsability4 & Common.Bit6) != 0;
            itmFile.KitUsability4.Conjurer = (header.KitUsability4 & Common.Bit7) != 0;
            itmFile.MinimumIntelligence = header.MinimumIntelligence;
            itmFile.MinimumDexterity = header.MinimumDexterity;
            itmFile.MinimumWisdom = header.MinimumWisdom;
            itmFile.MinimumConstitution = header.MinimumConstitution;
            itmFile.Proficiency = (Proficiency)header.Proficiency;
            itmFile.MinimumCharisma = header.MinimumCharisma;
            itmFile.Price = header.Price;
            itmFile.StackAmount = header.StackAmount;
            itmFile.InventoryIcon = header.InventoryIcon;
            itmFile.LoreToIdentify = header.LoreToIdentify;
            itmFile.GroundIcon = header.GroundIcon;
            itmFile.Weight = header.Weight;
            itmFile.UnidentifiedDescription = Common.ReadString(header.UnidentifiedDescription, TlkFile);
            itmFile.IdentifiedDescription = Common.ReadString(header.IdentifiedDescription, TlkFile);
            itmFile.DescriptionIcon = header.DescriptionIcon;
            itmFile.Enchantment = header.Enchantment;
            itmFile.ExtendedHeaderOffset = header.ExtendedHeaderOffset;
            itmFile.ExtendedHeaderCount = header.ExtendedHeaderCount;
            itmFile.FeatureBlockOffset = header.FeatureBlockOffset;
            itmFile.FeatureBlockEquippingIndex = header.FeatureBlockEquippingIndex;
            itmFile.FeatureBlockEquippingCount = header.FeatureBlockEquippingCount;

            foreach (var extendedHeader in itmExtendedHeaders)
            {
                var extendedHeader2 = new ItmExtendedHeader2();
                extendedHeader2.AlternaticeDamageBonus = extendedHeader.AlternaticeDamageBonus;
                extendedHeader2.AlternaticeDiceSides = extendedHeader.AlternaticeDiceSides;
                extendedHeader2.AlternaticeDiceThrown = extendedHeader.AlternaticeDiceThrown;
                extendedHeader2.AttackType = (AttackType)extendedHeader.AttackType;
                extendedHeader2.ChargeDepletionBehaviour = (ChargeDepletionBehaviour)extendedHeader.ChargeDepletionBehaviour;
                extendedHeader2.Charges = extendedHeader.Charges;
                extendedHeader2.DamageBonus = extendedHeader.DamageBonus;
                extendedHeader2.DamageType = (DamageType)extendedHeader.DamageType;
                extendedHeader2.DiceSides = extendedHeader.DiceSides;
                extendedHeader2.DiceThrown = extendedHeader.DiceThrown;
                extendedHeader2.FeatureBlockCount = extendedHeader.FeatureBlockCount;
                extendedHeader2.FeatureBlockOffset = extendedHeader.FeatureBlockOffset;
                extendedHeader2.Flags.AddStengthBonus = (extendedHeader.Flags & Common.Bit0) != 0;
                extendedHeader2.Flags.BreaksSantuaryInvisibility = (extendedHeader.Flags & Common.Bit1) != 0;
                extendedHeader2.Flags.DamageStrengthBonus = (extendedHeader.Flags & Common.Bit2) != 0;
                extendedHeader2.Flags.Thac0StrengthBonus = (extendedHeader.Flags & Common.Bit3) != 0;
                extendedHeader2.Flags.Bit4 = (extendedHeader.Flags & Common.Bit4) != 0;
                extendedHeader2.Flags.Bit5 = (extendedHeader.Flags & Common.Bit5) != 0;
                extendedHeader2.Flags.Bit6 = (extendedHeader.Flags & Common.Bit6) != 0;
                extendedHeader2.Flags.Bit7 = (extendedHeader.Flags & Common.Bit7) != 0;
                extendedHeader2.Flags.Bit8 = (extendedHeader.Flags & Common.Bit8) != 0;
                extendedHeader2.Flags.BreaksSantuaryInvisibility = (extendedHeader.Flags & Common.Bit9) != 0;
                extendedHeader2.Flags.Hostile = (extendedHeader.Flags & Common.Bit10) != 0;
                extendedHeader2.Flags.RechargeAfterResting = (extendedHeader.Flags & Common.Bit11) != 0;
                extendedHeader2.Flags.Bit12 = (extendedHeader.Flags & Common.Bit12) != 0;
                extendedHeader2.Flags.Bit13 = (extendedHeader.Flags & Common.Bit13) != 0;
                extendedHeader2.Flags.Bit14 = (extendedHeader.Flags & Common.Bit14) != 0;
                extendedHeader2.Flags.Bit15 = (extendedHeader.Flags & Common.Bit15) != 0;
                extendedHeader2.Flags.Bit16 = (extendedHeader.Flags & Common.Bit16) != 0;
                extendedHeader2.Flags.Bit17 = (extendedHeader.Flags & Common.Bit17) != 0;
                extendedHeader2.Flags.Bit18 = (extendedHeader.Flags & Common.Bit18) != 0;
                extendedHeader2.Flags.Bit19 = (extendedHeader.Flags & Common.Bit19) != 0;
                extendedHeader2.Flags.Bit20 = (extendedHeader.Flags & Common.Bit20) != 0;
                extendedHeader2.Flags.Bit21 = (extendedHeader.Flags & Common.Bit21) != 0;
                extendedHeader2.Flags.Bit22 = (extendedHeader.Flags & Common.Bit22) != 0;
                extendedHeader2.Flags.Bit23 = (extendedHeader.Flags & Common.Bit23) != 0;
                extendedHeader2.Flags.Bit24 = (extendedHeader.Flags & Common.Bit24) != 0;
                extendedHeader2.Flags.Bit25 = (extendedHeader.Flags & Common.Bit25) != 0;
                extendedHeader2.Flags.CannotTargetInvisible = (extendedHeader.Flags & Common.Bit26) != 0;
                extendedHeader2.Flags.Bit27 = (extendedHeader.Flags & Common.Bit27) != 0;
                extendedHeader2.Flags.Bit28 = (extendedHeader.Flags & Common.Bit28) != 0;
                extendedHeader2.Flags.Bit29 = (extendedHeader.Flags & Common.Bit29) != 0;
                extendedHeader2.Flags.Bit30 = (extendedHeader.Flags & Common.Bit30) != 0;
                extendedHeader2.Flags.Bit31 = (extendedHeader.Flags & Common.Bit31) != 0;
                extendedHeader2.IdentificationRequirement.IdRequired = (extendedHeader.IdentificationRequirement & Common.Bit0) != 0;
                extendedHeader2.IdentificationRequirement.NonIdRequired = (extendedHeader.IdentificationRequirement & Common.Bit1) != 0;
                extendedHeader2.IdentificationRequirement.Bit2 = (extendedHeader.IdentificationRequirement & Common.Bit2) != 0;
                extendedHeader2.IdentificationRequirement.Bit3 = (extendedHeader.IdentificationRequirement & Common.Bit3) != 0;
                extendedHeader2.IdentificationRequirement.Bit4 = (extendedHeader.IdentificationRequirement & Common.Bit4) != 0;
                extendedHeader2.IdentificationRequirement.Bit5 = (extendedHeader.IdentificationRequirement & Common.Bit5) != 0;
                extendedHeader2.IdentificationRequirement.Bit6 = (extendedHeader.IdentificationRequirement & Common.Bit6) != 0;
                extendedHeader2.IdentificationRequirement.Bit7 = (extendedHeader.IdentificationRequirement & Common.Bit7) != 0;
                extendedHeader2.IsBowArrow = (ProjectileQualifier)extendedHeader.IsBowArrow;
                extendedHeader2.IsCrossbowBolt = (ProjectileQualifier)extendedHeader.IsCrossbowBolt;
                extendedHeader2.IsMiscProjectile = (ProjectileQualifier)extendedHeader.IsMiscProjectile;
                extendedHeader2.Location = (Location)extendedHeader.Location;
                extendedHeader2.MeleeAnimation1 = extendedHeader.MeleeAnimation1;
                extendedHeader2.MeleeAnimation2 = extendedHeader.MeleeAnimation2;
                extendedHeader2.MeleeAnimation3 = extendedHeader.MeleeAnimation3;
                extendedHeader2.PrimaryType = extendedHeader.PrimaryType;
                extendedHeader2.ProjectileAnimation = extendedHeader.ProjectileAnimation;
                extendedHeader2.LauncherType = (LauncherType)extendedHeader.LauncherType;
                extendedHeader2.Range = extendedHeader.Range;
                extendedHeader2.SecondaryType = extendedHeader.SecondaryType;
                extendedHeader2.Speed = extendedHeader.Speed;
                extendedHeader2.TargetCount = extendedHeader.TargetCount;
                extendedHeader2.TargetType = (TargetType)extendedHeader.TargetType;
                extendedHeader2.Thac0Bonus = extendedHeader.Thac0Bonus;
                extendedHeader2.UseIcon = extendedHeader.UseIcon;

                br.BaseStream.Seek(header.FeatureBlockOffset + (header.FeatureBlockEquippingCount * 48), SeekOrigin.Begin);
                for (int i = 0; i < extendedHeader.FeatureBlockCount; i++)
                {
                    var itmFeatureBlock = (ItmFeatureBlockBinary)Common.ReadStruct(br, typeof(ItmFeatureBlockBinary));

                    var itmFeatureBlock2 = new ItmFeatureBlock2();
                    itmFeatureBlock2.DiceSides = itmFeatureBlock.DiceSides;
                    itmFeatureBlock2.DiceThrown = itmFeatureBlock.DiceThrown;
                    itmFeatureBlock2.Duration = itmFeatureBlock.Duration;
                    itmFeatureBlock2.Opcode = itmFeatureBlock.Opcode;
                    itmFeatureBlock2.Parameter1 = itmFeatureBlock.Parameter1;
                    itmFeatureBlock2.Parameter2 = itmFeatureBlock.Parameter2;
                    itmFeatureBlock2.Power = itmFeatureBlock.Power;
                    itmFeatureBlock2.Probability1 = itmFeatureBlock.Probability1;
                    itmFeatureBlock2.Probability2 = itmFeatureBlock.Probability2;
                    itmFeatureBlock2.Resistance.DispellableAffectedByMagicResistance = (itmFeatureBlock.Resistance & Common.Bit0) != 0;
                    itmFeatureBlock2.Resistance.IgnoreMagicResistance = (itmFeatureBlock.Resistance & Common.Bit1) != 0;
                    itmFeatureBlock2.Resistance.Bit2 = (itmFeatureBlock.Resistance & Common.Bit2) != 0;
                    itmFeatureBlock2.Resistance.Bit3 = (itmFeatureBlock.Resistance & Common.Bit3) != 0;
                    itmFeatureBlock2.Resistance.Bit4 = (itmFeatureBlock.Resistance & Common.Bit4) != 0;
                    itmFeatureBlock2.Resistance.Bit5 = (itmFeatureBlock.Resistance & Common.Bit5) != 0;
                    itmFeatureBlock2.Resistance.Bit6 = (itmFeatureBlock.Resistance & Common.Bit6) != 0;
                    itmFeatureBlock2.Resistance.Bit7 = (itmFeatureBlock.Resistance & Common.Bit7) != 0;
                    itmFeatureBlock2.Resource = itmFeatureBlock.Resource;
                    itmFeatureBlock2.SavingThrowBonus = itmFeatureBlock.SavingThrowBonus;
                    itmFeatureBlock2.SavingThrowType.Spells = (itmFeatureBlock.SavingThrowType & Common.Bit0) != 0;
                    itmFeatureBlock2.SavingThrowType.Breath = (itmFeatureBlock.SavingThrowType & Common.Bit1) != 0;
                    itmFeatureBlock2.SavingThrowType.ParalyzePoisonDeath = (itmFeatureBlock.SavingThrowType & Common.Bit2) != 0;
                    itmFeatureBlock2.SavingThrowType.Wands = (itmFeatureBlock.SavingThrowType & Common.Bit3) != 0;
                    itmFeatureBlock2.SavingThrowType.PetrifyPolymorph = (itmFeatureBlock.SavingThrowType & Common.Bit4) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit5 = (itmFeatureBlock.SavingThrowType & Common.Bit5) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit6 = (itmFeatureBlock.SavingThrowType & Common.Bit6) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit7 = (itmFeatureBlock.SavingThrowType & Common.Bit7) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit8 = (itmFeatureBlock.SavingThrowType & Common.Bit8) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit9 = (itmFeatureBlock.SavingThrowType & Common.Bit9) != 0;
                    itmFeatureBlock2.SavingThrowType.IgnorePrimaryTarget = (itmFeatureBlock.SavingThrowType & Common.Bit10) != 0;
                    itmFeatureBlock2.SavingThrowType.IgnoreSecondaryTarget = (itmFeatureBlock.SavingThrowType & Common.Bit11) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit12 = (itmFeatureBlock.SavingThrowType & Common.Bit12) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit13 = (itmFeatureBlock.SavingThrowType & Common.Bit13) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit14 = (itmFeatureBlock.SavingThrowType & Common.Bit14) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit15 = (itmFeatureBlock.SavingThrowType & Common.Bit15) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit16 = (itmFeatureBlock.SavingThrowType & Common.Bit16) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit17 = (itmFeatureBlock.SavingThrowType & Common.Bit17) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit18 = (itmFeatureBlock.SavingThrowType & Common.Bit18) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit19 = (itmFeatureBlock.SavingThrowType & Common.Bit19) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit20 = (itmFeatureBlock.SavingThrowType & Common.Bit20) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit21 = (itmFeatureBlock.SavingThrowType & Common.Bit21) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit22 = (itmFeatureBlock.SavingThrowType & Common.Bit22) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit23 = (itmFeatureBlock.SavingThrowType & Common.Bit23) != 0;
                    itmFeatureBlock2.SavingThrowType.BypassMirrorImage = (itmFeatureBlock.SavingThrowType & Common.Bit24) != 0;
                    itmFeatureBlock2.SavingThrowType.IgnoreDifficulty = (itmFeatureBlock.SavingThrowType & Common.Bit25) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit26 = (itmFeatureBlock.SavingThrowType & Common.Bit26) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit27 = (itmFeatureBlock.SavingThrowType & Common.Bit27) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit28 = (itmFeatureBlock.SavingThrowType & Common.Bit28) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit29 = (itmFeatureBlock.SavingThrowType & Common.Bit29) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit30 = (itmFeatureBlock.SavingThrowType & Common.Bit30) != 0;
                    itmFeatureBlock2.SavingThrowType.Bit31 = (itmFeatureBlock.SavingThrowType & Common.Bit31) != 0;
                    itmFeatureBlock2.TargetType = (OpcodeTargetType)itmFeatureBlock.TargetType;
                    itmFeatureBlock2.TimingMode = (OpcodeTimingMode)itmFeatureBlock.TimingMode;
                    itmFeatureBlock2.Special = itmFeatureBlock.Special;

                    extendedHeader2.itmFeatureBlocks.Add(itmFeatureBlock2);
                }

                itmFile.itmExtendedHeader.Add(extendedHeader2);
            }

            foreach (var featureBlock in itmFeatureBlocks)
            {
                var itmFeatureBlock2 = new ItmFeatureBlock2();
                itmFeatureBlock2.DiceSides = featureBlock.DiceSides;
                itmFeatureBlock2.DiceThrown = featureBlock.DiceThrown;
                itmFeatureBlock2.Duration = featureBlock.Duration;
                itmFeatureBlock2.Opcode = featureBlock.Opcode;
                itmFeatureBlock2.Parameter1 = featureBlock.Parameter1;
                itmFeatureBlock2.Parameter2 = featureBlock.Parameter2;
                itmFeatureBlock2.Power = featureBlock.Power;
                itmFeatureBlock2.Probability1 = featureBlock.Probability1;
                itmFeatureBlock2.Probability2 = featureBlock.Probability2;
                itmFeatureBlock2.Resistance.DispellableAffectedByMagicResistance = (featureBlock.Resistance & Common.Bit0) != 0;
                itmFeatureBlock2.Resistance.IgnoreMagicResistance = (featureBlock.Resistance & Common.Bit1) != 0;
                itmFeatureBlock2.Resistance.Bit2 = (featureBlock.Resistance & Common.Bit2) != 0;
                itmFeatureBlock2.Resistance.Bit3 = (featureBlock.Resistance & Common.Bit3) != 0;
                itmFeatureBlock2.Resistance.Bit4 = (featureBlock.Resistance & Common.Bit4) != 0;
                itmFeatureBlock2.Resistance.Bit5 = (featureBlock.Resistance & Common.Bit5) != 0;
                itmFeatureBlock2.Resistance.Bit6 = (featureBlock.Resistance & Common.Bit6) != 0;
                itmFeatureBlock2.Resistance.Bit7 = (featureBlock.Resistance & Common.Bit7) != 0;
                itmFeatureBlock2.Resource = featureBlock.Resource;
                itmFeatureBlock2.SavingThrowBonus = featureBlock.SavingThrowBonus;
                itmFeatureBlock2.SavingThrowType.Spells = (featureBlock.SavingThrowType & Common.Bit0) != 0;
                itmFeatureBlock2.SavingThrowType.Breath = (featureBlock.SavingThrowType & Common.Bit1) != 0;
                itmFeatureBlock2.SavingThrowType.ParalyzePoisonDeath = (featureBlock.SavingThrowType & Common.Bit2) != 0;
                itmFeatureBlock2.SavingThrowType.Wands = (featureBlock.SavingThrowType & Common.Bit3) != 0;
                itmFeatureBlock2.SavingThrowType.PetrifyPolymorph = (featureBlock.SavingThrowType & Common.Bit4) != 0;
                itmFeatureBlock2.SavingThrowType.Bit5 = (featureBlock.SavingThrowType & Common.Bit5) != 0;
                itmFeatureBlock2.SavingThrowType.Bit6 = (featureBlock.SavingThrowType & Common.Bit6) != 0;
                itmFeatureBlock2.SavingThrowType.Bit7 = (featureBlock.SavingThrowType & Common.Bit7) != 0;
                itmFeatureBlock2.SavingThrowType.Bit8 = (featureBlock.SavingThrowType & Common.Bit8) != 0;
                itmFeatureBlock2.SavingThrowType.Bit9 = (featureBlock.SavingThrowType & Common.Bit9) != 0;
                itmFeatureBlock2.SavingThrowType.IgnorePrimaryTarget = (featureBlock.SavingThrowType & Common.Bit10) != 0;
                itmFeatureBlock2.SavingThrowType.IgnoreSecondaryTarget = (featureBlock.SavingThrowType & Common.Bit11) != 0;
                itmFeatureBlock2.SavingThrowType.Bit12 = (featureBlock.SavingThrowType & Common.Bit12) != 0;
                itmFeatureBlock2.SavingThrowType.Bit13 = (featureBlock.SavingThrowType & Common.Bit13) != 0;
                itmFeatureBlock2.SavingThrowType.Bit14 = (featureBlock.SavingThrowType & Common.Bit14) != 0;
                itmFeatureBlock2.SavingThrowType.Bit15 = (featureBlock.SavingThrowType & Common.Bit15) != 0;
                itmFeatureBlock2.SavingThrowType.Bit16 = (featureBlock.SavingThrowType & Common.Bit16) != 0;
                itmFeatureBlock2.SavingThrowType.Bit17 = (featureBlock.SavingThrowType & Common.Bit17) != 0;
                itmFeatureBlock2.SavingThrowType.Bit18 = (featureBlock.SavingThrowType & Common.Bit18) != 0;
                itmFeatureBlock2.SavingThrowType.Bit19 = (featureBlock.SavingThrowType & Common.Bit19) != 0;
                itmFeatureBlock2.SavingThrowType.Bit20 = (featureBlock.SavingThrowType & Common.Bit20) != 0;
                itmFeatureBlock2.SavingThrowType.Bit21 = (featureBlock.SavingThrowType & Common.Bit21) != 0;
                itmFeatureBlock2.SavingThrowType.Bit22 = (featureBlock.SavingThrowType & Common.Bit22) != 0;
                itmFeatureBlock2.SavingThrowType.Bit23 = (featureBlock.SavingThrowType & Common.Bit23) != 0;
                itmFeatureBlock2.SavingThrowType.BypassMirrorImage = (featureBlock.SavingThrowType & Common.Bit24) != 0;
                itmFeatureBlock2.SavingThrowType.IgnoreDifficulty = (featureBlock.SavingThrowType & Common.Bit25) != 0;
                itmFeatureBlock2.SavingThrowType.Bit26 = (featureBlock.SavingThrowType & Common.Bit26) != 0;
                itmFeatureBlock2.SavingThrowType.Bit27 = (featureBlock.SavingThrowType & Common.Bit27) != 0;
                itmFeatureBlock2.SavingThrowType.Bit28 = (featureBlock.SavingThrowType & Common.Bit28) != 0;
                itmFeatureBlock2.SavingThrowType.Bit29 = (featureBlock.SavingThrowType & Common.Bit29) != 0;
                itmFeatureBlock2.SavingThrowType.Bit30 = (featureBlock.SavingThrowType & Common.Bit30) != 0;
                itmFeatureBlock2.SavingThrowType.Bit31 = (featureBlock.SavingThrowType & Common.Bit31) != 0;
                itmFeatureBlock2.TargetType = (OpcodeTargetType)featureBlock.TargetType;
                itmFeatureBlock2.TimingMode = (OpcodeTimingMode)featureBlock.TimingMode;
                itmFeatureBlock2.Special = featureBlock.Special;

                itmFile.itmFeatureBlocks.Add(itmFeatureBlock2);
            }

            itmFile.Checksum = HashGenerator.GenerateKey(itmFile);
            return itmFile;
        }
    }
}