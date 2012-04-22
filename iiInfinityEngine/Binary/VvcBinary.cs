using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct VvcHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public array8 Animation;
        public array8 Animation2;
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
        public array8 BitmapPalette;
        public Int32 ZPosition;
        public Int32 CentreX;
        public Int32 CentreY;
        public Int32 LightingBrightness;
        public Int32 Duration;
        public array8 InternalName;
        public Int32 Bam1Sequence;
        public Int32 Bam2Sequence;
        public Int32 CurrentAnimationSequence;
        public Int32 UseContinuousSequence;
        public array8 Wav1;
        public array8 Wav2;
        public array8 AlphaBlendingAnimation;
        public Int32 Bam3Sequence;
        public array8 Wav3;
        public array336 Unused3;
    }
}