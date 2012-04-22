using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core
{
    interface IAreFileReader
    {
        AreFile Read(string filename);
        AreFile Read(Stream s);
    }
}