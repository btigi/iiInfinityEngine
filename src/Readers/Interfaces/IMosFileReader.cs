using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface IMosFileReader
    {
        MosFile Read(string filename);
        MosFile Read(Stream s);
    }
}