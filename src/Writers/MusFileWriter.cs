using System.IO;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Writers.Interfaces;
using System;

namespace ii.InfinityEngine.Writers
{
    public class MusFileWriter : IMusFileWriter
    {
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not MusFile)
                throw new ArgumentException("File is not a valid mus file");

            var musFile = file as MusFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(musFile) == musFile.Checksum))
                return false;

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            File.WriteAllText(filename, musFile.Contents);
            return true;
        }
    }
}