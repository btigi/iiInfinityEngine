using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface IStoFileReader
    {
        StoFile Read(string filename);
        StoFile Read(Stream s);
    }
}