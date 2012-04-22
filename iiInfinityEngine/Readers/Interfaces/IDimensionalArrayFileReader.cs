using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface IDimensionalArrayFileReader
    {
        DimensionalArrayFile Read(string filename);
        DimensionalArrayFile Read(Stream s);
    }
}