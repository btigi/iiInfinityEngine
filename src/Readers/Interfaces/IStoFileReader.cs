using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface IStoFileReader
    {
        StoFile Read(string filename);
        StoFile Read(Stream s);
    }
}