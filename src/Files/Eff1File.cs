using System;

namespace ii.InfinityEngine.Files
{
    [Serializable]
    public class Eff1File : IEFile
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

        public Int16 Opcode { get; set; }
        public EffTargetType TargetType { get; set; }
        public byte Power { get; set; }
        public Int32 Parameter1 { get; set; }
        public Int32 Parameter2 { get; set; }
        public OpcodeTargetType TimingMode { get; set; }
        public byte Resistance { get; set; } //TODO:eff1
        public Int32 Duration { get; set; }
        public byte Probability1 { get; set; }
        public byte Probability2 { get; set; }
        public array8 Resource { get; set; }
        public Int32 DiceThrown { get; set; }
        public Int32 DiceSides { get; set; }
        public Int32 SavingThrowType { get; set; } //TODO:eff1
        public Int32 SavingThrowBonus { get; set; }
        public Int32 Unknown { get; set; }
    }
}