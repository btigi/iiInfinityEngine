using System.IO;

namespace iiInfinityEngine.Core
{
    /// <summary>
    /// Copies files with optional backup support
    /// </summary>
    public class IEFileCopier
    {
        /// <summary>
        /// The Backup Manager to use for all copy operations
        /// </summary>
        public BackupManager BackupManager { get; set; }

        /// <summary>
        /// Copy a file.
        /// </summary>
        /// <param name="source">The full path and filename of the source file</param>
        /// <param name="destination">The full path and filename of the destination file</param>
        /// <remarks>If the destination file exists it is replaced with the source file (after being backup up by the backup manager, if available)</remarks>
        public void CopyFile(string source, string destination)
        {
            if (BackupManager != null)
            {
                BackupManager.BackupFile(destination);
            }
            File.Copy(source, destination, overwrite: true);
        }
    }
}