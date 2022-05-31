using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iiInfinityEngine.Core.Binary;

namespace iiInfinityEngine.Core.Files
{
    //TODO: We've assumed a creature can only have level 1 innates
    [Serializable]
    public class CreFile : IEFile
    {
        public CreFile()
        {
            MemorisedSpells.MageLevel1 = new List<CreMemorisedSpell2>();
            MemorisedSpells.MageLevel2 = new List<CreMemorisedSpell2>();
            MemorisedSpells.MageLevel3 = new List<CreMemorisedSpell2>();
            MemorisedSpells.MageLevel4 = new List<CreMemorisedSpell2>();
            MemorisedSpells.MageLevel5 = new List<CreMemorisedSpell2>();
            MemorisedSpells.MageLevel6 = new List<CreMemorisedSpell2>();
            MemorisedSpells.MageLevel7 = new List<CreMemorisedSpell2>();
            MemorisedSpells.MageLevel8 = new List<CreMemorisedSpell2>();
            MemorisedSpells.MageLevel9 = new List<CreMemorisedSpell2>();
            MemorisedSpells.PriestLevel1 = new List<CreMemorisedSpell2>();
            MemorisedSpells.PriestLevel2 = new List<CreMemorisedSpell2>();
            MemorisedSpells.PriestLevel3 = new List<CreMemorisedSpell2>();
            MemorisedSpells.PriestLevel4 = new List<CreMemorisedSpell2>();
            MemorisedSpells.PriestLevel5 = new List<CreMemorisedSpell2>();
            MemorisedSpells.PriestLevel6 = new List<CreMemorisedSpell2>();
            MemorisedSpells.PriestLevel7 = new List<CreMemorisedSpell2>();
            MemorisedSpells.Innate = new List<CreMemorisedSpell2>();

            KnownSpells.MageLevel1 = new List<CreKnownSpell2>();
            KnownSpells.MageLevel2 = new List<CreKnownSpell2>();
            KnownSpells.MageLevel3 = new List<CreKnownSpell2>();
            KnownSpells.MageLevel4 = new List<CreKnownSpell2>();
            KnownSpells.MageLevel5 = new List<CreKnownSpell2>();
            KnownSpells.MageLevel6 = new List<CreKnownSpell2>();
            KnownSpells.MageLevel7 = new List<CreKnownSpell2>();
            KnownSpells.MageLevel8 = new List<CreKnownSpell2>();
            KnownSpells.MageLevel9 = new List<CreKnownSpell2>();
            KnownSpells.PriestLevel1 = new List<CreKnownSpell2>();
            KnownSpells.PriestLevel2 = new List<CreKnownSpell2>();
            KnownSpells.PriestLevel3 = new List<CreKnownSpell2>();
            KnownSpells.PriestLevel4 = new List<CreKnownSpell2>();
            KnownSpells.PriestLevel5 = new List<CreKnownSpell2>();
            KnownSpells.PriestLevel6 = new List<CreKnownSpell2>();
            KnownSpells.PriestLevel7 = new List<CreKnownSpell2>();
            KnownSpells.Innate = new List<CreKnownSpell2>();
        }

