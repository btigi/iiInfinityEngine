using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface IMenuFileReader
    {
        MenuFile Read(string filename);
        MenuFile Read(Stream s);
    }
}