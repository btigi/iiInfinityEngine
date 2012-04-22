using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface IEffFileReader
    {
        EffFile Read(string filename);
        EffFile Read(Stream s);
    }
}