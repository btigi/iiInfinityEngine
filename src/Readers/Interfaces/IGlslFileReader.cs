using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface IGlslFileReader
    {
        GlslFile Read(string filename);
        GlslFile Read(Stream s);
    }
}