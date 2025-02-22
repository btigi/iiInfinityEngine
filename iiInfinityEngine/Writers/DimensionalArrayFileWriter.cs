using System.IO;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;
using System;

namespace iiInfinityEngine.Core.Writers
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