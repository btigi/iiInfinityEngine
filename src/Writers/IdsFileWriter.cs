using System;
using System.IO;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Writers.Interfaces;

namespace ii.InfinityEngine.Writers
{
    public class IdsFileWriter : IIdsFileWriter
    {
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not IdsFile)
                throw new ArgumentException("File is not a valid ids file");

            var idsFile = file as IdsFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(idsFile) == idsFile.Checksum))
                return false;

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            File.WriteAllText(filename, idsFile.contents);
            return true;
        }
    }
}