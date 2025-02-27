using System;
using System.Collections.Generic;

namespace ii.InfinityEngine.Files
{
    [Serializable]
    public class ItmFile : IEFile
    {
        public List<ItmExtendedHeader2> itmExtendedHeaders = [];
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

        public ItmFile()
        {
            Flags = new ItmFlags();
            Usability1 = new Usability1();
            Usability2 = new Usability2();
            Usability3 = new Usability3();
            Usability4 = new Usability4();
            KitUsability1 = new KitUsability1();
            KitUsability2 = new KitUsability2();
            KitUsability3 = new KitUsability3();
            KitUsability4 = new KitUsability4();
        }

        public IEString UnidentifiedName { get; set; }
        public IEString IdentifiedName { get; set; }
        public array8 ReplacementItem { get; set; }
        public ItmFlags Flags { get; set; }
        public ItemType ItemType { get; set; }
        public Usability1 Usability1 { get; set; }
        public Usability2 Usability2 { get; set; }
        public Usability3 Usability3 { get; set; }
        public Usability4 Usability4 { get; set; }
        public array2 Animation { get; set; }
        public Int16 MinimumLevel { get; set; }
        public Int16 MinimumStrength { get; set; }
        public byte MinimumStrengthBonus { get; set; }
        public KitUsability1 KitUsability1 { get; set; }
        public byte MinimumIntelligence { get; set; }
        public KitUsability2 KitUsability2 { get; set; }
        public byte MinimumDexterity { get; set; }
        public KitUsability3 KitUsability3 { get; set; }
        public byte MinimumWisdom { get; set; }
        public KitUsability4 KitUsability4 { get; set; }
        public byte MinimumConstitution { get; set; }
        public Proficiency Proficiency { get; set; }
        public Int16 MinimumCharisma { get; set; }
        public Int32 Price { get; set; }
        public Int16 StackAmount { get; set; }
        public array8 InventoryIcon { get; set; }
        public Int16 LoreToIdentify { get; set; }
        public array8 GroundIcon { get; set; }
        public Int32 Weight { get; set; }
        public IEString UnidentifiedDescription { get; set; }
        public IEString IdentifiedDescription { get; set; }
        public array8 DescriptionIcon { get; set; }
        public Int32 Enchantment { get; set; }
        public Int32 ExtendedHeaderOffset { get; set; }
        public Int16 ExtendedHeaderCount { get; set; }
        public Int32 FeatureBlockOffset { get; set; }
        public Int16 FeatureBlockEquippingIndex { get; set; }
        public Int16 FeatureBlockEquippingCount { get; set; }
    }

    [Serializable]
    public class ItmExtendedHeader2
    {
        public ItmExtendedHeader2()
        {
            IdentificationRequirement = new IdentificationRequirement();
            Flags = new Flags();
        }            

        public AttackType AttackType { get; set; }
        public IdentificationRequirement IdentificationRequirement;
        public Location Location { get; set; }
        public byte AlternaticeDiceSides { get; set; }
        public array8 UseIcon { get; set; }
        public TargetType TargetType { get; set; }
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
        public DamageType DamageType { get; set; }
        public Int16 FeatureBlockCount { get; set; }
        public Int16 FeatureBlockOffset { get; set; }
        public Int16 Charges { get; set; }
        public ChargeDepletionBehaviour ChargeDepletionBehaviour { get; set; }
        public Flags Flags { get; set; }
        public Int16 ProjectileAnimation { get; set; }
        public Int16 MeleeAnimation1 { get; set; }
        public Int16 MeleeAnimation2 { get; set; }
        public Int16 MeleeAnimation3 { get; set; }
        public ProjectileQualifier IsBowArrow { get; set; }
        public ProjectileQualifier IsCrossbowBolt { get; set; }
        public ProjectileQualifier IsMiscProjectile { get; set; }

        public List<ItmFeatureBlock2> itmFeatureBlocks = [];
    }

    [Serializable]
    public class ItmFeatureBlock2
    {
        public ItmFeatureBlock2()
        {
            SavingThrowType = new SavingThrowType();
            Resistance = new Resistance();
        }

