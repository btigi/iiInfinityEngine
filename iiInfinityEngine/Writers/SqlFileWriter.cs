using System.IO;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;
using System;

namespace iiInfinityEngine.Core.Writers
{
    public class SqlFileWriter : ISqlFileWriter
    {
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not SqlFile)
                throw new ArgumentException("File is not a valid sql file");

            var sqlFile = file as SqlFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(sqlFile) == sqlFile.Checksum))
                return false;

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            File.WriteAllText(filename, sqlFile.Contents);
            return true;
        }
    }
}