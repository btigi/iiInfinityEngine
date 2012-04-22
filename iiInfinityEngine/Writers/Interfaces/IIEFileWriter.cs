
namespace iiInfinityEngine.Core.Writers.Interfaces
{
    public interface IIEFileWriter
    {
        bool Write(string filename, IEFile file, bool SaveIfNotChanged = false);
        BackupManager BackupManger { get; set; }
    }
}
