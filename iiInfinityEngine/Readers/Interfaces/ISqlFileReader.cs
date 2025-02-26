using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface ISqlFileReader
    {
        SqlFile Read(string filename);
        SqlFile Read(Stream s);
    }
}