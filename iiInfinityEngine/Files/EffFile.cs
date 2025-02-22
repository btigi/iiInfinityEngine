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
        public Int32 Resistance { get; set; } //TODO:eff
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
        
        public Int32 FlagsFromParent { get; set; } //TODO:eff -> make a class with 32 bits, and add this field to the binary, reader and writer


        public Int32 Projectile { get; set; }
        public Int32 ParentResourceSlot { get; set; }
        public array32 Variable { get; set; }
        public Int32 CasterLevel { get; set; }
        public Int32 Unknown5 { get; set; } //TODO:eff first apply
        public Int32 SecondaryType { get; set; }
        public Int32 Unknownd4_1 { get; set; } //TODO:eff name after hex offset
        public Int32 Unknownd4_2 { get; set; }
        public Int32 Unknownd4_3 { get; set; }
        public Int32 Unknownd4_4 { get; set; }
        public Int32 Unknownd4_5 { get; set; }
        public Int32 Unknownd4_6 { get; set; }
        public Int32 Unknownd4_7 { get; set; }
        public Int32 Unknownd4_8 { get; set; }
        public Int32 Unknownd4_9 { get; set; }
        public Int32 Unknownd4_10 { get; set; }
        public Int32 Unknownd4_11 { get; set; }
        public Int32 Unknownd4_12 { get; set; }
        public Int32 Unknownd4_13 { get; set; }
        public Int32 Unknownd4_14 { get; set; }
        public Int32 Unknownd4_15 { get; set; }
    }

    public enum EffTargetType
    {
        None = 0,
        Self,
        ProjectileTarget,
        Party,
        Everyone,
        EveryoneExceptParty,
        CasterGroup,
        TargetGroup,
        EveryoneExceptSelf,
        OriginalCaster
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
}