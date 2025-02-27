using System;
using System.Runtime.InteropServices;

namespace ii.InfinityEngine.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct EffHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public array4 ftype2;
        public array4 fversion2;
        public Int32 Opcode;
        public Int32 TargetType;
        public Int32 Power;
        public Int32 Parameter1;
        public Int32 Parameter2;
        public Int16 TimingMode;
        public Int16 Unknown26;
        public Int32 Duration;
        public Int16 Probability1;
        public Int16 Probability2;
        public array8 Resource;
        public Int32 DiceThrown;
        public Int32 DiceSides;
        public Int32 SavingThrowType;
        public Int32 SavingThrowBonus;
        public Int32 Special;
        public Int32 PrimaryType;
        public Int32 Unknown50;
        public Int32 LowestAffectedLevelFromParent;
        public Int32 HighestAffectedLevelFromParent;
        public Int32 Resistance;
        public Int32 Parameter3;
        public Int32 Parameter4;
        public Int32 Parameter5;
        public Int32 TimeApplied;
        public array8 Resource2;
        public array8 Resource3;
        public Int32 CasterXCoordinate;
        public Int32 CasterYCoordinate;
        public Int32 TargetXCoordinate;
        public Int32 TargetYCoordinate;
        public Int32 ResourceTypeFromParent;
        public array8 ParentResource;
        public Int32 FlagsFromParent;
        public Int32 Projectile;
        public Int32 ParentResourceSlot;
        public array32 Variable;
        public Int32 CasterLevel;
        public Int32 FirstApply;
        public Int32 SecondaryType;
        public Int32 Unknownd4;
        public Int32 Unknownd8;
        public Int32 Unknowndc;
        public Int32 Unknowne0;
        public Int32 Unknowne4;
        public Int32 Unknowne8;
        public Int32 Unknownec;
        public Int32 Unknownf0;
        public Int32 Unknownf4;
        public Int32 Unknownf8;
        public Int32 Unknownfc;
        public Int32 Unknownd100;
        public Int32 Unknownd104;
        public Int32 Unknownd108;
        public Int32 Unknownd10c;
    }
}