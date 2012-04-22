using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    public class BifFile : IEFile
    {
        public List<BifFileEntry2> Resources = new List<BifFileEntry2>();
        public List<BifTilesetEntry2> Tilesets = new List<BifTilesetEntry2>();

        public List<WfxFile> wfx = new List<WfxFile>();
        public List<EffFile> effects = new List<EffFile>();
        public List<ItmFile> items = new List<ItmFile>();
        public List<SplFile> spells = new List<SplFile>();
        public List<StoFile> stores = new List<StoFile>();
        public List<CreFile> creatures = new List<CreFile>();
        public List<ProFile> projectiles = new List<ProFile>();
        public List<IdsFile> identifiers = new List<IdsFile>();
        public List<DimensionalArrayFile> dimensionalArrays = new List<DimensionalArrayFile>();
        public List<AreFile> areas = new List<AreFile>();
        public List<WmpFile> worldmaps = new List<WmpFile>();
        public List<VvcFile> vvcs = new List<VvcFile>();
        
        public Int32 FileCount;
        public Int32 TilesetCount;
        public Int32 FileOffset;
        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private IEFileType fileType = IEFileType.Bif;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }
    }
    
    public struct BifFileEntry2
    {
        public Int32 resourceLocator;
        public Int32 resourceOffset;
        public Int32 resourceSize;
        public Int16 resourceType;
        public Int16 Unknown;
    }

    public struct BifTilesetEntry2
    {
        public Int32 resourceLocator;
        public Int32 resourceOffset;
        public Int32 tileCount;
        public Int32 tileSize;
        public Int16 resourceType;
        public Int16 Unknown;
    }
}