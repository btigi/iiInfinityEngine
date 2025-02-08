using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct WfxHeaderBinary
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 4)]
        public char[] ftype;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 4)]
        public char[] fversion;
        public Int32 SrCurveRadius;
        public Int32 Flags;
        public Int32 RandomFrequencyVariation;
        public Int32 RandomVolumeVariation;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 240)]
        public byte[] Unused;
    }
}
