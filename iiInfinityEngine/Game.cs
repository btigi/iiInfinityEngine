using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Readers;
using iiInfinityEngine.Core.Writers;
using iiInfinityEngine.Core.Writers.Interfaces;
using Ini;

namespace iiInfinityEngine.Core
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
        public List<EffFile> Effects = [];
        public List<IdsFile> Identifiers = [];
        public List<ItmFile> Items = [];
        public List<MosFile> Mosaics = [];
        public List<ProFile> Projectiles = [];
        public List<SplFile> Spells = [];
        public List<StoFile> Stores = [];
        public List<VvcFile> VisualEffects = [];
        public List<WfxFile> Wfxs = [];
        public List<TisFile> Tilesets = [];

        public TlkFile Tlk { get; private set; }

        public Game()
        {
            LoadEssentialResources("chitin.key", "dialog.tlk");
        }

        public Game(string gameDirectory)
        {
            var key = Path.Combine(gameDirectory, "chitin.key");
            var tlk = Path.Combine(gameDirectory, "dialog.tlk");
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

        public void LoadResources(IEFileType resourceType)
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

            LoadOverride(gameDirectory);
            */
            #endregion

            var bifList = new List<(KeyBifEntry2 entry, int index)>();
            var relevantResources = key.Resources.Where(a => a.ResourceType == resourceType).ToList();
            foreach (var c in relevantResources)
            {
                bifList.Add(key.BifFiles[c.BifIndex]);
            }
            bifList = bifList.Distinct().ToList();

            var fileTypes = new List<IEFileType>() { resourceType };
            LoadResourcesFromBifs(gameDirectory, bifList, relevantResources, fileTypes);
            LoadOverride(gameDirectory, fileTypes);
        }

        public void LoadAllResources()
        {
            var fileTypes = new List<IEFileType>() { IEFileType.Sto,
                                                     IEFileType.Spl,
                                                     IEFileType.Itm,
                                                     IEFileType.Eff,
                                                     IEFileType.Cre,
                                                     IEFileType.Pro,
                                                     IEFileType.Ids,
                                                     IEFileType.DimensionalArray,
                                                     IEFileType.Are,
                                                     IEFileType.Wmp,
                                                     IEFileType.Vvc,
                                                     IEFileType.Wfx,
                                                     IEFileType.Tis };

            LoadResourcesFromBifs(gameDirectory, key.BifFiles, key.Resources, fileTypes);
            LoadOverride(gameDirectory, fileTypes);
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
                    Identifiers.AddRange(bifFile.identifiers);
                    Items.AddRange(bifFile.items);
                    Mosaics.AddRange(bifFile.mosaics);
                    Projectiles.AddRange(bifFile.projectiles);
                    Spells.AddRange(bifFile.spells);
                    Stores.AddRange(bifFile.stores);
                    VisualEffects.AddRange(bifFile.vvcs);
                    Wfxs.AddRange(bifFile.wfx);
                    Tilesets.AddRange(bifFile.tilesets);
                }
                bifIndex++;
            }
        }

        private void LoadOverride(string directory, List<IEFileType> resourceTypes)
        {
            var dimensionalArrayReader = new DimensionalArrayFileReader();
            var areReader = new AreFileBinaryReader();
            var creReader = new CreFileBinaryReader();
            var dlgReader = new DlgFileBinaryReader();
            var effReader = new EffFileBinaryReader();
            var idsReader = new IdsFileReader();
            var itmReader = new ItmFileBinaryReader();
            var mosReader = new MosFileBinaryReader();
            var proReader = new ProFileBinaryReader();
            var splReader = new SplFileBinaryReader();
            var stoReader = new StoFileBinaryReader();
            var vvcReader = new VvcFileBinaryReader();
            var wfxReader = new WfxFileBinaryReader();
            var tisReader = new TisFileBinaryReader();
            var fileExtensions = resourceTypes.Select(s => $".{s.ToString().Replace("DimensionalArray", "2da").ToLower()}");
            foreach (var file in Directory.GetFiles(Path.Combine(directory, "override")))
            {
                var extension = Path.GetExtension(file.ToLower());
                if (!fileExtensions.Contains(extension))
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
                    case ".mos":
                        var mos = mosReader.Read(file);
                        mos.Filename = file;
                        Mosaics.Add(mos);
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
                    break;
                case IEFileType.Cre:
                    writer = new CreFileBinaryWriter();
                    break;
                case IEFileType.Dlg:
                    writer = new DlgFileBinaryWriter();
                    break;
                case IEFileType.DimensionalArray:
                    writer = new DimensionalArrayFileWriter();
                    break;
                case IEFileType.Eff:
                    writer = new EffFileBinaryWriter();
                    break;
                case IEFileType.Ids:
                    writer = new IdsFileWriter();
                    break;
                case IEFileType.Itm:
                    writer = new ItmFileBinaryWriter();
                    break;
                case IEFileType.Pro:
                    writer = new ProFileBinaryWriter();
                    break;
                case IEFileType.Spl:
                    writer = new SplFileBinaryWriter();
                    break;
                case IEFileType.Sto:
                    writer = new StoFileBinaryWriter();
                    break;
                case IEFileType.Tlk:
                    writer = new TlkFileBinaryWriter();
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
    }
}