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
        public Int16 CastinGraphic;
        public char Unknown;
        public Int16 PrimaryType;
        public byte SecondaryType;
        public Int32 Unknown1;
        public Int32 Unknown2;
        public Int32 Unknown3;
        public Int32 SpellLevel;
        public Int16 Unknown4;
        public array8 SpellBookIcon;
        public Int16 Unknown5;
        public Int32 Unknown6;
        public Int32 Unknown7;
        public Int32 Unknown8;
        public Int32 UnidentifiedDescription;
        public Int32 IdentifiedDescription;
        public Int32 Unknown9;
        public Int32 Unknown10;
        public Int32 Unknown11;
        public Int32 ExtendedHeaderOffset;
        public Int16 ExtendedHeaderCount;
        public Int32 FeatureBlockOffset;
        public Int16 FeatureBlockCastingIndex;
        public Int16 FeatureBlockCastingCount;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct SplExtendedHeaderBinary
    {
        public char SpellForm;
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