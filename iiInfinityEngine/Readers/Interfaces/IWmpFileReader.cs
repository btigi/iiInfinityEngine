using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core
{
    interface IWmpFileReader
    {
        WmpFile Read(string filename);
        WmpFile Read(Stream s);
    }
}