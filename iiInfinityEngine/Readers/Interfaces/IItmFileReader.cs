using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core
{
    interface IItmFileReader
    {
        ItmFile Read(string filename);
        ItmFile Read(Stream s);
    }
}