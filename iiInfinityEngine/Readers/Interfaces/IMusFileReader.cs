using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface IMusFileReader
    {
        MusFile Read(string filename);
        MusFile Read(Stream s);
    }
}