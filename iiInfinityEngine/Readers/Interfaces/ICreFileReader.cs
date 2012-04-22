using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core
{
    interface ICreFileReader
    {
        CreFile Read(string filename);
        CreFile Read(Stream s);
    }
}