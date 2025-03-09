using System.Runtime.InteropServices;

namespace ii.InfinityEngine.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct VefHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public int Vef1Offset;
        public int Vef1Count;
        public int Vef2Offset;
        public int Vef2Count;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct VefBodyBinary
    {
        public int TicksUntilStart;
        public int Unused4;
        public int TicksUntilLoop;
        public int ResourceType;
        public array8 Resref;
        public int ContinuousCycles;
        public array124 Unused1c;
    }
}