using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface ITlkFileReader
    {
        TlkFile Read(string filename);
        TlkFile Read(Stream s);
    }
}