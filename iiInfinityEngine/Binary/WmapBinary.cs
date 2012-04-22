using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct WmapHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
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
        public Int32 AreaName;
        public Int32 Unknown1;
        public Int32 Unknown2;
        public Int32 AreaCount;
        public Int32 AreaOffset;
        public Int32 AreaLinkOffset;
        public Int32 AreaLinkCount;
        public array8 MapIconsBam;
        public array128 Unknown;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct WmapAreaBinary
    {
        public array8 AreaFilename;
        public array8 ShortName;
        public array32 LongName;
        public Int32 Flags;
        public Int32 SequenceBam;
        public Int32 XCoordinate;
        public Int32 YCoordinate;
        public Int32 Caption;
        public Int32 Name;
        public array8 LoadingMos;
        public Int32 NorthLinkIndex;
        public Int32 NorthLinkCount;
        public Int32 WestLinkIndex;
        public Int32 WestLinkCount;
        public Int32 SouthLinkIndex;
        public Int32 SouthLinkCount;
        public Int32 EastLinkIndex;
        public Int32 EastLinkCount;
        public array128 Unknown;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct WmapAreaLinkBinary
    {
        public Int32 DestintationAreaIndex;
        public array32 EntryPoint;
        public Int32 TravelTime;
        public Int32 DefaultEntryLocation;
        public array8 RandomEncounterArea1;
        public array8 RandomEncounterArea2;
        public array8 RandomEncounterArea3;
        public array8 RandomEncounterArea4;
        public array8 RandomEncounterArea5;
        public Int32 RandomEncounterProbability;
        public array128 Unknown;
    }
}