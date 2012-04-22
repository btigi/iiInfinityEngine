using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct Eff1BinaryBinary
    {
        public Int16 Opcode;
        public byte TargetType;
        public byte Power;
        public Int32 Parameter1;
        public Int32 Parameter2;
        public byte TimingMode;
        public byte DispelResistance;
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