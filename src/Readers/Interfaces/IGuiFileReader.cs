using System.IO;
using ii.InfinityEngine.Files;

namespace ii.InfinityEngine.Readers.Interfaces
{
    interface IGuiFileReader
    {
        GuiFile Read(string filename);
        GuiFile Read(Stream s);
    }
}