        public Int16 Opcode { get; set; }
        public OpcodeTargetType TargetType { get; set; }
        public byte Power { get; set; }
        public Int32 Parameter1 { get; set; }
        public Int32 Parameter2 { get; set; }
        public OpcodeTimingMode TimingMode { get; set; }
        public Resistance Resistance { get; set; }
        public Int32 Duration { get; set; }
        public byte Probability1 { get; set; }
        public byte Probability2 { get; set; }
        public array8 Resource { get; set; }
        public Int32 DiceThrown { get; set; }
        public Int32 DiceSides { get; set; }
        public SavingThrowType SavingThrowType { get; set; }
        public Int32 SavingThrowBonus { get; set; }
        public Int32 Special { get; set; }
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
    public class ItmFlags
    {
        public bool CriticalItem { get; set; }
        public bool TwoHanded { get; set; }
        public bool Movable { get; set; }
        public bool Displayable { get; set; }
        public bool Cursed { get; set; }
        public bool Bit5 { get; set; }
        public bool Magical { get; set; }
        public bool Bow { get; set; }
        public bool Silver { get; set; }
        public bool ColdIron { get; set; }
        public bool Stolen { get; set; }
        public bool Conversable { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
        public bool Bit16 { get; set; }
        public bool Bit17 { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool Bit20 { get; set; }
        public bool Bit21 { get; set; }
        public bool Bit22 { get; set; }
        public bool Bit23 { get; set; }
        public bool Bit24 { get; set; }
        public bool Bit25 { get; set; }
        public bool Bit26 { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool Bit31 { get; set; }
    }

    public enum AttackType : byte
    {
        None = 0,
        Melee,
        Ranged,
        Magical,
        Launcher
    }

    public enum Location : byte
    {
        None = 0,
        Weapon,
        Spell,
        Item,
        Innate
    }

    public enum TargetType : byte
    {
        None = 0,
        LivingActor,
        Inventory,
        DeadActor,
        Range,
        Caster,
        Crash,
        CasterEE
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

    public enum OpcodeTargetType : byte
    {
        None = 0,
        Self,
        ProjectileTarget,
        Party,
        Everyone,
        EveryoneExceptParty,
        CasterGroup,
        TargetGroup,
        EveryoneExceptSelf,
        OriginalCaster
    }

    public enum OpcodeTimingMode : byte
    {
        InstantLimited,
        InstantPermanent,
        InstantWhileEquipped,
        DelayLimited,
        DelayPermanent,
        DelayWhileEquipped,
        LimitedAfterDuration,
        PermanentAfterDuration,
        EquippedAfterDuration,
        InstantPermanentAfterDeath,
        Instant_Limited
    }

    public enum ChargeDepletionBehaviour : Int16
    {
        ItemRemains,
        ItemVanishes,
        ReplaceWithUsedUp,
        ItemRecharges
    }

    public enum DamageType : Int16
    {
        None,
        Piercing,
        Crushing,
        Slashing,
        Missile,
        Fist,
        PiercingCrushing_Better,
        PiercingSlashing_Better,
        CrushingSlashing_Worse,
        BluntMissile
    }

    [Serializable]
    public class Flags
    {
        public bool AddStengthBonus { get; set; }
        public bool Breakable { get; set; }
        public bool DamageStrengthBonus { get; set; }
        public bool Thac0StrengthBonus { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool BreaksSantuaryInvisibility { get; set; }
        public bool Hostile { get; set; }
        public bool RechargeAfterResting { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
        public bool Bit16 { get; set; }
        public bool Bit17 { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool Bit20 { get; set; }
        public bool Bit21 { get; set; }
        public bool Bit22 { get; set; }
        public bool Bit23 { get; set; }
        public bool Bit24 { get; set; }
        public bool Bit25 { get; set; }
        public bool CannotTargetInvisible { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool Bit31 { get; set; }
    }

    public enum Proficiency : byte
    {
        None = 0,
        BastardSword = 0x59,
        LongSword = 0x5a,
        ShortSword = 0x5b,
        Axe = 0x5c,
        TwoHandedSword = 0x5d,
        Katana = 0x5e,
        Scimitar = 0x5f,
        Dagger = 0x60,
        WarHammer = 0x61,
        Spear = 0x62,
        Halberd = 0x63,
        FlailMorningStar = 0x64,
        Mace = 0x65,
        QuarterStaff = 0x66,
        Crossbow = 0x67,
        LongBow = 0x68,
        ShortBow = 0x69,
        Darts = 0x6a,
        Sling = 0x6b,
        Blackjack = 0x6c,
        Gun = 0x6d,
        MartialArts = 0x6e,
        TwoHandedWeaponStyle = 0x6f,
        SwordAndShieldStyle = 0x70,
        SingleWeaponStyle = 0x71,
        TwoWeaponStyle = 0x72,
        Club = 0x73,
        ExtraProficiency2 = 0x74,
        ExtraProficiency3 = 0x75,
        ExtraProficiency4 = 0x76,
        ExtraProficiency5 = 0x77,
        ExtraProficiency6 = 0x78,
        ExtraProficiency7 = 0x79,
        ExtraProficiency8 = 0x7a,
        ExtraProficiency9 = 0x7b,
        ExtraProficiency10 = 0x7c,
        ExtraProficiency11 = 0x7d,
        ExtraProficiency12 = 0x7e,
        ExtraProficiency13 = 0x7f,
        ExtraProficiency14 = 0x80,
        ExtraProficiency15 = 0x81,
        ExtraProficiency16 = 0x82,
        ExtraProficiency17 = 0x83,
        ExtraProficiency18 = 0x84,
        ExtraProficiency19 = 0x85,
        ExtraProficiency20 = 0x86
    }

    public enum ProjectileQualifier : Int16
    {
        No = 0,
        Yes = 1
    }

    [Serializable]
    public class IdentificationRequirement
    {
        public bool IdRequired { get; set; }
        public bool NonIdRequired { get; set; }
        public bool Bit2 { get; set; }
        public bool Bit3 { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
    } 
}