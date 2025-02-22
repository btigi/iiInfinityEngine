using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class SplFile : IEFile
    {
        public List<SplExtendedHeader> splExtendedHeader = [];
        public List<SplFeatureBlock> splFeatureBlocks = [];

        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private readonly IEFileType fileType = IEFileType.Spl;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public SplFile()
        {
            Flags = new SpellFlags();
        }

        public IEString UnidentifiedName { get; set; }
        public IEString IdentifiedName { get; set; }
        public array8 CompletionSound { get; set; }
        public SpellFlags Flags { get; set; }
        public SpellType SpellType { get; set; }
        public Int32 ExclusionFlags { get; set; }
        public Int16 CastingGraphic { get; set; }
        public byte Unknown24 { get; set; }
        public byte PrimaryType { get; set; }
        public byte Unknown26 { get; set; }
        public byte SecondaryType { get; set; }
        public byte Unused28 { get; set; }
        public byte Unused29 { get; set; }
        public byte Unused2a { get; set; }
        public byte Unused2b { get; set; }
        public byte Unused2c { get; set; }
        public byte Unused2d { get; set; }
        public byte Unused2e { get; set; }
        public byte Unused2f { get; set; }
        public Int16 Unused30 { get; set; }
        public Int16 Unused32 { get; set; }
        public Int32 SpellLevel { get; set; }
        public Int16 Unused38 { get; set; }
        public array8 SpellBookIcon { get; set; }
        public Int16 Unused42 { get; set; }
        public array8 Unused44 { get; set; }
        public Int32 Unused4c { get; set; }
        public IEString UnidentifiedDescription { get; set; }
        public IEString IdentifiedDescription { get; set; }
        public array8 Unused58 { get; set; }
        public Int32 Unused60 { get; set; }
    }

    [Serializable]
    public class SplExtendedHeader
    {
        public SpellForm SpellForm { get; set; }
        public char Unused1 { get; set; }
        public SpellLocation Location { get; set; }
        public array8 MemorisedIcon { get; set; }
        public SpellTarget TargetType { get; set; }
        public byte TargetCount { get; set; }
        public Int16 Range { get; set; }
        public Int16 LevelRequired { get; set; }
        public Int16 CastingTime { get; set; }
        public Int16 TimesPerDay { get; set; }
        public Int16 Unused16 { get; set; }
        public Int16 Unused18 { get; set; }
        public Int16 Unused1a { get; set; }
        public Int16 Unused1c { get; set; }
        public Int16 FeatureBlockCount { get; set; }
        public Int16 FeatureBlockOffset { get; set; }
        public Int16 Unused22 { get; set; }
        public Int16 Unused24 { get; set; }
        public Int16 ProjectileAnimation { get; set; }

        public List<SplFeatureBlock> splFeatureBlocks = [];
    }

    [Serializable]
    public enum SpellForm : byte
    {
        None = 0,
        Melee,
        Ranged,
        Magical,
        Launcher
    }

    [Serializable]
    public enum SpellLocation : Int16
    {
        None = 0,
        Weapon,
        Spell,
        Item,
        Innate
    }

    [Serializable]
    public enum SpellTarget : byte
    {
        None = 0,
        LivingActor,
        Inventory,
        DeadActor,
        Range,
        Caster,
        Crash,
        CasterSpecial
    }

    [Serializable]
    public enum TimingMode
    {
        Duration = 0,
        Permanent = 1,
        WhileEquipped = 2,
        DelayedDuration = 3,
        Delayed1 = 4,
        Delayed2 = 5,
        Duration2 = 6,
        Permanent2 = 7,
        Permanent3 = 8,
        PermanentAfterDeath = 9,
        Trigger = 10,
        //4096=Absolute duration
    }

    [Serializable]
    public class SplFeatureBlock
    {
        public SplFeatureBlock()
        {
            SavingThrowType = new SpellSavingThrowType();
            Resistance = new Resistance();
        }

        public Int16 Opcode { get; set; }
        public SpellAbilityTarget TargetType { get; set; }
        public byte Power { get; set; }
        public Int32 Parameter1 { get; set; }
        public Int32 Parameter2 { get; set; }
        public TimingMode TimingMode { get; set; }
        public Resistance Resistance { get; set; }
        public Int32 Duration { get; set; }
        public byte Probability1 { get; set; }
        public byte Probability2 { get; set; }
        public array8 Resource { get; set; }
        public Int32 DiceThrown { get; set; }
        public Int32 DiceSides { get; set; }
        public SpellSavingThrowType SavingThrowType { get; set; }
        public Int32 SavingThrowBonus { get; set; }
        public Int32 Unused2c { get; set; }
    }

    [Serializable]
    public enum SpellAbilityTarget : byte
    {
        None = 0,
        Self,
        Projectile,
        Party,
        Everyone,
        EveryoneExceptParty,
        CasterGroup,
        TargetGroup,
        EveryoneExceptSelf,
        OriginalCater
    }

    [Serializable]
    public class SpellSavingThrowType
    {
        public bool Spells { get; set; }
        public bool Breath { get; set; }
        public bool ParalyzePoisonDeath { get; set; }
        public bool Wands { get; set; }
        public bool PetrifyPolymorph { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool Bit9 { get; set; }
        public bool IgnorePrimaryTarget { get; set; }
        public bool IgnoreSecondaryTarget { get; set; }
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
        public bool BypassMirrorImage { get; set; }
        public bool IgnoreDifficulty { get; set; }
        public bool Bit26 { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool Bit31 { get; set; }
    }

    [Serializable]
    public enum SpellType
    {
        Special,
        Wizard,
        Cleric,
        Psionic,
        Innate,
        Bardsong,
    }
}