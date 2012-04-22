using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface IWfxFileReader
    {
        WfxFile Read(string filename);
        WfxFile Read(Stream s);
    }
}