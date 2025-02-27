using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Writers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace ii.InfinityEngine.Writers
{
    public class WmapFileBinaryWriter : IWmpFileWriter
    {
        const int WmapBinaryHeaderSize = 16;
        const int WmapBinaryWorldmapSize = 184;
        const int WmapBinaryAreaSize = 240;

        public TlkFile TlkFile { get; set; }
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not WmpFile)
                throw new ArgumentException("File is not a valid wmap file");

            var wmpFile = file as WmpFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(wmpFile) == wmpFile.Checksum))
                return false;

            var binaryWorldmaps = new List<WmapWorldmapBinary>();
            var binaryAreas = new List<WmapAreaBinary>();
            var binaryLinks = new List<WmapAreaLinkBinary>();

            foreach (var worldmap in wmpFile.worldmaps)
            {
                var worldmapBinary = new WmapWorldmapBinary();
                worldmapBinary.AreaName = Common.WriteString(worldmap.AreaName, TlkFile);
                worldmapBinary.BackgroundMos = worldmap.BackgroundMos;
                worldmapBinary.Height = worldmap.Height;
                worldmapBinary.MapIconsBam = worldmap.MapIconsBam;
                worldmapBinary.MapNumber = worldmap.MapNumber;
                worldmapBinary.Unused = worldmap.Unused;
                worldmapBinary.StartCenteredOnX = worldmap.StartCenteredOnX;
                worldmapBinary.StartCenteredOnY = worldmap.StartCenteredOnY;
                worldmapBinary.Width = worldmap.Width;
                worldmapBinary.Flags = worldmap.Flags.ColouredIcons ? worldmapBinary.Flags | Common.Bit0 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.ColouredIcons ? worldmapBinary.Flags | Common.Bit1 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit2 ? worldmapBinary.Flags | Common.Bit2 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit3 ? worldmapBinary.Flags | Common.Bit3 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit4 ? worldmapBinary.Flags | Common.Bit4 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit5 ? worldmapBinary.Flags | Common.Bit5 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit6 ? worldmapBinary.Flags | Common.Bit6 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit7 ? worldmapBinary.Flags | Common.Bit7 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit8 ? worldmapBinary.Flags | Common.Bit8 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit9 ? worldmapBinary.Flags | Common.Bit9 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit10 ? worldmapBinary.Flags | Common.Bit10 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit11 ? worldmapBinary.Flags | Common.Bit11 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit12 ? worldmapBinary.Flags | Common.Bit12 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit13 ? worldmapBinary.Flags | Common.Bit13 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit14 ? worldmapBinary.Flags | Common.Bit14 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit15 ? worldmapBinary.Flags | Common.Bit15 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit16 ? worldmapBinary.Flags | Common.Bit16 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit17 ? worldmapBinary.Flags | Common.Bit17 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit18 ? worldmapBinary.Flags | Common.Bit18 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit19 ? worldmapBinary.Flags | Common.Bit19 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit20 ? worldmapBinary.Flags | Common.Bit20 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit21 ? worldmapBinary.Flags | Common.Bit21 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit22 ? worldmapBinary.Flags | Common.Bit22 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit23 ? worldmapBinary.Flags | Common.Bit23 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit24 ? worldmapBinary.Flags | Common.Bit24 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit25 ? worldmapBinary.Flags | Common.Bit25 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit26 ? worldmapBinary.Flags | Common.Bit26 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit27 ? worldmapBinary.Flags | Common.Bit27 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit28 ? worldmapBinary.Flags | Common.Bit28 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit29 ? worldmapBinary.Flags | Common.Bit29 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit30 ? worldmapBinary.Flags | Common.Bit30 : worldmapBinary.Flags;
                worldmapBinary.Flags = worldmap.Flags.Bit31 ? worldmapBinary.Flags | Common.Bit31 : worldmapBinary.Flags;

                var linkCount = 0;
                var areaCount = 0;
                foreach (var area in worldmap.areas)
                {
                    var binaryArea = new WmapAreaBinary();
                    binaryArea.AreaFilename = area.AreaFilename;
                    binaryArea.Caption = Common.WriteString(area.Caption, TlkFile);
                    binaryArea.Flags = area.Flags.AreaVisible ? binaryArea.Flags | Common.Bit0 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.AreaVisibleFromAdjacent ? binaryArea.Flags | Common.Bit1 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Reachable ? binaryArea.Flags | Common.Bit2 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.AlreadyVisited ? binaryArea.Flags | Common.Bit3 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit4 ? binaryArea.Flags | Common.Bit4 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit5 ? binaryArea.Flags | Common.Bit5 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit6 ? binaryArea.Flags | Common.Bit6 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit7 ? binaryArea.Flags | Common.Bit7 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit8 ? binaryArea.Flags | Common.Bit8 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit9 ? binaryArea.Flags | Common.Bit9 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit10 ? binaryArea.Flags | Common.Bit10 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit11 ? binaryArea.Flags | Common.Bit11 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit12 ? binaryArea.Flags | Common.Bit12 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit13 ? binaryArea.Flags | Common.Bit13 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit14 ? binaryArea.Flags | Common.Bit14 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit15 ? binaryArea.Flags | Common.Bit15 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit16 ? binaryArea.Flags | Common.Bit16 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit17 ? binaryArea.Flags | Common.Bit17 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit18 ? binaryArea.Flags | Common.Bit18 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit19 ? binaryArea.Flags | Common.Bit19 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit20 ? binaryArea.Flags | Common.Bit20 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit21 ? binaryArea.Flags | Common.Bit21 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit22 ? binaryArea.Flags | Common.Bit22 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit23 ? binaryArea.Flags | Common.Bit23 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit24 ? binaryArea.Flags | Common.Bit24 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit25 ? binaryArea.Flags | Common.Bit25 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit26 ? binaryArea.Flags | Common.Bit26 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit27 ? binaryArea.Flags | Common.Bit27 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit28 ? binaryArea.Flags | Common.Bit28 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit29 ? binaryArea.Flags | Common.Bit29 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit30 ? binaryArea.Flags | Common.Bit30 : binaryArea.Flags;
                    binaryArea.Flags = area.Flags.Bit31 ? binaryArea.Flags | Common.Bit31 : binaryArea.Flags;
                    binaryArea.LoadingMos = area.LoadingMos;
                    binaryArea.LongName = area.LongName;
                    binaryArea.Name = Common.WriteString(area.Name, TlkFile);
                    binaryArea.SequenceBam = area.SequenceBam;
                    binaryArea.ShortName = area.ShortName;
                    binaryArea.Unused = area.Unused;
                    binaryArea.XCoordinate = area.XCoordinate;
                    binaryArea.YCoordinate = area.YCoordinate;

                    binaryArea.NorthLinkCount = area.NorthLinks.Count;
                    binaryArea.NorthLinkIndex = binaryLinks.Count;
                    foreach (var link in area.NorthLinks)
                    {
                        var linkBinary = new WmapAreaLinkBinary();
                        linkBinary.DefaultEntryLocation = CalculateDefaultEntryLocationFlags(linkBinary.DefaultEntryLocation, link.DefaultEntryLocation);
                        linkBinary.DestintationAreaIndex = link.DestintationAreaIndex;
                        linkBinary.EntryPoint = link.EntryPoint;
                        linkBinary.RandomEncounterArea1 = link.RandomEncounterArea1;
                        linkBinary.RandomEncounterArea2 = link.RandomEncounterArea2;
                        linkBinary.RandomEncounterArea3 = link.RandomEncounterArea3;
                        linkBinary.RandomEncounterArea4 = link.RandomEncounterArea4;
                        linkBinary.RandomEncounterArea5 = link.RandomEncounterArea5;
                        linkBinary.RandomEncounterProbability = link.RandomEncounterProbability;
                        linkBinary.TravelTime = link.TravelTime;
                        linkBinary.Unused = link.Unused;
                        binaryLinks.Add(linkBinary);
                        linkCount++;
                    }

                    binaryArea.EastLinkCount = area.EastLinks.Count;
                    binaryArea.EastLinkIndex = binaryLinks.Count;
                    foreach (var link in area.EastLinks)
                    {
                        var linkBinary = new WmapAreaLinkBinary();
                        linkBinary.DefaultEntryLocation = CalculateDefaultEntryLocationFlags(linkBinary.DefaultEntryLocation, link.DefaultEntryLocation);
                        linkBinary.DestintationAreaIndex = link.DestintationAreaIndex;
                        linkBinary.EntryPoint = link.EntryPoint;
                        linkBinary.RandomEncounterArea1 = link.RandomEncounterArea1;
                        linkBinary.RandomEncounterArea2 = link.RandomEncounterArea2;
                        linkBinary.RandomEncounterArea3 = link.RandomEncounterArea3;
                        linkBinary.RandomEncounterArea4 = link.RandomEncounterArea4;
                        linkBinary.RandomEncounterArea5 = link.RandomEncounterArea5;
                        linkBinary.RandomEncounterProbability = link.RandomEncounterProbability;
                        linkBinary.TravelTime = link.TravelTime;
                        linkBinary.Unused = link.Unused;
                        binaryLinks.Add(linkBinary);
                        linkCount++;
                    }

                    binaryArea.SouthLinkCount = area.SouthLinks.Count;
                    binaryArea.SouthLinkIndex = binaryLinks.Count;
                    foreach (var link in area.SouthLinks)
                    {
                        var linkBinary = new WmapAreaLinkBinary();
                        linkBinary.DefaultEntryLocation = CalculateDefaultEntryLocationFlags(linkBinary.DefaultEntryLocation, link.DefaultEntryLocation);
                        linkBinary.DestintationAreaIndex = link.DestintationAreaIndex;
                        linkBinary.EntryPoint = link.EntryPoint;
                        linkBinary.RandomEncounterArea1 = link.RandomEncounterArea1;
                        linkBinary.RandomEncounterArea2 = link.RandomEncounterArea2;
                        linkBinary.RandomEncounterArea3 = link.RandomEncounterArea3;
                        linkBinary.RandomEncounterArea4 = link.RandomEncounterArea4;
                        linkBinary.RandomEncounterArea5 = link.RandomEncounterArea5;
                        linkBinary.RandomEncounterProbability = link.RandomEncounterProbability;
                        linkBinary.TravelTime = link.TravelTime;
                        linkBinary.Unused = link.Unused;
                        binaryLinks.Add(linkBinary);
                        linkCount++;
                    }

                    binaryArea.WestLinkCount = area.WestLinks.Count;
                    binaryArea.WestLinkIndex = binaryLinks.Count;
                    foreach (var link in area.WestLinks)
                    {
                        var linkBinary = new WmapAreaLinkBinary();
                        linkBinary.DefaultEntryLocation = CalculateDefaultEntryLocationFlags(linkBinary.DefaultEntryLocation, link.DefaultEntryLocation);
                        linkBinary.DestintationAreaIndex = link.DestintationAreaIndex;
                        linkBinary.EntryPoint = link.EntryPoint;
                        linkBinary.RandomEncounterArea1 = link.RandomEncounterArea1;
                        linkBinary.RandomEncounterArea2 = link.RandomEncounterArea2;
                        linkBinary.RandomEncounterArea3 = link.RandomEncounterArea3;
                        linkBinary.RandomEncounterArea4 = link.RandomEncounterArea4;
                        linkBinary.RandomEncounterArea5 = link.RandomEncounterArea5;
                        linkBinary.RandomEncounterProbability = link.RandomEncounterProbability;
                        linkBinary.TravelTime = link.TravelTime;
                        linkBinary.Unused = link.Unused;
                        binaryLinks.Add(linkBinary);
                        linkCount++;
                    }

                    binaryAreas.Add(binaryArea);
                    areaCount++;
                }

                worldmapBinary.AreaCount = areaCount;
                worldmapBinary.AreaOffset = WmapBinaryHeaderSize + (WmapBinaryWorldmapSize * wmpFile.worldmaps.Count);
                worldmapBinary.AreaLinkCount = linkCount;
                worldmapBinary.AreaLinkOffset = WmapBinaryHeaderSize + (WmapBinaryWorldmapSize * wmpFile.worldmaps.Count) + (WmapBinaryAreaSize * binaryAreas.Count);

                binaryWorldmaps.Add(worldmapBinary);
            }

            var headerBinary = new WmapHeaderBinary();
            headerBinary.ftype = new array4() { character1 = 'W', character2 = 'M', character3 = 'A', character4 = 'P' };
            headerBinary.fversion = new array4() { character1 = 'V', character2 = '1', character3 = '.', character4 = '0' };
            headerBinary.WorldmapCount = binaryWorldmaps.Count;
            headerBinary.WorldmapOffset = WmapBinaryHeaderSize;

            using var s = new MemoryStream();
            using var bw = new BinaryWriter(s);
            var headerAsBytes = Common.WriteStruct(headerBinary);
            bw.Write(headerAsBytes);

            foreach (var worldmap in binaryWorldmaps)
            {
                var worldmapAsBytes = Common.WriteStruct(worldmap);
                bw.Write(worldmapAsBytes);
            }

            foreach (var area in binaryAreas)
            {
                var areaAsBytes = Common.WriteStruct(area);
                bw.Write(areaAsBytes);
            }

            foreach (var link in binaryLinks)
            {
                var linkAsBytes = Common.WriteStruct(link);
                bw.Write(linkAsBytes);
            }

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            bw.BaseStream.Position = 0;
            bw.BaseStream.CopyTo(fs);
            fs.Flush(flushToDisk: true);
            return true;
        }


        private int CalculateDefaultEntryLocationFlags(int value, WmapAreaLinkFlags flags)
        {
            value = flags.MoveToNorthernSide ? value | Common.Bit0 : value;
            value = flags.MoveToEasternSide ? value | Common.Bit1 : value;
            value = flags.MoveToSouthernSide ? value | Common.Bit2 : value;
            value = flags.MoveToWesternSide ? value | Common.Bit3 : value;
            value = flags.Bit4 ? value | Common.Bit4 : value;
            value = flags.Bit5 ? value | Common.Bit5 : value;
            value = flags.Bit6 ? value | Common.Bit6 : value;
            value = flags.Bit7 ? value | Common.Bit7 : value;
            value = flags.Bit8 ? value | Common.Bit8 : value;
            value = flags.Bit9 ? value | Common.Bit9 : value;
            value = flags.Bit10 ? value | Common.Bit10 : value;
            value = flags.Bit11 ? value | Common.Bit11 : value;
            value = flags.Bit12 ? value | Common.Bit12 : value;
            value = flags.Bit13 ? value | Common.Bit13 : value;
            value = flags.Bit14 ? value | Common.Bit14 : value;
            value = flags.Bit15 ? value | Common.Bit15 : value;
            value = flags.Bit16 ? value | Common.Bit16 : value;
            value = flags.Bit17 ? value | Common.Bit17 : value;
            value = flags.Bit18 ? value | Common.Bit18 : value;
            value = flags.Bit19 ? value | Common.Bit19 : value;
            value = flags.Bit20 ? value | Common.Bit20 : value;
            value = flags.Bit21 ? value | Common.Bit21 : value;
            value = flags.Bit22 ? value | Common.Bit22 : value;
            value = flags.Bit23 ? value | Common.Bit23 : value;
            value = flags.Bit24 ? value | Common.Bit24 : value;
            value = flags.Bit25 ? value | Common.Bit25 : value;
            value = flags.Bit26 ? value | Common.Bit26 : value;
            value = flags.Bit27 ? value | Common.Bit27 : value;
            value = flags.Bit28 ? value | Common.Bit28 : value;
            value = flags.Bit29 ? value | Common.Bit29 : value;
            value = flags.Bit30 ? value | Common.Bit30 : value;
            value = flags.Bit31 ? value | Common.Bit31 : value;
            return value;
        }
    }
}