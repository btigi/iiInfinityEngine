using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface ISplFileReader
    {
        SplFile Read(string filename);
        SplFile Read(Stream s);
    }
}