using ii.InfinityEngine.Binary;
using System;
using System.Collections.Generic;

namespace ii.InfinityEngine.Files
{
    //TODO: We've assumed a creature can only have level 1 innates
    [Serializable]
    public class CreFile : IEFile
    {
        public CreFile()
        {
            MemorisedSpells = new CreMemorisedSpells();
            MemorisedSpells.MageLevel1 = [];
            MemorisedSpells.MageLevel2 = [];
            MemorisedSpells.MageLevel3 = [];
            MemorisedSpells.MageLevel4 = [];
            MemorisedSpells.MageLevel5 = [];
            MemorisedSpells.MageLevel6 = [];
            MemorisedSpells.MageLevel7 = [];
            MemorisedSpells.MageLevel8 = [];
            MemorisedSpells.MageLevel9 = [];
            MemorisedSpells.PriestLevel1 = [];
            MemorisedSpells.PriestLevel2 = [];
            MemorisedSpells.PriestLevel3 = [];
            MemorisedSpells.PriestLevel4 = [];
            MemorisedSpells.PriestLevel5 = [];
            MemorisedSpells.PriestLevel6 = [];
            MemorisedSpells.PriestLevel7 = [];
            MemorisedSpells.Innate = [];

            KnownSpells = new CreKnownSpells();
            KnownSpells.MageLevel1 = [];
            KnownSpells.MageLevel2 = [];
            KnownSpells.MageLevel3 = [];
            KnownSpells.MageLevel4 = [];
            KnownSpells.MageLevel5 = [];
            KnownSpells.MageLevel6 = [];
            KnownSpells.MageLevel7 = [];
            KnownSpells.MageLevel8 = [];
            KnownSpells.MageLevel9 = [];
            KnownSpells.PriestLevel1 = [];
            KnownSpells.PriestLevel2 = [];
            KnownSpells.PriestLevel3 = [];
            KnownSpells.PriestLevel4 = [];
            KnownSpells.PriestLevel5 = [];
            KnownSpells.PriestLevel6 = [];
            KnownSpells.PriestLevel7 = [];
            KnownSpells.Innate = [];

            Items = new CreItemSlots();
            Flags = new CreatureFlags();
            Kit = new Kit();
            StatusFlags = new StatusFlags();
        }

