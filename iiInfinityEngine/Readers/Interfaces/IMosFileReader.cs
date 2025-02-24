using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface IMosFileReader
    {
        MosFile Read(string filename);
        MosFile Read(Stream s);
    }
}