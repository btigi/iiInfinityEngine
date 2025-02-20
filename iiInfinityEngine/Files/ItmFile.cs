using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class ItmFile : IEFile
    {
        public List<ItmExtendedHeader2> itmExtendedHeader = [];
        public List<ItmFeatureBlock2> itmFeatureBlocks = [];

        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private readonly IEFileType fileType = IEFileType.Itm;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public IEString UnidentifiedName;
        public IEString IdentifiedName;
        public array8 ReplacementItem;
        public ItmFlags Flags;
        public ItemType ItemType;
        public Usability1 Usability1;
        public Usability2 Usability2;
        public Usability3 Usability3;
        public Usability4 Usability4;
        public array2 Animation;
        public Int16 MinimumLevel;
        public Int16 MinimumStrength;
        public byte MinimumStrengthBonus;
        public KitUsability1 KitUsability1;
        public byte MinimumIntelligence;
        public KitUsability2 KitUsability2;
        public byte MinimumDexterity;
        public KitUsability3 KitUsability3;
        public byte MinimumWisdom;
        public KitUsability4 KitUsability4;
        public byte MinimumConstitution;
        public byte Proficiency; //TODO:itm
        public Int16 MinimumCharisma;
        public Int32 Price;
        public Int16 StackAmount;
        public array8 InventoryIcon;
        public Int16 LoreToIdentify;
        public array8 GroundIcon;
        public Int32 Weight;
        public IEString UnidentifiedDescription;
        public IEString IdentifiedDescription;
        public array8 DescriptionIcon;
        public Int32 Enchantment;
        public Int32 ExtendedHeaderOffset;
        public Int16 ExtendedHeaderCount;
        public Int32 FeatureBlockOffset;
        public Int16 FeatureBlockEquippingIndex;
        public Int16 FeatureBlockEquippingCount;
    }

    [Serializable]
    public class ItmExtendedHeader2
    {
        public AttackType AttackType { get; set; }
        public byte IdentificationRequirement { get; set; } //TODO:itm
        public byte Location { get; set; } //TODO:itm
        public byte AlternaticeDiceSides { get; set; }
        public array8 UseIcon { get; set; }
        public char TargetType { get; set; } //TODO:itm
        public byte TargetCount { get; set; }
        public Int16 Range { get; set; }
        public LauncherType LauncherType { get; set; }
        public byte AlternaticeDiceThrown { get; set; }
        public byte Speed { get; set; }
        public byte AlternaticeDamageBonus { get; set; }
        public Int16 Thac0Bonus { get; set; }
        public byte DiceSides { get; set; }
        public byte PrimaryType { get; set; }
        public byte DiceThrown { get; set; }
        public byte SecondaryType { get; set; }
        public Int16 DamageBonus { get; set; }
        public Int16 DamageType { get; set; } //TODO:itm
        public Int16 FeatureBlockCount { get; set; }
        public Int16 FeatureBlockOffset { get; set; }
        public Int16 Charges { get; set; }
        public Int16 ChargeDepletionBehaviour { get; set; }//TODO;itm
        public Int32 Flags { get; set; }//TODO;itm
        public Int16 ProjectileAnimation { get; set; }
        public Int16 MeleeAnimation1 { get; set; }
        public Int16 MeleeAnimation2 { get; set; }
        public Int16 MeleeAnimation3 { get; set; }
        public Int16 IsBowArrow { get; set; }//TODO;itm
        public Int16 IsCrossbowBolt { get; set; }//TODO;itm
        public Int16 IsMiscProjectile { get; set; }//TODO;itm

        public List<ItmFeatureBlock2> itmFeatureBlocks = new List<ItmFeatureBlock2>();
    }

    [Serializable]
    public class ItmFeatureBlock2
    {
        public Int16 Opcode { get; set; }
        public byte TargetType { get; set; }//TODO;itm
        public byte Power { get; set; }
        public Int32 Parameter1 { get; set; }
        public Int32 Parameter2 { get; set; }
        public byte TimingMode { get; set; }//TODO;itm
        public byte Resistance { get; set; }//TODO;itm
        public Int32 Duration { get; set; }
        public byte Probability1 { get; set; }
        public byte Probability2 { get; set; }
        public array8 Resource { get; set; }
        public Int32 DiceThrown { get; set; }
        public Int32 DiceSides { get; set; }
        public Int32 SavingThrowType { get; set; }//TODO;itm
        public Int32 SavingThrowBonus { get; set; }
        public Int32 Unknown { get; set; }//TODO;itm
    }

    [Serializable]
    public class Usability1
    {
        public bool Chaotic_ { get; set; }
        public bool _Evil { get; set; }
        public bool _Good { get; set; }
        public bool _Neutral { get; set; }
        public bool Lawful_ { get; set; }
        public bool Neutral_ { get; set; }
        public bool Bard { get; set; }
        public bool Cleric { get; set; }
    }

    [Serializable]
    public class Usability2
    {
        public bool ClericMage { get; set; }
        public bool ClericThief { get; set; }
        public bool ClericRanger { get; set; }
        public bool Fighter { get; set; }
        public bool FighterDruid { get; set; }
        public bool FighterMage { get; set; }
        public bool FighterCleric { get; set; }
        public bool FighterMageCleric { get; set; }
    }

    [Serializable]
    public class Usability3
    {
        public bool FighterMageThief { get; set; }
        public bool FighterThief { get; set; }
        public bool Mage { get; set; }
        public bool MageThief { get; set; }
        public bool Paladin { get; set; }
        public bool Ranger { get; set; }
        public bool Thief { get; set; }
        public bool Elf { get; set; }
    }

    [Serializable]
    public class Usability4
    {
        public bool Dwarf { get; set; }
        public bool HalfElf { get; set; }
        public bool Halfling { get; set; }
        public bool Human { get; set; }
        public bool Gnome { get; set; }
        public bool Monk { get; set; }
        public bool Druid { get; set; }
        public bool HalfOrc { get; set; }
    }

    [Serializable]
    public class KitUsability1
    {
        public bool ClericOfTalos { get; set; }
        public bool ClericOfHelm { get; set; }
        public bool ClericOfLathander { get; set; }
        public bool TotemicDruid { get; set; }
        public bool ShapeshifterDruid { get; set; }
        public bool AvengerDruid { get; set; }
        public bool Barbarian { get; set; }
        public bool Wildmage { get; set; }
    }

    [Serializable]
    public class KitUsability2
    {
        public bool StalkerRanger { get; set; }
        public bool BeastermasterRanger { get; set; }
        public bool AssassinThief { get; set; }
        public bool BountyHunterThief { get; set; }
        public bool SwashbucklerThief { get; set; }
        public bool BladeBard { get; set; }
        public bool JesterBard { get; set; }
        public bool SkaldBard { get; set; }
    }

    [Serializable]
    public class KitUsability3
    {
        public bool Diviner { get; set; }
        public bool Enchanter { get; set; }
        public bool Illusionist { get; set; }
        public bool Invoker { get; set; }
        public bool Necromancer { get; set; }
        public bool Transmuter { get; set; }
        public bool All { get; set; }
        public bool Ferlain { get; set; }
    }

    [Serializable]
    public class KitUsability4
    {
        public bool BeserkerFighter { get; set; }
        public bool WizardslayerFighter { get; set; }
        public bool KensaiFighter { get; set; }
        public bool CavalierPaladin { get; set; }
        public bool InquisiterPaladin { get; set; }
        public bool UndeadHunterPaladin { get; set; }
        public bool Abjurer { get; set; }
        public bool Conjurer { get; set; }
    }

    [Serializable]
    public struct ItmFlags
    {
        public bool CriticalItem { get; set; }
        public bool TwoHanded { get; set; }
        public bool Movable { get; set; }
        public bool Displayable { get; set; }
        public bool Cursed { get; set; }
        public bool Unknown5 { get; set; }
        public bool Magical { get; set; }
        public bool Bow { get; set; }
        public bool Silver { get; set; }
        public bool ColdIron { get; set; }
        public bool Stolen { get; set; }
        public bool Conversable { get; set; }
        public bool Unknown12 { get; set; }
        public bool Unknown13 { get; set; }
        public bool Unknown14 { get; set; }
        public bool Unknown15 { get; set; }
        public bool Unknown16 { get; set; }
        public bool Unknown17 { get; set; }
        public bool Unknown18 { get; set; }
        public bool Unknown19 { get; set; }
        public bool Unknown20 { get; set; }
        public bool Unknown21 { get; set; }
        public bool Unknown22 { get; set; }
        public bool Unknown23 { get; set; }
        public bool Unknown24 { get; set; }
        public bool Unknown25 { get; set; }
        public bool Unknown26 { get; set; }
        public bool Unknown27 { get; set; }
        public bool Unknown28 { get; set; }
        public bool Unknown29 { get; set; }
        public bool Unknown30 { get; set; }
        public bool Unknown31 { get; set; }
    }

    public enum AttackType : byte
    {
        None = 0,
        Melee,
        Ranged,
        Magical,
        Launcher
    }

    public enum LauncherType : byte
    {
        None = 0,
        Bow,
        Crossbow,
        Sling,
        Spear = 40,
        ThrowingAxe = 100
    }    
}