        public List<Eff1File> Effects1 = [];
        public List<EmbeddedEffBinary> Effects2 = []; // TODO: this should not be a binary?
        public CreItemSlots Items { get; set; }
        public CreMemorisedSpells MemorisedSpells { get; set; }
        public CreKnownSpells KnownSpells { get; set; }

        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private readonly IEFileType fileType = IEFileType.Cre;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public IEString LongName { get; set; }
        public IEString ShortName { get; set; }
        public CreatureFlags Flags { get; set; }
        public Int32 XPReward { get; set; }
        public Int32 PowerLevel { get; set; }
        public Int32 Gold { get; set; }
        public StatusFlags StatusFlags { get; set; }
        public Int16 CurrentHP { get; set; }
        public Int16 MaximumHP { get; set; }
        public Int32 Animation { get; set; }
        public byte MetalColourIndex { get; set; }
        public byte MinorColourIndex { get; set; }
        public byte MajorColourIndex { get; set; }
        public byte SkinColourIndex { get; set; }
        public byte LeatherColourIndex { get; set; }
        public byte ArmorColourIndex { get; set; }
        public byte HairColourIndex { get; set; }
        public byte EffVersion { get; set; }
        public array8 SmallPortrait { get; set; }
        public array8 LargePortrait { get; set; }
        public byte Reputation { get; set; }
        public byte HideInShadows { get; set; }
        public Int16 ArmorClassNatural { get; set; }
        public Int16 ArmorClassEffective { get; set; }
        public Int16 CrushingModifuer { get; set; }
        public Int16 MissileModifier { get; set; }
        public Int16 PiercingModifier { get; set; }
        public Int16 SlashingModifier { get; set; }
        public byte Thac0 { get; set; }
        public byte NumberOfAttacks { get; set; }
        public byte SaveVsDeath { get; set; }
        public byte SaveVsWands { get; set; }
        public byte SaveVsPolymorph { get; set; }
        public byte SaveVsBreath { get; set; }
        public byte SaveVsSpells { get; set; }
        public byte FireResistance { get; set; }
        public byte ColdResistance { get; set; }
        public byte ElectricityResistance { get; set; }
        public byte AcidResistance { get; set; }
        public byte MagicResistance { get; set; }
        public byte MagicFireResistance { get; set; }
        public byte MagicColdResistance { get; set; }
        public byte SlashingResistance { get; set; }
        public byte CrushingResistance { get; set; }
        public byte PiercingResistance { get; set; }
        public byte MissileResistance { get; set; }
        public byte DetectIllusion { get; set; }
        public byte SetTraps { get; set; }
        public byte Lore { get; set; }
        public byte LockPicking { get; set; }
        public byte Stealth { get; set; }
        public byte FindTraps { get; set; }
        public byte PickPockets { get; set; }
        public byte Fatigue { get; set; }
        public byte Intoxication { get; set; }
        public byte Luck { get; set; }
        public byte UnusedLargeSwordProficiency { get; set; }
        public byte UnusedSmallSwordProficiency { get; set; }
        public byte UnusedBowProficiency { get; set; }
        public byte UnusedSpearProficiency { get; set; }
        public byte UnusedBluntProficiency { get; set; }
        public byte UnusedSpikedProficiency { get; set; }
        public byte UnusedAxeProficiency { get; set; }
        public byte UnusedMissileProficiency { get; set; }
        public byte Unused1Proficiency { get; set; }
        public byte Unused2Proficiency { get; set; }
        public byte Unused3Proficiency { get; set; }
        public byte Unused4Proficiency { get; set; }
        public byte Unused5Proficiency { get; set; }
        public byte Unused6Proficiency { get; set; }
        public byte Unused7Proficiency { get; set; }
        public byte NightmareMode { get; set; }
        public byte Translucency { get; set; }
        public byte ReputationKill { get; set; }
        public byte ReputationJoin { get; set; }
        public byte ReputationLeave { get; set; }
        public byte TurnUndead { get; set; }
        public byte Tracking { get; set; }
        public array32 TrackingTarget { get; set; }
        public IEString Strref1 { get; set; }
        public IEString Strref2 { get; set; }
        public IEString Strref3 { get; set; }
        public IEString Strref4 { get; set; }
        public IEString Strref5 { get; set; }
        public IEString Strref6 { get; set; }
        public IEString Strref7 { get; set; }
        public IEString Strref8 { get; set; }
        public IEString Strref9 { get; set; }
        public IEString Strref10 { get; set; }
        public IEString Strref11 { get; set; }
        public IEString Strref12 { get; set; }
        public IEString Strref13 { get; set; }
        public IEString Strref14 { get; set; }
        public IEString Strref15 { get; set; }
        public IEString Strref16 { get; set; }
        public IEString Strref17 { get; set; }
        public IEString Strref18 { get; set; }
        public IEString Strref19 { get; set; }
        public IEString Strref20 { get; set; }
        public IEString Strref21 { get; set; }
        public IEString Strref22 { get; set; }
        public IEString Strref23 { get; set; }
        public IEString Strref24 { get; set; }
        public IEString Strref25 { get; set; }
        public IEString Strref26 { get; set; }
        public IEString Strref27 { get; set; }
        public IEString Strref28 { get; set; }
        public IEString Strref29 { get; set; }
        public IEString Strref30 { get; set; }
        public IEString Strref31 { get; set; }
        public IEString Strref32 { get; set; }
        public IEString Strref33 { get; set; }
        public IEString Strref34 { get; set; }
        public IEString Strref35 { get; set; }
        public IEString Strref36 { get; set; }
        public IEString Strref37 { get; set; }
        public IEString Strref38 { get; set; }
        public IEString Strref39 { get; set; }
        public IEString Strref40 { get; set; }
        public IEString Strref41 { get; set; }
        public IEString Strref42 { get; set; }
        public IEString Strref43 { get; set; }
        public IEString Strref44 { get; set; }
        public IEString Strref45 { get; set; }
        public IEString Strref46 { get; set; }
        public IEString Strref47 { get; set; }
        public IEString Strref48 { get; set; }
        public IEString Strref49 { get; set; }
        public IEString Strref50 { get; set; }
        public IEString Strref51 { get; set; }
        public IEString Strref52 { get; set; }
        public IEString Strref53 { get; set; }
        public IEString Strref54 { get; set; }
        public IEString Strref55 { get; set; }
        public IEString Strref56 { get; set; }
        public IEString Strref57 { get; set; }
        public IEString Strref58 { get; set; }
        public IEString Strref59 { get; set; }
        public IEString Strref60 { get; set; }
        public IEString Strref61 { get; set; }
        public IEString Strref62 { get; set; }
        public IEString Strref63 { get; set; }
        public IEString Strref64 { get; set; }
        public IEString Strref65 { get; set; }
        public IEString Strref66 { get; set; }
        public IEString Strref67 { get; set; }
        public IEString Strref68 { get; set; }
        public IEString Strref69 { get; set; }
        public IEString Strref70 { get; set; }
        public IEString Strref71 { get; set; }
        public IEString Strref72 { get; set; }
        public IEString Strref73 { get; set; }
        public IEString Strref74 { get; set; }
        public IEString Strref75 { get; set; }
        public IEString Strref76 { get; set; }
        public IEString Strref77 { get; set; }
        public IEString Strref78 { get; set; }
        public IEString Strref79 { get; set; }
        public IEString Strref80 { get; set; }
        public IEString Strref81 { get; set; }
        public IEString Strref82 { get; set; }
        public IEString Strref83 { get; set; }
        public IEString Strref84 { get; set; }
        public IEString Strref85 { get; set; }
        public IEString Strref86 { get; set; }
        public IEString Strref87 { get; set; }
        public IEString Strref88 { get; set; }
        public IEString Strref89 { get; set; }
        public IEString Strref90 { get; set; }
        public IEString Strref91 { get; set; }
        public IEString Strref92 { get; set; }
        public IEString Strref93 { get; set; }
        public IEString Strref94 { get; set; }
        public IEString Strref95 { get; set; }
        public IEString Strref96 { get; set; }
        public IEString Strref97 { get; set; }
        public IEString Strref98 { get; set; }
        public IEString Strref99 { get; set; }
        public IEString Strref100 { get; set; }
        public byte Level1 { get; set; }
        public byte Level2 { get; set; }
        public byte Level3 { get; set; }
        public byte Sex { get; set; }
        public byte Strength { get; set; }
        public byte StrengthBonus { get; set; }
        public byte Intelligence { get; set; }
        public byte Wisdom { get; set; }
        public byte Dexterity { get; set; }
        public byte Constitution { get; set; }
        public byte Charisma { get; set; }
        public byte Morale { get; set; }
        public byte MoraleBreak { get; set; }
        public byte RacialEnemy { get; set; }
        public Int16 MoraleRecoveryTime { get; set; }
        public Kit Kit { get; set; }
        public array8 ScriptOverride { get; set; }
        public array8 ScriptClass { get; set; }
        public array8 ScriptRace { get; set; }
        public array8 ScriptGeneral { get; set; }
        public array8 ScriptDefault { get; set; }
        public byte EnemyAlly { get; set; }
        public byte General { get; set; }
        public byte Race { get; set; }
        public byte Class { get; set; }
        public byte Specific { get; set; }
        public byte Gender { get; set; }
        public byte ObjectIdRef1 { get; set; }
        public byte ObjectIdRef2 { get; set; }
        public byte ObjectIdRef3 { get; set; }
        public byte ObjectIdRef4 { get; set; }
        public byte ObjectIdRef5 { get; set; }
        public byte Alignment { get; set; }
        public Int16 GlobalActorEnumeration { get; set; }
        public Int16 LocalActorEnumeration { get; set; }
        public array32 DeathVariable { get; set; }
        public Int32 KnownSpellsoffset { get; set; }
        public Int32 KnownSpellsCount { get; set; }
        public Int32 SpellMemorizationOffset { get; set; }
        public Int32 SpellMemorizationCount { get; set; }
        public Int32 MemorizedSpellsOffset { get; set; }
        public Int32 MemorizedSpellsCount { get; set; }
        public Int32 ItemSlotOffset { get; set; }
        public Int32 ItemOffset { get; set; }
        public Int32 ItemCount { get; set; }
        public Int32 EffectOffset { get; set; }
        public Int32 EffectCount { get; set; }
        public array8 DialogFile { get; set; }
    }

