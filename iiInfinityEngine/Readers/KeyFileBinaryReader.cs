using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using System.Diagnostics.CodeAnalysis;

namespace iiInfinityEngine.Core.Readers
{
    public class KeyFileBinaryReader : IKeyFileReader
    {
        //Note: KEY files do not support 'original file'
        List<KeyBifEntryBinary> bifEntries = new List<KeyBifEntryBinary>();
        List<KeyBifResourceBinary> bifResources = new List<KeyBifResourceBinary>();

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public KeyFile Read(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {
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

                KeyFile keyFile = new KeyFile();
                for (int i = 0; i < bifEntries.Count; i++)
                {
                    var bifEntry = bifEntries[i];
                    br.BaseStream.Seek(bifEntry.filenameOffset, SeekOrigin.Begin);

                    KeyBifEntry2 entry = new KeyBifEntry2();
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

                    keyFile.BifFiles.Add(entry);
                }

                foreach (var bifResource in bifResources)
                {
                    KeyBifResource2 resource = new KeyBifResource2();
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
}