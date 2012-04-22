using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core
{
    interface IProFileReader
    {
        ProFile Read(string filename);
        ProFile Read(Stream s);
    }
}