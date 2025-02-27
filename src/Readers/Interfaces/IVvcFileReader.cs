using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface IVvcFileReader
    {
        VvcFile Read(string filename);
        VvcFile Read(Stream s);
    }
}