using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct KeyHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int32 bifEntryCount;
        public Int32 bifResourceCount;
        public Int32 bifEntryOffset;
        public Int32 bifResourceOffset;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct KeyBifEntryBinary
    {
        public Int32 length;
        public Int32 filenameOffset;
        public Int16 filenameLength;
        public Int16 fileLocation;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct KeyBifResourceBinary
    {
        public array8 resourceName;
        public Int16 resourceType;
        public Int32 resourceLocator;
    }
}