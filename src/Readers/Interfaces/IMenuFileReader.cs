using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface IMenuFileReader
    {
        MenuFile Read(string filename);
        MenuFile Read(Stream s);
    }
}