using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;

namespace iiInfinityEngine.Core.Writers
{
    public class WmapFileBinaryWriter : IWmpFileWriter
    {
        const int WmapBinaryHeaderSize = 16;
        const int WmapBinaryWorldmapSize = 184;
        const int WmapBinaryAreaSize = 240;
        
        public TlkFile TlkFile { get; set; }
        public BackupManager BackupManger { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (!(file is WmpFile))
                throw new ArgumentException("File is not a valid creature file");

            var wmpFile = file as WmpFile;

            if (!(forceSave) && (MD5HashGenerator.GenerateKey(wmpFile) == wmpFile.Checksum))
                return false;

            List<WmapWorldmapBinary> binaryWorldmaps = new List<WmapWorldmapBinary>();
            List<WmapAreaBinary> binaryAreas = new List<WmapAreaBinary>();
            List<WmapAreaLinkBinary> binaryLinks = new List<WmapAreaLinkBinary>();

            foreach (var worldmap in wmpFile.worldmaps)
            {
                var worldmapBinary = new WmapWorldmapBinary();
                worldmapBinary.AreaName = Common.WriteString(worldmap.AreaName, TlkFile);
                worldmapBinary.BackgroundMos = new array8(worldmap.BackgroundMos);
                worldmapBinary.Height = worldmap.Height;
                worldmapBinary.MapIconsBam = new array8(worldmap.MapIconsBam);
                worldmapBinary.MapNumber = worldmap.MapNumber;
                worldmapBinary.Unknown = worldmap.Unknown0038;
                worldmapBinary.Unknown1 = worldmap.Unknown0018;
                worldmapBinary.Unknown2= worldmap.Unknown001c;
                worldmapBinary.Width = worldmap.Width;

                var linkCount = 0;
                var areaCount = 0;
                foreach (var area in worldmap.areas)
                {
                    var binaryArea = new WmapAreaBinary();
                    binaryArea.AreaFilename = new array8(area.AreaFilename);
                    binaryArea.Caption = Common.WriteString(area.Caption, TlkFile);
                    binaryArea.Flags = area.Flags;
                    binaryArea.LoadingMos = new array8(area.LoadingMos);
                    binaryArea.LongName = new array32(area.LongName);
                    binaryArea.Name = Common.WriteString(area.Name, TlkFile);
                    binaryArea.SequenceBam = area.SequenceBam;
                    binaryArea.ShortName = new array8(area.ShortName);
                    binaryArea.Unknown = area.Unknown0070;
                    binaryArea.XCoordinate = area.XCoordinate;
                    binaryArea.YCoordinate = area.YCoordinate;

                    binaryArea.NorthLinkCount = area.NorthLinks.Count;
                    binaryArea.NorthLinkIndex = binaryLinks.Count;
                    foreach (var link in area.NorthLinks)
                    {
                        var linkBinary = new WmapAreaLinkBinary();
                        linkBinary.DefaultEntryLocation = link.DefaultEntryLocation;
                        linkBinary.DestintationAreaIndex = link.DefaultEntryLocation;
                        linkBinary.EntryPoint = new array32(link.EntryPoint);
                        linkBinary.RandomEncounterArea1 = new array8(link.RandomEncounterArea1);
                        linkBinary.RandomEncounterArea2 = new array8(link.RandomEncounterArea2);
                        linkBinary.RandomEncounterArea3 = new array8(link.RandomEncounterArea3);
                        linkBinary.RandomEncounterArea4 = new array8(link.RandomEncounterArea4);
                        linkBinary.RandomEncounterArea5 = new array8(link.RandomEncounterArea5);
                        linkBinary.RandomEncounterProbability = link.RandomEncounterProbability;
                        linkBinary.TravelTime = link.TravelTime;
                        linkBinary.Unknown = link.Unknown0058;
                        binaryLinks.Add(linkBinary);
                        linkCount++;
                    }

                    binaryArea.EastLinkCount = area.EastLinks.Count;
                    binaryArea.EastLinkIndex = binaryLinks.Count;
                    foreach (var link in area.EastLinks)
                    {
                        var linkBinary = new WmapAreaLinkBinary();
                        linkBinary.DefaultEntryLocation = link.DefaultEntryLocation;
                        linkBinary.DestintationAreaIndex = link.DefaultEntryLocation;
                        linkBinary.EntryPoint = new array32(link.EntryPoint);
                        linkBinary.RandomEncounterArea1 = new array8(link.RandomEncounterArea1);
                        linkBinary.RandomEncounterArea2 = new array8(link.RandomEncounterArea2);
                        linkBinary.RandomEncounterArea3 = new array8(link.RandomEncounterArea3);
                        linkBinary.RandomEncounterArea4 = new array8(link.RandomEncounterArea4);
                        linkBinary.RandomEncounterArea5 = new array8(link.RandomEncounterArea5);
                        linkBinary.RandomEncounterProbability = link.RandomEncounterProbability;
                        linkBinary.TravelTime = link.TravelTime;
                        linkBinary.Unknown = link.Unknown0058;
                        binaryLinks.Add(linkBinary);
                        linkCount++;
                    }

                    binaryArea.SouthLinkCount = area.SouthLinks.Count;
                    binaryArea.SouthLinkIndex = binaryLinks.Count;
                    foreach (var link in area.SouthLinks)
                    {
                        var linkBinary = new WmapAreaLinkBinary();
                        linkBinary.DefaultEntryLocation = link.DefaultEntryLocation;
                        linkBinary.DestintationAreaIndex = link.DefaultEntryLocation;
                        linkBinary.EntryPoint = new array32(link.EntryPoint);
                        linkBinary.RandomEncounterArea1 = new array8(link.RandomEncounterArea1);
                        linkBinary.RandomEncounterArea2 = new array8(link.RandomEncounterArea2);
                        linkBinary.RandomEncounterArea3 = new array8(link.RandomEncounterArea3);
                        linkBinary.RandomEncounterArea4 = new array8(link.RandomEncounterArea4);
                        linkBinary.RandomEncounterArea5 = new array8(link.RandomEncounterArea5);
                        linkBinary.RandomEncounterProbability = link.RandomEncounterProbability;
                        linkBinary.TravelTime = link.TravelTime;
                        linkBinary.Unknown = link.Unknown0058;
                        binaryLinks.Add(linkBinary);
                        linkCount++;
                    }

                    binaryArea.WestLinkCount = area.WestLinks.Count;
                    binaryArea.WestLinkIndex = binaryLinks.Count;
                    foreach (var link in area.WestLinks)
                    {
                        var linkBinary = new WmapAreaLinkBinary();
                        linkBinary.DefaultEntryLocation = link.DefaultEntryLocation;
                        linkBinary.DestintationAreaIndex = link.DefaultEntryLocation;
                        linkBinary.EntryPoint = new array32(link.EntryPoint);
                        linkBinary.RandomEncounterArea1 = new array8(link.RandomEncounterArea1);
                        linkBinary.RandomEncounterArea2 = new array8(link.RandomEncounterArea2);
                        linkBinary.RandomEncounterArea3 = new array8(link.RandomEncounterArea3);
                        linkBinary.RandomEncounterArea4 = new array8(link.RandomEncounterArea4);
                        linkBinary.RandomEncounterArea5 = new array8(link.RandomEncounterArea5);
                        linkBinary.RandomEncounterProbability = link.RandomEncounterProbability;
                        linkBinary.TravelTime = link.TravelTime;
                        linkBinary.Unknown = link.Unknown0058;
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

            WmapHeaderBinary headerBinary = new WmapHeaderBinary();
            headerBinary.ftype = new array4() { character1 = 'W', character2 = 'M', character3 = 'A', character4 = 'P' };
            headerBinary.fversion = new array4() { character1 = 'V', character2 = '1', character3 = '.', character4 = '0' };
            headerBinary.WorldmapCount = binaryWorldmaps.Count;
            headerBinary.WorldmapOffset = WmapBinaryHeaderSize;

            using (MemoryStream s = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(s))
                {
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

                    if (BackupManger != null)
                    {
                        BackupManger.BackupFile(file, file.Filename, file.FileType, this);
                    }

                    using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    {
                        bw.BaseStream.Position = 0;
                        bw.BaseStream.CopyTo(fs);
                        fs.Flush(flushToDisk: true);
                    }
                }
            }
            return true;
        }
    }
}