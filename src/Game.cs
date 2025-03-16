using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Readers;
using ii.InfinityEngine.Writers;
using ii.InfinityEngine.Writers.Interfaces;
using Ini;

namespace ii.InfinityEngine
{
    public class Game
    {
        public BackupManager backupManager;

        private KeyFile key;
        private string gameDirectory;
        private string hd = String.Empty;
        private string cd1 = String.Empty;
        private string cd2 = String.Empty;
        private string cd3 = String.Empty;
        private string cd4 = String.Empty;
        private string cd5 = String.Empty;
        private string cd6 = String.Empty;

        public List<AreFile> Areas = [];
        public List<CreFile> Creatures = [];
        public List<DimensionalArrayFile> DimensionalArrays = [];
        public List<DlgFile> Dialogs = [];
        public List<GamFile> Games = [];
        public List<EffFile> Effects = [];
        public List<GuiFile> Guis = [];
        public List<IdsFile> Identifiers = [];
        public List<ItmFile> Items = [];
        public List<MosFile> Mosaics = [];
        public List<PltFile> Paperdolls = [];
        public List<MusFile> Playlists = [];
        public List<ProFile> Projectiles = [];
        public List<GlslFile> Shaders = [];
        public List<SplFile> Spells = [];
        public List<SqlFile> Sqls = [];
        public List<StoFile> Stores = [];
        public List<VvcFile> VisualEffects = [];
        public List<WfxFile> Wfxs = [];
        public List<TisFile> Tilesets = [];
        public List<MenuFile> Menus = [];
        public List<LuaFile> Luas = [];
        public List<VefFile> Vefs = [];
        public List<WmpFile> Worldmaps = [];

        public TlkFile Tlk { get; private set; }

        public Game(string keyPath, string tlkPath)
        {
            var key = Path.Combine(Path.IsPathRooted(keyPath) ? keyPath : Path.Combine(Directory.GetCurrentDirectory(), keyPath), "chitin.key");
            var tlk = Path.Combine(Path.IsPathRooted(tlkPath) ? tlkPath : Path.Combine(Directory.GetCurrentDirectory(), tlkPath), "dialog.tlk");
            LoadEssentialResources(key, tlk);
        }

        private void LoadEssentialResources(string chitinLocation, string dialogLocation)
        {
            Tlk = new TlkFileBinaryReader().Read(dialogLocation);
            Tlk.Filename = "dialog.tlk";

            gameDirectory = Path.GetDirectoryName(chitinLocation);
            ReadIniFile(gameDirectory);

            KeyFileBinaryReader keyReader = new KeyFileBinaryReader();
            key = keyReader.Read(chitinLocation);
        }

        public void LoadResources(List<IEFileType> resourceTypes, Regex regex = null)
        {
            #region Parallel
            /*
            var bifList = new List<(KeyBifEntry2 entry, int index)>();
            var relevantResources = key.Resources.Where(a => a.ResourceType == resourceType).ToList();
            foreach (var c in relevantResources)
            {
                bifList.Add(key.BifFiles[c.BifIndex]);
            }
            bifList = bifList.Distinct().ToList();

            var parallelOptions = new System.Threading.Tasks.ParallelOptions();
            //parallelOptions.MaxDegreeOfParallelism = 150;
            System.Threading.Tasks.Parallel.ForEach(bifList, parallelOptions, bif =>
            {
                var cdDir = GetDirectoryLocation(bif.entry);
                var bifName = Path.Combine(gameDirectory, cdDir, bif.entry.Filename);
                if (File.Exists(bifName))
                {
                    var bbr = new BifFileBinaryReader();
                    var bifFileStream = new FileStream(bifName, FileMode.Open, FileAccess.Read);
                    //bbr.TlkFile = Tlk;
                    var bifFile = bbr.Read(bifFileStream, relevantResources.Where(a => a.BifIndex == bif.Item2).ToList(), [resourceType]);

                    Areas.AddRange(bifFile.areas);
                    Creatures.AddRange(bifFile.creatures);
                    DimensionalArrays.AddRange(bifFile.dimensionalArrays);
                    Effects.AddRange(bifFile.effects);
                    Identifiers.AddRange(bifFile.identifiers);
                    Items.AddRange(bifFile.items);
                    Projectiles.AddRange(bifFile.projectiles);
                    Spells.AddRange(bifFile.spells);
                    Stores.AddRange(bifFile.stores);
                    VisualEffects.AddRange(bifFile.vvcs);
                    Wfxs.AddRange(bifFile.wfx);
                }
            });

            var fileTypes = new List<IEFileType>() { resourceType };
            LoadOverride(gameDirectory, fileTypes);
            */
            #endregion

            var bifList = new List<(KeyBifEntry2 entry, int index)>();
            var relevantResources = key.Resources.Where(a => resourceTypes.Contains(a.ResourceType) && (regex?.IsMatch(a.Filename) ?? true)).ToList();
            foreach (var c in relevantResources)
            {
                bifList.Add(key.BifFiles[c.BifIndex]);
            }
            bifList = bifList.Distinct().ToList();

            LoadResourcesFromBifs(gameDirectory, bifList, relevantResources, resourceTypes);
            LoadDirectory(Path.Combine(gameDirectory, "override"), resourceTypes, regex);
        }

