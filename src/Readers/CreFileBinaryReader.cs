﻿using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;
using System.Collections.Generic;
using System.IO;

namespace ii.InfinityEngine.Readers
{
    public class CreFileBinaryReader : ICreFileReader
    {
        public TlkFile TlkFile { get; set; }

        public CreFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public CreFile Read(Stream s)
        {
            using var br = new BinaryReader(s);
            var creFile = ParseFile(br);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            creFile.OriginalFile = ParseFile(br);
            return creFile;
        }

        private CreFile ParseFile(BinaryReader br)
        {
            var header = (CreHeaderBinary)Common.ReadStruct(br, typeof(CreHeaderBinary));

            if (header.ftype.ToString() != "CRE ")
                return new CreFile();

            List<CreKnownSpellBinary> creKnownSpells = [];
            List<CreSpellMemorisationInfoBinary> creSpellMemorisations = [];
            List<CreMemorisedSpellBinary> creMemorisedSpells = [];
            List<Eff1BinaryBinary> creEffects1 = [];
            List<EmbeddedEffBinary> creEffects2 = new List<EmbeddedEffBinary>();
            List<CreItemBinary> creItems = [];
            List<short> creItemSlots = [];

            br.BaseStream.Seek(header.KnownSpellsoffset, SeekOrigin.Begin);
            for (int i = 0; i < header.KnownSpellsCount; i++)
            {
                var knowSpell = (CreKnownSpellBinary)Common.ReadStruct(br, typeof(CreKnownSpellBinary));
                creKnownSpells.Add(knowSpell);
            }

            br.BaseStream.Seek(header.SpellMemorizationOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.SpellMemorizationCount; i++)
            {
                var creSpellMemorisation = (CreSpellMemorisationInfoBinary)Common.ReadStruct(br, typeof(CreSpellMemorisationInfoBinary));
                creSpellMemorisations.Add(creSpellMemorisation);
            }

            br.BaseStream.Seek(header.MemorizedSpellsOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.MemorizedSpellsCount; i++)
            {
                var creMemorisedSpell = (CreMemorisedSpellBinary)Common.ReadStruct(br, typeof(CreMemorisedSpellBinary));
                creMemorisedSpells.Add(creMemorisedSpell);
            }

            br.BaseStream.Seek(header.EffectOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.EffectCount; i++)
            {
                if (header.EffVersion == 0)
                {
                    var creEffect = (Eff1BinaryBinary)Common.ReadStruct(br, typeof(Eff1BinaryBinary));
                    creEffects1.Add(creEffect);
                }
                else
                {
                    var creEffect = (EmbeddedEffBinary)Common.ReadStruct(br, typeof(EmbeddedEffBinary));
                    creEffects2.Add(creEffect);
                }
            }

            br.BaseStream.Seek(header.ItemOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.ItemCount; i++)
            {
                var creItem = (CreItemBinary)Common.ReadStruct(br, typeof(CreItemBinary));
                creItems.Add(creItem);
            }

            br.BaseStream.Seek(header.ItemSlotOffset, SeekOrigin.Begin);
            for (int i = 0; i < 40; i++)
            {
                var creItemSlot = (short)Common.ReadStruct(br, typeof(short));
                creItemSlots.Add(creItemSlot);
            }

            var creFile = new CreFile();
            creFile.Flags.ShowLongname = (header.Flags & Common.Bit0) != 0;
            creFile.Flags.NoCorpse = (header.Flags & Common.Bit1) != 0;
            creFile.Flags.KeepCorpse = (header.Flags & Common.Bit2) != 0;
            creFile.Flags.OriginalFighter = (header.Flags & Common.Bit3) != 0;
            creFile.Flags.OriginalMage = (header.Flags & Common.Bit4) != 0;
            creFile.Flags.OriginalCleric = (header.Flags & Common.Bit5) != 0;
            creFile.Flags.OriginalThief = (header.Flags & Common.Bit6) != 0;
            creFile.Flags.OriginalDruid = (header.Flags & Common.Bit7) != 0;
            creFile.Flags.OriginalRanger = (header.Flags & Common.Bit8) != 0;
            creFile.Flags.FallenPaladin = (header.Flags & Common.Bit9) != 0;
            creFile.Flags.FallenRanger = (header.Flags & Common.Bit10) != 0;
            creFile.Flags.Exportable = (header.Flags & Common.Bit11) != 0;
            creFile.Flags.HideInjuryStatus = (header.Flags & Common.Bit12) != 0;
            creFile.Flags.QuestCritical = (header.Flags & Common.Bit13) != 0;
            creFile.Flags.MovingBetweenAreas = (header.Flags & Common.Bit14) != 0;
            creFile.Flags.BeenInParty = (header.Flags & Common.Bit15) != 0;
            creFile.Flags.RestoreItem = (header.Flags & Common.Bit16) != 0;
            creFile.Flags.ClearRestoreItem = (header.Flags & Common.Bit17) != 0;
            creFile.Flags.Bit18 = (header.Flags & Common.Bit18) != 0;
            creFile.Flags.Bit19 = (header.Flags & Common.Bit19) != 0;
            creFile.Flags.PreventExplodingDeath = (header.Flags & Common.Bit20) != 0;
            creFile.Flags.Bit21 = (header.Flags & Common.Bit21) != 0;
            creFile.Flags.DoNotApplyNightmareModeModifiers = (header.Flags & Common.Bit22) != 0;
            creFile.Flags.NotTooltip = (header.Flags & Common.Bit23) != 0;
            creFile.Flags.RandomWalkEa = (header.Flags & Common.Bit24) != 0;
            creFile.Flags.RandomWalkGeneral = (header.Flags & Common.Bit25) != 0;
            creFile.Flags.RandomWalkRace = (header.Flags & Common.Bit26) != 0;
            creFile.Flags.RandomWalkClass = (header.Flags & Common.Bit27) != 0;
            creFile.Flags.RandomWalkSpecific = (header.Flags & Common.Bit28) != 0;
            creFile.Flags.RandomWalkGender = (header.Flags & Common.Bit29) != 0;
            creFile.Flags.RandomWalkAlignment = (header.Flags & Common.Bit30) != 0;
            creFile.Flags.UnInterruptable = (header.Flags & Common.Bit31) != 0;
            creFile.AcidResistance = header.AcidResistance;
            creFile.Alignment = header.Alignment;
            creFile.Animation = header.Animation;
            creFile.ArmorClassEffective = header.ArmorClassEffective;
            creFile.ArmorClassNatural = header.ArmorClassNatural;
            creFile.ArmorColourIndex = header.ArmorColourIndex;
            creFile.Charisma = header.Charisma;
            creFile.Class = header.Class;
            creFile.ColdResistance = header.ColdResistance;
            creFile.Constitution = header.Constitution;
            creFile.CrushingModifuer = header.CrushingModifuer;
            creFile.CrushingResistance = header.CrushingResistance;
            creFile.CurrentHP = header.CurrentHP;
            creFile.DeathVariable = header.DeathVariable;
            creFile.DetectIllusion = header.DetectIllusion;
            creFile.Dexterity = header.Dexterity;
            creFile.DialogFile = header.DialogFile;
            creFile.EffectCount = header.EffectCount;
            creFile.EffectOffset = header.EffectOffset;
            creFile.EffVersion = header.EffVersion;
            creFile.ElectricityResistance = header.ElectricityResistance;
            creFile.EnemyAlly = header.EnemyAlly;
            creFile.Fatigue = header.Fatigue;
            creFile.FindTraps = header.FindTraps;
            creFile.FireResistance = header.FireResistance;
            creFile.Gender = header.Gender;
            creFile.General = header.General;
            creFile.GlobalActorEnumeration = header.GlobalActorEnumeration;
            creFile.Gold = header.Gold;
            creFile.HairColourIndex = header.HairColourIndex;
            creFile.HideInShadows = header.HideInShadows;
            creFile.Intelligence = header.Intelligence;
            creFile.Intoxication = header.Intoxication;
            creFile.ItemCount = header.ItemCount;
            creFile.ItemOffset = header.ItemOffset;
            creFile.ItemSlotOffset = header.ItemSlotOffset;
            creFile.Kit.Transmuter = (header.Kit & Common.Bit0) != 0;
            creFile.Kit.Necromancer = (header.Kit & Common.Bit1) != 0;
            creFile.Kit.Invoker = (header.Kit & Common.Bit2) != 0;
            creFile.Kit.Illusionist = (header.Kit & Common.Bit3) != 0;
            creFile.Kit.Enchanter = (header.Kit & Common.Bit4) != 0;
            creFile.Kit.Diviner = (header.Kit & Common.Bit5) != 0;
            creFile.Kit.Conjurer = (header.Kit & Common.Bit6) != 0;
            creFile.Kit.Abjurer = (header.Kit & Common.Bit7) != 0;
            creFile.Kit.ClericOfLathander = (header.Kit & Common.Bit8) != 0;
            creFile.Kit.ClericOfHelm = (header.Kit & Common.Bit9) != 0;
            creFile.Kit.ClericOfTalos = (header.Kit & Common.Bit10) != 0;
            creFile.Kit.Avenger = (header.Kit & Common.Bit11) != 0;
            creFile.Kit.Shapeshifter = (header.Kit & Common.Bit12) != 0;
            creFile.Kit.Totemic = (header.Kit & Common.Bit13) != 0;
            creFile.Kit.Skald = (header.Kit & Common.Bit14) != 0;
            creFile.Kit.Jester = (header.Kit & Common.Bit15) != 0;
            creFile.Kit.Blade = (header.Kit & Common.Bit16) != 0;
            creFile.Kit.Swashbuckler = (header.Kit & Common.Bit17) != 0;
            creFile.Kit.Bountyhunter = (header.Kit & Common.Bit18) != 0;
            creFile.Kit.Assassin = (header.Kit & Common.Bit19) != 0;
            creFile.Kit.Beastmaster = (header.Kit & Common.Bit20) != 0;
            creFile.Kit.Stalker = (header.Kit & Common.Bit21) != 0;
            creFile.Kit.Archer = (header.Kit & Common.Bit22) != 0;
            creFile.Kit.Undeadhunter = (header.Kit & Common.Bit23) != 0;
            creFile.Kit.Inquisitor = (header.Kit & Common.Bit24) != 0;
            creFile.Kit.Cavalier = (header.Kit & Common.Bit25) != 0;
            creFile.Kit.Kensai = (header.Kit & Common.Bit26) != 0;
            creFile.Kit.WizardSlayer = (header.Kit & Common.Bit27) != 0;
            creFile.Kit.Berserker = (header.Kit & Common.Bit28) != 0;
            creFile.Kit.TrueClass = (header.Kit & Common.Bit29) != 0;
            creFile.Kit.Barbarian = (header.Kit & Common.Bit30) != 0;
            creFile.Kit.None = (header.Kit & Common.Bit31) != 0;
            creFile.KnownSpellsCount = header.KnownSpellsCount;
            creFile.KnownSpellsoffset = header.KnownSpellsoffset;
            creFile.LargePortrait = header.LargePortrait;
            creFile.LeatherColourIndex = header.LeatherColourIndex;
            creFile.Level1 = header.Level1;
            creFile.Level2 = header.Level2;
            creFile.Level3 = header.Level3;
            creFile.LocalActorEnumeration = header.LocalActorEnumeration;
            creFile.LockPicking = header.LockPicking;
            creFile.LongName = Common.ReadString(header.LongName, TlkFile);
            creFile.Lore = header.Lore;
            creFile.Luck = header.Luck;
            creFile.MagicColdResistance = header.MagicColdResistance;
            creFile.MagicFireResistance = header.MagicFireResistance;
            creFile.MagicResistance = header.MagicResistance;
            creFile.MajorColourIndex = header.MajorColourIndex;
            creFile.MaximumHP = header.MaximumHP;
            creFile.MemorizedSpellsCount = header.MemorizedSpellsCount;
            creFile.MemorizedSpellsOffset = header.MemorizedSpellsOffset;
            creFile.MetalColourIndex = header.MetalColourIndex;
            creFile.MinorColourIndex = header.MinorColourIndex;
            creFile.MissileModifier = header.MissileModifier;
            creFile.MissileResistance = header.MissileResistance;
            creFile.Morale = header.Morale;
            creFile.MoraleBreak = header.MoraleBreak;
            creFile.MoraleRecoveryTime = header.MagicColdResistance;
            creFile.NumberOfAttacks = header.NumberOfAttacks;
            creFile.ObjectIdRef1 = header.ObjectIdRef1;
            creFile.ObjectIdRef2 = header.ObjectIdRef2;
            creFile.ObjectIdRef3 = header.ObjectIdRef3;
            creFile.ObjectIdRef4 = header.ObjectIdRef4;
            creFile.ObjectIdRef5 = header.ObjectIdRef5;
            creFile.PickPockets = header.PickPockets;
            creFile.PiercingModifier = header.PiercingModifier;
            creFile.PiercingResistance = header.PiercingResistance;
            creFile.PowerLevel = header.PowerLevel;
            creFile.Race = header.Race;
            creFile.RacialEnemy = header.RacialEnemy;
            creFile.Reputation = header.Reputation;
            creFile.SaveVsBreath = header.SaveVsBreath;
            creFile.SaveVsDeath = header.SaveVsDeath;
            creFile.SaveVsPolymorph = header.SaveVsPolymorph;
            creFile.SaveVsSpells = header.SaveVsSpells;
            creFile.SaveVsWands = header.SaveVsWands;
            creFile.ScriptClass = header.ScriptClass;
            creFile.ScriptDefault = header.ScriptDefault;
            creFile.ScriptGeneral = header.ScriptGeneral;
            creFile.ScriptOverride = header.ScriptOverride;
            creFile.ScriptRace = header.ScriptRace;
            creFile.SetTraps = header.SetTraps;
            creFile.Sex = header.Sex;
            creFile.ShortName = Common.ReadString(header.ShortName, TlkFile);
            creFile.SkinColourIndex = header.SkinColourIndex;
            creFile.SlashingModifier = header.SlashingModifier;
            creFile.SlashingResistance = header.SlashingResistance;
            creFile.SmallPortrait = header.SmallPortrait;
            creFile.Specific = header.Specific;
            creFile.SpellMemorizationCount = header.SpellMemorizationCount;
            creFile.SpellMemorizationOffset = header.SpellMemorizationOffset;
            creFile.StatusFlags.Sleeping = (header.StatusFlags & Common.Bit0) != 0;
            creFile.StatusFlags.Berserk = (header.StatusFlags & Common.Bit1) != 0;
            creFile.StatusFlags.Panic = (header.StatusFlags & Common.Bit2) != 0;
            creFile.StatusFlags.StoneDeath = (header.StatusFlags & Common.Bit3) != 0;
            creFile.StatusFlags.Infravision = (header.StatusFlags & Common.Bit4) != 0;
            creFile.StatusFlags.Helpless = (header.StatusFlags & Common.Bit5) != 0;
            creFile.StatusFlags.FrozenDeath = (header.StatusFlags & Common.Bit6) != 0;
            creFile.StatusFlags.StoneDeath = (header.StatusFlags & Common.Bit7) != 0;
            creFile.StatusFlags.ExplodingDeath = (header.StatusFlags & Common.Bit8) != 0;
            creFile.StatusFlags.FlameDeath = (header.StatusFlags & Common.Bit9) != 0;
            creFile.StatusFlags.AcidDeath = (header.StatusFlags & Common.Bit10) != 0;
            creFile.StatusFlags.Dead = (header.StatusFlags & Common.Bit11) != 0;
            creFile.StatusFlags.Silenced = (header.StatusFlags & Common.Bit12) != 0;
            creFile.StatusFlags.Charmed = (header.StatusFlags & Common.Bit13) != 0;
            creFile.StatusFlags.Poisoned = (header.StatusFlags & Common.Bit14) != 0;
            creFile.StatusFlags.Hasted = (header.StatusFlags & Common.Bit15) != 0;
            creFile.StatusFlags.Slowed = (header.StatusFlags & Common.Bit16) != 0;
            creFile.StatusFlags.Infravision = (header.StatusFlags & Common.Bit17) != 0;
            creFile.StatusFlags.Blind = (header.StatusFlags & Common.Bit18) != 0;
            creFile.StatusFlags.Diseased = (header.StatusFlags & Common.Bit19) != 0;
            creFile.StatusFlags.Feebleminded = (header.StatusFlags & Common.Bit20) != 0;
            creFile.StatusFlags.Nondetection = (header.StatusFlags & Common.Bit21) != 0;
            creFile.StatusFlags.ImprovedInvisibility = (header.StatusFlags & Common.Bit22) != 0;
            creFile.StatusFlags.Bless = (header.StatusFlags & Common.Bit23) != 0;
            creFile.StatusFlags.Chant = (header.StatusFlags & Common.Bit24) != 0;
            creFile.StatusFlags.DrawUponHolyMight = (header.StatusFlags & Common.Bit25) != 0;
            creFile.StatusFlags.Luck = (header.StatusFlags & Common.Bit26) != 0;
            creFile.StatusFlags.Aid = (header.StatusFlags & Common.Bit27) != 0;
            creFile.StatusFlags.ChantBad = (header.StatusFlags & Common.Bit28) != 0;
            creFile.StatusFlags.Blur = (header.StatusFlags & Common.Bit29) != 0;
            creFile.StatusFlags.MirrorImage = (header.StatusFlags & Common.Bit30) != 0;
            creFile.StatusFlags.Confused = (header.StatusFlags & Common.Bit31) != 0;
            creFile.Stealth = header.Stealth;
            creFile.Strength = header.Strength;
            creFile.StrengthBonus = header.StrengthBonus;
            creFile.Strref1 = Common.ReadString(header.Strref1, TlkFile);
            creFile.Strref2 = Common.ReadString(header.Strref2, TlkFile);
            creFile.Strref3 = Common.ReadString(header.Strref3, TlkFile);
            creFile.Strref4 = Common.ReadString(header.Strref4, TlkFile);
            creFile.Strref5 = Common.ReadString(header.Strref5, TlkFile);
            creFile.Strref6 = Common.ReadString(header.Strref6, TlkFile);
            creFile.Strref7 = Common.ReadString(header.Strref7, TlkFile);
            creFile.Strref8 = Common.ReadString(header.Strref8, TlkFile);
            creFile.Strref9 = Common.ReadString(header.Strref9, TlkFile);
            creFile.Strref10 = Common.ReadString(header.Strref10, TlkFile);
            creFile.Strref11 = Common.ReadString(header.Strref11, TlkFile);
            creFile.Strref12 = Common.ReadString(header.Strref12, TlkFile);
            creFile.Strref13 = Common.ReadString(header.Strref13, TlkFile);
            creFile.Strref14 = Common.ReadString(header.Strref14, TlkFile);
            creFile.Strref15 = Common.ReadString(header.Strref15, TlkFile);
            creFile.Strref16 = Common.ReadString(header.Strref16, TlkFile);
            creFile.Strref17 = Common.ReadString(header.Strref17, TlkFile);
            creFile.Strref18 = Common.ReadString(header.Strref18, TlkFile);
            creFile.Strref19 = Common.ReadString(header.Strref19, TlkFile);
            creFile.Strref20 = Common.ReadString(header.Strref20, TlkFile);
            creFile.Strref21 = Common.ReadString(header.Strref21, TlkFile);
            creFile.Strref22 = Common.ReadString(header.Strref22, TlkFile);
            creFile.Strref23 = Common.ReadString(header.Strref23, TlkFile);
            creFile.Strref24 = Common.ReadString(header.Strref24, TlkFile);
            creFile.Strref25 = Common.ReadString(header.Strref25, TlkFile);
            creFile.Strref26 = Common.ReadString(header.Strref26, TlkFile);
            creFile.Strref27 = Common.ReadString(header.Strref27, TlkFile);
            creFile.Strref28 = Common.ReadString(header.Strref28, TlkFile);
            creFile.Strref29 = Common.ReadString(header.Strref29, TlkFile);
            creFile.Strref30 = Common.ReadString(header.Strref30, TlkFile);
            creFile.Strref31 = Common.ReadString(header.Strref31, TlkFile);
            creFile.Strref32 = Common.ReadString(header.Strref32, TlkFile);
            creFile.Strref33 = Common.ReadString(header.Strref33, TlkFile);
            creFile.Strref34 = Common.ReadString(header.Strref34, TlkFile);
            creFile.Strref35 = Common.ReadString(header.Strref35, TlkFile);
            creFile.Strref36 = Common.ReadString(header.Strref36, TlkFile);
            creFile.Strref37 = Common.ReadString(header.Strref37, TlkFile);
            creFile.Strref38 = Common.ReadString(header.Strref38, TlkFile);
            creFile.Strref39 = Common.ReadString(header.Strref39, TlkFile);
            creFile.Strref40 = Common.ReadString(header.Strref40, TlkFile);
            creFile.Strref41 = Common.ReadString(header.Strref41, TlkFile);
            creFile.Strref42 = Common.ReadString(header.Strref42, TlkFile);
            creFile.Strref43 = Common.ReadString(header.Strref43, TlkFile);
            creFile.Strref44 = Common.ReadString(header.Strref44, TlkFile);
            creFile.Strref45 = Common.ReadString(header.Strref45, TlkFile);
            creFile.Strref46 = Common.ReadString(header.Strref46, TlkFile);
            creFile.Strref47 = Common.ReadString(header.Strref47, TlkFile);
            creFile.Strref48 = Common.ReadString(header.Strref48, TlkFile);
            creFile.Strref49 = Common.ReadString(header.Strref49, TlkFile);
            creFile.Strref50 = Common.ReadString(header.Strref50, TlkFile);
            creFile.Strref51 = Common.ReadString(header.Strref51, TlkFile);
            creFile.Strref52 = Common.ReadString(header.Strref52, TlkFile);
            creFile.Strref53 = Common.ReadString(header.Strref53, TlkFile);
            creFile.Strref54 = Common.ReadString(header.Strref54, TlkFile);
            creFile.Strref55 = Common.ReadString(header.Strref55, TlkFile);
            creFile.Strref56 = Common.ReadString(header.Strref56, TlkFile);
            creFile.Strref57 = Common.ReadString(header.Strref57, TlkFile);
            creFile.Strref58 = Common.ReadString(header.Strref58, TlkFile);
            creFile.Strref59 = Common.ReadString(header.Strref59, TlkFile);
            creFile.Strref60 = Common.ReadString(header.Strref60, TlkFile);
            creFile.Strref61 = Common.ReadString(header.Strref61, TlkFile);
            creFile.Strref62 = Common.ReadString(header.Strref62, TlkFile);
            creFile.Strref63 = Common.ReadString(header.Strref63, TlkFile);
            creFile.Strref64 = Common.ReadString(header.Strref64, TlkFile);
            creFile.Strref65 = Common.ReadString(header.Strref65, TlkFile);
            creFile.Strref66 = Common.ReadString(header.Strref66, TlkFile);
            creFile.Strref67 = Common.ReadString(header.Strref67, TlkFile);
            creFile.Strref68 = Common.ReadString(header.Strref68, TlkFile);
            creFile.Strref69 = Common.ReadString(header.Strref69, TlkFile);
            creFile.Strref70 = Common.ReadString(header.Strref70, TlkFile);
            creFile.Strref71 = Common.ReadString(header.Strref71, TlkFile);
            creFile.Strref72 = Common.ReadString(header.Strref72, TlkFile);
            creFile.Strref73 = Common.ReadString(header.Strref73, TlkFile);
            creFile.Strref74 = Common.ReadString(header.Strref74, TlkFile);
            creFile.Strref75 = Common.ReadString(header.Strref75, TlkFile);
            creFile.Strref76 = Common.ReadString(header.Strref76, TlkFile);
            creFile.Strref77 = Common.ReadString(header.Strref77, TlkFile);
            creFile.Strref78 = Common.ReadString(header.Strref78, TlkFile);
            creFile.Strref79 = Common.ReadString(header.Strref79, TlkFile);
            creFile.Strref80 = Common.ReadString(header.Strref80, TlkFile);
            creFile.Strref81 = Common.ReadString(header.Strref81, TlkFile);
            creFile.Strref82 = Common.ReadString(header.Strref82, TlkFile);
            creFile.Strref83 = Common.ReadString(header.Strref83, TlkFile);
            creFile.Strref84 = Common.ReadString(header.Strref84, TlkFile);
            creFile.Strref85 = Common.ReadString(header.Strref85, TlkFile);
            creFile.Strref86 = Common.ReadString(header.Strref86, TlkFile);
            creFile.Strref87 = Common.ReadString(header.Strref87, TlkFile);
            creFile.Strref88 = Common.ReadString(header.Strref88, TlkFile);
            creFile.Strref89 = Common.ReadString(header.Strref89, TlkFile);
            creFile.Strref90 = Common.ReadString(header.Strref90, TlkFile);
            creFile.Strref91 = Common.ReadString(header.Strref91, TlkFile);
            creFile.Strref92 = Common.ReadString(header.Strref92, TlkFile);
            creFile.Strref93 = Common.ReadString(header.Strref93, TlkFile);
            creFile.Strref94 = Common.ReadString(header.Strref94, TlkFile);
            creFile.Strref95 = Common.ReadString(header.Strref95, TlkFile);
            creFile.Strref96 = Common.ReadString(header.Strref96, TlkFile);
            creFile.Strref97 = Common.ReadString(header.Strref97, TlkFile);
            creFile.Strref98 = Common.ReadString(header.Strref98, TlkFile);
            creFile.Strref99 = Common.ReadString(header.Strref99, TlkFile);
            creFile.Strref100 = Common.ReadString(header.Strref100, TlkFile);
            creFile.Thac0 = header.Thac0;
            creFile.Tracking = header.Tracking;
            creFile.Unused1Proficiency = header.Unused1Proficiency;
            creFile.Unused2Proficiency = header.Unused2Proficiency;
            creFile.Unused3Proficiency = header.Unused3Proficiency;
            creFile.Unused4Proficiency = header.Unused4Proficiency;
            creFile.Unused5Proficiency = header.Unused5Proficiency;
            creFile.Unused6Proficiency = header.Unused6Proficiency;
            creFile.Unused7Proficiency = header.Unused7Proficiency;
            creFile.NightmareMode = header.NightmareMode;
            creFile.Translucency = header.Translucency;
            creFile.ReputationKill = header.ReputationKill;
            creFile.ReputationJoin = header.ReputationJoin;
            creFile.ReputationLeave = header.ReputationLeave;
            creFile.TurnUndead = header.TurnUndead;
            creFile.Tracking = header.Tracking;
            creFile.TrackingTarget = header.TrackingTarget;
            creFile.UnusedAxeProficiency = header.UnusedAxe;
            creFile.UnusedBluntProficiency = header.UnusedBlunt;
            creFile.UnusedBowProficiency = header.UnusedBows;
            creFile.UnusedLargeSwordProficiency = header.UnusedLargeSwords;
            creFile.UnusedMissileProficiency = header.UnusedMissile;
            creFile.UnusedSmallSwordProficiency = header.UnusedSmallSwords;
            creFile.UnusedSpearProficiency = header.UnusedSpears;
            creFile.UnusedSpikedProficiency = header.UnusedSpiked;
            creFile.NightmareMode = header.NightmareMode;
            creFile.Translucency = header.Translucency;
            creFile.ReputationKill = header.ReputationKill;
            creFile.ReputationJoin = header.ReputationJoin;
            creFile.ReputationLeave = header.ReputationLeave;
            creFile.TurnUndead = header.TurnUndead;
            creFile.Tracking = header.Tracking;
            creFile.TrackingTarget = header.TrackingTarget;
            creFile.Wisdom = header.Wisdom;
            creFile.XPReward = header.XPReward;

            foreach (var creEffect in creEffects1)
            {
                var creEffect2 = new Eff1File();
                creEffect2.DiceSides = creEffect.DiceSides;
                creEffect2.DiceThrown = creEffect.DiceThrown;
                creEffect2.Resistance = creEffect.Resistance;
                creEffect2.Duration = creEffect.Duration;
                creEffect2.Opcode = creEffect.Opcode;
                creEffect2.Parameter1 = creEffect.Parameter1;
                creEffect2.Parameter2 = creEffect.Parameter2;
                creEffect2.Power = creEffect.Power;
                creEffect2.Probability1 = creEffect.Probability1;
                creEffect2.Probability2 = creEffect.Probability2;
                creEffect2.Resource = creEffect.Resource;
                creEffect2.SavingThrowBonus = creEffect.SavingThrowBonus;
                creEffect2.SavingThrowType = creEffect.SavingThrowType;
                creEffect2.TargetType = creEffect.TargetType;
                creEffect2.TimingMode = (OpcodeTargetType)creEffect.TimingMode;
                creEffect2.Unknown = creEffect.Unknown;
                creFile.Effects1.Add(creEffect2);
            }

            foreach (var creEffect in creEffects2)
            {
                var creEffect2 = new EmbeddedEffBinary();
                creEffect2.CasterLevel = creEffect.CasterLevel;
                creEffect2.CasterXCoordinate = creEffect.CasterXCoordinate;
                creEffect2.CasterYCoordinate = creEffect.CasterYCoordinate;
                creEffect2.DiceSides = creEffect.DiceSides;
                creEffect2.DiceThrown = creEffect.DiceThrown;
                creEffect2.Duration = creEffect.Duration;
                creEffect2.HighestAffectedLevelFromParent = creEffect.HighestAffectedLevelFromParent;
                creEffect2.LowestAffectedLevelFromParent = creEffect.LowestAffectedLevelFromParent;
                creEffect2.Opcode = creEffect.Opcode;
                creEffect2.Parameter1 = creEffect.Parameter1;
                creEffect2.Parameter2 = creEffect.Parameter2;
                creEffect2.Parameter3 = creEffect.Parameter3;
                creEffect2.Parameter4 = creEffect.Parameter4;
                creEffect2.ParentResource = creEffect.ParentResource;
                creEffect2.ParentResourceSlot = creEffect.ParentResourceSlot;
                creEffect2.Power = creEffect.Power;
                creEffect2.PrimaryType = creEffect.PrimaryType;
                creEffect2.Probability1 = creEffect.Probability1;
                creEffect2.Probability2 = creEffect.Probability2;
                creEffect2.Projectile = creEffect.Projectile;
                creEffect2.Resistance = creEffect.Resistance;
                creEffect2.Resource = creEffect.Resource;
                creEffect2.Resource2 = creEffect.Resource2;
                creEffect2.Resource3 = creEffect.Resource3;
                creEffect2.ResourceTypeFromParent = creEffect.ResourceTypeFromParent;
                creEffect2.SavingThrowBonus = creEffect.SavingThrowBonus;
                creEffect2.SavingThrowType = creEffect.SavingThrowType;
                creEffect2.SecondaryType = creEffect.SecondaryType;
                creEffect2.SetLocalVariableIfNonExistant = creEffect.SetLocalVariableIfNonExistant;
                creEffect2.TargetType = creEffect.TargetType;
                creEffect2.TargetXCoordinate = creEffect.TargetXCoordinate;
                creEffect2.TargetYCoordinate = creEffect.TargetYCoordinate;
                creEffect2.TimingMode = creEffect.TimingMode;
                creEffect2.Unknown = creEffect.Unknown;
                creEffect2.Unknown2 = creEffect.Unknown2;
                creEffect2.Unknown3 = creEffect.Unknown3;
                creEffect2.Unknown4 = creEffect.Unknown4;
                creEffect2.Unknown5 = creEffect.Unknown5;
                creEffect2.Unknownd4_1 = creEffect.Unknownd4_1;
                creEffect2.Unknownd4_2 = creEffect.Unknownd4_2;
                creEffect2.Unknownd4_3 = creEffect.Unknownd4_3;
                creEffect2.Unknownd4_4 = creEffect.Unknownd4_4;
                creEffect2.Unknownd4_5 = creEffect.Unknownd4_5;
                creEffect2.Unknownd4_6 = creEffect.Unknownd4_6;
                creEffect2.Unknownd4_7 = creEffect.Unknownd4_7;
                creEffect2.Unknownd4_8 = creEffect.Unknownd4_8;
                creEffect2.Unknownd4_9 = creEffect.Unknownd4_9;
                creEffect2.Unknownd4_10 = creEffect.Unknownd4_10;
                creEffect2.Unknownd4_11 = creEffect.Unknownd4_11;
                creEffect2.Unknownd4_12 = creEffect.Unknownd4_12;
                creEffect2.Unknownd4_13 = creEffect.Unknownd4_13;
                creEffect2.Unknownd4_14 = creEffect.Unknownd4_14;
                creEffect2.Unknownd4_15 = creEffect.Unknownd4_15;
                creEffect2.Variable = creEffect.Variable;
                creFile.Effects2.Add(creEffect2);
            }


            foreach (var info in creSpellMemorisations)
            {
                for (int i = 0; i < info.SpellCount; i++)
                {
                    //Note: This means we cannot set NumberOfSpellSlots (after effects)
                    var memorisedSpell = creMemorisedSpells[info.SpellOffset + i];
                    var memorisedSpell2 = new CreMemorisedSpell2();
                    memorisedSpell2.Filename = memorisedSpell.Filename;
                    memorisedSpell2.IsMemorised = memorisedSpell.Memorised != 0;

                    if (info.SpellLevel == 0 && info.SpellType == 0)
                        creFile.MemorisedSpells.PriestLevel1.Add(memorisedSpell2);
                    if (info.SpellLevel == 1 && info.SpellType == 0)
                        creFile.MemorisedSpells.PriestLevel2.Add(memorisedSpell2);
                    if (info.SpellLevel == 2 && info.SpellType == 0)
                        creFile.MemorisedSpells.PriestLevel3.Add(memorisedSpell2);
                    if (info.SpellLevel == 3 && info.SpellType == 0)
                        creFile.MemorisedSpells.PriestLevel4.Add(memorisedSpell2);
                    if (info.SpellLevel == 4 && info.SpellType == 0)
                        creFile.MemorisedSpells.PriestLevel5.Add(memorisedSpell2);
                    if (info.SpellLevel == 5 && info.SpellType == 0)
                        creFile.MemorisedSpells.PriestLevel6.Add(memorisedSpell2);
                    if (info.SpellLevel == 6 && info.SpellType == 0)
                        creFile.MemorisedSpells.PriestLevel7.Add(memorisedSpell2);

                    if (info.SpellLevel == 0 && info.SpellType == 1)
                        creFile.MemorisedSpells.MageLevel1.Add(memorisedSpell2);
                    if (info.SpellLevel == 1 && info.SpellType == 1)
                        creFile.MemorisedSpells.MageLevel2.Add(memorisedSpell2);
                    if (info.SpellLevel == 2 && info.SpellType == 1)
                        creFile.MemorisedSpells.MageLevel3.Add(memorisedSpell2);
                    if (info.SpellLevel == 3 && info.SpellType == 1)
                        creFile.MemorisedSpells.MageLevel4.Add(memorisedSpell2);
                    if (info.SpellLevel == 4 && info.SpellType == 1)
                        creFile.MemorisedSpells.MageLevel5.Add(memorisedSpell2);
                    if (info.SpellLevel == 5 && info.SpellType == 1)
                        creFile.MemorisedSpells.MageLevel6.Add(memorisedSpell2);
                    if (info.SpellLevel == 6 && info.SpellType == 1)
                        creFile.MemorisedSpells.MageLevel7.Add(memorisedSpell2);
                    if (info.SpellLevel == 7 && info.SpellType == 1)
                        creFile.MemorisedSpells.MageLevel8.Add(memorisedSpell2);
                    if (info.SpellLevel == 8 && info.SpellType == 1)
                        creFile.MemorisedSpells.MageLevel9.Add(memorisedSpell2);

                    if (info.SpellLevel == 0 && info.SpellType == 2)
                        creFile.MemorisedSpells.Innate.Add(memorisedSpell2);
                    //We've assumed all innates are level 1
                }
            }


            foreach (var knownSpell in creKnownSpells)
            {
                var knownSpell2 = new CreKnownSpell2();
                knownSpell2.Filename = knownSpell.Filename;
                if (knownSpell.SpellLevel == 0 && knownSpell.SpellType == 0)
                    creFile.KnownSpells.PriestLevel1.Add(knownSpell2);
                if (knownSpell.SpellLevel == 1 && knownSpell.SpellType == 0)
                    creFile.KnownSpells.PriestLevel2.Add(knownSpell2);
                if (knownSpell.SpellLevel == 2 && knownSpell.SpellType == 0)
                    creFile.KnownSpells.PriestLevel3.Add(knownSpell2);
                if (knownSpell.SpellLevel == 3 && knownSpell.SpellType == 0)
                    creFile.KnownSpells.PriestLevel4.Add(knownSpell2);
                if (knownSpell.SpellLevel == 4 && knownSpell.SpellType == 0)
                    creFile.KnownSpells.PriestLevel5.Add(knownSpell2);
                if (knownSpell.SpellLevel == 5 && knownSpell.SpellType == 0)
                    creFile.KnownSpells.PriestLevel6.Add(knownSpell2);
                if (knownSpell.SpellLevel == 6 && knownSpell.SpellType == 0)
                    creFile.KnownSpells.PriestLevel7.Add(knownSpell2);

                if (knownSpell.SpellLevel == 0 && knownSpell.SpellType == 1)
                    creFile.KnownSpells.MageLevel1.Add(knownSpell2);
                if (knownSpell.SpellLevel == 1 && knownSpell.SpellType == 1)
                    creFile.KnownSpells.MageLevel2.Add(knownSpell2);
                if (knownSpell.SpellLevel == 2 && knownSpell.SpellType == 1)
                    creFile.KnownSpells.MageLevel3.Add(knownSpell2);
                if (knownSpell.SpellLevel == 3 && knownSpell.SpellType == 1)
                    creFile.KnownSpells.MageLevel4.Add(knownSpell2);
                if (knownSpell.SpellLevel == 4 && knownSpell.SpellType == 1)
                    creFile.KnownSpells.MageLevel5.Add(knownSpell2);
                if (knownSpell.SpellLevel == 5 && knownSpell.SpellType == 1)
                    creFile.KnownSpells.MageLevel6.Add(knownSpell2);
                if (knownSpell.SpellLevel == 6 && knownSpell.SpellType == 1)
                    creFile.KnownSpells.MageLevel7.Add(knownSpell2);
                if (knownSpell.SpellLevel == 7 && knownSpell.SpellType == 1)
                    creFile.KnownSpells.MageLevel8.Add(knownSpell2);
                if (knownSpell.SpellLevel == 8 && knownSpell.SpellType == 1)
                    creFile.KnownSpells.MageLevel9.Add(knownSpell2);

                if (knownSpell.SpellLevel == 0 && knownSpell.SpellType == 2)
                    creFile.KnownSpells.Innate.Add(knownSpell2);
                //We've assumed all innates are level 1
            }

            var interimItems = new List<CreItem2>();
            foreach (var creItem in creItems)
            {
                var creItem2 = new CreItem2();
                creItem2.Flags = new CreatureItemFlags();
                creItem2.Charges1 = creItem.Charges1;
                creItem2.Charges2 = creItem.Charges2;
                creItem2.Charges3 = creItem.Charges3;
                creItem2.ExpiryHour = creItem.ExpiryHour;
                creItem2.ExpiryValue = creItem.ExpiryValue;
                creItem2.Filename = creItem.Filename;
                creItem2.Flags.IsIdentified = (creItem.Flags & Common.Bit0) != 0;
                creItem2.Flags.IsStealable = (creItem.Flags & Common.Bit1) != 0;
                creItem2.Flags.IsStolen = (creItem.Flags & Common.Bit2) != 0;
                creItem2.Flags.IsDroppable = (creItem.Flags & Common.Bit3) != 0;
                creItem2.Flags.Bit4 = (creItem.Flags & Common.Bit4) != 0;
                creItem2.Flags.Bit5 = (creItem.Flags & Common.Bit5) != 0;
                creItem2.Flags.Bit6 = (creItem.Flags & Common.Bit6) != 0;
                creItem2.Flags.Bit7 = (creItem.Flags & Common.Bit7) != 0;
                creItem2.Flags.Bit8 = (creItem.Flags & Common.Bit8) != 0;
                creItem2.Flags.Bit9 = (creItem.Flags & Common.Bit9) != 0;
                creItem2.Flags.Bit10 = (creItem.Flags & Common.Bit10) != 0;
                creItem2.Flags.Bit11 = (creItem.Flags & Common.Bit11) != 0;
                creItem2.Flags.Bit12 = (creItem.Flags & Common.Bit12) != 0;
                creItem2.Flags.Bit13 = (creItem.Flags & Common.Bit13) != 0;
                creItem2.Flags.Bit14 = (creItem.Flags & Common.Bit14) != 0;
                creItem2.Flags.Bit15 = (creItem.Flags & Common.Bit15) != 0;
                creItem2.Flags.Bit16 = (creItem.Flags & Common.Bit16) != 0;
                creItem2.Flags.Bit17 = (creItem.Flags & Common.Bit17) != 0;
                creItem2.Flags.Bit18 = (creItem.Flags & Common.Bit18) != 0;
                creItem2.Flags.Bit19 = (creItem.Flags & Common.Bit19) != 0;
                creItem2.Flags.Bit20 = (creItem.Flags & Common.Bit20) != 0;
                creItem2.Flags.Bit21 = (creItem.Flags & Common.Bit21) != 0;
                creItem2.Flags.Bit22 = (creItem.Flags & Common.Bit22) != 0;
                creItem2.Flags.Bit23 = (creItem.Flags & Common.Bit23) != 0;
                creItem2.Flags.Bit24 = (creItem.Flags & Common.Bit24) != 0;
                creItem2.Flags.Bit25 = (creItem.Flags & Common.Bit25) != 0;
                creItem2.Flags.Bit26 = (creItem.Flags & Common.Bit26) != 0;
                creItem2.Flags.Bit27 = (creItem.Flags & Common.Bit27) != 0;
                creItem2.Flags.Bit28 = (creItem.Flags & Common.Bit28) != 0;
                creItem2.Flags.Bit29 = (creItem.Flags & Common.Bit29) != 0;
                creItem2.Flags.Bit30 = (creItem.Flags & Common.Bit30) != 0;
                creItem2.Flags.Bit31 = (creItem.Flags & Common.Bit31) != 0;
                interimItems.Add(creItem2);
            }

            creFile.Items.Helmet = creItemSlots[0] != -1 ? interimItems[creItemSlots[0]] : creFile.Items.Helmet;
            creFile.Items.Armor = creItemSlots[1] != -1 ? interimItems[creItemSlots[1]] : creFile.Items.Armor;
            creFile.Items.Shield = creItemSlots[2] != -1 ? interimItems[creItemSlots[2]] : creFile.Items.Shield;
            creFile.Items.Gloves = creItemSlots[3] != -1 ? interimItems[creItemSlots[3]] : creFile.Items.Gloves;
            creFile.Items.RingLeft = creItemSlots[4] != -1 ? interimItems[creItemSlots[4]] : creFile.Items.RingLeft;
            creFile.Items.RingRight = creItemSlots[5] != -1 ? interimItems[creItemSlots[5]] : creFile.Items.RingRight;
            creFile.Items.Amulet = creItemSlots[6] != -1 ? interimItems[creItemSlots[6]] : creFile.Items.Amulet;
            creFile.Items.Belt = creItemSlots[7] != -1 ? interimItems[creItemSlots[7]] : creFile.Items.Belt;
            creFile.Items.Boots = creItemSlots[8] != -1 ? interimItems[creItemSlots[8]] : creFile.Items.Boots;
            creFile.Items.Weapon1 = creItemSlots[9] != -1 ? interimItems[creItemSlots[9]] : creFile.Items.Weapon1;
            creFile.Items.Weapon2 = creItemSlots[10] != -1 ? interimItems[creItemSlots[10]] : creFile.Items.Weapon2;
            creFile.Items.Weapon3 = creItemSlots[11] != -1 ? interimItems[creItemSlots[11]] : creFile.Items.Weapon3;
            creFile.Items.Weapon4 = creItemSlots[12] != -1 ? interimItems[creItemSlots[12]] : creFile.Items.Weapon4;
            creFile.Items.Quiver1 = creItemSlots[13] != -1 ? interimItems[creItemSlots[13]] : creFile.Items.Quiver1;
            creFile.Items.Quiver2 = creItemSlots[14] != -1 ? interimItems[creItemSlots[14]] : creFile.Items.Quiver2;
            creFile.Items.Quiver3 = creItemSlots[15] != -1 ? interimItems[creItemSlots[15]] : creFile.Items.Quiver3;
            creFile.Items.Quiver4 = creItemSlots[16] != -1 ? interimItems[creItemSlots[16]] : creFile.Items.Quiver4;
            creFile.Items.Cloak = creItemSlots[17] != -1 ? interimItems[creItemSlots[17]] : creFile.Items.Cloak;
            creFile.Items.QuickItem1 = creItemSlots[18] != -1 ? interimItems[creItemSlots[18]] : creFile.Items.QuickItem1;
            creFile.Items.QuickItem2 = creItemSlots[19] != -1 ? interimItems[creItemSlots[19]] : creFile.Items.QuickItem2;
            creFile.Items.QuickItem3 = creItemSlots[20] != -1 ? interimItems[creItemSlots[20]] : creFile.Items.QuickItem3;
            creFile.Items.InventoryItem1 = creItemSlots[21] != -1 ? interimItems[creItemSlots[21]] : creFile.Items.InventoryItem1;
            creFile.Items.InventoryItem2 = creItemSlots[22] != -1 ? interimItems[creItemSlots[22]] : creFile.Items.InventoryItem2;
            creFile.Items.InventoryItem3 = creItemSlots[23] != -1 ? interimItems[creItemSlots[23]] : creFile.Items.InventoryItem3;
            creFile.Items.InventoryItem4 = creItemSlots[24] != -1 ? interimItems[creItemSlots[24]] : creFile.Items.InventoryItem4;
            creFile.Items.InventoryItem5 = creItemSlots[25] != -1 ? interimItems[creItemSlots[25]] : creFile.Items.InventoryItem5;
            creFile.Items.InventoryItem6 = creItemSlots[26] != -1 ? interimItems[creItemSlots[26]] : creFile.Items.InventoryItem6;
            creFile.Items.InventoryItem7 = creItemSlots[27] != -1 ? interimItems[creItemSlots[27]] : creFile.Items.InventoryItem7;
            creFile.Items.InventoryItem8 = creItemSlots[28] != -1 ? interimItems[creItemSlots[28]] : creFile.Items.InventoryItem8;
            creFile.Items.InventoryItem9 = creItemSlots[29] != -1 ? interimItems[creItemSlots[29]] : creFile.Items.InventoryItem9;
            creFile.Items.InventoryItem10 = creItemSlots[30] != -1 ? interimItems[creItemSlots[30]] : creFile.Items.InventoryItem10;
            creFile.Items.InventoryItem11 = creItemSlots[31] != -1 ? interimItems[creItemSlots[31]] : creFile.Items.InventoryItem11;
            creFile.Items.InventoryItem12 = creItemSlots[32] != -1 ? interimItems[creItemSlots[32]] : creFile.Items.InventoryItem12;
            creFile.Items.InventoryItem13 = creItemSlots[33] != -1 ? interimItems[creItemSlots[33]] : creFile.Items.InventoryItem13;
            creFile.Items.InventoryItem14 = creItemSlots[34] != -1 ? interimItems[creItemSlots[34]] : creFile.Items.InventoryItem14;
            creFile.Items.InventoryItem15 = creItemSlots[35] != -1 ? interimItems[creItemSlots[35]] : creFile.Items.InventoryItem15;
            creFile.Items.InventoryItem16 = creItemSlots[36] != -1 ? interimItems[creItemSlots[36]] : creFile.Items.InventoryItem16;
            creFile.Items.MagicWeapon = creItemSlots[37] != -1 ? interimItems[creItemSlots[37]] : creFile.Items.MagicWeapon;
            creFile.Items.SelectedWeapon = creItemSlots[38];
            creFile.Items.SelectedWeaponAbility = creItemSlots[39];

            creFile.Checksum = HashGenerator.GenerateKey(creFile);
            return creFile;
        }
    }
}