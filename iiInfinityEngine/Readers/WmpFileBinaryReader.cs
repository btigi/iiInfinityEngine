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
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var f = Read(fs);
                f.Filename = Path.GetFileName(filename);
                return f;
            }
        }
        
        public WmpFile Read(Stream s)
        {
            using (BinaryReader br = new BinaryReader(s))
            {
                var wmpFile = ParseFile(br);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                wmpFile.OriginalFile = ParseFile(br);
                return wmpFile;
            }
        }

        private WmpFile ParseFile(BinaryReader br)
        {

            var header = (WmapHeaderBinary)Common.ReadStruct(br, typeof(WmapHeaderBinary));

            List<WmapWorldmapBinary> binaryWorldmaps = new List<WmapWorldmapBinary>();
            List<WmapAreaBinary> binaryAreas = new List<WmapAreaBinary>();
            List<WmapAreaLinkBinary> binaryLinks = new List<WmapAreaLinkBinary>();

            if (header.ftype.ToString() != "WMAP")
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
            WmpFile wmpFile = new WmpFile();
            foreach (var worldmap in binaryWorldmaps)
            {
                var areas2 = new List<WmapArea>();
                for (int i = 0; i < worldmap.AreaCount; i++)
                {
                    var area2 = new WmapArea();
                    area2.AreaFilename = binaryAreas[cummulativeAreaCount + i].AreaFilename.ToString();
                    area2.Caption = Common.ReadString(binaryAreas[cummulativeAreaCount + i].Caption, TlkFile);
                    area2.Flags = binaryAreas[cummulativeAreaCount + i].Flags;
                    area2.LoadingMos = binaryAreas[cummulativeAreaCount + i].LoadingMos.ToString();
                    area2.LongName = binaryAreas[cummulativeAreaCount + i].LongName.ToString();
                    area2.Name = Common.ReadString(binaryAreas[cummulativeAreaCount + i].Name, TlkFile);
                    area2.SequenceBam = binaryAreas[cummulativeAreaCount + i].SequenceBam;
                    area2.ShortName = binaryAreas[cummulativeAreaCount + i].ShortName.ToString();
                    area2.Unknown0070 = binaryAreas[cummulativeAreaCount + i].Unknown;
                    area2.XCoordinate = binaryAreas[cummulativeAreaCount + i].XCoordinate;
                    area2.YCoordinate = binaryAreas[cummulativeAreaCount + i].YCoordinate;


                    var northLinks = new List<WmapAreaLink>();
                    for (int j = 0; j < binaryAreas[cummulativeAreaCount + i].NorthLinkCount; j++)
                    {
                        var offset = j + (binaryAreas[cummulativeAreaCount + i].NorthLinkIndex - worldmap.AreaLinkOffset) / 216;
                        var link = new WmapAreaLink();
                        link.DefaultEntryLocation = binaryLinks[offset].DefaultEntryLocation;
                        link.DestintationAreaIndex = binaryLinks[offset].DestintationAreaIndex;
                        link.EntryPoint = binaryLinks[offset].EntryPoint.ToString();
                        link.RandomEncounterArea1 = binaryLinks[offset].RandomEncounterArea1.ToString();
                        link.RandomEncounterArea2 = binaryLinks[offset].RandomEncounterArea2.ToString();
                        link.RandomEncounterArea3 = binaryLinks[offset].RandomEncounterArea3.ToString();
                        link.RandomEncounterArea4 = binaryLinks[offset].RandomEncounterArea4.ToString();
                        link.RandomEncounterArea5 = binaryLinks[offset].RandomEncounterArea5.ToString();
                        link.RandomEncounterProbability = binaryLinks[offset].RandomEncounterProbability;
                        link.TravelTime = binaryLinks[offset].TravelTime;
                        link.Unknown0058 = binaryLinks[offset].Unknown;
                        northLinks.Add(link);
                    }

                    var eastLinks = new List<WmapAreaLink>();
                    for (int j = 0; j < binaryAreas[cummulativeAreaCount + i].EastLinkCount; j++)
                    {
                        var offset = j + (binaryAreas[cummulativeAreaCount + i].EastLinkIndex - worldmap.AreaLinkOffset) / 216;
                        var link = new WmapAreaLink();
                        link.DefaultEntryLocation = binaryLinks[offset].DefaultEntryLocation;
                        link.DestintationAreaIndex = binaryLinks[offset].DestintationAreaIndex;
                        link.EntryPoint = binaryLinks[offset].EntryPoint.ToString();
                        link.RandomEncounterArea1 = binaryLinks[offset].RandomEncounterArea1.ToString();
                        link.RandomEncounterArea2 = binaryLinks[offset].RandomEncounterArea2.ToString();
                        link.RandomEncounterArea3 = binaryLinks[offset].RandomEncounterArea3.ToString();
                        link.RandomEncounterArea4 = binaryLinks[offset].RandomEncounterArea4.ToString();
                        link.RandomEncounterArea5 = binaryLinks[offset].RandomEncounterArea5.ToString();
                        link.RandomEncounterProbability = binaryLinks[offset].RandomEncounterProbability;
                        link.TravelTime = binaryLinks[offset].TravelTime;
                        link.Unknown0058 = binaryLinks[offset].Unknown;
                        eastLinks.Add(link);
                    }

                    var southLinks = new List<WmapAreaLink>();
                    for (int j = 0; j < binaryAreas[cummulativeAreaCount + i].SouthLinkCount; j++)
                    {
                        var offset = j + (binaryAreas[cummulativeAreaCount + i].SouthLinkIndex - worldmap.AreaLinkOffset) / 216;
                        var link = new WmapAreaLink();
                        link.DefaultEntryLocation = binaryLinks[offset].DefaultEntryLocation;
                        link.DestintationAreaIndex = binaryLinks[offset].DestintationAreaIndex;
                        link.EntryPoint = binaryLinks[offset].EntryPoint.ToString();
                        link.RandomEncounterArea1 = binaryLinks[offset].RandomEncounterArea1.ToString();
                        link.RandomEncounterArea2 = binaryLinks[offset].RandomEncounterArea2.ToString();
                        link.RandomEncounterArea3 = binaryLinks[offset].RandomEncounterArea3.ToString();
                        link.RandomEncounterArea4 = binaryLinks[offset].RandomEncounterArea4.ToString();
                        link.RandomEncounterArea5 = binaryLinks[offset].RandomEncounterArea5.ToString();
                        link.RandomEncounterProbability = binaryLinks[offset].RandomEncounterProbability;
                        link.TravelTime = binaryLinks[offset].TravelTime;
                        link.Unknown0058 = binaryLinks[offset].Unknown;
                        southLinks.Add(link);
                    }

                    var westLinks = new List<WmapAreaLink>();
                    for (int j = 0; j < binaryAreas[cummulativeAreaCount + i].WestLinkCount; j++)
                    {
                        var offset = j + (binaryAreas[cummulativeAreaCount + i].WestLinkIndex - worldmap.AreaLinkOffset) / 216;
                        var link = new WmapAreaLink();
                        link.DefaultEntryLocation = binaryLinks[offset].DefaultEntryLocation;
                        link.DestintationAreaIndex = binaryLinks[offset].DestintationAreaIndex;
                        link.EntryPoint = binaryLinks[offset].EntryPoint.ToString();
                        link.RandomEncounterArea1 = binaryLinks[offset].RandomEncounterArea1.ToString();
                        link.RandomEncounterArea2 = binaryLinks[offset].RandomEncounterArea2.ToString();
                        link.RandomEncounterArea3 = binaryLinks[offset].RandomEncounterArea3.ToString();
                        link.RandomEncounterArea4 = binaryLinks[offset].RandomEncounterArea4.ToString();
                        link.RandomEncounterArea5 = binaryLinks[offset].RandomEncounterArea5.ToString();
                        link.RandomEncounterProbability = binaryLinks[offset].RandomEncounterProbability;
                        link.TravelTime = binaryLinks[offset].TravelTime;
                        link.Unknown0058 = binaryLinks[offset].Unknown;
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
                worldmap2.BackgroundMos = worldmap.BackgroundMos.ToString();
                worldmap2.Height = worldmap.Height;
                worldmap2.MapIconsBam = worldmap.MapIconsBam.ToString();
                worldmap2.MapNumber = worldmap.MapNumber;
                worldmap2.Unknown0038 = worldmap.Unknown;
                worldmap2.Unknown0018 = worldmap.Unknown1;
                worldmap2.Unknown001c = worldmap.Unknown2;
                worldmap2.Width = worldmap.Width;

                wmpFile.worldmaps.Add(worldmap2);
                cummulativeAreaCount++;
            }

            wmpFile.Checksum = MD5HashGenerator.GenerateKey(wmpFile);
            return wmpFile;
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