        public void LoadAllResources()
        {
            var fileTypes = new List<IEFileType>() { IEFileType.Sto,
                                                     IEFileType.Spl,
                                                     IEFileType.Itm,
                                                     IEFileType.Eff,
                                                     IEFileType.Gam,
                                                     IEFileType.Glsl,
                                                     IEFileType.Gui,
                                                     IEFileType.Cre,
                                                     IEFileType.Mus,
                                                     IEFileType.Plt,
                                                     IEFileType.Pro,
                                                     IEFileType.Ids,
                                                     IEFileType.DimensionalArray,
                                                     IEFileType.Are,
                                                     IEFileType.Wmp,
                                                     IEFileType.Sql,
                                                     IEFileType.Vvc,
                                                     IEFileType.Wfx,
                                                     IEFileType.Tis,
                                                     IEFileType.Menu,
                                                     IEFileType.Lua,
                                                     IEFileType.Vef};

            LoadResourcesFromBifs(gameDirectory, key.BifFiles, key.Resources, fileTypes);
            LoadDirectory(Path.Combine(gameDirectory, "override"), fileTypes);
        }

        private void LoadResourcesFromBifs(string directory, List<(KeyBifEntry2 entry, int index)> files, List<KeyBifResource2> resources, List<IEFileType> fileTypes)
        {
            int bifIndex = 0;
            foreach (var bif in files)
            {
                var cdDir = GetDirectoryLocation(bif.entry);
                var bifName = Path.Combine(directory, cdDir, bif.entry.Filename);
                if (File.Exists(bifName))
                {
                    var bbr = new BifFileBinaryReader();
                    using var bifFileStream = new FileStream(bifName, FileMode.Open, FileAccess.Read);
                    bbr.TlkFile = Tlk;
                    var bifFile = bbr.Read(bifFileStream, resources.Where(a => a.BifIndex == bif.index).ToList(), fileTypes);
                    Areas.AddRange(bifFile.areas);
                    Creatures.AddRange(bifFile.creatures);
                    Dialogs.AddRange(bifFile.dialogs);
                    DimensionalArrays.AddRange(bifFile.dimensionalArrays);
                    Effects.AddRange(bifFile.effects);
                    Games.AddRange(bifFile.games);
                    Guis.AddRange(bifFile.guis);
                    Identifiers.AddRange(bifFile.identifiers);
                    Items.AddRange(bifFile.items);
                    Luas.AddRange(bifFile.luas);
                    Menus.AddRange(bifFile.menus);
                    Mosaics.AddRange(bifFile.mosaics);
                    Paperdolls.AddRange(bifFile.paperdolls);
                    Playlists.AddRange(bifFile.playlists);
                    Projectiles.AddRange(bifFile.projectiles);
                    Shaders.AddRange(bifFile.shaders);
                    Spells.AddRange(bifFile.spells);
                    Sqls.AddRange(bifFile.sqls);
                    Stores.AddRange(bifFile.stores);
                    VisualEffects.AddRange(bifFile.vvcs);
                    Vefs.AddRange(bifFile.vefs);
                    Wfxs.AddRange(bifFile.wfx);
                    Worldmaps.AddRange(bifFile.worldmaps);
                    Tilesets.AddRange(bifFile.tilesets);
                }
                bifIndex++;
            }
        }