        public List<Eff1File> Effects1 = new List<Eff1File>();
        public List<EmbeddedEffBinary> Effects2 = new List<EmbeddedEffBinary>(); // TODO: this should not be a binary?
        public CreItemSlots Items;
        public CreMemorisedSpells MemorisedSpells;
        public CreKnownSpells KnownSpells;

        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private IEFileType fileType = IEFileType.Cre;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public IEString LongName;
        public IEString ShortName;
        public CreatureFlags Flags;
        public Int32 XPReward;
        public Int32 PowerLevel;
        public Int32 Gold;
        public Int32 StatusFlags; // state.ids
        public Int16 CurrentHP;
        public Int16 MaximumHP;
        public Int32 Animation;
        public byte MetalColourIndex;
        public byte MinorColourIndex;
        public byte MajorColourIndex;
        public byte SkinColourIndex;
        public byte LeatherColourIndex;
        public byte ArmorColourIndex;
        public byte HairColourIndex;
        public byte EffVersion;
        public array8 SmallPortrait;
        public array8 LargePortrait;
        public byte Reputation;
        public byte HideInShadows;
        public Int16 ArmorClassNatural;
        public Int16 ArmorClassEffective;
        public Int16 CrushingModifuer;
        public Int16 MissileModifier;
        public Int16 PiercingModifier;
        public Int16 SlashingModifier;
        public byte Thac0;
        public byte NumberOfAttacks;
        public byte SaveVsDeath;
        public byte SaveVsWands;
        public byte SaveVsPolymorph;
        public byte SaveVsBreath;
        public byte SaveVsSpells;
        public byte FireResistance;
        public byte ColdResistance;
        public byte ElectricityResistance;
        public byte AcidResistance;
        public byte MagicResistance;
        public byte MagicFireResistance;
        public byte MagicColdResistance;
        public byte SlashingResistance;
        public byte CrushingResistance;
        public byte PiercingResistance;
        public byte MissileResistance;
        public byte DetectIllusion;
        public byte SetTraps;
        public byte Lore;
        public byte LockPicking;
        public byte Stealth;
        public byte FindTraps;
        public byte PickPockets;
        public byte Fatigue;
        public byte Intoxication;
        public byte Luck;
        public byte UnusedLargeSwords;
        public byte UnusedSmallSwords;
        public byte UnusedBows;
        public byte UnusedSpears;
        public byte UnusedBlunt;
        public byte UnusedSpiked;
        public byte UnusedAxe;
        public byte UnusedMissile; //
        public Int32 Unknownx1;
        public Int32 Unknownx2;
        public Int32 Unknownx3;
        public byte Unknownx4;
        public byte Tracking;
        public array32 Unknownx5;
        public IEString Strref1;
        public IEString Strref2;
        public IEString Strref3;
        public IEString Strref4;
        public IEString Strref5;
        public IEString Strref6;
        public IEString Strref7;
        public IEString Strref8;
        public IEString Strref9;
        public IEString Strref10;
        public IEString Strref11;
        public IEString Strref12;
        public IEString Strref13;
        public IEString Strref14;
        public IEString Strref15;
        public IEString Strref16;
        public IEString Strref17;
        public IEString Strref18;
        public IEString Strref19;
        public IEString Strref20;
        public IEString Strref21;
        public IEString Strref22;
        public IEString Strref23;
        public IEString Strref24;
        public IEString Strref25;
        public IEString Strref26;
        public IEString Strref27;
        public IEString Strref28;
        public IEString Strref29;
        public IEString Strref30;
        public IEString Strref31;
        public IEString Strref32;
        public IEString Strref33;
        public IEString Strref34;
        public IEString Strref35;
        public IEString Strref36;
        public IEString Strref37;
        public IEString Strref38;
        public IEString Strref39;
        public IEString Strref40;
        public IEString Strref41;
        public IEString Strref42;
        public IEString Strref43;
        public IEString Strref44;
        public IEString Strref45;
        public IEString Strref46;
        public IEString Strref47;
        public IEString Strref48;
        public IEString Strref49;
        public IEString Strref50;
        public IEString Strref51;
        public IEString Strref52;
        public IEString Strref53;
        public IEString Strref54;
        public IEString Strref55;
        public IEString Strref56;
        public IEString Strref57;
        public IEString Strref58;
        public IEString Strref59;
        public IEString Strref60;
        public IEString Strref61;
        public IEString Strref62;
        public IEString Strref63;
        public IEString Strref64;
        public IEString Strref65;
        public IEString Strref66;
        public IEString Strref67;
        public IEString Strref68;
        public IEString Strref69;
        public IEString Strref70;
        public IEString Strref71;
        public IEString Strref72;
        public IEString Strref73;
        public IEString Strref74;
        public IEString Strref75;
        public IEString Strref76;
        public IEString Strref77;
        public IEString Strref78;
        public IEString Strref79;
        public IEString Strref80;
        public IEString Strref81;
        public IEString Strref82;
        public IEString Strref83;
        public IEString Strref84;
        public IEString Strref85;
        public IEString Strref86;
        public IEString Strref87;
        public IEString Strref88;
        public IEString Strref89;
        public IEString Strref90;
        public IEString Strref91;
        public IEString Strref92;
        public IEString Strref93;
        public IEString Strref94;
        public IEString Strref95;
        public IEString Strref96;
        public IEString Strref97;
        public IEString Strref98;
        public IEString Strref99;
        public IEString Strref100;
        public byte Level1;
        public byte Level2;
        public byte Level3;
        public byte Sex;
        public byte Strength;
        public byte StrengthBonus;
        public byte Intelligence;
        public byte Wisdom;
        public byte Dexterity;
        public byte Constitution;
        public byte Charisma;
        public byte Morale;
        public byte MoraleBreak;
        public byte RacialEnemy;
        public Int16 MoraleRecoveryTime;
        public Int32 Kit;
        public array8 ScriptOverride;
        public array8 ScriptClass;
        public array8 ScriptRace;
        public array8 ScriptGeneral;
        public array8 ScriptDefault;
        public byte EnemyAlly;
        public byte General;
        public byte Race;
        public byte Class;
        public byte Specific;
        public byte Gender;
        public byte ObjectIdRef1;
        public byte ObjectIdRef2;
        public byte ObjectIdRef3;
        public byte ObjectIdRef4;
        public byte ObjectIdRef5;
        public byte Alignment;
        public Int16 GlobalActorEnumeration;
        public Int16 LocalActorEnumeration;
        public array32 DeathVariable;
        public Int32 KnownSpellsoffset;
        public Int32 KnownSpellsCount;
        public Int32 SpellMemorizationOffset;
        public Int32 SpellMemorizationCount;
        public Int32 MemorizedSpellsOffset;
        public Int32 MemorizedSpellsCount;
        public Int32 ItemSlotOffset;
        public Int32 ItemOffset;
        public Int32 ItemCount;
        public Int32 EffectOffset;
        public Int32 EffectCount;
        public array8 DialogFile;
    }

