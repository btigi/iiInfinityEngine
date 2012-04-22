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
        private IEFileType fileType = IEFileType.Wfx;
        public IEFileType FileType { get { return fileType; } }
        [NonSerialized]
        private IEFile originalFile;
        public IEFile OriginalFile { get { return originalFile; } set { originalFile = value; } }

        public Int32 SrCurveRadius { get; set; }
        public WfxFlags Flags;
        public Int32 RandomFrequencyVariation { get; set; }
        public Int32 RandomVolumeVariation { get; set; }
        public array218 Unused0018 { get; set; }
    }

    [Serializable]
    public struct WfxFlags
    {
        public bool CutsceneVolumeEnabled { get; set; }
        public bool CustomSRCurveRadiusEnabled { get; set; }
        public bool RandomFrequencyVariationEnabled { get; set; }
        public bool RandomVolumeVariationEnabled { get; set; }
        public bool EnvironmentalAudioEnabled { get; set; }
    }
}