        public void LoadDirectory(string directory, List<IEFileType> resourceTypes, Regex regex = null)
        {
            var dimensionalArrayReader = new DimensionalArrayFileReader();
            var areReader = new AreFileBinaryReader();
            var creReader = new CreFileBinaryReader();
            var dlgReader = new DlgFileBinaryReader();
            var effReader = new EffFileBinaryReader();
            var gamReader = new GamFileBinaryReader();
            var glslReader = new GlslFileReader();
            var guiReader = new GuiFileReader();
            var idsReader = new IdsFileReader();
            var itmReader = new ItmFileBinaryReader();
            var luaReader = new LuaFileReader();
            var menuReader = new MenuFileReader();
            var mosReader = new MosFileBinaryReader();
            var musReader = new MusFileReader();
            var pltReader = new PltFileBinaryReader();
            var proReader = new ProFileBinaryReader();
            var splReader = new SplFileBinaryReader();
            var sqlReader = new SqlFileReader();
            var stoReader = new StoFileBinaryReader();
            var vvcReader = new VvcFileBinaryReader();
            var vefReader = new VefFileBinaryReader();
            var wfxReader = new WfxFileBinaryReader();
            var tisReader = new TisFileBinaryReader();
            var fileExtensions = resourceTypes.Select(s => $".{s.ToString().Replace("DimensionalArray", "2da").ToLower()}");
            foreach (var file in Directory.GetFiles(directory))
            {
                var extension = Path.GetExtension(file.ToLower());
                if (!fileExtensions.Contains(extension) || (regex != null && !regex.IsMatch(file)))
                    continue;

                switch (extension)
                {
                    case ".2da":
                        var dimentionalArray = dimensionalArrayReader.Read(file);
                        dimentionalArray.Filename = file;
                        DimensionalArrays.Add(dimentionalArray);
                        break;
                    case ".are":
                        var area = areReader.Read(file);
                        area.Filename = file;
                        Areas.Add(area);
                        break;
                    case ".cre":
                        var creature = creReader.Read(file);
                        creature.Filename = file;
                        Creatures.Add(creature);
                        break;
                    case ".dlg":
                        var dialog = dlgReader.Read(file);
                        dialog.Filename = file;
                        Dialogs.Add(dialog);
                        break;
                    case ".eff":
                        var effect = effReader.Read(file);
                        effect.Filename = file;
                        Effects.Add(effect);
                        break;
                    case ".gam":
                        var game = gamReader.Read(file);
                        game.Filename = file;
                        Games.Add(game);
                        break;
                    case ".glsl":
                        var glsl = glslReader.Read(file);
                        glsl.Filename = file;
                        Shaders.Add(glsl);
                        break;
                    case ".gui":
                        var gui = guiReader.Read(file);
                        gui.Filename = file;
                        Guis.Add(gui);
                        break;
                    case ".ids":
                        var identifier = idsReader.Read(file);
                        identifier.Filename = file;
                        Identifiers.Add(identifier);
                        break;
                    case ".itm":
                        var item = itmReader.Read(file);
                        item.Filename = file;
                        Items.Add(item);
                        break;
                    case ".lua":
                        var lua = luaReader.Read(file);
                        lua.Filename = file;
                        Luas.Add(lua);
                        break;
                    case ".menu":
                        var menu = menuReader.Read(file);
                        menu.Filename = file;
                        Menus.Add(menu);
                        break;
                    case ".mos":
                        var mos = mosReader.Read(file);
                        mos.Filename = file;
                        Mosaics.Add(mos);
                        break;
                    case ".mus":
                        var mus = musReader.Read(file);
                        mus.Filename = file;
                        Playlists.Add(mus);
                        break;
                    case ".plt":
                        var plt = pltReader.Read(file, null);
                        plt.Filename = file;
                        Paperdolls.Add(plt);
                        break;
                    case ".pro":
                        var projectile = proReader.Read(file);
                        projectile.Filename = file;
                        Projectiles.Add(projectile);
                        break;
                    case ".spl":
                        var spell = splReader.Read(file);
                        spell.Filename = file;
                        Spells.Add(spell);
                        break;
                    case ".sql":
                        var sql = sqlReader.Read(file);
                        sql.Filename = file;
                        Sqls.Add(sql);
                        break;
                    case ".sto":
                        var store = stoReader.Read(file);
                        store.Filename = file;
                        Stores.Add(store);
                        break;
                    case ".vvc":
                        var visualEffect = vvcReader.Read(file);
                        visualEffect.Filename = file;
                        VisualEffects.Add(visualEffect);
                        break;
                    case ".vef":
                        var vef = vefReader.Read(file);
                        vef.Filename = file;
                        Vefs.Add(vef);
                        break;
                    case ".wfx":
                        var wavEffect = wfxReader.Read(file);
                        wavEffect.Filename = file;
                        Wfxs.Add(wavEffect);
                        break;
                    case ".tis":
                        var tileset = tisReader.Read(file);
                        tileset.Filename = file;
                        Tilesets.Add(tileset);
                        break;
                }
            }
        }

        private void ReadIniFile(string directory)
        {
            var ini = new IniFile(Path.Combine(directory, "baldur.ini"));
            hd = ini.IniReadValue("Alias", "HD0:");
            cd1 = ini.IniReadValue("Alias", "CD1:");
            cd2 = ini.IniReadValue("Alias", "CD2:");
            cd3 = ini.IniReadValue("Alias", "CD3:");
            cd4 = ini.IniReadValue("Alias", "CD4:");
            cd5 = ini.IniReadValue("Alias", "CD5:");
            cd6 = ini.IniReadValue("Alias", "CD6:");
        }

