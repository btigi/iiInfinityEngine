using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class WmpFile : IEFile
    {
        public List<WmapWorldmap> worldmaps = [];

        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private IEFileType fileType = IEFileType.Wmp;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }
    }

    [Serializable]
    public class WmapWorldmap
    {
        public List<WmapArea> areas = new();

        public string BackgroundMos;
        public Int32 Width;
        public Int32 Height;
        public Int32 MapNumber;
        public IEString AreaName;
        public Int32 StartCenteredOnX;
        public Int32 StartCenteredOnY;
        public string MapIconsBam;
        public WmapWorldmapFlags Flags;
        public byte[] Unused;
    }

    [Serializable]
    public class WmapArea
    {
        public List<WmapAreaLink> NorthLinks = new();
        public List<WmapAreaLink> EastLinks = new();
        public List<WmapAreaLink> SouthLinks = [];
        public List<WmapAreaLink> WestLinks = [];

        public string AreaFilename;
        public string ShortName;
        public string LongName;
        public WmpAreaFlags Flags;
        public Int32 SequenceBam;
        public Int32 XCoordinate;
        public Int32 YCoordinate;
        public IEString Caption;
        public IEString Name;
        public string LoadingMos;
        public byte[] Unused;
    }

    [Serializable]
    public struct WmapAreaLink
    {
        public Int32 DestintationAreaIndex;
        public string EntryPoint;
        public Int32 TravelTime;
        public WmapAreaLinkFlags DefaultEntryLocation;
        public string RandomEncounterArea1;
        public string RandomEncounterArea2;
        public string RandomEncounterArea3;
        public string RandomEncounterArea4;
        public string RandomEncounterArea5;
        public Int32 RandomEncounterProbability;
        public byte[] Unused;
    }

    [Serializable]
    public struct WmapWorldmapFlags
    {
        public bool ColouredIcons;
        public bool IgnorePalette;
        public bool Bit2;
        public bool Bit3;
        public bool Bit4;
        public bool Bit5;
        public bool Bit6;
        public bool Bit7;
        public bool Bit8;
        public bool Bit9;
        public bool Bit10;
        public bool Bit11;
        public bool Bit12;
        public bool Bit13;
        public bool Bit14;
        public bool Bit15;
        public bool Bit16;
        public bool Bit17;
        public bool Bit18;
        public bool Bit19;
        public bool Bit20;
        public bool Bit21;
        public bool Bit22;
        public bool Bit23;
        public bool Bit24;
        public bool Bit25;
        public bool Bit26;
        public bool Bit27;
        public bool Bit28;
        public bool Bit29;
        public bool Bit30;
        public bool Bit31;
    }

    [Serializable]
    public struct WmpAreaFlags
    {
        public bool AreaVisible;
        public bool AreaVisibleFromAdjacent;
        public bool Reachable;
        public bool AlreadyVisited;
        public bool Bit4;
        public bool Bit5;
        public bool Bit6;
        public bool Bit7;
        public bool Bit8;
        public bool Bit9;
        public bool Bit10;
        public bool Bit11;
        public bool Bit12;
        public bool Bit13;
        public bool Bit14;
        public bool Bit15;
        public bool Bit16;
        public bool Bit17;
        public bool Bit18;
        public bool Bit19;
        public bool Bit20;
        public bool Bit21;
        public bool Bit22;
        public bool Bit23;
        public bool Bit24;
        public bool Bit25;
        public bool Bit26;
        public bool Bit27;
        public bool Bit28;
        public bool Bit29;
        public bool Bit30;
        public bool Bit31;
    }

    [Serializable]
    public struct WmapAreaLinkFlags
    {
        public bool MoveToNorthernSide;
        public bool MoveToEasternSide;
        public bool MoveToSouthernSide;
        public bool MoveToWesternSide;
        public bool Bit4;
        public bool Bit5;
        public bool Bit6;
        public bool Bit7;
        public bool Bit8;
        public bool Bit9;
        public bool Bit10;
        public bool Bit11;
        public bool Bit12;
        public bool Bit13;
        public bool Bit14;
        public bool Bit15;
        public bool Bit16;
        public bool Bit17;
        public bool Bit18;
        public bool Bit19;
        public bool Bit20;
        public bool Bit21;
        public bool Bit22;
        public bool Bit23;
        public bool Bit24;
        public bool Bit25;
        public bool Bit26;
        public bool Bit27;
        public bool Bit28;
        public bool Bit29;
        public bool Bit30;
        public bool Bit31;
    }
}