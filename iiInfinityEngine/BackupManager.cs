using System;
using System.IO;
using iiInfinityEngine.Core.Writers.Interfaces;

namespace iiInfinityEngine.Core
{
    public class BackupManager
    {
        public const string BackupFileLogName = "index.log";
        public string BackupDirectory { get; set; }

        public BackupManager(string backupDirectory)
        {
            BackupDirectory = backupDirectory;
        }

        public void BackupFile(IEFile file, string filename, IEFileType filetype, IIEFileWriter writer)
        {
            if (!File.Exists(filename))
                return;

            if (!Directory.Exists(BackupDirectory))
                Directory.CreateDirectory(BackupDirectory);

            var location = Path.Combine(BackupDirectory, Path.GetFileName(filename));

            if (File.Exists(location))
                return;

            if (file.OriginalFile != null)
            {
                writer.BackupManger = null;
                writer.Write(location, file.OriginalFile, SaveIfNotChanged: true);
                writer.BackupManger = this;
            }

            Log(filename, "replace");
        }

        public void BackupFile(string destination)
        {
            if (!Directory.Exists(BackupDirectory))
                Directory.CreateDirectory(BackupDirectory);

            var filename = Path.GetFileName(destination);
            var location = Path.Combine(BackupDirectory, Path.GetFileName(filename));

            if (!File.Exists(destination))
            {
                Log(destination, "new");
                return;
            }

            var backupLocation = Path.Combine(BackupDirectory, filename);

            if (File.Exists(backupLocation))
                return;

            File.Copy(destination, backupLocation);

            Log(destination, "replace");
        }

        public void Uninstall()
        {
            var backupLogFileName = Path.Combine(BackupDirectory, BackupFileLogName);
            using (StreamReader file = new StreamReader(backupLogFileName))
            {
                string line = String.Empty;
                while ((line = file.ReadLine()) != null)
                {
                    var data = line.Split('|');
                    var xoperation = Path.GetFileName(data[0]);
                    var xfile = data[1];

                    if (xoperation == "new")
                    {
                        File.Delete(xfile);
                    }
                    else
                    {
                        File.Copy(Path.Combine(BackupDirectory, Path.GetFileName(xfile)), xfile, true);
                        File.Delete(xfile);
                    }
                }
            }
        }

        private void Log(string destination, string operation)
        {
            using (StreamWriter log = new StreamWriter(Path.Combine(BackupDirectory, BackupFileLogName), true))
            {
                log.WriteLine(operation + "|" + destination);
            }
        }
    }
}