    [Serializable]
    public class CreatureFlags
    {
        public bool ShowLongname { get; set; }
        public bool NoCorpse { get; set; }
        public bool KeepCorpse { get; set; }
        public bool OriginalFighter { get; set; }
        public bool OriginalMage { get; set; }
        public bool OriginalCleric { get; set; }
        public bool OriginalThief { get; set; }
        public bool OriginalDruid { get; set; }
        public bool OriginalRanger { get; set; }
        public bool FallenPaladin { get; set; }
        public bool FallenRanger { get; set; }
        public bool Exportable { get; set; }
        public bool HideInjuryStatus { get; set; }
        public bool QuestCritical { get; set; }
        public bool MovingBetweenAreas { get; set; }
        public bool BeenInParty { get; set; }
        public bool RestoreItem { get; set; }
        public bool ClearRestoreItem { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool PreventExplodingDeath { get; set; }
        public bool Bit21 { get; set; }
        public bool DoNotApplyNightmareModeModifiers { get; set; }
        public bool NotTooltip { get; set; }
        public bool RandomWalkEa { get; set; }
        public bool RandomWalkGeneral { get; set; }
        public bool RandomWalkRace { get; set; }
        public bool RandomWalkClass { get; set; }
        public bool RandomWalkSpecific { get; set; }
        public bool RandomWalkGender { get; set; }
        public bool RandomWalkAlignment { get; set; }
        public bool UnInterruptable { get; set; }
    }

