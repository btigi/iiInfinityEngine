using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface IWfxFileReader
    {
        WfxFile Read(string filename);
        WfxFile Read(Stream s);
    }
}