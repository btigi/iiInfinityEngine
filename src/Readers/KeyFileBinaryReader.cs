using System;
using System.Buffers.Binary;
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

        public KeyFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 1 << 16, FileOptions.SequentialScan);
            using var br = new BinaryReader(fs);
            var header = (KeyHeaderBinary)Common.ReadStruct(br, typeof(KeyHeaderBinary));

            br.BaseStream.Seek(header.bifEntryOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.bifEntryCount; i++)
            {
                bifEntries.Add(Common.ReadStruct<KeyBifEntryBinary>(br));
            }

            var keyFile = new KeyFile();
            for (int i = 0; i < bifEntries.Count; i++)
            {
                var bifEntry = bifEntries[i];
                br.BaseStream.Seek(bifEntry.filenameOffset, SeekOrigin.Begin);

                var entry = new KeyBifEntry2();
                entry.Filename = new string(br.ReadChars(bifEntry.filenameLength)).Replace("\0", "");
                entry.Length = bifEntry.length;
                entry.IsInCacheDirectory = (bifEntry.fileLocation & 1) != 0;
                entry.IsInDataDirectory = (bifEntry.fileLocation & 2) != 0;
                entry.IsOnCD1 = (bifEntry.fileLocation & 4) != 0;
                entry.IsOnCD2 = (bifEntry.fileLocation & 8) != 0;
                entry.IsOnCD3 = (bifEntry.fileLocation & 16) != 0;
                entry.IsOnCD4 = (bifEntry.fileLocation & 32) != 0;
                entry.IsOnCD5 = (bifEntry.fileLocation & 64) != 0;
                entry.IsOnCD6 = (bifEntry.fileLocation & 128) != 0;

                keyFile.BifFiles.Add((entry, i));
            }

            const int resourceEntrySize = 14;
            br.BaseStream.Seek(header.bifResourceOffset, SeekOrigin.Begin);
            var resourceBlock = br.ReadBytes(header.bifResourceCount * resourceEntrySize);
            keyFile.Resources.Capacity = header.bifResourceCount;

            for (int i = 0; i < header.bifResourceCount; i++)
            {
                int offset = i * resourceEntrySize;
                var resource = new KeyBifResource2
                {
                    ResourceName = new array8(resourceBlock.AsSpan(offset, 8)).ToString(),
                    ResourceType = (IEFileType)BinaryPrimitives.ReadInt16LittleEndian(resourceBlock.AsSpan(offset + 8))
                };
                int locator = BinaryPrimitives.ReadInt32LittleEndian(resourceBlock.AsSpan(offset + 10));
                resource.BifIndex = locator >> 20;
                resource.NonTileSetIndex = Convert.ToInt16(locator & 0xFFF);
                resource.TileSetIndex = Convert.ToInt16((locator & 0xFC000) >> 14);
                keyFile.Resources.Add(resource);
            }
            return keyFile;
        }
    }
}