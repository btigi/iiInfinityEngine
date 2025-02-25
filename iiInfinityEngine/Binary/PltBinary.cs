using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct PltHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int16 Unknown1;
        public Int16 Unknown2;
        public Int16 Unknown3;
        public Int16 Unknown4;
        public Int32 Width;
        public Int32 Height;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct PltBodyBinary
    {
        public byte Column;
        public byte Row;
    }
}