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
            if (!(file is DimensionalArrayFile))
                throw new ArgumentException("File is not a valid 2da file");

            var dimensionalArrayFile = file as DimensionalArrayFile;

            if (!(forceSave) && (MD5HashGenerator.GenerateKey(dimensionalArrayFile) == dimensionalArrayFile.Checksum))
                return false;

            if (BackupManger != null)
            {
                BackupManger.BackupFile(file, file.Filename, file.FileType, this);
            }

            File.WriteAllText(filename, dimensionalArrayFile.contents);
            return true;
        }
    }
}