using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core
{
    interface IKeyFileReader
    {
        KeyFile Read(string filename);
    }
}