using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface ITisFileReader
    {
        TisFile Read(string filename);
        TisFile Read(Stream s, bool fromBiff, int tileCount, int tileLength, int tileDimension);
    }
}