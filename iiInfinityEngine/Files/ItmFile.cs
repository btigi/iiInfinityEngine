using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class ItmFile : IEFile
    {
        public List<ItmExtendedHeader2> itmExtendedHeader = new List<ItmExtendedHeader2>();
        public List<ItmFeatureBlock2> itmFeatureBlocks = new List<ItmFeatureBlock2>();

        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private IEFileType fileType = IEFileType.Itm;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public IEString UnidentifiedName;
        public IEString IdentifiedName;
        public string ReplacementItem;
        public ItmFlags Flags;
        public ItemType ItemType;
        public char Usability1;
        public char Usability2;
        public char Usability3;
        public char Usability4;
        public string Animation;
        public char MinimumLevel;
        public char Unknown;
        public char MinimumStrength;
        public char Unknown2;
        public char MinimumStrengthBonus;
        public char KitUsability1;
        public char MinimumIntelligence;
        public char KitUsability2;
        public char MinimumDexterity;
        public char KitUsability3;
        public char MinimumWisdom;
        public char KitUsability4;
        public char MinimumConstitution;
        public char Proficiency;
        public char MinimumCharisma;
        public char Unknown3;
        public Int32 Price;
        public Int16 StackAmount;
        public string InventoryIcon;
        public Int16 LoreToIdentify;
        public string GroundIcon;
        public Int32 Weight;
        public IEString UnidentifiedDescription;
        public IEString IdentifiedDescription;
        public string DescriptionIcon;
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
        public char AttackType { get; set; }
        public char IdentificationRequirement { get; set; }
        public char Location { get; set; }
        public char AlternaticeDiceSides { get; set; }
        public array8 UseIcon { get; set; }
        public char TargetType { get; set; }
        public char TargetCount { get; set; }
        public Int16 Range { get; set; }
        public byte ProjectileType { get; set; }
        public byte AlternaticeDiceThrown { get; set; }
        public byte Speed { get; set; }
        public byte AlternaticeDamageBonus { get; set; }
        public Int16 Thac0Bonus { get; set; }
        public byte DiceSides { get; set; }
        public byte PrimaryType { get; set; }
        public byte DiceThrown { get; set; }
        public byte SecondaryType { get; set; }
        public Int16 DamageBonus { get; set; }
        public Int16 DamageType { get; set; }
        public Int16 FeatureBlockCount { get; set; }
        public Int16 FeatureBlockOffset { get; set; }
        public Int16 Charges { get; set; }
        public Int16 ChargeDepletionBehaviour { get; set; }
        public Int16 Flags { get; set; }
        public Int16 Unknown { get; set; }
        public Int16 ProjectileAnimation { get; set; }
        public Int16 MeleeAnimation1 { get; set; }
        public Int16 MeleeAnimation2 { get; set; }
        public Int16 MeleeAnimation3 { get; set; }
        public Int16 IsBowArrow { get; set; }
        public Int16 IsCrossbowBolt { get; set; }
        public Int16 IsMiscProjectile { get; set; }

        public List<ItmFeatureBlock2> itmFeatureBlocks = new List<ItmFeatureBlock2>();
    }

    [Serializable]
    public class ItmFeatureBlock2
    {
        public Int16 Opcode { get; set; }
        public byte TargetType { get; set; }
        public byte Power { get; set; }
        public Int32 Parameter1 { get; set; }
        public Int32 Parameter2 { get; set; }
        public byte TimingMode { get; set; }
        public byte Resistance { get; set; }
        public Int32 Duration { get; set; }
        public byte Probability1 { get; set; }
        public byte Probability2 { get; set; }
        public array8 Resource { get; set; }
        public Int32 DiceThrown { get; set; }
        public Int32 DiceSides { get; set; }
        public Int32 SavingThrowType { get; set; }
        public Int32 SavingThrowBonus { get; set; }
        public Int32 Unknown { get; set; }
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
}