        private string GetDirectoryLocation(KeyBifEntry2 bifFile)
        {
            var cdDir = String.Empty;
            if (bifFile.IsInCacheDirectory)
                return hd;
            if (bifFile.IsOnCD1)
                return cd1;
            if (bifFile.IsOnCD2)
                return cd2;
            if (bifFile.IsOnCD3)
                return cd3;
            if (bifFile.IsOnCD4)
                return cd4;
            if (bifFile.IsOnCD5)
                return cd5;
            if (bifFile.IsOnCD6)
                cdDir = cd6;
            return cdDir;
        }

        public bool Save<S, T>(IEFile file, IIEFileWriter writer)
            where S : IEFile
            where T : IIEFileWriter, new()
        {
            var fileSaved = writer.Write(file.Filename, file);
            return fileSaved;
        }

        /// <summary>
        /// Save an IE file to the filename specified within the file, using the default binary writer. If an file
        /// already exists it is backup up using the backup manager scoped to the Game class. The file will only be
        /// saved if it has been changed since loading.
        /// </summary>
        /// <typeparam name="T">The type file to be saved</typeparam>
        /// <param name="file">The file to be saved</param>
        /// <returns>True if the file was saved, false otherwise.</returns>
        public bool Save<T>(IEFile file) where T : IEFile
        {
            IIEFileWriter writer = null;
            switch (file.FileType)
            {
                case IEFileType.Are:
                    writer = new AreFileBinaryWriter();
                    (writer as AreFileBinaryWriter).TlkFile = Tlk;
                    break;
                case IEFileType.Cre:
                    writer = new CreFileBinaryWriter();
                    (writer as CreFileBinaryWriter).TlkFile = Tlk;
                    break;
                case IEFileType.Dlg:
                    writer = new DlgFileBinaryWriter();
                    (writer as DlgFileBinaryWriter).TlkFile = Tlk;
                    break;
                case IEFileType.DimensionalArray:
                    writer = new DimensionalArrayFileWriter();
                    break;
                case IEFileType.Eff:
                    writer = new EffFileBinaryWriter();
                    break;
                case IEFileType.Gam:
                    writer = new GamFileBinaryWriter();
                    (writer as GamFileBinaryWriter).TlkFile = Tlk;
                    break;
                case IEFileType.Glsl:
                    writer = new GlslFileWriter();
                    break;
                case IEFileType.Ids:
                    writer = new IdsFileWriter();
                    break;
                case IEFileType.Itm:
                    writer = new ItmFileBinaryWriter();
                    (writer as ItmFileBinaryWriter).TlkFile = Tlk;
                    break;
                case IEFileType.Pro:
                    writer = new ProFileBinaryWriter();
                    break;
                case IEFileType.Spl:
                    writer = new SplFileBinaryWriter();
                    (writer as SplFileBinaryWriter).TlkFile = Tlk;
                    break;
                case IEFileType.Sql:
                    writer = new SqlFileWriter();
                    break;
                case IEFileType.Sto:
                    writer = new StoFileBinaryWriter();
                    break;
                case IEFileType.Tlk:
                    writer = new TlkFileBinaryWriter();
                    break;
                case IEFileType.Vef:
                    writer = new VefFileBinaryWriter();
                    break;
                case IEFileType.Vvc:
                    writer = new VvcFileBinaryWriter();
                    break;
                case IEFileType.Wfx:
                    writer = new WfxFileBinaryWriter();
                    break;
                case IEFileType.Wmp:
                    writer = new WmapFileBinaryWriter();
                    break;
            }

            writer.BackupManger = this.backupManager;
            var fileSaved = writer.Write(file.Filename, file);
            return fileSaved;
        }

        public (bool success, byte[] bytes) ExtractFile(string filename)
        {
            var relevantResources = key.Resources.Where(w => w.Filename == filename.ToUpper()).SingleOrDefault();
            var bif = key.BifFiles[relevantResources.BifIndex];
            var cdDir = GetDirectoryLocation(bif.entry);
            var bifName = Path.Combine(gameDirectory, cdDir, bif.entry.Filename);
            if (File.Exists(bifName))
            {
                var bbr = new BifFileBinaryReader();
                using var bifFileStream = new FileStream(bifName, FileMode.Open, FileAccess.Read);
                bbr.TlkFile = Tlk;
                return bbr.ReadRaw(bifFileStream, new List<KeyBifResource2>() { relevantResources });
            }

            return (false, null);
        }
    }
}