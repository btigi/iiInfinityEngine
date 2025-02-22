using System;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class EffFile : IEFile
    {
        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private readonly IEFileType fileType = IEFileType.Eff;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public EffFile()
        {
            FlagsFromParent = new SpellFlags();
        }

        public Int32 Opcode { get; set; }
        public EffTargetType TargetType { get; set; }
        public Int32 Power { get; set; }
        public Int32 Parameter1 { get; set; }
        public Int32 Parameter2 { get; set; }
        public EffTimingMode TimingMode { get; set; }
        public Int16 Unknown26 { get; set; }
        public Int32 Duration { get; set; }
        public Int16 Probability1 { get; set; }
        public Int16 Probability2 { get; set; }
        public array8 Resource { get; set; }
        public Int32 DiceThrown { get; set; }
        public Int32 DiceSides { get; set; }
        public SavingThrowType SavingThrowType { get; set; }
        public Int32 SavingThrowBonus { get; set; }
        public Int32 Special { get; set; }
        public Int32 PrimaryType { get; set; }
        public Int32 Unknown50 { get; set; }
        public Int32 LowestAffectedLevelFromParent { get; set; }
        public Int32 HighestAffectedLevelFromParent { get; set; }
        public EffResistance Resistance { get; set; }
        public Int32 Parameter3 { get; set; }
        public Int32 Parameter4 { get; set; }
        public Int32 Parameter5 { get; set; }
        public Int32 TimeApplied { get; set; }
        public array8 Resource2 { get; set; }
        public array8 Resource3 { get; set; }
        public Int32 CasterXCoordinate { get; set; }
        public Int32 CasterYCoordinate { get; set; }
        public Int32 TargetXCoordinate { get; set; }
        public Int32 TargetYCoordinate { get; set; }
        public ResourceTypeFromParent ResourceTypeFromParent { get; set; }
        public array8 ParentResource { get; set; } //TODO:eff - must be ALL CAPS        
        public SpellFlags FlagsFromParent { get; set; }
        public Int32 Projectile { get; set; }
        public Int32 ParentResourceSlot { get; set; }
        public array32 Variable { get; set; }
        public Int32 CasterLevel { get; set; }
        public Int32 FirstApply { get; set; }
        public Int32 SecondaryType { get; set; }
        public Int32 Unknownd4 { get; set; }
        public Int32 Unknownd8 { get; set; }
        public Int32 Unknowndc { get; set; }        
        public Int32 Unknowne0 { get; set; }
        public Int32 Unknowne4 { get; set; }
        public Int32 Unknowne8 { get; set; }
        public Int32 Unknownec { get; set; }
        public Int32 Unknownf0 { get; set; }
        public Int32 Unknownf4 { get; set; }
        public Int32 Unknownf8 { get; set; }
        public Int32 Unknownfc { get; set; }
        public Int32 Unknownd100 { get; set; }
        public Int32 Unknownd104 { get; set; }
        public Int32 Unknownd108 { get; set; }
        public Int32 Unknownd10c { get; set; }
    }

    public enum EffTimingMode : Int16
    {
        InstantLimited,
        InstantPermanent,
        InstantWhileEquipped,
        DelayLimited,
        DelayPermanent,
        DelayWhileEquipped,
        LimitedAfterDuration,
        PermanentAfterDuration,
        EquippedAfterDuration,
        InstantPermanentAfterDeath,
        Instant_Limited
    }

    public enum ResourceTypeFromParent
    {
        None,
        Spell,
        Item
    }

    [Serializable]
    public class EffResistance
    {
        public bool DispellableAffectedByMagicResistance { get; set; }
        public bool IgnoreMagicResistance { get; set; }
        public bool SelfTargetted { get; set; }
        public bool Bit3 { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool Bit9 { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
        public bool Bit16 { get; set; }
        public bool Bit17 { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool Bit20 { get; set; }
        public bool Bit21 { get; set; }
        public bool Bit22 { get; set; }
        public bool Bit23 { get; set; }
        public bool Bit24 { get; set; }
        public bool Bit25 { get; set; }
        public bool Bit26 { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool EffectAppliedByItem { get; set; }
    }
}