using System.IO;
using iiInfinityEngine.Core.Files;
using System.Collections.Generic;

namespace iiInfinityEngine.Core
{
    interface IBifFileReader
    {
        /*BifFile Read(string filename);*/
        BifFile Read(Stream s, List<KeyBifResource2> resources, List<IEFileType> fileTypes);
    }
}