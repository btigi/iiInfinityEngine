﻿using System.IO;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Writers.Interfaces;
using System;

namespace iiInfinityEngine.Core.Writers
{
    public class GuiFileWriter : IGuiFileWriter
    {
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not GuiFile)
                throw new ArgumentException("File is not a valid gui file");

            var guiFile = file as GuiFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(guiFile) == guiFile.Checksum))
                return false;

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            File.WriteAllText(filename, guiFile.Contents);
            return true;
        }
    }
}