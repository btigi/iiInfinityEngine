using System.IO;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;
using System;

namespace iiInfinityEngine.Core.Writers
{
    public class LuaFileWriter : ILuaFileWriter
    {
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not LuaFile)
                throw new ArgumentException("File is not a valid lua file");

            var luaFile = file as LuaFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(luaFile) == luaFile.Checksum))
                return false;

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            File.WriteAllText(filename, luaFile.Contents);
            return true;
        }
    }
}