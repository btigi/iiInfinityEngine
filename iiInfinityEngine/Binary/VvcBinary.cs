using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct VvcHeaderBinary
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 4)]
        public char[] ftype;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 4)]
        public char[] fversion;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] Animation;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] Animation2; // Unused ?
        public ushort DisplayFlags;
        public ushort ColourFlags;
        public Int32 Unused;
        public Int32 SequenceFlags;
        public Int32 Unused2;
        public Int32 XPosition;
        public Int32 YPosition;
        public Int32 UseOrientation;
        public Int32 FrameRate;
        public Int32 OrientationCount;
        public Int32 BaseOrientation;
        public Int32 PositionFlags;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] BitmapPalette;
        public Int32 ZPosition;
        public Int32 CentreX;
        public Int32 CentreY;
        public Int32 LightingBrightness;
        public Int32 Duration;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] InternalName;
        public Int32 Bam1Sequence;
        public Int32 Bam2Sequence;
        public Int32 CurrentAnimationSequence;
        public Int32 UseContinuousSequence;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] Wav1;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] Wav2;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] AlphaBlendingAnimation;
        public Int32 Bam3Sequence;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] Wav3;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 336)]
        public byte[] Unused3;
    }
}