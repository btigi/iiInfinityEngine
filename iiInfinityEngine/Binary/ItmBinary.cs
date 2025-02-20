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
        public byte Usability1;
        public byte Usability2;
        public byte Usability3;
        public byte Usability4;
        public array2 Animation;
        public Int16 MinimumLevel;
        public Int16 MinimumStrength;
        public byte MinimumStrengthBonus;
        public byte KitUsability1;
        public byte MinimumIntelligence;
        public byte KitUsability2;
        public byte MinimumDexterity;
        public byte KitUsability3;
        public byte MinimumWisdom;
        public byte KitUsability4;
        public byte MinimumConstitution;
        public byte Proficiency;
        public Int16 MinimumCharisma;
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
        public byte AttackType;
        public byte IdentificationRequirement;
        public byte Location;
        public byte AlternaticeDiceSides;
        public array8 UseIcon;
        public char TargetType;
        public byte TargetCount;
        public Int16 Range;
        public byte LauncherType;
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
        public Int32 Flags;
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