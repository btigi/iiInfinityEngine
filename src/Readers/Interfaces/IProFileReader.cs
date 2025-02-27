using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine
{
    interface IProFileReader
    {
        ProFile Read(string filename);
        ProFile Read(Stream s);
    }
}