using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface ITisFileReader
    {
        TisFile Read(string filename);
        TisFile Read(Stream s, bool fromBiff, int tileCount, int tileLength, int tileDimension);
    }
}