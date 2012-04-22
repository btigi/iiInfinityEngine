using System;

namespace iiInfinityEngine.Core.Files
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
        private IEFileType fileType = IEFileType.Eff;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }
        public Int16 Opcode;
        public byte TargetType;
        public byte Power;
        public Int32 Parameter1;
        public Int32 Parameter2;
        public byte TimingMode;
        public byte DispelResistance;
        public Int32 Duration;
        public byte Probability1;
        public byte Probability2;
        public string resource;
        public Int32 DiceThrown;
        public Int32 DiceSides;
        public Int32 SavingThrowType;
        public Int32 SavingThrowBonus;
        public Int32 Unknown;
    }
}