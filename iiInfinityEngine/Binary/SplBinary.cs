using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct SplHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int32 UnidentifiedName;
        public Int32 IdentifiedName;
        public array8 CompletionSound;
        public Int32 Flags;
        public Int16 SpellType;
        public Int32 ExclusionFlags;
        public Int16 CastingGraphic;        
        public byte Unused24;
        public byte PrimaryType;
        public byte Unused26;
        public byte SecondaryType;
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
        public Int32 SpellLevel;
        public Int16 Unused38;
        public array8 SpellBookIcon;
        public Int16 Unused42;
        public array8 Unused44;
        public Int32 Unused4c;
        public Int32 UnidentifiedDescription;
        public Int32 IdentifiedDescription;
        public array8 Unused58;
        public Int32 Unused60;
        public Int32 ExtendedHeaderOffset;
        public Int16 ExtendedHeaderCount;
        public Int32 FeatureBlockOffset;
        public Int16 FeatureBlockCastingIndex;
        public Int16 FeatureBlockCastingCount;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct SplExtendedHeaderBinary
    {
        public byte SpellForm;
        public char Unknown;
        public char Location;
        public char Unknown2;
        public array8 MemorisedIcon;
        public char TargetType;
        public char TargetCount;
        public Int16 Range;
        public Int16 LevelRequired;
        public Int32 CastingTime;
        public Int16 DiceSides;
        public Int16 DiceThrown; // Unused
        public Int16 Enchantment; // Unused
        public Int16 DamageType; // Unused
        public Int16 FeatureBlockCount;
        public Int16 FeatureBlockOffset;
        public Int16 Charges; // Unused
        public Int16 ChargeDepletionBehaviour; // Unused
        public Int16 ProjectileAnimation;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct SplFeatureBlockBinary
    {
        public Int16 Opcode;
        public byte TargetType;
        public byte Power;
        public Int32 Parameter1;
        public Int32 Parameter2;
        public byte TimingMode;
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
}