using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface IDimensionalArrayFileReader
    {
        DimensionalArrayFile Read(string filename);
        DimensionalArrayFile Read(Stream s);
    }
}