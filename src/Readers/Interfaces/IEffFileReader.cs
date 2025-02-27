using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface IEffFileReader
    {
        EffFile Read(string filename);
        EffFile Read(Stream s);
    }
}