using System.IO;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Writers.Interfaces;
using System;

namespace ii.InfinityEngine.Writers
{
    public class GlslFileWriter : IGlslFileWriter
    {
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not GlslFile)
                throw new ArgumentException("File is not a valid glsl file");

            var glslFile = file as GlslFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(glslFile) == glslFile.Checksum))
                return false;

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            File.WriteAllText(filename, glslFile.Contents);
            return true;
        }
    }
}