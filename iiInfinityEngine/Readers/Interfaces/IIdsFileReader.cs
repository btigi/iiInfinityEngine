using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface IIdsFileReader
    {
        IdsFile Read(string filename);
        IdsFile Read(Stream s);
    }
}