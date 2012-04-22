using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class SplFile : IEFile
    {
        public List<SplExtendedHeader2> splExtendedHeader = new List<SplExtendedHeader2>();
        public List<SplFeatureBlock2> splFeatureBlocks = new List<SplFeatureBlock2>();

        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private IEFileType fileType = IEFileType.Spl;
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
        public char Unknown { get; set; }
        public Int16 PrimaryType { get; set; }
        public byte SecondaryType { get; set; }
        public Int32 Unknown1 { get; set; }
        public Int32 Unknown2 { get; set; }
        public Int32 Unknown3 { get; set; }
        public Int32 SpellLevel { get; set; }
        public Int16 Unknown4 { get; set; }
        public array8 SpellBookIcon { get; set; }
        public Int16 Unknown5 { get; set; }
        public Int32 Unknown6 { get; set; }
        public Int32 Unknown7 { get; set; }
        public Int32 Unknown8 { get; set; }
        public IEString UnidentifiedDescription { get; set; }
        public IEString IdentifiedDescription { get; set; }
        public Int32 Unknown9 { get; set; }
        public Int32 Unknown10 { get; set; }
        public Int32 Unknown11 { get; set; }
    }

    [Serializable]
    public class SplExtendedHeader2
    {
        public char SpellForm { get; set; }
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
        public bool Byte1Bit0 { get; set; }
        public bool Byte1Bit1 { get; set; }
        public bool Byte1Bit2 { get; set; }
        public bool Byte1Bit3 { get; set; }
        public bool Byte1Bit4 { get; set; }
        public bool Byte1Bit5 { get; set; }
        public bool Byte1Bit6 { get; set; }
        public bool Byte1Bit7 { get; set; }

        public bool Byte2Bit0 { get; set; }
        public bool Byte2Bit1 { get; set; }
        public bool Byte2Bit2 { get; set; } // hostile
        public bool Byte2Bit3 { get; set; } // no LOS required
        public bool Byte2Bit4 { get; set; }
        public bool Byte2Bit5 { get; set; } // outdoors only
        public bool Byte2Bit6 { get; set; } // non-magical
        public bool Byte2Bit7 { get; set; } // trigger/contingency

        public bool Byte3Bit0 { get; set; } // not in combat
        public bool Byte3Bit1 { get; set; }
        public bool Byte3Bit2 { get; set; }
        public bool Byte3Bit3 { get; set; }
        public bool Byte3Bit4 { get; set; }
        public bool Byte3Bit5 { get; set; }
        public bool Byte3Bit6 { get; set; }
        public bool Byte3Bit7 { get; set; }

        public bool Byte4Bit0 { get; set; }
        public bool Byte4Bit1 { get; set; }
        public bool Byte4Bit2 { get; set; }
        public bool Byte4Bit3 { get; set; }
        public bool Byte4Bit4 { get; set; }
        public bool Byte4Bit5 { get; set; }
        public bool Byte4Bit6 { get; set; }
        public bool Byte4Bit7 { get; set; }
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