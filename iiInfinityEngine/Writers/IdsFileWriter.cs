using System;
using System.IO;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;

namespace iiInfinityEngine.Core.Writers
{
    public class IdsFileWriter : IIdsFileWriter
    {
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (!(file is IdsFile))
                throw new ArgumentException("File is not a valid eff file");

            var idsFile = file as IdsFile;

            if (!(forceSave) && (MD5HashGenerator.GenerateKey(idsFile) == idsFile.Checksum))
                return false;

            if (BackupManger != null)
            {
                BackupManger.BackupFile(file, file.Filename, file.FileType, this);
            }

            File.WriteAllText(filename, idsFile.contents);
            return true;
        }
    }
}