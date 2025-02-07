using System.IO.Compression;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace iiInfinityEngine.Core
{
    public class MosConverter
    {
        public class RGBA
        {
            public byte Red { get; set; }
            public byte Green { get; set; }
            public byte Blue { get; set; }
            public byte Alpha { get; set; }
        }

        public Bitmap Convert(string filename)
        {
            using (var s = new MemoryStream())
            {
                using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                using (var br = new BinaryReader(fs))
                {
                    var signature = string.Join("", br.ReadChars(4));
                    var version = string.Join("", br.ReadChars(4));

                    if (version == "V1  " && signature == "MOS ")
                    {
                        fs.Position = 0;
                        fs.CopyTo(s);
                    }

                    if (version == "V1  " && signature == "MOSC")
                    {
                        var fi = new FileInfo(filename);

                        var uncompressedDataLength = br.ReadInt32();
                        var bytes = br.ReadBytes((int)fi.Length - 12);

                        using (MemoryStream compressedStream = new MemoryStream(bytes))
                        using (MemoryStream decompressedStream = new MemoryStream())
                        {
                            ZLibStream zlibStream = new ZLibStream(compressedStream, CompressionMode.Decompress);
                            zlibStream.CopyTo(decompressedStream);
                            var decompressedData = decompressedStream.ToArray();
                            s.Write(decompressedData, 0, uncompressedDataLength);
                        }
                    }
                }

                s.Position = 0;
                using (var br = new BinaryReader(s))
                {
                    return HandleMos(br);
                }
            }
        }

        private Bitmap HandleMos(BinaryReader br)
        {
            var signature = string.Join("", br.ReadChars(4));
            var version = string.Join("", br.ReadChars(4));

            if (version == "V1  " && signature == "MOS ")
            {
                var palettes = new List<RGBA[]>();
                var tileDataOffsets = new List<int>();
                var blockDatas = new List<byte[]>();

                // header
                var width = br.ReadInt16();
                var height = br.ReadInt16();
                var columnCount = br.ReadInt16();
                var rowCount = br.ReadInt16();
                var blockSize = br.ReadInt32();
                var paletteOffset = br.ReadInt32();

                // palettes
                br.BaseStream.Seek(paletteOffset, SeekOrigin.Begin);
                for (var i = 0; i < rowCount * columnCount; i++)
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
                for (var i = 0; i < rowCount * columnCount; i++)
                {
                    tileDataOffsets.Add(br.ReadInt32());
                }

                // tile data
                for (var row = 0; row < rowCount; row++)
                {
                    var pixelRow = blockSize;
                    // The last row may not be a full BlockSize
                    if ((row == rowCount - 1) && ((height % blockSize) != 0))
                    {
                        pixelRow = height % blockSize;
                    }

                    for (var column = 0; column < columnCount; column++)
                    {
                        // The last column may not be a full BlockSize
                        var pixelCol = blockSize;
                        if ((column == columnCount - 1) && ((width % blockSize) != 0))
                        {
                            pixelCol = width % blockSize;
                        }

                        var tileData = br.ReadBytes(pixelRow * pixelCol);
                        blockDatas.Add(tileData);
                    }
                }

                var bytes = new byte[width * height * 4];
                var byteIndex = 0;
                for (int row = 0; row < rowCount; row++)
                {
                    var pixelRow = blockSize;
                    // The last row may not be a full BlockSize
                    if ((row == rowCount - 1) && ((height % blockSize) != 0))
                    {
                        pixelRow = height % blockSize;
                    }

                    for (int k = 0; k < pixelRow; k++)
                    {
                        for (int column = 0; column < columnCount; column++)
                        {
                            var blockIndex = (columnCount * row) + column;
                            //// If we have 1 column, we need a special case
                            //if (row == 0 || columnCount == 1)
                            //{
                            //    blockIndex = row + 7;
                            //}

                            var pixelCol = blockSize;
                            // The last column may not be a full BlockSize
                            if ((column == columnCount - 1) && ((width % blockSize) != 0))
                            {
                                pixelCol = width % blockSize;
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

                var img = new Bitmap(width, height, width * 4, PixelFormat.Format32bppArgb, System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0));

                //img.Save(@"D:\aa.bmp", ImageFormat.Bmp);
                return img;
            }
            return new Bitmap(1, 1);
        }
    }
}