using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine
{
    interface IAreFileReader
    {
        AreFile Read(string filename);
        AreFile Read(Stream s);
    }
}