    [Serializable]
    public class CreItemSlots
    {
        public CreItem2 Helmet { get; set; }
        public CreItem2 Armor { get; set; }
        public CreItem2 Shield { get; set; }
        public CreItem2 Gloves { get; set; }
        public CreItem2 RingLeft { get; set; }
        public CreItem2 RingRight { get; set; }
        public CreItem2 Amulet { get; set; }
        public CreItem2 Belt { get; set; }
        public CreItem2 Boots { get; set; }
        public CreItem2 Weapon1 { get; set; }
        public CreItem2 Weapon2 { get; set; }
        public CreItem2 Weapon3 { get; set; }
        public CreItem2 Weapon4 { get; set; }
        public CreItem2 Quiver1 { get; set; }
        public CreItem2 Quiver2 { get; set; }
        public CreItem2 Quiver3 { get; set; }
        public CreItem2 Quiver4 { get; set; }
        public CreItem2 Cloak { get; set; }
        public CreItem2 QuickItem1 { get; set; }
        public CreItem2 QuickItem2 { get; set; }
        public CreItem2 QuickItem3 { get; set; }
        public CreItem2 InventoryItem1 { get; set; }
        public CreItem2 InventoryItem2 { get; set; }
        public CreItem2 InventoryItem3 { get; set; }
        public CreItem2 InventoryItem4 { get; set; }
        public CreItem2 InventoryItem5 { get; set; }
        public CreItem2 InventoryItem6 { get; set; }
        public CreItem2 InventoryItem7 { get; set; }
        public CreItem2 InventoryItem8 { get; set; }
        public CreItem2 InventoryItem9 { get; set; }
        public CreItem2 InventoryItem10 { get; set; }
        public CreItem2 InventoryItem11 { get; set; }
        public CreItem2 InventoryItem12 { get; set; }
        public CreItem2 InventoryItem13 { get; set; }
        public CreItem2 InventoryItem14 { get; set; }
        public CreItem2 InventoryItem15 { get; set; }
        public CreItem2 InventoryItem16 { get; set; }
        public CreItem2 MagicWeapon { get; set; }
        public short SelectedWeapon { get; set; }
        public short SelectedWeaponAbility { get; set; }
    }

    [Serializable]
    public class CreKnownSpells
    {
        public List<CreKnownSpell2> MageLevel1 { get; set; }
        public List<CreKnownSpell2> MageLevel2 { get; set; }
        public List<CreKnownSpell2> MageLevel3 { get; set; }
        public List<CreKnownSpell2> MageLevel4 { get; set; }
        public List<CreKnownSpell2> MageLevel5 { get; set; }
        public List<CreKnownSpell2> MageLevel6 { get; set; }
        public List<CreKnownSpell2> MageLevel7 { get; set; }
        public List<CreKnownSpell2> MageLevel8 { get; set; }
        public List<CreKnownSpell2> MageLevel9 { get; set; }
        public List<CreKnownSpell2> PriestLevel1 { get; set; }
        public List<CreKnownSpell2> PriestLevel2 { get; set; }
        public List<CreKnownSpell2> PriestLevel3 { get; set; }
        public List<CreKnownSpell2> PriestLevel4 { get; set; }
        public List<CreKnownSpell2> PriestLevel5 { get; set; }
        public List<CreKnownSpell2> PriestLevel6 { get; set; }
        public List<CreKnownSpell2> PriestLevel7 { get; set; }
        public List<CreKnownSpell2> Innate { get; set; }
    }

    [Serializable]
    public class CreKnownSpell2
    {
        public array8 Filename { get; set; }
    }

