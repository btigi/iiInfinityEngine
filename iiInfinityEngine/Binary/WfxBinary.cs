using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct WfxHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int32 SrCurveRadius;
        public Int32 Flags;
        public Int32 RandomFrequencyVariation;
        public Int32 RandomVolumeVariation;
        public array218 Unused;
    }
}
