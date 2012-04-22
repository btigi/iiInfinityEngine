using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct BifHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int32 FileCount;
        public Int32 TilesetCount;
        public Int32 FileOffset;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct BifFileEntryBinary
    {
        public Int32 resourceLocator;
        public Int32 resourceOffset;
        public Int32 resourceSize;
        public Int16 resourceType;
        public Int16 Unknown;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct BifTilesetEntryBinary
    {
        public Int32 resourceLocator;
        public Int32 resourceOffset;
        public Int32 tileCount;
        public Int32 tileSize;
        public Int16 resourceType;
        public Int16 Unknown;
    }



    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct BifCHeaderV1Binary
    {
        public array4 ftype;
        public array4 fversion;
        public Int32 FileLength;
    }
}