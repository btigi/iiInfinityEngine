using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using System.IO.Compression;

namespace iiInfinityEngine.Core.Readers
{
    public class BifFileBinaryReader : IBifFileReader
    {
        //Note: BIFF files do not support 'original file'
        List<BifFileEntryBinary> fileStructs = new List<BifFileEntryBinary>();
        List<BifTilesetEntryBinary> tileStructs = new List<BifTilesetEntryBinary>();
        List<SplFile> spells = new List<SplFile>();
        List<StoFile> stores = new List<StoFile>();
        List<ItmFile> items = new List<ItmFile>();
        List<WfxFile> wfxs = new List<WfxFile>();
        List<EffFile> effs = new List<EffFile>();
        List<CreFile> creatures = new List<CreFile>();
        List<ProFile> projectiles = new List<ProFile>();
        List<IdsFile> identifiers = new List<IdsFile>();
        List<DimensionalArrayFile> dimensionalArrays = new List<DimensionalArrayFile>();
        List<AreFile> areas = new List<AreFile>();
        List<WmpFile> worldmaps = new List<WmpFile>();
        List<VvcFile> vvcs = new List<VvcFile>();

        public TlkFile TlkFile { get; set; }

        public BifFile Read(Stream s, List<KeyBifResource2> resources, List<IEFileType> fileTypes)
        {
            BinaryReader br = new BinaryReader(s);
            try
            {
                var header = (BifHeaderBinary)Common.ReadStruct(br, typeof(BifHeaderBinary));

                if ((header.ftype.ToString() == "BIFC") && (header.fversion.ToString() == "V1.0"))
                {
                    br.BaseStream.Seek(0, SeekOrigin.Begin);
                    var headerC = (BifCHeaderV1Binary)Common.ReadStruct(br, typeof(BifCHeaderV1Binary));

                    var m = new MemoryStream();
                    int size = 0;
                    var theseBytes = new byte[headerC.FileLength];
                    while (size < headerC.FileLength)
                    {
                        var decompressedLength = br.ReadInt32();
                        var compressedLength = br.ReadInt32();
                        var bytes = br.ReadBytes(compressedLength);

                        m.Write(Ionic.Zlib.ZlibStream.UncompressBuffer(bytes), 0, decompressedLength);
                        size = size + decompressedLength;
                    }
                    /*
                    using (FileStream fs = new FileStream(@"D:\out.bif", FileMode.Create))
                    {
                        m.CopyTo(fs);
                        fs.Flush();
                    }
                    */

                    if (br != null)
                    {
                        ((IDisposable)br).Dispose();
                    }
                    s = new MemoryStream();
                    m.Position = 0;
                    m.CopyTo(s);
                    s.Position = 0;

                    br = new BinaryReader(s);
                    header = (BifHeaderBinary)Common.ReadStruct(br, typeof(BifHeaderBinary));
                }

                Debug.WriteLine("Bif is uncompressed - reading");

                br.BaseStream.Seek(header.FileOffset, SeekOrigin.Begin);
                for (int i = 0; i < header.FileCount; i++)
                {
                    var resource = (BifFileEntryBinary)Common.ReadStruct(br, typeof(BifFileEntryBinary));
                    fileStructs.Add(resource);
                }

                for (int i = 0; i < header.TilesetCount; i++)
                {
                    var resource = (BifTilesetEntryBinary)Common.ReadStruct(br, typeof(BifTilesetEntryBinary));
                    tileStructs.Add(resource);
                }

                int ix = 0;
                foreach (var f in fileStructs)
                {
                    if (fileTypes.Contains((IEFileType)f.resourceType))
                    {
                        br.BaseStream.Seek(f.resourceOffset, SeekOrigin.Begin);
                        var file = br.ReadBytes(f.resourceSize);
                        using (MemoryStream ms = new MemoryStream(file))
                        {
                            KeyBifResource2 resource;
                            switch ((IEFileType)f.resourceType)
                            {
                                case IEFileType.Are:
                                    try
                                    {
                                        AreFileBinaryReader are = new AreFileBinaryReader();
                                        var area = (AreFile)are.Read(ms);
                                        resource = resources.Where(a => a.NonTileSetIndex == (f.resourceLocator & 0xFFF)).SingleOrDefault();
                                        if (resource != null)
                                        {
                                            area.Filename = resource.ResourceName + "." + resource.ResourceType;
                                        }
                                        areas.Add(area);
                                    }
                                    catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                                    break;

                                case IEFileType.Pro:
                                    try
                                    {
                                        ProFileBinaryReader pro = new ProFileBinaryReader();
                                        var projectile = (ProFile)pro.Read(ms);
                                        resource = resources.Where(a => a.NonTileSetIndex == (f.resourceLocator & 0xFFF)).SingleOrDefault();
                                        if (resource != null)
                                        {
                                            projectile.Filename = resource.ResourceName + "." + resource.ResourceType;
                                        }
                                        projectiles.Add(projectile);
                                    }
                                    catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                                    break;

                                case IEFileType.Ids:
                                    try
                                    {
                                        IdsFileReader ids = new IdsFileReader();
                                        var identifier = (IdsFile)ids.Read(ms);
                                        resource = resources.Where(a => a.NonTileSetIndex == (f.resourceLocator & 0xFFF)).SingleOrDefault();
                                        if (resource != null)
                                        {
                                            identifier.Filename = resource.ResourceName + "." + resource.ResourceType;
                                        }
                                        identifiers.Add(identifier);
                                    }
                                    catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                                    break;

                                case IEFileType.DimensionalArray:
                                    try
                                    {
                                        DimensionalArrayFileReader twoDeeAy = new DimensionalArrayFileReader();
                                        var dimensionalArray = (DimensionalArrayFile)twoDeeAy.Read(ms);
                                        resource = resources.Where(a => a.NonTileSetIndex == (f.resourceLocator & 0xFFF)).SingleOrDefault();
                                        if (resource != null)
                                        {
                                            // We special case this filename, as we can't set the FileType identifier to start with a number
                                            dimensionalArray.Filename = resource.ResourceName + ".2da";
                                        }
                                        dimensionalArrays.Add(dimensionalArray);
                                    }
                                    catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                                    break;

                                case IEFileType.Cre:
                                    try
                                    {
                                        CreFileBinaryReader cre = new CreFileBinaryReader();
                                        var creature = (CreFile)cre.Read(ms);
                                        resource = resources.Where(a => a.NonTileSetIndex == (f.resourceLocator & 0xFFF)).SingleOrDefault();
                                        if (resource != null)
                                        {
                                            creature.Filename = resource.ResourceName + "." + resource.ResourceType;
                                        }
                                        creatures.Add(creature);
                                    }
                                    catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                                    break;

                                case IEFileType.Eff:
                                    try
                                    {
                                        EffFileBinaryReader eff = new EffFileBinaryReader();
                                        var effect = (EffFile)eff.Read(ms);
                                        resource = resources.Where(a => a.NonTileSetIndex == (f.resourceLocator & 0xFFF)).SingleOrDefault();
                                        if (resource != null)
                                        {
                                            effect.Filename = resource.ResourceName + "." + resource.ResourceType;
                                        }
                                        effs.Add(effect);
                                    }
                                    catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                                    break;

                                case IEFileType.Itm:
                                    try
                                    {
                                        ItmFileBinaryReader itm = new ItmFileBinaryReader();
                                        itm.TlkFile = TlkFile;
                                        var item = (ItmFile)itm.Read(ms);
                                        resource = resources.Where(a => a.NonTileSetIndex == (f.resourceLocator & 0xFFF)).SingleOrDefault();
                                        if (resource != null)
                                        {
                                            item.Filename = resource.ResourceName + "." + resource.ResourceType;
                                        }
                                        items.Add(item);
                                    }
                                    catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                                    break;

                                case IEFileType.Spl:
                                    try
                                    {
                                        SplFileBinaryReader spl = new SplFileBinaryReader();
                                        var spell = (SplFile)spl.Read(ms);
                                        resource = resources.Where(a => a.NonTileSetIndex == (f.resourceLocator & 0xFFF)).SingleOrDefault();
                                        if (resource != null)
                                        {
                                            spell.Filename = resource.ResourceName + "." + resource.ResourceType;
                                        }
                                        spells.Add(spell);
                                    }
                                    catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                                    break;

                                case IEFileType.Sto:
                                    try
                                    {
                                        StoFileBinaryReader sto = new StoFileBinaryReader();
                                        var store = (StoFile)sto.Read(ms);
                                        resource = resources.Where(a => a.NonTileSetIndex == (f.resourceLocator & 0xFFF)).SingleOrDefault();
                                        if (resource != null)
                                        {
                                            store.Filename = resource.ResourceName + "." + resource.ResourceType;
                                        }
                                        stores.Add(store);
                                    }
                                    catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                                    break;

                                case IEFileType.Vvc:
                                    try
                                    {
                                        VvcFileBinaryReader vvc = new VvcFileBinaryReader();
                                        var vvcFile = (VvcFile)vvc.Read(ms);
                                        resource = resources.Where(a => a.NonTileSetIndex == (f.resourceLocator & 0xFFF)).SingleOrDefault();
                                        if (resource != null)
                                        {
                                            vvcFile.Filename = resource.ResourceName + "." + resource.ResourceType;
                                        }
                                        vvcs.Add(vvcFile);
                                    }
                                    catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                                    break;

                                case IEFileType.Wfx:
                                    try
                                    {
                                        WfxFileBinaryReader wfx = new WfxFileBinaryReader();
                                        var wfxfile = (WfxFile)wfx.Read(ms);
                                        resource = resources.Where(a => a.NonTileSetIndex == (f.resourceLocator & 0xFFF)).SingleOrDefault();
                                        if (resource != null)
                                        {
                                            wfxfile.Filename = resource.ResourceName + "." + resource.ResourceType;
                                        }
                                        wfxs.Add(wfxfile);
                                    }
                                    catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                                    break;

                                case IEFileType.Wmp:
                                    try
                                    {
                                        WmpFileBinaryReader wmp = new WmpFileBinaryReader();
                                        var worldmap = (WmpFile)wmp.Read(ms);
                                        resource = resources.Where(a => a.NonTileSetIndex == (f.resourceLocator & 0xFFF)).SingleOrDefault();
                                        if (resource != null)
                                        {
                                            worldmap.Filename = resource.ResourceName + "." + resource.ResourceType;
                                        }
                                        worldmaps.Add(worldmap);
                                    }
                                    catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                                    break;
                            }
                        }
                    }
                    ix++;
                }

                BifFile bifFile = new BifFile();
                bifFile.items = items;
                bifFile.spells = spells;
                bifFile.effects = effs;
                bifFile.wfx = wfxs;
                bifFile.stores = stores;
                bifFile.creatures = creatures;
                bifFile.projectiles = projectiles;
                bifFile.identifiers = identifiers;
                bifFile.dimensionalArrays = dimensionalArrays;
                bifFile.areas = areas;
                bifFile.worldmaps = worldmaps;
                bifFile.vvcs = vvcs;
                return bifFile;
            }
            finally
            {
                if (br != null)
                {
                    ((IDisposable)br).Dispose();
                }
            }
        }
    }
}