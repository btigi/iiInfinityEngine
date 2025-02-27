using ii.InfinityEngine.Files;

namespace ii.InfinityEngine
{
    interface IKeyFileReader
    {
        KeyFile Read(string filename);
    }
}