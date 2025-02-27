using System;
using System.Runtime.InteropServices;

namespace ii.InfinityEngine.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct TisHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
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