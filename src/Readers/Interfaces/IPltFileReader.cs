using ii.InfinityEngine.Files;
using System.Drawing;
using System.IO;

namespace ii.InfinityEngine
{
    interface IPltFileReader
    {
        PltFile Read(string filename, Bitmap palette);
        PltFile Read(Stream s, Bitmap palette);
    }
}