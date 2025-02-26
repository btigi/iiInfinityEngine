using System.IO;
using iiInfinityEngine.Core.Files;

namespace iiInfinityEngine.Core.Readers.Interfaces
{
    interface IGuiFileReader
    {
        GuiFile Read(string filename);
        GuiFile Read(Stream s);
    }
}