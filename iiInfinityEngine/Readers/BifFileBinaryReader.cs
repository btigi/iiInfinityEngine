using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using System.IO.Compression;
using System.Runtime.ConstrainedExecution;

namespace iiInfinityEngine.Core.Readers
{
    public class BifFileBinaryReader : IBifFileReader
    {
        //Note: BIFF files do not support 'original file'
        List<BifFileEntryBinary> fileStructs = [];
        List<BifTilesetEntryBinary> tileStructs = [];
        List<SplFile> spells = [];
        List<StoFile> stores = [];
        List<DlgFile> dialogs = [];
        List<ItmFile> items = [];
        List<WfxFile> wfxs = [];
        List<EffFile> effs = [];
        List<CreFile> creatures = [];
        List<ProFile> projectiles = [];
        List<IdsFile> identifiers = [];
        List<DimensionalArrayFile> dimensionalArrays = [];
        List<AreFile> areas = [];
        List<MosFile> mosaics = [];
        List<WmpFile> worldmaps = [];
        List<VvcFile> vvcs = [];
        List<TisFile> tilesets = [];

        public TlkFile TlkFile { get; set; }

        public BifFile Read(Stream s, List<KeyBifResource2> resources, List<IEFileType> fileTypes)
        {
            var br = new BinaryReader(s);
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

                        using (MemoryStream compressedStream = new MemoryStream(bytes))
                        using (MemoryStream decompressedStream = new MemoryStream())
                        {
                            ZLibStream zlibStream = new ZLibStream(compressedStream, CompressionMode.Decompress);
                            zlibStream.CopyTo(decompressedStream);
                            var decompressedData = decompressedStream.ToArray();
                            m.Write(decompressedData, 0, decompressedLength);
                        }

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
                        using var ms = new MemoryStream(file);
                        KeyBifResource2 resource;
                        switch ((IEFileType)f.resourceType)
                        {
                            case IEFileType.Are:
                                try
                                {
                                    var are = new AreFileBinaryReader();
                                    are.TlkFile = TlkFile;
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
                                    var pro = new ProFileBinaryReader();
                                    pro.TlkFile = TlkFile;
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
                                    var ids = new IdsFileReader();
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
                                    var twoDeeAy = new DimensionalArrayFileReader();
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
                                    var cre = new CreFileBinaryReader();
                                    cre.TlkFile = TlkFile;
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

                            case IEFileType.Dlg:
                                try
                                {
                                    var dlg = new DlgFileBinaryReader();
                                    dlg.TlkFile = TlkFile;
                                    var dialog = (DlgFile)dlg.Read(ms);
                                    resource = resources.Where(a => a.NonTileSetIndex == (f.resourceLocator & 0xFFF)).SingleOrDefault();
                                    if (resource != null)
                                    {
                                        dialog.Filename = resource.ResourceName + "." + resource.ResourceType;
                                    }
                                    dialogs.Add(dialog);
                                }
                                catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                                break;

                            case IEFileType.Eff:
                                try
                                {
                                    var eff = new EffFileBinaryReader();
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
                                    var itm = new ItmFileBinaryReader();
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

                            case IEFileType.Mos:
                                try
                                {
                                    var mos = new MosFileBinaryReader();
                                    var mosFile = (MosFile)mos.Read(ms);
                                    resource = resources.Where(a => a.NonTileSetIndex == (f.resourceLocator & 0xFFF)).SingleOrDefault();
                                    if (resource != null)
                                    {
                                        mosFile.Filename = resource.ResourceName + "." + resource.ResourceType;
                                    }
                                    mosaics.Add(mosFile);
                                }
                                catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                                break;

                            case IEFileType.Spl:
                                try
                                {
                                    var spl = new SplFileBinaryReader();
                                    spl.TlkFile = TlkFile;
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
                                    var sto = new StoFileBinaryReader();
                                    sto.TlkFile = TlkFile;
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
                                    var vvc = new VvcFileBinaryReader();
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
                                    var wfx = new WfxFileBinaryReader();
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
                                    var wmp = new WmpFileBinaryReader();
                                    wmp.TlkFile = TlkFile;
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
                    ix++;
                }

                foreach (var ts in tileStructs)
                {
                    br.BaseStream.Seek(ts.resourceOffset, SeekOrigin.Begin);
                    var byteCount = ts.tileSize * ts.tileCount;
                    var file = br.ReadBytes(byteCount);
                    using var ms = new MemoryStream(file);
                    KeyBifResource2 resource;
                    switch ((IEFileType)ts.resourceType)
                    {
                        case IEFileType.Tis:
                            try
                            {
                                var tis = new TisFileBinaryReader();
                                tis.FromBiff = true;
                                var tisfile = (TisFile)tis.Read(ms, true, ts.tileCount, ts.tileSize, 64);
                                resource = resources.Where(a => a.TileSetIndex == ((ts.resourceLocator & 0x000FC000) >> 14)).SingleOrDefault();
                                if (resource != null)
                                {
                                    tisfile.Filename = resource.ResourceName + "." + resource.ResourceType;
                                }
                                tilesets.Add(tisfile);
                            }
                            catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
                            break;
                    }
                }

                var bifFile = new BifFile();
                bifFile.items = items;
                bifFile.spells = spells;
                bifFile.effects = effs;
                bifFile.wfx = wfxs;
                bifFile.stores = stores;
                bifFile.dialogs = dialogs;
                bifFile.creatures = creatures;
                bifFile.projectiles = projectiles;
                bifFile.identifiers = identifiers;
                bifFile.dimensionalArrays = dimensionalArrays;
                bifFile.areas = areas;
                bifFile.mosaics = mosaics;
                bifFile.worldmaps = worldmaps;
                bifFile.vvcs = vvcs;
                bifFile.tilesets = tilesets;
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