    [Serializable]
    public class CreMemorisedSpells
    {
        public List<CreMemorisedSpell2> MageLevel1 { get; set; }
        public List<CreMemorisedSpell2> MageLevel2 { get; set; }
        public List<CreMemorisedSpell2> MageLevel3 { get; set; }
        public List<CreMemorisedSpell2> MageLevel4 { get; set; }
        public List<CreMemorisedSpell2> MageLevel5 { get; set; }
        public List<CreMemorisedSpell2> MageLevel6 { get; set; }
        public List<CreMemorisedSpell2> MageLevel7 { get; set; }
        public List<CreMemorisedSpell2> MageLevel8 { get; set; }
        public List<CreMemorisedSpell2> MageLevel9 { get; set; }
        public List<CreMemorisedSpell2> PriestLevel1 { get; set; }
        public List<CreMemorisedSpell2> PriestLevel2 { get; set; }
        public List<CreMemorisedSpell2> PriestLevel3 { get; set; }
        public List<CreMemorisedSpell2> PriestLevel4 { get; set; }
        public List<CreMemorisedSpell2> PriestLevel5 { get; set; }
        public List<CreMemorisedSpell2> PriestLevel6 { get; set; }
        public List<CreMemorisedSpell2> PriestLevel7 { get; set; }
        public List<CreMemorisedSpell2> Innate { get; set; }
    }

    [Serializable]
    public class CreMemorisedSpell2
    {
        public array8 Filename { get; set; }
        public bool IsMemorised { get; set; }
    }

    [Serializable]
    public class CreItem2
    {
        public array8 Filename { get; set; }
        public byte ExpiryHour { get; set; }
        public byte ExpiryValue { get; set; }
        public Int16 Charges1 { get; set; }
        public Int16 Charges2 { get; set; }
        public Int16 Charges3 { get; set; }
        public CreatureItemFlags Flags { get; set; }
    }

    [Serializable]
    public class CreKnownSpells2
    {
        public array8 Filename { get; set; }
        public Int16 SpellLevel { get; set; }
        public Int16 SpellType { get; set; }
    }

    [Serializable]
    public class CreatureItemFlags
    {
        public bool IsIdentified { get; set; }
        public bool IsStealable { get; set; }
        public bool IsStolen { get; set; }
        public bool IsDroppable { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool Bit9 { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
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

    [Serializable]
    public class StatusFlags
    {
        public bool Sleeping { get; set; }
        public bool Berserk { get; set; }
        public bool Panic { get; set; }
        public bool Stunned { get; set; }
        public bool Invisible { get; set; }
        public bool Helpless { get; set; }
        public bool FrozenDeath { get; set; }
        public bool StoneDeath { get; set; }
        public bool ExplodingDeath { get; set; }
        public bool FlameDeath { get; set; }
        public bool AcidDeath { get; set; }
        public bool Dead { get; set; }
        public bool Silenced { get; set; }
        public bool Charmed { get; set; }
        public bool Poisoned { get; set; }
        public bool Hasted { get; set; }
        public bool Slowed { get; set; }
        public bool Infravision { get; set; }
        public bool Blind { get; set; }
        public bool Diseased { get; set; }
        public bool Feebleminded { get; set; }
        public bool Nondetection { get; set; }
        public bool ImprovedInvisibility { get; set; }
        public bool Bless { get; set; }
        public bool Chant { get; set; }
        public bool DrawUponHolyMight { get; set; }
        public bool Luck { get; set; }
        public bool Aid { get; set; }
        public bool ChantBad { get; set; }
        public bool Blur { get; set; }
        public bool MirrorImage { get; set; }
        public bool Confused { get; set; }
    }

    [Serializable]
    public class Kit
    {
        public bool None { get; set; }
        public bool Barbarian { get; set; }
        public bool TrueClass { get; set; }
        public bool Berserker { get; set; }
        public bool WizardSlayer { get; set; }
        public bool Kensai { get; set; }
        public bool Cavalier { get; set; }
        public bool Inquisitor { get; set; }
        public bool Undeadhunter { get; set; }
        public bool Archer { get; set; }
        public bool Stalker { get; set; }
        public bool Beastmaster { get; set; }
        public bool Assassin { get; set; }
        public bool Bountyhunter { get; set; }
        public bool Swashbuckler { get; set; }
        public bool Blade { get; set; }
        public bool Jester { get; set; }
        public bool Skald { get; set; }
        public bool Totemic { get; set; }
        public bool Shapeshifter { get; set; }
        public bool Avenger { get; set; }
        public bool ClericOfTalos { get; set; }
        public bool ClericOfHelm { get; set; }
        public bool ClericOfLathander { get; set; }
        public bool Abjurer { get; set; }
        public bool Conjurer { get; set; }
        public bool Diviner { get; set; }
        public bool Enchanter { get; set; }
        public bool Illusionist { get; set; }
        public bool Invoker { get; set; }
        public bool Necromancer { get; set; }
        public bool Transmuter { get; set; }
    }

    [Serializable]
    public enum MemorisedSpellType
    {
        Priest,
        Wizard,
        Innate
    }
}