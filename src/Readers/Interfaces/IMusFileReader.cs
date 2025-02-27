using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface IMusFileReader
    {
        MusFile Read(string filename);
        MusFile Read(Stream s);
    }
}