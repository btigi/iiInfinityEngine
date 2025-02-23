using System;

namespace iiInfinityEngine.Core.Files
{
    [Serializable]
    public class WfxFile : IEFile
    {
        [NonSerialized]
        private string checksum;
        public string Checksum { get { return checksum; } set { checksum = value; } }
        [NonSerialized]
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        [NonSerialized]
        private readonly IEFileType fileType = IEFileType.Wfx;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public WfxFile()
        {
            Flags = new WfxFlags();
        }

        public Int32 SrCurveRadius { get; set; }
        public WfxFlags Flags { get; set; }
        public Int32 RandomFrequencyVariation { get; set; }
        public Int32 RandomVolumeVariation { get; set; }
        public array240 Unused0018 { get; set; }
    }

    [Serializable]
    public class WfxFlags
    {
        public bool CutsceneVolumeEnabled { get; set; }
        public bool CustomSRCurveRadiusEnabled { get; set; }
        public bool RandomFrequencyVariationEnabled { get; set; }
        public bool RandomVolumeVariationEnabled { get; set; }
        public bool EnvironmentalAudioEnabled { get; set; }
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
        public bool Bit31 { get; set; }
    }
}