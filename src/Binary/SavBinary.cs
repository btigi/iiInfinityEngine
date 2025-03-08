using System.Runtime.InteropServices;

namespace ii.InfinityEngine.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct SavHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
    }
}