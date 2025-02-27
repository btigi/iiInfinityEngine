using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine
{
    interface IItmFileReader
    {
        ItmFile Read(string filename);
        ItmFile Read(Stream s);
    }
}