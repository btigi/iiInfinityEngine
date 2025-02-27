using System;
using System.Runtime.InteropServices;

namespace ii.InfinityEngine.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct CreHeaderBinary
    {
        public array4 ftype { get; set; }
        public array4 fversion { get; set; }
        public Int32 LongName { get; set; }
        public Int32 ShortName { get; set; }
        public Int32 Flags { get; set; }
        public Int32 XPReward { get; set; }
        public Int32 PowerLevel { get; set; }
        public Int32 Gold { get; set; }
        public Int32 StatusFlags { get; set; } // state.ids
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
        public byte UnusedLargeSwords { get; set; }
        public byte UnusedSmallSwords { get; set; }
        public byte UnusedBows { get; set; }
        public byte UnusedSpears { get; set; }
        public byte UnusedBlunt { get; set; }
        public byte UnusedSpiked { get; set; }
        public byte UnusedAxe { get; set; }
        public byte UnusedMissile { get; set; }       
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
        public Int32 Strref1 { get; set; }
        public Int32 Strref2 { get; set; }
        public Int32 Strref3 { get; set; }
        public Int32 Strref4 { get; set; }
        public Int32 Strref5 { get; set; }
        public Int32 Strref6 { get; set; }
        public Int32 Strref7 { get; set; }
        public Int32 Strref8 { get; set; }
        public Int32 Strref9 { get; set; }
        public Int32 Strref10 { get; set; }
        public Int32 Strref11 { get; set; }
        public Int32 Strref12 { get; set; }
        public Int32 Strref13 { get; set; }
        public Int32 Strref14 { get; set; }
        public Int32 Strref15 { get; set; }
        public Int32 Strref16 { get; set; }
        public Int32 Strref17 { get; set; }
        public Int32 Strref18 { get; set; }
        public Int32 Strref19 { get; set; }
        public Int32 Strref20 { get; set; }
        public Int32 Strref21 { get; set; }
        public Int32 Strref22 { get; set; }
        public Int32 Strref23 { get; set; }
        public Int32 Strref24 { get; set; }
        public Int32 Strref25 { get; set; }
        public Int32 Strref26 { get; set; }
        public Int32 Strref27 { get; set; }
        public Int32 Strref28 { get; set; }
        public Int32 Strref29 { get; set; }
        public Int32 Strref30 { get; set; }
        public Int32 Strref31 { get; set; }
        public Int32 Strref32 { get; set; }
        public Int32 Strref33 { get; set; }
        public Int32 Strref34 { get; set; }
        public Int32 Strref35 { get; set; }
        public Int32 Strref36 { get; set; }
        public Int32 Strref37 { get; set; }
        public Int32 Strref38 { get; set; }
        public Int32 Strref39 { get; set; }
        public Int32 Strref40 { get; set; }
        public Int32 Strref41 { get; set; }
        public Int32 Strref42 { get; set; }
        public Int32 Strref43 { get; set; }
        public Int32 Strref44 { get; set; }
        public Int32 Strref45 { get; set; }
        public Int32 Strref46 { get; set; }
        public Int32 Strref47 { get; set; }
        public Int32 Strref48 { get; set; }
        public Int32 Strref49 { get; set; }
        public Int32 Strref50 { get; set; }
        public Int32 Strref51 { get; set; }
        public Int32 Strref52 { get; set; }
        public Int32 Strref53 { get; set; }
        public Int32 Strref54 { get; set; }
        public Int32 Strref55 { get; set; }
        public Int32 Strref56 { get; set; }
        public Int32 Strref57 { get; set; }
        public Int32 Strref58 { get; set; }
        public Int32 Strref59 { get; set; }
        public Int32 Strref60 { get; set; }
        public Int32 Strref61 { get; set; }
        public Int32 Strref62 { get; set; }
        public Int32 Strref63 { get; set; }
        public Int32 Strref64 { get; set; }
        public Int32 Strref65 { get; set; }
        public Int32 Strref66 { get; set; }
        public Int32 Strref67 { get; set; }
        public Int32 Strref68 { get; set; }
        public Int32 Strref69 { get; set; }
        public Int32 Strref70 { get; set; }
        public Int32 Strref71 { get; set; }
        public Int32 Strref72 { get; set; }
        public Int32 Strref73 { get; set; }
        public Int32 Strref74 { get; set; }
        public Int32 Strref75 { get; set; }
        public Int32 Strref76 { get; set; }
        public Int32 Strref77 { get; set; }
        public Int32 Strref78 { get; set; }
        public Int32 Strref79 { get; set; }
        public Int32 Strref80 { get; set; }
        public Int32 Strref81 { get; set; }
        public Int32 Strref82 { get; set; }
        public Int32 Strref83 { get; set; }
        public Int32 Strref84 { get; set; }
        public Int32 Strref85 { get; set; }
        public Int32 Strref86 { get; set; }
        public Int32 Strref87 { get; set; }
        public Int32 Strref88 { get; set; }
        public Int32 Strref89 { get; set; }
        public Int32 Strref90 { get; set; }
        public Int32 Strref91 { get; set; }
        public Int32 Strref92 { get; set; }
        public Int32 Strref93 { get; set; }
        public Int32 Strref94 { get; set; }
        public Int32 Strref95 { get; set; }
        public Int32 Strref96 { get; set; }
        public Int32 Strref97 { get; set; }
        public Int32 Strref98 { get; set; }
        public Int32 Strref99 { get; set; }
        public Int32 Strref100 { get; set; }
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
        public UInt32 Kit { get; set; }
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