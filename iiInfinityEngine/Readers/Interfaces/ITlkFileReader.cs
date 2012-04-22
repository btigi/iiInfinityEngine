using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface ITlkFileReader
    {
        TlkFile Read(string filename);
        TlkFile Read(Stream s);
    }
}