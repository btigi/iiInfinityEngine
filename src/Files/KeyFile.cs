using System;
using System.Collections.Generic;

namespace ii.InfinityEngine.Files
{
    public class KeyFile : IEFile
    {
        public List<(KeyBifEntry2 entry, int index)> BifFiles = [];
        public List<KeyBifResource2> Resources = [];
        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private readonly IEFileType fileType = IEFileType.Key;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }
    }

    public class KeyBifResource2
    {
        public string ResourceName { get; set; }
        public IEFileType ResourceType { get; set; }
        public string Filename { get { return $"{ResourceName}.{ResourceType.ToString().ToUpper()}"; } }

        //bits 31-20
        private Int32 bifIndex;
        public Int32 BifIndex
        {
            get
            {
                return bifIndex;
            }
            set
            {
                if (value <= 4095) //  max value in 12 bits
                {
                    bifIndex = value;
                }
                else
                {
                    throw new ArgumentException("value");
                }
            }
        }

        //bits 19-14
        private Int16 tileSetIndex;
        public Int16 TileSetIndex
        {
            get
            {
                return tileSetIndex;
            }
            set
            {
                if (value <= 63) //  max value in 6 bits
                {
                    tileSetIndex = value;
                }
                else
                {
                    throw new ArgumentException("value");
                }
            }
        }

        //bits 13-0
        private Int16 nonTileSetIndex;
        public Int16 NonTileSetIndex
        {
            get
            {
                return nonTileSetIndex;
            }
            set
            {
                if (value <= 16383) //  max value in 14 bits
                {
                    nonTileSetIndex = value;
                }
                else
                {
                    throw new ArgumentException("value");
                }
            }
        }
    }

    public class KeyBifEntry2
    {
        public Int32 Length { get; set; }
        public string Filename { get; set; }
        public bool IsOnCD1 { get; set; }
        public bool IsOnCD2 { get; set; }
        public bool IsOnCD3 { get; set; }
        public bool IsOnCD4 { get; set; }
        public bool IsOnCD5 { get; set; }
        public bool IsOnCD6 { get; set; }
        public bool IsInCacheDirectory { get; set; }
        public bool IsInDataDirectory { get; set; }
    }
}