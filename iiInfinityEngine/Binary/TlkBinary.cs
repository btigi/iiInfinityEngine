using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct TlkHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int16 LanguageId;
        public Int32 StringCount;
        public Int32 StringOffset;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct TlkEntryBinary
    {
        public Int16 Flags;
        public array8 Sound;
        public Int32 VolumeVariance;
        public Int32 PitchVariance;
        public Int32 StringIndex;
        public Int32 StringLength;
    }
}