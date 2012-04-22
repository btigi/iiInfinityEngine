using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface ISplFileReader
    {
        SplFile Read(string filename);
        SplFile Read(Stream s);
    }
}