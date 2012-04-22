using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct CreHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int32 LongName;
        public Int32 ShortName;
        public Int32 Flags;
        public Int32 XPReward;
        public Int32 PowerLevel;
        public Int32 Gold;
        public Int32 StatusFlags; // state.ids
        public Int16 CurrentHP;
        public Int16 MaximumHP;
        public Int16 Animation;
        public Int16 Unknown;
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
        public byte SaveVsWanrds;
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
        public byte UnusedMissile;       
        public Int32 Unknownx1;
        public Int32 Unknownx2;
        public Int32 Unknownx3;
        public byte Unknownx4;
        public byte Tracking;
        public array32 Unknownx5;
        public Int32 Strref1;
        public Int32 Strref2;
        public Int32 Strref3;
        public Int32 Strref4;
        public Int32 Strref5;
        public Int32 Strref6;
        public Int32 Strref7;
        public Int32 Strref8;
        public Int32 Strref9;
        public Int32 Strref10;
        public Int32 Strref11;
        public Int32 Strref12;
        public Int32 Strref13;
        public Int32 Strref14;
        public Int32 Strref15;
        public Int32 Strref16;
        public Int32 Strref17;
        public Int32 Strref18;
        public Int32 Strref19;
        public Int32 Strref20;
        public Int32 Strref21;
        public Int32 Strref22;
        public Int32 Strref23;
        public Int32 Strref24;
        public Int32 Strref25;
        public Int32 Strref26;
        public Int32 Strref27;
        public Int32 Strref28;
        public Int32 Strref29;
        public Int32 Strref30;
        public Int32 Strref31;
        public Int32 Strref32;
        public Int32 Strref33;
        public Int32 Strref34;
        public Int32 Strref35;
        public Int32 Strref36;
        public Int32 Strref37;
        public Int32 Strref38;
        public Int32 Strref39;
        public Int32 Strref40;
        public Int32 Strref41;
        public Int32 Strref42;
        public Int32 Strref43;
        public Int32 Strref44;
        public Int32 Strref45;
        public Int32 Strref46;
        public Int32 Strref47;
        public Int32 Strref48;
        public Int32 Strref49;
        public Int32 Strref50;
        public Int32 Strref51;
        public Int32 Strref52;
        public Int32 Strref53;
        public Int32 Strref54;
        public Int32 Strref55;
        public Int32 Strref56;
        public Int32 Strref57;
        public Int32 Strref58;
        public Int32 Strref59;
        public Int32 Strref60;
        public Int32 Strref61;
        public Int32 Strref62;
        public Int32 Strref63;
        public Int32 Strref64;
        public Int32 Strref65;
        public Int32 Strref66;
        public Int32 Strref67;
        public Int32 Strref68;
        public Int32 Strref69;
        public Int32 Strref70;
        public Int32 Strref71;
        public Int32 Strref72;
        public Int32 Strref73;
        public Int32 Strref74;
        public Int32 Strref75;
        public Int32 Strref76;
        public Int32 Strref77;
        public Int32 Strref78;
        public Int32 Strref79;
        public Int32 Strref80;
        public Int32 Strref81;
        public Int32 Strref82;
        public Int32 Strref83;
        public Int32 Strref84;
        public Int32 Strref85;
        public Int32 Strref86;
        public Int32 Strref87;
        public Int32 Strref88;
        public Int32 Strref89;
        public Int32 Strref90;
        public Int32 Strref91;
        public Int32 Strref92;
        public Int32 Strref93;
        public Int32 Strref94;
        public Int32 Strref95;
        public Int32 Strref96;
        public Int32 Strref97;
        public Int32 Strref98;
        public Int32 Strref99;
        public Int32 Strref100;
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

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct CreKnownSpellBinary
    {
        public array8 Filename { get; set; }
        public Int16 SpellLevel { get; set; }
        public Int16 SpellType { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct CreSpellMemorisationInfoBinary
    {
        public Int16 SpellLevel { get; set; }
        public Int16 SlotCount { get; set; }
        public Int16 CurrentSlotCount { get; set; }
        public Int16 SpellType { get; set; }
        public Int32 SpellOffset { get; set; }
        public Int32 SpellCount { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct CreMemorisedSpellBinary
    {
        public array8 Filename { get; set; }
        public Int32 Memorised { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct CreItemBinary
    {
        public array8 Filename { get; set; }
        public byte ExpiryHour { get; set; }
        public byte ExpiryValue { get; set; }
        public Int16 Charges1 { get; set; }
        public Int16 Charges2 { get; set; }
        public Int16 Charges3 { get; set; }
        public Int32 Flags { get; set; }
    }
}