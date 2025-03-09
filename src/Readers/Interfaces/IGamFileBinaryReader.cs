using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine
{
    interface IGamFileBinaryReader
    {
        GamFile Read(string filename);
        GamFile Read(Stream s);
    }
}