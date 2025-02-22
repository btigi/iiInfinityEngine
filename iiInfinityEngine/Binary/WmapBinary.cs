using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct WmapHeaderBinary
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 4)]
        public char[] ftype;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 4)]
        public char[] fversion;
        public Int32 WorldmapCount;
        public Int32 WorldmapOffset;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct WmapWorldmapBinary
    {
        public array8 BackgroundMos;
        public Int32 Width;
        public Int32 Height;
        public Int32 MapNumber;
        public Int32 AreaName; // strref
        public Int32 StartCenteredOnX;
        public Int32 StartCenteredOnY;
        public Int32 AreaCount;
        public Int32 AreaOffset;
        public Int32 AreaLinkOffset;
        public Int32 AreaLinkCount;
        public array8 MapIconsBam;
        public Int32 Flags;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 124)]
        public byte[] Unused;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct WmapAreaBinary
    {
        public array8 AreaFilename;
        public array8 ShortName;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 32)]
        public char[] LongName;
        public Int32 Flags;
        public Int32 SequenceBam;
        public Int32 XCoordinate;
        public Int32 YCoordinate;
        public Int32 Caption; // strref
        public Int32 Name; // strref
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] LoadingMos;
        public Int32 NorthLinkIndex;
        public Int32 NorthLinkCount;
        public Int32 WestLinkIndex;
        public Int32 WestLinkCount;
        public Int32 SouthLinkIndex;
        public Int32 SouthLinkCount;
        public Int32 EastLinkIndex;
        public Int32 EastLinkCount;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 128)]
        public byte[] Unused;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct WmapAreaLinkBinary
    {
        public Int32 DestintationAreaIndex;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 32)]
        public char[] EntryPoint;
        public Int32 TravelTime; // time / 4
        public Int32 DefaultEntryLocation;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] RandomEncounterArea1;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] RandomEncounterArea2;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] RandomEncounterArea3;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] RandomEncounterArea4;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] RandomEncounterArea5;
        public Int32 RandomEncounterProbability;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 128)]
        public byte[] Unused;
    }
}