    [Serializable]
    public struct CreatureFlags
    {
        //A multiclass char is indicated is by the absence of any of the "original class" flags being set
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
        public bool CanActivateTriggers { get; set; }
        public bool BeenInParty { get; set; }
        public bool RestoreItem { get; set; }
        public bool ClearRestoreItem { get; set; }
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
    public struct CreItemSlots
    {
        public CreItem2 Helmet;
        public CreItem2 Armor;
        public CreItem2 Shield;
        public CreItem2 Gloves;
        public CreItem2 RingLeft;
        public CreItem2 RingRight;
        public CreItem2 Amulet;
        public CreItem2 Belt;
        public CreItem2 Boots;
        public CreItem2 Weapon1;
        public CreItem2 Weapon2;
        public CreItem2 Weapon3;
        public CreItem2 Weapon4;
        public CreItem2 Quiver1;
        public CreItem2 Quiver2;
        public CreItem2 Quiver3;
        public CreItem2 Unknown;
        public CreItem2 Cloak;
        public CreItem2 QuickItem1;
        public CreItem2 QuickItem2;
        public CreItem2 QuickItem3;
        public CreItem2 InventoryItem1;
        public CreItem2 InventoryItem2;
        public CreItem2 InventoryItem3;
        public CreItem2 InventoryItem4;
        public CreItem2 InventoryItem5;
        public CreItem2 InventoryItem6;
        public CreItem2 InventoryItem7;
        public CreItem2 InventoryItem8;
        public CreItem2 InventoryItem9;
        public CreItem2 InventoryItem10;
        public CreItem2 InventoryItem11;
        public CreItem2 InventoryItem12;
        public CreItem2 InventoryItem13;
        public CreItem2 InventoryItem14;
        public CreItem2 InventoryItem15;
        public CreItem2 InventoryItem16;
        public CreItem2 MagicWeapon;
        public short SelectedWeapon;
        public short SelectedWeaponAbility;
    }

    [Serializable]
    public struct CreKnownSpells
    {
        public List<CreKnownSpell2> MageLevel1;
        public List<CreKnownSpell2> MageLevel2;
        public List<CreKnownSpell2> MageLevel3;
        public List<CreKnownSpell2> MageLevel4;
        public List<CreKnownSpell2> MageLevel5;
        public List<CreKnownSpell2> MageLevel6;
        public List<CreKnownSpell2> MageLevel7;
        public List<CreKnownSpell2> MageLevel8;
        public List<CreKnownSpell2> MageLevel9;
        public List<CreKnownSpell2> PriestLevel1;
        public List<CreKnownSpell2> PriestLevel2;
        public List<CreKnownSpell2> PriestLevel3;
        public List<CreKnownSpell2> PriestLevel4;
        public List<CreKnownSpell2> PriestLevel5;
        public List<CreKnownSpell2> PriestLevel6;
        public List<CreKnownSpell2> PriestLevel7;
        public List<CreKnownSpell2> Innate;
    }

    [Serializable]
    public struct CreKnownSpell2
    {
        public string Filename { get; set; }
    }

    [Serializable]
    public struct CreMemorisedSpells
    {
        public List<CreMemorisedSpell2> MageLevel1;
        public List<CreMemorisedSpell2> MageLevel2;
        public List<CreMemorisedSpell2> MageLevel3;
        public List<CreMemorisedSpell2> MageLevel4;
        public List<CreMemorisedSpell2> MageLevel5;
        public List<CreMemorisedSpell2> MageLevel6;
        public List<CreMemorisedSpell2> MageLevel7;
        public List<CreMemorisedSpell2> MageLevel8;
        public List<CreMemorisedSpell2> MageLevel9;
        public List<CreMemorisedSpell2> PriestLevel1;
        public List<CreMemorisedSpell2> PriestLevel2;
        public List<CreMemorisedSpell2> PriestLevel3;
        public List<CreMemorisedSpell2> PriestLevel4;
        public List<CreMemorisedSpell2> PriestLevel5;
        public List<CreMemorisedSpell2> PriestLevel6;
        public List<CreMemorisedSpell2> PriestLevel7;
        public List<CreMemorisedSpell2> Innate;
    }

    [Serializable]
    public struct CreMemorisedSpell2
    {
        public string Filename { get; set; }
        public bool IsMemorised { get; set; }
    }

    [Serializable]
    public struct CreItem2
    {
        public string Filename { get; set; }
        public byte ExpiryHour { get; set; }
        public byte ExpiryValue { get; set; }
        public Int16 Charges1 { get; set; }
        public Int16 Charges2 { get; set; }
        public Int16 Charges3 { get; set; }
        public Int32 Flags { get; set; }
    }

    [Serializable]
    public struct CreKnownSpells2
    {
        public string Filename { get; set; }
        public Int16 SpellLevel { get; set; }
        public Int16 SpellType { get; set; }
    }

    [Serializable]
    public struct CreatureItemFlags
    {
        public bool IsIdentified { get; set; }
        public bool IsStealable { get; set; }
        public bool IsStolen { get; set; }
        public bool IsDroppable { get; set; }
    }

    [Serializable]
    public enum MemorisedSpellType
    {
        Priest,
        Wizard,
        Innate
    }
}