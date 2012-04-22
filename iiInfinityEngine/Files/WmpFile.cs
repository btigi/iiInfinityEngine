using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class WmpFile : IEFile
    {
        public List<WmapWorldmap> worldmaps = new List<WmapWorldmap>();

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
        public List<WmapArea> areas = new List<WmapArea>();

        public string BackgroundMos;
        public Int32 Width;
        public Int32 Height;
        public Int32 MapNumber;
        public IEString AreaName;
        public Int32 Unknown0018;
        public Int32 Unknown001c;
        public string MapIconsBam;
        public array128 Unknown0038;
    }

    [Serializable]
    public class WmapArea
    {
        public List<WmapAreaLink> NorthLinks = new List<WmapAreaLink>();
        public List<WmapAreaLink> EastLinks = new List<WmapAreaLink>();
        public List<WmapAreaLink> SouthLinks = new List<WmapAreaLink>();
        public List<WmapAreaLink> WestLinks = new List<WmapAreaLink>();

        public string AreaFilename;
        public string ShortName;
        public string LongName;
        public Int32 Flags;
        public Int32 SequenceBam;
        public Int32 XCoordinate;
        public Int32 YCoordinate;
        public IEString Caption;
        public IEString Name;
        public string LoadingMos;
        public array128 Unknown0070;
    }

    [Serializable]
    public struct WmapAreaLink
    {
        public Int32 DestintationAreaIndex;
        public string EntryPoint;
        public Int32 TravelTime;
        public Int32 DefaultEntryLocation;
        public string RandomEncounterArea1;
        public string RandomEncounterArea2;
        public string RandomEncounterArea3;
        public string RandomEncounterArea4;
        public string RandomEncounterArea5;
        public Int32 RandomEncounterProbability;
        public array128 Unknown0058;
    }
}