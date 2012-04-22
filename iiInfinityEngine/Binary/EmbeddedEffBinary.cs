using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EmbeddedEffBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int32 Opcode;
        public Int32 TargetType;
        public Int32 Power;
        public Int32 Parameter1;
        public Int32 Parameter2;
        public Int16 TimingMode;
        public Int16 Unknown;
        public Int32 Duration;
        public Int16 Probability1;
        public Int16 Probability2;
        public array8 Resource;
        public Int32 DiceThrown;
        public Int32 DiceSides;
        public Int32 SavingThrowType;
        public Int32 SavingThrowBonus;
        public Int32 SetLocalVariableIfNonExistant;
        public Int32 PrimaryType;
        public Int32 Unknown2;
        public Int32 LowestAffectedLevelFromParent;
        public Int32 HighestAffectedLevelFromParent;
        public Int32 Resistance;
        public Int32 Parameter3;
        public Int32 Parameter4;
        public array8 Resource2;
        public array8 Resource3; // VVC aka Parameter 5
        public Int32 Unknown3;
        public Int32 Unknown4;
        public Int32 CasterXCoordinate;
        public Int32 CasterYCoordinate;
        public Int32 TargetXCoordinate;
        public Int32 TargetYCoordinate;
        public Int32 ResourceTypeFromParent;
        public array8 ParentResource;
        public Int32 ParentResourceFlags;
        public Int32 Projectile;
        public Int32 ParentResourceSlot;
        public array32 Variable;
        public Int32 CasterLevel;
        public Int32 Unknown5;
        public Int32 SecondaryType;
        public Int32 Unknownd4_1;
        public Int32 Unknownd4_2;
        public Int32 Unknownd4_3;
        public Int32 Unknownd4_4;
        public Int32 Unknownd4_5;
        public Int32 Unknownd4_6;
        public Int32 Unknownd4_7;
        public Int32 Unknownd4_8;
        public Int32 Unknownd4_9;
        public Int32 Unknownd4_10;
        public Int32 Unknownd4_11;
        public Int32 Unknownd4_12;
        public Int32 Unknownd4_13;
        public Int32 Unknownd4_14;
        public Int32 Unknownd4_15;
    }
}