using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine
{
    interface ICreFileReader
    {
        CreFile Read(string filename);
        CreFile Read(Stream s);
    }
}