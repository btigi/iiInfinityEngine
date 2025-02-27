using System.IO;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Writers.Interfaces;
using System;

namespace ii.InfinityEngine.Writers
{
    public class DimensionalArrayFileWriter : IDimensionalArrayFileWriter
    {
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not DimensionalArrayFile)
                throw new ArgumentException("File is not a valid 2da file");

            var dimensionalArrayFile = file as DimensionalArrayFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(dimensionalArrayFile) == dimensionalArrayFile.Checksum))
                return false;

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            File.WriteAllText(filename, dimensionalArrayFile.Contents);
            return true;
        }
    }
}