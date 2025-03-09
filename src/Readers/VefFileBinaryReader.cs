using System.IO;
using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;
using ii.InfinityEngine.Readers.Interfaces;

namespace ii.InfinityEngine.Readers
{
    public class VefFileBinaryReader : IVefFileReader
    {
        public VefFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public VefFile Read(Stream s)
        {
            using var br = new BinaryReader(s);
            var vefFile = ParseFile(br);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            vefFile.OriginalFile = ParseFile(br);
            return vefFile;
        }

        private VefFile ParseFile(BinaryReader br)
        {
            var header = (VefHeaderBinary)Common.ReadStruct(br, typeof(VefHeaderBinary));

            if (header.ftype.ToString() != "VEF ")
                return new VefFile();

            var vefFile = new VefFile();
            br.BaseStream.Seek(header.Vef1Offset, SeekOrigin.Begin);
            for (var i = 0; i < header.Vef1Count; i++)
            {
                var vefBody = (VefBodyBinary)Common.ReadStruct(br, typeof(VefBodyBinary));
                var vef = new VefBody();
                vef.TicksUntilStart = vefBody.TicksUntilStart;
                vef.Unused4 = vefBody.Unused4;
                vef.TicksUntilLoop = vefBody.TicksUntilLoop;
                vef.ResourceType = vefBody.ResourceType;
                vef.ResRef = vefBody.Resref;
                vef.ContinuousCycles = vefBody.ContinuousCycles;
                vef.Unused1c = vefBody.Unused1c;
                vefFile.PrimaryComponents.Add(vef);
            }

            br.BaseStream.Seek(header.Vef2Offset, SeekOrigin.Begin);
            for (var i = 0; i < header.Vef2Count; i++)
            {
                var vefBody = (VefBodyBinary)Common.ReadStruct(br, typeof(VefBodyBinary));
                var vef = new VefBody();
                vef.TicksUntilStart = vefBody.TicksUntilStart;
                vef.Unused4 = vefBody.Unused4;
                vef.TicksUntilLoop = vefBody.TicksUntilLoop;
                vef.ResourceType = vefBody.ResourceType;
                vef.ResRef = vefBody.Resref;
                vef.ContinuousCycles = vefBody.ContinuousCycles;
                vef.Unused1c = vefBody.Unused1c;
                vefFile.SecondaryComponents.Add(vef);
            }

            vefFile.Checksum = HashGenerator.GenerateKey(vefFile);
            return vefFile;
        }
    }
}