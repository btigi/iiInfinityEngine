using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface IVvcFileReader
    {
        VvcFile Read(string filename);
        VvcFile Read(Stream s);
    }
}