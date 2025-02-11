using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class SplFile : IEFile
    {
        public List<SplExtendedHeader2> splExtendedHeader = [];
        public List<SplFeatureBlock2> splFeatureBlocks = [];

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

        public IEString UnidentifiedName { get; set; }
        public IEString IdentifiedName { get; set; }
        public array8 CompletionSound { get; set; }
        public SpellFlags Flags;
        public SpellType SpellType { get; set; }
        public Int32 ExclusionFlags { get; set; }
        public Int16 CastingGraphic { get; set; }
        public byte Unknown24 { get; set; }
        public byte PrimaryType { get; set; }
        public byte Unknown26 { get; set; }
        public byte SecondaryType { get; set; }
        public byte Unused28;
        public byte Unused29;
        public byte Unused2a;
        public byte Unused2b;
        public byte Unused2c;
        public byte Unused2d;
        public byte Unused2e;
        public byte Unused2f;
        public Int16 Unused30;
        public Int16 Unused32;
        public Int32 SpellLevel { get; set; }
        public Int16 Unused38 { get; set; }
        public array8 SpellBookIcon { get; set; }
        public Int16 Unused42 { get; set; }
        public array8 Unused44 { get; set; }        
        public Int32 Unused4c { get; set; }
        public IEString UnidentifiedDescription { get; set; }
        public IEString IdentifiedDescription { get; set; }
        public array8 Unused58;
        public Int32 Unused60;
    }

    [Serializable]
    public class SplExtendedHeader2
    {
        public byte SpellForm { get; set; }
        public char Unknown { get; set; }
        public char Location { get; set; }
        public char Unknown2 { get; set; }
        public array8 MemorisedIcon { get; set; }
        public char TargetType { get; set; }
        public char TargetCount { get; set; }
        public Int16 Range { get; set; }
        public Int16 LevelRequired { get; set; }
        public Int32 CastingTime { get; set; }
        public Int16 DiceSides { get; set; }
        public Int16 DiceThrown { get; set; } // Unused
        public Int16 Enchantment { get; set; } // Unused
        public Int16 DamageType { get; set; } // Unused
        public Int16 FeatureBlockCount { get; set; }
        public Int16 FeatureBlockOffset { get; set; }
        public Int16 Charges { get; set; } // Unused
        public Int16 ChargeDepletionBehaviour { get; set; } // Unused
        public Int16 ProjectileAnimation { get; set; }

        public List<SplFeatureBlock2> splFeatureBlocks = new List<SplFeatureBlock2>();
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
    public class SplFeatureBlock2
    {
        public Int16 Opcode;
        public byte TargetType;
        public byte Power;
        public Int32 Parameter1;
        public Int32 Parameter2;
        public TimingMode TimingMode;
        public byte Resistance;
        public Int32 Duration;
        public byte Probability1;
        public byte Probability2;
        public array8 Resource;
        public Int32 DiceThrown;
        public Int32 DiceSides;
        public Int32 SavingThrowType;
        public Int32 SavingThrowBonus;
        public Int32 Unknown;
    }

    [Serializable]
    public struct SpellFlags
    {
        public bool Bit0 { get; set; }
        public bool Bit1 { get; set; }
        public bool Bit2 { get; set; }
        public bool Bit3 { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool BreaksSanctuaryInvisibility { get; set; }
        public bool Hostile { get; set; }
        public bool NoLOSRequired { get; set; }
        public bool AllowSpotting { get; set; }
        public bool OutdoorsOnly { get; set; }
        public bool IgnoreWildSurgeDeadMagic { get; set; }
        public bool IgnoreWildSurge { get; set; }
        public bool NonCombatAbility { get; set; }
        public bool Bit17 { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool Bit20 { get; set; }
        public bool Bit21 { get; set; }
        public bool Bit22 { get; set; }
        public bool Bit23 { get; set; }
        public bool CanTargetInvisible { get; set; }
        public bool CastableWhenSilenced { get; set; }
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