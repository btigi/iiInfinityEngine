using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine
{
    interface IDlgFileReader
    {
        DlgFile Read(string filename);
        DlgFile Read(Stream s);
    }
}