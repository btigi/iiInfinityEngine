using iiInfinityEngine.Core.Files;
using System.Drawing;
using System.IO;

namespace iiInfinityEngine.Core
{
    interface IPltFileReader
    {
        PltFile Read(string filename, Bitmap palette);
        PltFile Read(Stream s, Bitmap palette);
    }
}