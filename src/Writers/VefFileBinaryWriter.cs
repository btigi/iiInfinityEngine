using System.IO;
using ii.InfinityEngine.Binary;
using ii.InfinityEngine.Files;
using System;
using ii.InfinityEngine.Writers.Interfaces;
using System.Runtime.InteropServices;

namespace ii.InfinityEngine.Writers
{
    public class VefFileBinaryWriter : IVefFileWriter
    {
        public BackupManager BackupManger { get; set; }

        public bool Write(string filename, IEFile file, bool forceSave = false)
        {
            if (file is not VefFile)
                throw new ArgumentException("File is not a valid vef file");

            var vefFile = file as VefFile;

            if (!(forceSave) && (HashGenerator.GenerateKey(vefFile) == vefFile.Checksum))
                return false;

            var header = new VefHeaderBinary();
            header.ftype = new array4() { character1 = 'V', character2 = 'E', character3 = 'F', character4 = ' ' };
            header.fversion = new array4() { character1 = '\0', character2 = '\0', character3 = '\0', character4 = '\0' };
            header.Vef1Offset = 24;
            header.Vef1Count = vefFile.PrimaryComponents.Count;
            header.Vef2Offset = 24 + vefFile.PrimaryComponents.Count * Marshal.SizeOf(typeof(VefBodyBinary));
            header.Vef2Count = vefFile.SecondaryComponents.Count;

            using var s = new MemoryStream();
            using var bw = new BinaryWriter(s);
            var headerAsBytes = Common.WriteStruct(header);
            bw.Write(headerAsBytes);

            foreach (var vef in vefFile.PrimaryComponents)
            {
                var vefBody = new VefBodyBinary();
                vefBody.TicksUntilStart = vef.TicksUntilStart;
                vefBody.Unused4 = vef.Unused4;
                vefBody.TicksUntilLoop = vef.TicksUntilLoop;
                vefBody.ResourceType = vef.ResourceType;
                vefBody.Resref = vef.ResRef;
                vefBody.ContinuousCycles = vef.ContinuousCycles;
                vefBody.Unused1c = vef.Unused1c;
                var vefBodyAsBytes = Common.WriteStruct(vefBody);
                bw.Write(vefBodyAsBytes);
            }

            foreach (var vef in vefFile.SecondaryComponents)
            {
                var vefBody = new VefBodyBinary();
                vefBody.TicksUntilStart = vef.TicksUntilStart;
                vefBody.Unused4 = vef.Unused4;
                vefBody.TicksUntilLoop = vef.TicksUntilLoop;
                vefBody.ResourceType = vef.ResourceType;
                vefBody.Resref = vef.ResRef;
                vefBody.ContinuousCycles = vef.ContinuousCycles;
                vefBody.Unused1c = vef.Unused1c;
                var vefBodyAsBytes = Common.WriteStruct(vefBody);
                bw.Write(vefBodyAsBytes);
            }

            BackupManger?.BackupFile(file, file.Filename, file.FileType, this);

            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            bw.BaseStream.Position = 0;
            bw.BaseStream.CopyTo(fs);
            fs.Flush(flushToDisk: true);
            return true;
        }
    }
}