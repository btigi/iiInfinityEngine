using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface IVefFileReader
    {
        VefFile Read(string filename);
        VefFile Read(Stream s);
    }
}