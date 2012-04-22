using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iiInfinityEngine.Core
{
    /// <summary>
    /// Defines common properties an IE file must support
    /// </summary>
    public interface IEFile
    {
        string Filename { get; set; }
        string Checksum { get; set; }
        IEFileType FileType { get; }
        IEFile OriginalFile { get; set; }
    }
}
