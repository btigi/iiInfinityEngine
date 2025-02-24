using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct MosHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int16 Width; // Pixels
        public Int16 Height; // Pixels
        public Int16 Columns; // Blocks
        public Int16 Rows; // Blocks
        public Int32 BlockSize;
        public Int32 PaletteOffset;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct MosPaletteBinary
    {
        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct MosTileOffsetBinary
    {
        public Int32 Offset;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct MosCHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int32 FileLength;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct Mos2HeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int32 Width; // Pixels
        public Int32 Height; // Pixels
        public Int32 BlockCount;
        public Int32 BlockOffset;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct Mos2DataBlockBinary
    {
        public Int32 Thing;
        public Int32 SourceXCoord;
        public Int32 SourceYCoord;
        public Int32 Width;
        public Int32 Height;
        public Int32 TargetXCoord;
        public Int32 TargetYCoord;
    }
}