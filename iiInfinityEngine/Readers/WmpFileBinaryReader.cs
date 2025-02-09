using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers
{
    public class WmpFileBinaryReader : IWmpFileReader
    {
        const int WmapBinaryAreaSize = 216;

        public TlkFile TlkFile { get; set; }

        public WmpFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public WmpFile Read(Stream s)
        {
            using var br = new BinaryReader(s);
            var wmpFile = ParseFile(br);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            wmpFile.OriginalFile = ParseFile(br);
            return wmpFile;
        }

        private WmpFile ParseFile(BinaryReader br)
        {
            var header = (WmapHeaderBinary)Common.ReadStruct(br, typeof(WmapHeaderBinary));

            var binaryWorldmaps = new List<WmapWorldmapBinary>();
            var binaryAreas = new List<WmapAreaBinary>();
            var binaryLinks = new List<WmapAreaLinkBinary>();

            if (Common.TryGetString(header.ftype) != "WMAP")
                return new WmpFile();

            br.BaseStream.Seek(header.WorldmapOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.WorldmapCount; i++)
            {
                var worldmap = (WmapWorldmapBinary)Common.ReadStruct(br, typeof(WmapWorldmapBinary));
                binaryWorldmaps.Add(worldmap);
            }

            foreach (var worldmap in binaryWorldmaps)
            {
                br.BaseStream.Seek(worldmap.AreaOffset, SeekOrigin.Begin);
                for (int i = 0; i < worldmap.AreaCount; i++)
                {
                    var area = (WmapAreaBinary)Common.ReadStruct(br, typeof(WmapAreaBinary));
                    area.EastLinkIndex = (area.EastLinkIndex * WmapBinaryAreaSize) + worldmap.AreaLinkOffset;
                    area.NorthLinkIndex = (area.NorthLinkIndex * WmapBinaryAreaSize) + worldmap.AreaLinkOffset;
                    area.SouthLinkIndex = (area.SouthLinkIndex * WmapBinaryAreaSize) + worldmap.AreaLinkOffset;
                    area.WestLinkIndex = (area.WestLinkIndex * WmapBinaryAreaSize) + worldmap.AreaLinkOffset;
                    binaryAreas.Add(area);
                }
            }

            foreach (var area in binaryAreas)
            {
                var offsets = new LinkInfo[] {
                        new LinkInfo("East", area.EastLinkIndex, area.EastLinkCount),
                        new LinkInfo("North", area.NorthLinkIndex, area.NorthLinkCount),
                        new LinkInfo("South", area.SouthLinkIndex, area.SouthLinkCount),
                        new LinkInfo("West", area.WestLinkIndex, area.WestLinkCount) };

                var first = offsets.Min();
                first.Exclude = true;

                var second = offsets.Min();
                second.Exclude = true;

                var third = offsets.Min();
                third.Exclude = true;

                var fourth = offsets.Min();
                fourth.Exclude = true;

                br.BaseStream.Seek(first.Offset, SeekOrigin.Begin);
                for (int i = 0; i < first.Count; i++)
                {
                    var link = (WmapAreaLinkBinary)Common.ReadStruct(br, typeof(WmapAreaLinkBinary));
                    binaryLinks.Add(link);
                }

                br.BaseStream.Seek(second.Offset, SeekOrigin.Begin);
                for (int i = 0; i < second.Count; i++)
                {
                    var link = (WmapAreaLinkBinary)Common.ReadStruct(br, typeof(WmapAreaLinkBinary));
                    binaryLinks.Add(link);
                }

                br.BaseStream.Seek(third.Offset, SeekOrigin.Begin);
                for (int i = 0; i < third.Count; i++)
                {
                    var link = (WmapAreaLinkBinary)Common.ReadStruct(br, typeof(WmapAreaLinkBinary));
                    binaryLinks.Add(link);
                }

                br.BaseStream.Seek(fourth.Offset, SeekOrigin.Begin);
                for (int i = 0; i < fourth.Count; i++)
                {
                    var link = (WmapAreaLinkBinary)Common.ReadStruct(br, typeof(WmapAreaLinkBinary));
                    binaryLinks.Add(link);
                }
            }

            var cummulativeAreaCount = 0;
            var wmpFile = new WmpFile();
            foreach (var worldmap in binaryWorldmaps)
            {
                var areas2 = new List<WmapArea>();
                for (int i = 0; i < worldmap.AreaCount; i++)
                {
                    var area2 = new WmapArea();
                    area2.AreaFilename = Common.TryGetString(binaryAreas[cummulativeAreaCount + i].AreaFilename);
                    area2.Caption = Common.ReadString(binaryAreas[cummulativeAreaCount + i].Caption, TlkFile);
                    area2.Flags.AreaVisible = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit0) != 0;
                    area2.Flags.AreaVisibleFromAdjacent = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit1) != 0;
                    area2.Flags.Reachable = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit2) != 0;
                    area2.Flags.AlreadyVisited = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit3) != 0;
                    area2.Flags.Bit4 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit4) != 0;
                    area2.Flags.Bit5 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit5) != 0;
                    area2.Flags.Bit6 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit6) != 0;
                    area2.Flags.Bit7 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit7) != 0;
                    area2.Flags.Bit8 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit8) != 0;
                    area2.Flags.Bit9 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit9) != 0;
                    area2.Flags.Bit10 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit10) != 0;
                    area2.Flags.Bit11 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit11) != 0;
                    area2.Flags.Bit12 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit12) != 0;
                    area2.Flags.Bit13 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit13) != 0;
                    area2.Flags.Bit14 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit14) != 0;
                    area2.Flags.Bit15 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit15) != 0;
                    area2.Flags.Bit16 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit16) != 0;
                    area2.Flags.Bit17 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit17) != 0;
                    area2.Flags.Bit18 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit18) != 0;
                    area2.Flags.Bit19 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit19) != 0;
                    area2.Flags.Bit20 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit20) != 0;
                    area2.Flags.Bit21 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit21) != 0;
                    area2.Flags.Bit22 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit22) != 0;
                    area2.Flags.Bit23 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit23) != 0;
                    area2.Flags.Bit24 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit24) != 0;
                    area2.Flags.Bit25 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit25) != 0;
                    area2.Flags.Bit26 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit26) != 0;
                    area2.Flags.Bit27 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit27) != 0;
                    area2.Flags.Bit28 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit28) != 0;
                    area2.Flags.Bit29 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit29) != 0;
                    area2.Flags.Bit30 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit30) != 0;
                    area2.Flags.Bit31 = (binaryAreas[cummulativeAreaCount + i].Flags & Common.Bit31) != 0;

                    area2.LoadingMos = Common.TryGetString(binaryAreas[cummulativeAreaCount + i].LoadingMos);
                    area2.LongName = Common.TryGetString(binaryAreas[cummulativeAreaCount + i].LongName);
                    area2.Name = Common.ReadString(binaryAreas[cummulativeAreaCount + i].Name, TlkFile);
                    area2.SequenceBam = binaryAreas[cummulativeAreaCount + i].SequenceBam;
                    area2.ShortName = Common.TryGetString(binaryAreas[cummulativeAreaCount + i].ShortName);
                    area2.Unused = binaryAreas[cummulativeAreaCount + i].Unused;
                    area2.XCoordinate = binaryAreas[cummulativeAreaCount + i].XCoordinate;
                    area2.YCoordinate = binaryAreas[cummulativeAreaCount + i].YCoordinate;

                    var northLinks = new List<WmapAreaLink>();
                    for (int j = 0; j < binaryAreas[cummulativeAreaCount + i].NorthLinkCount; j++)
                    {
                        var offset = j + (binaryAreas[cummulativeAreaCount + i].NorthLinkIndex - worldmap.AreaLinkOffset) / 216;
                        var link = new WmapAreaLink();

                        link.DefaultEntryLocation = CalculateDefaultEntryLocationFlags(link.DefaultEntryLocation, binaryLinks[offset].DefaultEntryLocation);
                        link.DestintationAreaIndex = binaryLinks[offset].DestintationAreaIndex;
                        link.EntryPoint = Common.TryGetString(binaryLinks[offset].EntryPoint);
                        link.RandomEncounterArea1 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea1);
                        link.RandomEncounterArea2 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea2);
                        link.RandomEncounterArea3 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea3);
                        link.RandomEncounterArea4 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea4);
                        link.RandomEncounterArea5 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea5);
                        link.RandomEncounterProbability = binaryLinks[offset].RandomEncounterProbability;
                        link.TravelTime = binaryLinks[offset].TravelTime;
                        link.Unused = binaryLinks[offset].Unused;
                        northLinks.Add(link);
                    }

                    var eastLinks = new List<WmapAreaLink>();
                    for (int j = 0; j < binaryAreas[cummulativeAreaCount + i].EastLinkCount; j++)
                    {
                        var offset = j + (binaryAreas[cummulativeAreaCount + i].EastLinkIndex - worldmap.AreaLinkOffset) / 216;
                        var link = new WmapAreaLink();
                        link.DefaultEntryLocation = CalculateDefaultEntryLocationFlags(link.DefaultEntryLocation, binaryLinks[offset].DefaultEntryLocation);
                        link.DestintationAreaIndex = binaryLinks[offset].DestintationAreaIndex;
                        link.EntryPoint = Common.TryGetString(binaryLinks[offset].EntryPoint);
                        link.RandomEncounterArea1 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea1);
                        link.RandomEncounterArea2 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea2);
                        link.RandomEncounterArea3 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea3);
                        link.RandomEncounterArea4 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea4);
                        link.RandomEncounterArea5 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea5);
                        link.RandomEncounterProbability = binaryLinks[offset].RandomEncounterProbability;
                        link.TravelTime = binaryLinks[offset].TravelTime;
                        link.Unused = binaryLinks[offset].Unused;
                        eastLinks.Add(link);
                    }

                    var southLinks = new List<WmapAreaLink>();
                    for (int j = 0; j < binaryAreas[cummulativeAreaCount + i].SouthLinkCount; j++)
                    {
                        var offset = j + (binaryAreas[cummulativeAreaCount + i].SouthLinkIndex - worldmap.AreaLinkOffset) / 216;
                        var link = new WmapAreaLink();
                        link.DefaultEntryLocation = CalculateDefaultEntryLocationFlags(link.DefaultEntryLocation, binaryLinks[offset].DefaultEntryLocation);
                        link.DestintationAreaIndex = binaryLinks[offset].DestintationAreaIndex;
                        link.EntryPoint = Common.TryGetString(binaryLinks[offset].EntryPoint);
                        link.RandomEncounterArea1 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea1);
                        link.RandomEncounterArea2 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea2);
                        link.RandomEncounterArea3 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea3);
                        link.RandomEncounterArea4 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea4);
                        link.RandomEncounterArea5 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea5);
                        link.RandomEncounterProbability = binaryLinks[offset].RandomEncounterProbability;
                        link.TravelTime = binaryLinks[offset].TravelTime;
                        link.Unused = binaryLinks[offset].Unused;
                        southLinks.Add(link);
                    }

                    var westLinks = new List<WmapAreaLink>();
                    for (int j = 0; j < binaryAreas[cummulativeAreaCount + i].WestLinkCount; j++)
                    {
                        var offset = j + (binaryAreas[cummulativeAreaCount + i].WestLinkIndex - worldmap.AreaLinkOffset) / 216;
                        var link = new WmapAreaLink();
                        link.DefaultEntryLocation = CalculateDefaultEntryLocationFlags(link.DefaultEntryLocation, binaryLinks[offset].DefaultEntryLocation);
                        link.DestintationAreaIndex = binaryLinks[offset].DestintationAreaIndex;
                        link.EntryPoint = Common.TryGetString(binaryLinks[offset].EntryPoint);
                        link.RandomEncounterArea1 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea1);
                        link.RandomEncounterArea2 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea2);
                        link.RandomEncounterArea3 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea3);
                        link.RandomEncounterArea4 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea4);
                        link.RandomEncounterArea5 = Common.TryGetString(binaryLinks[offset].RandomEncounterArea5);
                        link.RandomEncounterProbability = binaryLinks[offset].RandomEncounterProbability;
                        link.TravelTime = binaryLinks[offset].TravelTime;
                        link.Unused = binaryLinks[offset].Unused;
                        westLinks.Add(link);
                    }

                    area2.EastLinks = eastLinks;
                    area2.NorthLinks = northLinks;
                    area2.SouthLinks = southLinks;
                    area2.WestLinks = westLinks;
                    areas2.Add(area2);
                }

                var worldmap2 = new WmapWorldmap();
                worldmap2.AreaName = Common.ReadString(worldmap.AreaName, TlkFile);
                worldmap2.areas = areas2;
                worldmap2.BackgroundMos = Common.TryGetString(worldmap.BackgroundMos);
                worldmap2.Height = worldmap.Height;
                worldmap2.MapIconsBam = Common.TryGetString(worldmap.MapIconsBam);
                worldmap2.MapNumber = worldmap.MapNumber;
                worldmap2.Unused = worldmap.Unused;
                worldmap2.StartCenteredOnX = worldmap.StartCenteredOnX;
                worldmap2.StartCenteredOnY = worldmap.StartCenteredOnY;
                worldmap2.Width = worldmap.Width;
                worldmap2.Flags.ColouredIcons = (worldmap.Flags & Common.Bit0) != 0;
                worldmap2.Flags.IgnorePalette = (worldmap.Flags & Common.Bit1) != 0;
                worldmap2.Flags.Bit2 = (worldmap.Flags & Common.Bit2) != 0;
                worldmap2.Flags.Bit3 = (worldmap.Flags & Common.Bit3) != 0;
                worldmap2.Flags.Bit4 = (worldmap.Flags & Common.Bit4) != 0;
                worldmap2.Flags.Bit5 = (worldmap.Flags & Common.Bit5) != 0;
                worldmap2.Flags.Bit6 = (worldmap.Flags & Common.Bit6) != 0;
                worldmap2.Flags.Bit7 = (worldmap.Flags & Common.Bit7) != 0;
                worldmap2.Flags.Bit8 = (worldmap.Flags & Common.Bit8) != 0;
                worldmap2.Flags.Bit9 = (worldmap.Flags & Common.Bit9) != 0;
                worldmap2.Flags.Bit10 = (worldmap.Flags & Common.Bit10) != 0;
                worldmap2.Flags.Bit11 = (worldmap.Flags & Common.Bit11) != 0;
                worldmap2.Flags.Bit12 = (worldmap.Flags & Common.Bit12) != 0;
                worldmap2.Flags.Bit13 = (worldmap.Flags & Common.Bit13) != 0;
                worldmap2.Flags.Bit14 = (worldmap.Flags & Common.Bit14) != 0;
                worldmap2.Flags.Bit15 = (worldmap.Flags & Common.Bit15) != 0;
                worldmap2.Flags.Bit16 = (worldmap.Flags & Common.Bit16) != 0;
                worldmap2.Flags.Bit17 = (worldmap.Flags & Common.Bit17) != 0;
                worldmap2.Flags.Bit18 = (worldmap.Flags & Common.Bit18) != 0;
                worldmap2.Flags.Bit19 = (worldmap.Flags & Common.Bit19) != 0;
                worldmap2.Flags.Bit20 = (worldmap.Flags & Common.Bit20) != 0;
                worldmap2.Flags.Bit21 = (worldmap.Flags & Common.Bit21) != 0;
                worldmap2.Flags.Bit22 = (worldmap.Flags & Common.Bit22) != 0;
                worldmap2.Flags.Bit23 = (worldmap.Flags & Common.Bit23) != 0;
                worldmap2.Flags.Bit24 = (worldmap.Flags & Common.Bit24) != 0;
                worldmap2.Flags.Bit25 = (worldmap.Flags & Common.Bit25) != 0;
                worldmap2.Flags.Bit26 = (worldmap.Flags & Common.Bit26) != 0;
                worldmap2.Flags.Bit27 = (worldmap.Flags & Common.Bit27) != 0;
                worldmap2.Flags.Bit28 = (worldmap.Flags & Common.Bit28) != 0;
                worldmap2.Flags.Bit29 = (worldmap.Flags & Common.Bit29) != 0;
                worldmap2.Flags.Bit30 = (worldmap.Flags & Common.Bit30) != 0;
                worldmap2.Flags.Bit31 = (worldmap.Flags & Common.Bit31) != 0;

                wmpFile.worldmaps.Add(worldmap2);
                cummulativeAreaCount++;
            }

            wmpFile.Checksum = HashGenerator.GenerateKey(wmpFile);
            return wmpFile;
        }

        private WmapAreaLinkFlags CalculateDefaultEntryLocationFlags(WmapAreaLinkFlags flags, int value)
        {
            flags.MoveToNorthernSide = (value & Common.Bit0) != 0;
            flags.MoveToEasternSide = (value & Common.Bit1) != 0;
            flags.MoveToSouthernSide = (value & Common.Bit2) != 0;
            flags.MoveToWesternSide = (value & Common.Bit3) != 0;
            flags.Bit4 = (value & Common.Bit4) != 0;
            flags.Bit5 = (value & Common.Bit5) != 0;
            flags.Bit6 = (value & Common.Bit6) != 0;
            flags.Bit7 = (value & Common.Bit7) != 0;
            flags.Bit8 = (value & Common.Bit8) != 0;
            flags.Bit9 = (value & Common.Bit9) != 0;
            flags.Bit10 = (value & Common.Bit10) != 0;
            flags.Bit11 = (value & Common.Bit11) != 0;
            flags.Bit12 = (value & Common.Bit12) != 0;
            flags.Bit13 = (value & Common.Bit13) != 0;
            flags.Bit14 = (value & Common.Bit14) != 0;
            flags.Bit15 = (value & Common.Bit15) != 0;
            flags.Bit16 = (value & Common.Bit16) != 0;
            flags.Bit17 = (value & Common.Bit17) != 0;
            flags.Bit18 = (value & Common.Bit18) != 0;
            flags.Bit19 = (value & Common.Bit19) != 0;
            flags.Bit20 = (value & Common.Bit20) != 0;
            flags.Bit21 = (value & Common.Bit21) != 0;
            flags.Bit22 = (value & Common.Bit22) != 0;
            flags.Bit23 = (value & Common.Bit23) != 0;
            flags.Bit24 = (value & Common.Bit24) != 0;
            flags.Bit25 = (value & Common.Bit25) != 0;
            flags.Bit26 = (value & Common.Bit26) != 0;
            flags.Bit27 = (value & Common.Bit27) != 0;
            flags.Bit28 = (value & Common.Bit28) != 0;
            flags.Bit29 = (value & Common.Bit29) != 0;
            flags.Bit30 = (value & Common.Bit30) != 0;
            flags.Bit31 = (value & Common.Bit31) != 0;
            return flags;
        }
    }

    class LinkInfo : IComparable
    {
        public string Name { get; set; }
        public Int32 Offset { get; set; }
        public Int32 Count { get; set; }
        public bool Exclude { get; set; }

        public LinkInfo(string Name, Int32 Offset, Int32 Count)
        {
            this.Name = Name;
            this.Offset = Offset;
            this.Count = Count;
        }

        public int CompareTo(object obj)
        {
            var info = (obj as LinkInfo);

            if (this.Exclude)
                return 1;

            if (info.Exclude)
                return -1;

            if (info.Offset == this.Offset)
                return 0;

            if (this.Offset < info.Offset)
            {
                return -1;
            }

            if (this.Offset > info.Offset)
            {
                return 1;
            }
            return 0;
        }
    }
}