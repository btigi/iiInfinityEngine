using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine
{
    interface IWmpFileReader
    {
        WmpFile Read(string filename);
        WmpFile Read(Stream s);
    }
}