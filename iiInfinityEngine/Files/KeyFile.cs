using System;
using System.Collections.Generic;

namespace iiInfinityEngine.Core.Files
{
    public class KeyFile : IEFile
    {
        public List<KeyBifEntry2> BifFiles = new List<KeyBifEntry2>();
        public List<KeyBifResource2> Resources = new List<KeyBifResource2>();
        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private IEFileType fileType = IEFileType.Key;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }
    }

    public class KeyBifResource2
    {/*
        public T Load<T>() where T : new() 
        {
            // attempt to load from override
            // otherwise load from bif

            switch (this.ResourceType)
            {
                case FileType.Sto:
                    var x = new T();
                    (x as StoFile).Lore = 1;
                    return x;

                case FileType.Bam:
                    return new T();

            }

            return default(T);
        }*/

        public string ResourceName { get; set; }
        public IEFileType ResourceType { get; set; }

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

        //bits 13- 0
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
        public Int32 length;
        public string filename;
        public bool IsOnCD1;
        public bool IsOnCD2;
        public bool IsOnCD3;
        public bool IsOnCD4;
        public bool IsOnCD5;
        public bool IsOnCD6;
        public bool IsInCacheDirectory;
        public bool IsInDataDirectory;
    }
}