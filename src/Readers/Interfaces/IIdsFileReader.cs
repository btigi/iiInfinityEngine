using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface IIdsFileReader
    {
        IdsFile Read(string filename);
        IdsFile Read(Stream s);
    }
}