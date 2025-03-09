using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine
{
    interface IGamFileReader
    {
        GamFile Read(string filename);
        GamFile Read(Stream s);
    }
}