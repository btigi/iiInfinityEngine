using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface ILuaFileReader
    {
        LuaFile Read(string filename);
        LuaFile Read(Stream s);
    }
}