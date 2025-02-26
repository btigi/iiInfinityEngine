using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    public class BifFile : IEFile
    {
        public List<BifFileEntry2> Resources = [];
        public List<BifTilesetEntry2> Tilesets = [];

        public List<WfxFile> wfx = [];
        public List<EffFile> effects = [];
        public List<GuiFile> guis = [];
        public List<ItmFile> items = [];
        public List<SplFile> spells = [];
        public List<StoFile> stores = [];
        public List<DlgFile> dialogs = [];
        public List<CreFile> creatures = [];
        public List<PltFile> paperdolls = [];
        public List<MusFile> playlists = [];
        public List<ProFile> projectiles = [];
        public List<IdsFile> identifiers = [];
        public List<DimensionalArrayFile> dimensionalArrays = [];
        public List<AreFile> areas = [];
        public List<MosFile> mosaics = [];
        public List<WmpFile> worldmaps = [];
        public List<VvcFile> vvcs = [];
        public List<GlslFile> shaders = [];
        public List<SqlFile> sqls = [];
        public List<TisFile> tilesets = [];

        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private readonly IEFileType fileType = IEFileType.Bif;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public Int32 FileCount { get; set; }
        public Int32 TilesetCount { get; set; }
        public Int32 FileOffset { get; set; }
    }

    public struct BifFileEntry2
    {
        public Int32 ResourceLocator { get; set; }
        public Int32 ResourceOffset { get; set; }
        public Int32 ResourceSize { get; set; }
        public Int16 ResourceType { get; set; }
        public Int16 Unknowne { get; set; }
    }

    public struct BifTilesetEntry2
    {
        public Int32 ResourceLocator { get; set; }
        public Int32 ResourceOffset { get; set; }
        public Int32 TileCount { get; set; }
        public Int32 TileSize { get; set; }
        public Int16 ResourceType { get; set; }
        public Int16 Unknowne { get; set; }
    }
}