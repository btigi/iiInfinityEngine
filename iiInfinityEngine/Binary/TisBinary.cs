using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct TisHeaderBinary
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 4)]
        public char[] ftype;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 4)]
        public char[] fversion;
        public Int32 TileCount;
        public Int32 TileLength;
        public Int32 TileOffset;
        public Int32 TileDimension;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct TisPaletteBinary
    {
        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;
    }
}