using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Readers.Interfaces;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using static iiInfinityEngine.Core.MosConverter;

namespace iiInfinityEngine.Core.Readers
{
    public class MosFileBinaryReader : IMosFileReader
    {
        public MosFile Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var f = Read(fs);
            f.Filename = Path.GetFileName(filename);
            return f;
        }

        public MosFile Read(Stream s)
        {
            using var br = new BinaryReader(s);
            var mosFile = ParseFile(br);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            mosFile.OriginalFile = ParseFile(br);
            return mosFile;
        }

        private MosFile ParseFile(BinaryReader br)
        {
            var header = (MosHeaderBinary)Common.ReadStruct(br, typeof(MosHeaderBinary));
            var streamReader = br;

            if ((header.ftype.ToString() == "MOSC") && (header.fversion.ToString() == "V1  "))
            {
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                var headerC = (MosCHeaderBinary)Common.ReadStruct(br, typeof(MosCHeaderBinary));

                var s = new MemoryStream();
                var compressedBytes = br.ReadBytes((int)br.BaseStream.Length);
                using (var m = new MemoryStream())
                {
                    using var compressedStream = new MemoryStream(compressedBytes);
                    using var decompressedStream = new MemoryStream();
                    var zlibStream = new ZLibStream(compressedStream, CompressionMode.Decompress);
                    zlibStream.CopyTo(decompressedStream);
                    var decompressedData = decompressedStream.ToArray();
                    s.Write(decompressedData, 0, headerC.FileLength);
                    m.Position = 0;
                    m.CopyTo(s);
                }
                s.Position = 0;
                br = new BinaryReader(s);

                streamReader = new BinaryReader(s);

                header = (MosHeaderBinary)Common.ReadStruct(streamReader, typeof(MosHeaderBinary));
            }

            if (header.ftype.ToString() != "MOS ")
                return new MosFile();

            if (header.fversion.ToString() == "V2  ")
            {
                List<Mos2DataBlockBinary> dataBlocks = new List<Mos2DataBlockBinary>();

                br.BaseStream.Seek(-24, SeekOrigin.Current);
                var header2 = (Mos2HeaderBinary)Common.ReadStruct(streamReader, typeof(Mos2HeaderBinary));

                br.BaseStream.Seek(header2.BlockOffset, SeekOrigin.Begin);
                for (int i = 0; i < header2.BlockCount; i++)
                {
                    var dataBlock = (Mos2DataBlockBinary)Common.ReadStruct(streamReader, typeof(Mos2DataBlockBinary));
                    dataBlocks.Add(dataBlock);
                }
            }

            var mosFile = new MosFile();
            mosFile.BlockSize = header.BlockSize;
            mosFile.Columns = header.Columns;
            mosFile.Rows = header.Rows;

            var palettes = new List<RGBA[]>();
            var tileDataOffsets = new List<int>();
            var blockDatas = new List<byte[]>();

            // palettes
            br.BaseStream.Seek(header.PaletteOffset, SeekOrigin.Begin);
            for (var i = 0; i < header.Rows * header.Columns; i++)
            {
                var colourData = new RGBA[256];
                for (var j = 0; j < 256; j++)
                {
                    var blue = br.ReadByte();
                    var green = br.ReadByte();
                    var red = br.ReadByte();
                    var alpha = br.ReadByte();
                    colourData[j] = new RGBA() { Red = red, Green = green, Blue = blue, Alpha = alpha };
                }
                palettes.Add(colourData);
            }

            // tile offsets
            for (var i = 0; i < header.Rows * header.Columns; i++)
            {
                tileDataOffsets.Add(br.ReadInt32());
            }

            // tile data
            for (var row = 0; row < header.Rows; row++)
            {
                var pixelRow = header.BlockSize;
                // The last row may not be a full BlockSize
                if ((row == header.Rows - 1) && ((header.Height % header.BlockSize) != 0))
                {
                    pixelRow = header.Height % header.BlockSize;
                }

                for (var column = 0; column < header.Columns; column++)
                {
                    // The last column may not be a full BlockSize
                    var pixelCol = header.BlockSize;
                    if ((column == header.Columns - 1) && ((header.Width % header.BlockSize) != 0))
                    {
                        pixelCol = header.Width % header.BlockSize;
                    }

                    var tileData = br.ReadBytes(pixelRow * pixelCol);
                    blockDatas.Add(tileData);
                }
            }

            var bytes = new byte[header.Width * header.Height * 4];
            var byteIndex = 0;
            for (int row = 0; row < header.Rows; row++)
            {
                var pixelRow = header.BlockSize;
                // The last row may not be a full BlockSize
                if ((row == header.Rows - 1) && ((header.Height % header.BlockSize) != 0))
                {
                    pixelRow = header.Height % header.BlockSize;
                }

                for (int k = 0; k < pixelRow; k++)
                {
                    for (int column = 0; column < header.Columns; column++)
                    {
                        var blockIndex = (header.Columns * row) + column;
                        //// If we have 1 column, we need a special case
                        //if (row == 0 || columnCount == 1)
                        //{
                        //    blockIndex = row + 7;
                        //}

                        var pixelCol = header.BlockSize;
                        // The last column may not be a full BlockSize
                        if ((column == header.Columns - 1) && ((header.Width % header.BlockSize) != 0))
                        {
                            pixelCol = header.Width % header.BlockSize;
                        }

                        for (var m = 0; m < pixelCol; m++)
                        {
                            bytes[byteIndex] = palettes[blockIndex][blockDatas[blockIndex][(k * pixelCol) + m]].Blue;
                            bytes[byteIndex + 1] = palettes[blockIndex][blockDatas[blockIndex][(k * pixelCol) + m]].Green;
                            bytes[byteIndex + 2] = palettes[blockIndex][blockDatas[blockIndex][(k * pixelCol) + m]].Red;
                            bytes[byteIndex + 3] = 255;
                            byteIndex += 4;
                        }
                    }
                }
            }

            var img = new Bitmap(header.Width, header.Height, header.Width * 4, PixelFormat.Format32bppArgb, System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0));

            mosFile.Image = img;
            mosFile.Checksum = HashGenerator.GenerateKey(mosFile);
            return mosFile;
        }
    }
}