using System;
using System.Runtime.InteropServices;

namespace iiInfinityEngine.Core.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct DlgHeaderBinary
    {
        public array4 ftype;
        public array4 fversion;
        public Int32 StateCount;
        public Int32 StateOffset;
        public Int32 TransitionCount;
        public Int32 TransitionOffset;
        public Int32 StateTriggerOffset;
        public Int32 StateTriggerCount;
        public Int32 TransitionTriggerOffset;
        public Int32 TransitionTriggerCount;
        public Int32 ActionTableOffset;
        public Int32 ActionTableCount;
        public Int32 Flags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct StateBinary
    {
        public Int32 ResponseText;
        public Int32 TransitionIndex;
        public Int32 TransitionCount;
        public Int32 TriggerIndex;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct TransitionBinary
    {
        public Int32 Flags;
        public Int32 TransitionText;
        public Int32 JournalText;
        public Int32 TransitionTrigger;
        public Int32 ActionIndex;
        public array8 Dialog;
        public Int32 NextState;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct StateTriggerBinary
    {
        public Int32 StateTriggerStringOffset;
        public Int32 StateTriggerStringLength;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct TransitionTriggerBinary
    {
        public Int32 TransitionTriggerStringOffset;
        public Int32 TransitionTriggerStringLength;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ActionTableBinary
    {
        public Int32 ActionTableStringOffset;
        public Int32 ActionTableStringLength;
    }
}