using System;
using System.Collections.Generic;
using System.IO;
using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers
{
    public class KeyFileBinaryReader : IKeyFileReader
    {
        //Note: KEY files do not support 'original file'
        List<KeyBifEntryBinary> bifEntries = [];
        List<KeyBifResourceBinary> bifResources = [];

        public KeyFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open);
            using var br = new BinaryReader(fs);
            var header = (KeyHeaderBinary)Common.ReadStruct(br, typeof(KeyHeaderBinary));

            br.BaseStream.Seek(header.bifEntryOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.bifEntryCount; i++)
            {
                var bifEntry = (KeyBifEntryBinary)Common.ReadStruct(br, typeof(KeyBifEntryBinary));
                bifEntries.Add(bifEntry);
            }

            br.BaseStream.Seek(header.bifResourceOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.bifResourceCount; i++)
            {
                var bifResource = (KeyBifResourceBinary)Common.ReadStruct(br, typeof(KeyBifResourceBinary));
                bifResources.Add(bifResource);
            }

            var keyFile = new KeyFile();
            for (int i = 0; i < bifEntries.Count; i++)
            {
                var bifEntry = bifEntries[i];
                br.BaseStream.Seek(bifEntry.filenameOffset, SeekOrigin.Begin);

                var entry = new KeyBifEntry2();
                entry.Filename = new string(br.ReadChars(bifEntry.filenameLength)).Replace("\0", "");
                entry.Length = bifEntry.length;
                entry.IsInCacheDirectory = Convert.ToBoolean((bifEntry.fileLocation & 1) != 0);
                entry.IsInDataDirectory = Convert.ToBoolean((bifEntry.fileLocation & 2) != 0);
                entry.IsOnCD1 = Convert.ToBoolean((bifEntry.fileLocation & 4) != 0);
                entry.IsOnCD2 = Convert.ToBoolean((bifEntry.fileLocation & 8) != 0);
                entry.IsOnCD3 = Convert.ToBoolean((bifEntry.fileLocation & 16) != 0);
                entry.IsOnCD4 = Convert.ToBoolean((bifEntry.fileLocation & 32) != 0);
                entry.IsOnCD5 = Convert.ToBoolean((bifEntry.fileLocation & 64) != 0);
                entry.IsOnCD6 = Convert.ToBoolean((bifEntry.fileLocation & 128) != 0);

                keyFile.BifFiles.Add((entry, i));
            }

            foreach (var bifResource in bifResources)
            {
                var resource = new KeyBifResource2();
                resource.ResourceName = bifResource.resourceName.ToString();
                resource.ResourceType = (IEFileType)bifResource.resourceType;
                resource.BifIndex = bifResource.resourceLocator >> 20;
                resource.NonTileSetIndex = Convert.ToInt16(bifResource.resourceLocator & 0xFFF);
                resource.TileSetIndex = Convert.ToInt16((bifResource.resourceLocator & 0xFC000) >> 14);

                //Debug.WriteLine(String.Format("{0}{1},{2},{3},{4},{5},{6}", "", resource.ResourceName, resource.ResourceType, bifResource.resourceLocator, resource.BifIndex, Convert.ToInt16(bifResource.resourceLocator & 0xFFF), resource.TileSetIndex));

                keyFile.Resources.Add(resource);
            }
            return keyFile;
        }
    }
}