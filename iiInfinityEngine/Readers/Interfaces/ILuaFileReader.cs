using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface ILuaFileReader
    {
        LuaFile Read(string filename);
        LuaFile Read(Stream s);
    }
}