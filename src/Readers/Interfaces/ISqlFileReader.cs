using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface ISqlFileReader
    {
        SqlFile Read(string filename);
        SqlFile Read(Stream s);
    }
}