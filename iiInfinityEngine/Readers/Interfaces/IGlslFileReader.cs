using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface IGlslFileReader
    {
        GlslFile Read(string filename);
        GlslFile Read(Stream s);
    }
}