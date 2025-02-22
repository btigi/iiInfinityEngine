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
        private readonly IEFileType fileType = IEFileType.Wmp;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }
    }

    [Serializable]
    public class WmapWorldmap
    {
        public List<WmapArea> areas = [];

        public WmapWorldmap()
        {
            Flags = new WmapWorldmapFlags();
        }

        public string BackgroundMos { get; set; }
        public Int32 Width { get; set; }
        public Int32 Height { get; set; }
        public Int32 MapNumber { get; set; }
        public IEString AreaName { get; set; }
        public Int32 StartCenteredOnX { get; set; }
        public Int32 StartCenteredOnY { get; set; }
        public string MapIconsBam { get; set; }
        public WmapWorldmapFlags Flags { get; set; }
        public byte[] Unused { get; set; }
    }

    [Serializable]
    public class WmapArea
    {
        public List<WmapAreaLink> NorthLinks = [];
        public List<WmapAreaLink> EastLinks = [];
        public List<WmapAreaLink> SouthLinks = [];
        public List<WmapAreaLink> WestLinks = [];

        public WmapArea()
        {
            Flags = new WmpAreaFlags();
        }

        public string AreaFilename { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public WmpAreaFlags Flags { get; set; }
        public Int32 SequenceBam { get; set; }
        public Int32 XCoordinate { get; set; }
        public Int32 YCoordinate { get; set; }
        public IEString Caption { get; set; }
        public IEString Name { get; set; }
        public string LoadingMos { get; set; }
        public byte[] Unused { get; set; }
    }

    [Serializable]
    public class WmapAreaLink
    {
        public WmapAreaLink()
        {
            DefaultEntryLocation = new WmapAreaLinkFlags();
        }

        public Int32 DestintationAreaIndex { get; set; }
        public string EntryPoint { get; set; }
        public Int32 TravelTime { get; set; }
        public WmapAreaLinkFlags DefaultEntryLocation { get; set; }
        public string RandomEncounterArea1 { get; set; }
        public string RandomEncounterArea2 { get; set; }
        public string RandomEncounterArea3 { get; set; }
        public string RandomEncounterArea4 { get; set; }
        public string RandomEncounterArea5 { get; set; }
        public Int32 RandomEncounterProbability { get; set; }
        public byte[] Unused { get; set; }
    }

    [Serializable]
    public class WmapWorldmapFlags
    {
        public bool ColouredIcons { get; set; }
        public bool IgnorePalette { get; set; }
        public bool Bit2 { get; set; }
        public bool Bit3 { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool Bit9 { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
        public bool Bit16 { get; set; }
        public bool Bit17 { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool Bit20 { get; set; }
        public bool Bit21 { get; set; }
        public bool Bit22 { get; set; }
        public bool Bit23 { get; set; }
        public bool Bit24 { get; set; }
        public bool Bit25 { get; set; }
        public bool Bit26 { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool Bit31 { get; set; }
    }

    [Serializable]
    public class WmpAreaFlags
    {
        public bool AreaVisible { get; set; }
        public bool AreaVisibleFromAdjacent { get; set; }
        public bool Reachable { get; set; }
        public bool AlreadyVisited { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool Bit9 { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
        public bool Bit16 { get; set; }
        public bool Bit17 { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool Bit20 { get; set; }
        public bool Bit21 { get; set; }
        public bool Bit22 { get; set; }
        public bool Bit23 { get; set; }
        public bool Bit24 { get; set; }
        public bool Bit25 { get; set; }
        public bool Bit26 { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool Bit31 { get; set; }
    }

    [Serializable]
    public class WmapAreaLinkFlags
    {
        public bool MoveToNorthernSide { get; set; }
        public bool MoveToEasternSide { get; set; }
        public bool MoveToSouthernSide { get; set; }
        public bool MoveToWesternSide { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool Bit9 { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
        public bool Bit16 { get; set; }
        public bool Bit17 { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool Bit20 { get; set; }
        public bool Bit21 { get; set; }
        public bool Bit22 { get; set; }
        public bool Bit23 { get; set; }
        public bool Bit24 { get; set; }
        public bool Bit25 { get; set; }
        public bool Bit26 { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool Bit31 { get; set; }
    }
}