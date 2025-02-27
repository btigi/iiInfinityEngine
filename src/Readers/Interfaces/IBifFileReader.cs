using System.IO;
using ii.InfinityEngine.Files;
using System.Collections.Generic;

namespace ii.InfinityEngine
{
    interface IBifFileReader
    {
        /*BifFile Read(string filename);*/
        BifFile Read(Stream s, List<KeyBifResource2> resources, List<IEFileType> fileTypes);
    }
}