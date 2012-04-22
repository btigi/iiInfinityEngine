using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ItmHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int32 UnidentifiedName;
        public Int32 IdentifiedName;
        public array8 ReplacementItem;
        public Int32 Flags;
        public Int16 ItemType;
        public char Usability1;
        public char Usability2;
        public char Usability3;
        public char Usability4;
        public array2 Animation;
        public char MinimumLevel;
        public char Unknown;
        public char MinimumStrength;
        public char Unknown2;
        public char MinimumStrengthBonus;
        public char KitUsability1;
        public char MinimumIntelligence;
        public char KitUsability2;
        public char MinimumDexterity;
        public char KitUsability3;
        public char MinimumWisdom;
        public char KitUsability4;
        public char MinimumConstitution;
        public char Proficiency;
        public char MinimumCharisma;
        public char Unknown3;
        public Int32 Price;
        public Int16 StackAmount;
        public array8 InventoryIcon;
        public Int16 LoreToIdentify;
        public array8 GroundIcon;
        public Int32 Weight;
        public Int32 UnidentifiedDescription;
        public Int32 IdentifiedDescription;
        public array8 DescriptionIcon;
        public Int32 Enchantment;
        public Int32 ExtendedHeaderOffset;
        public Int16 ExtendedHeaderCount;
        public Int32 FeatureBlockOffset;
        public Int16 FeatureBlockEquippingIndex;
        public Int16 FeatureBlockEquippingCount;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ItmExtendedHeaderBinary
    {
        public char AttackType;
        public char IdentificationRequirement;
        public char Location;
        public char AlternaticeDiceSides;
        public array8 UseIcon;
        public char TargetType;
        public char TargetCount;
        public Int16 Range;
        public byte ProjectileType;
        public byte AlternaticeDiceThrown;
        public byte Speed;
        public byte AlternaticeDamageBonus;
        public Int16 Thac0Bonus;
        public byte DiceSides;
        public byte PrimaryType;
        public byte DiceThrown;
        public byte SecondaryType;
        public Int16 DamageBonus;
        public Int16 DamageType;
        public Int16 FeatureBlockCount;
        public Int16 FeatureBlockOffset;
        public Int16 Charges;
        public Int16 ChargeDepletionBehaviour;
        public Int16 Flags;
        public Int16 Unknown;
        public Int16 ProjectileAnimation;
        public Int16 MeleeAnimation1;
        public Int16 MeleeAnimation2;
        public Int16 MeleeAnimation3;
        public Int16 IsBowArrow;
        public Int16 IsCrossbowBolt;
        public Int16 IsMiscProjectile;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ItmFeatureBlockBinary
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