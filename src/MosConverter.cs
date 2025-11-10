using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace ii.InfinityEngine
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

        public Bitmap FromMos(string filename)
        {
            using var s = new MemoryStream();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
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

                using var compressedStream = new MemoryStream(bytes);
                using var decompressedStream = new MemoryStream();
                var zlibStream = new ZLibStream(compressedStream, CompressionMode.Decompress);
                zlibStream.CopyTo(decompressedStream);
                var decompressedData = decompressedStream.ToArray();
                s.Write(decompressedData, 0, uncompressedDataLength);
            }

            s.Position = 0;
            using var uncompressedBr = new BinaryReader(s);
            return HandleMos(uncompressedBr);
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
                for (var row = 0; row < rowCount; row++)
                {
                    var pixelRow = blockSize;
                    // The last row may not be a full BlockSize
                    if ((row == rowCount - 1) && ((height % blockSize) != 0))
                    {
                        pixelRow = height % blockSize;
                    }

                    for (var k = 0; k < pixelRow; k++)
                    {
                        for (var column = 0; column < columnCount; column++)
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
                return img;
            }
            return new Bitmap(1, 1);
        }

        public void ToMos(Image image, string filename, int blockSize = 64)
        {
            // Convert Image to Bitmap with Format32bppArgb so we only need deal with one format
            Bitmap bitmap;
            var createdBitmap = false;

            if (image is Bitmap bmp && bmp.PixelFormat == PixelFormat.Format32bppArgb)
            {
                bitmap = bmp;
            }
            else
            {
                bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
                using var g = Graphics.FromImage(bitmap);
                g.DrawImage(image, 0, 0);
                createdBitmap = true;
            }

            try
            {
                var width = (short)bitmap.Width;
                var height = (short)bitmap.Height;
                var columnCount = (short)((width + blockSize - 1) / blockSize);
                var rowCount = (short)((height + blockSize - 1) / blockSize);

                // Initialize block arrays (one per block)
                var numBlocks = rowCount * columnCount;
                var blockPixelsList = new List<List<Color>>(numBlocks);
                var blockPalettes = new List<Dictionary<uint, int>>(numBlocks);
                var blockColorLists = new List<List<RGBA>>(numBlocks);
                var blockIndices = new List<List<byte>>(numBlocks);

                for (var i = 0; i < numBlocks; i++)
                {
                    blockPixelsList.Add(new List<Color>());
                    blockPalettes.Add(new Dictionary<uint, int>());
                    blockColorLists.Add(new List<RGBA>());
                    blockIndices.Add(new List<byte>());
                }

                var palettes = new List<RGBA[]>();
                var blockDatas = new List<byte[]>();

                // Lock so we can avoid SetPixel
                var rect = new Rectangle(0, 0, width, height);
                var bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                try
                {
                    var stride = bitmapData.Stride;
                    var size = stride * height;
                    var bitmapBytes = new byte[size];
                    Marshal.Copy(bitmapData.Scan0, bitmapBytes, 0, size);

                    for (var blockRow = 0; blockRow < rowCount; blockRow++)
                    {
                        var pixelRowsInBlock = blockSize;
                        if ((blockRow == rowCount - 1) && ((height % blockSize) != 0))
                        {
                            pixelRowsInBlock = height % blockSize;
                        }

                        // For each pixel row in this block row
                        for (var pixelRowInBlock = 0; pixelRowInBlock < pixelRowsInBlock; pixelRowInBlock++)
                        {
                            var imageY = blockRow * blockSize + pixelRowInBlock;

                            // Process entire width (all columns)
                            for (var imageX = 0; imageX < width; imageX++)
                            {
                                var blockCol = imageX / blockSize;
                                var pixelColInBlock = imageX % blockSize;
                                var blockIndex = blockCol + (blockRow * columnCount);

                                // Calculate pixel offset in bitmap data
                                var offset = (imageY * stride) + (imageX * 4);
                                var blue = bitmapBytes[offset];
                                var green = bitmapBytes[offset + 1];
                                var red = bitmapBytes[offset + 2];
                                var alpha = bitmapBytes[offset + 3];

                                var color = Color.FromArgb(alpha, red, green, blue);
                                var colorKey = (uint)((alpha << 24) | (red << 16) | (green << 8) | blue);

                                // Add to block's pixel list
                                blockPixelsList[blockIndex].Add(color);

                                // Build palette for this block
                                if (!blockPalettes[blockIndex].ContainsKey(colorKey))
                                {
                                    // Enforce palette restrictions
                                    if (blockColorLists[blockIndex].Count < 256)
                                    {
                                        var paletteIndex = blockColorLists[blockIndex].Count;
                                        blockPalettes[blockIndex][colorKey] = paletteIndex;
                                        blockColorLists[blockIndex].Add(new RGBA
                                        {
                                            Red = red,
                                            Green = green,
                                            Blue = blue,
                                            Alpha = alpha
                                        });
                                        blockIndices[blockIndex].Add((byte)paletteIndex);
                                    }
                                    else
                                    {
                                        // Find nearest color
                                        var nearestIndex = FindNearestColorIndex(color, blockColorLists[blockIndex]);
                                        blockPalettes[blockIndex][colorKey] = nearestIndex;
                                        blockIndices[blockIndex].Add((byte)nearestIndex);
                                    }
                                }
                                else
                                {
                                    blockIndices[blockIndex].Add((byte)blockPalettes[blockIndex][colorKey]);
                                }
                            }
                        }
                    }

                    for (var i = 0; i < numBlocks; i++)
                    {
                        // Pad palette if required
                        while (blockColorLists[i].Count < 256)
                        {
                            blockColorLists[i].Add(new RGBA { Red = 0, Green = 0, Blue = 0, Alpha = 0 });
                        }

                        palettes.Add(blockColorLists[i].ToArray());
                        blockDatas.Add(blockIndices[i].ToArray());
                    }
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }



                // Write MOS file
                using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
                using var bw = new BinaryWriter(fs);

                bw.Write("MOS ".ToCharArray());
                bw.Write("V1  ".ToCharArray());
                bw.Write(width);
                bw.Write(height);
                bw.Write(columnCount);
                bw.Write(rowCount);
                bw.Write(blockSize);

                // Calculate palette offset (right after header)
                // Header size: 4 (signature) + 4 (version) + 2 (width) + 2 (height) + 2 (columns) + 2 (rows) + 4 (blocksize) + 4 (paletteoffset) = 24
                var headerSize = 24;
                var paletteOffset = headerSize;
                bw.Write(paletteOffset);

                // Write palettes (at paletteOffset, right after header)
                foreach (var palette in palettes)
                {
                    foreach (var color in palette)
                    {
                        bw.Write(color.Blue);
                        bw.Write(color.Green);
                        bw.Write(color.Red);
                        bw.Write(color.Alpha);
                    }
                }

                // Calculate tile offsets as cumulative data counts
                // Offsets are relative to the start of the tile data section
                var tileOffsets = new List<int>();
                var cumulativeOffset = 0;

                foreach (var blockData in blockDatas)
                {
                    tileOffsets.Add(cumulativeOffset);
                    cumulativeOffset += blockData.Length;
                }

                // Tile offsets
                foreach (var offset in tileOffsets)
                {
                    bw.Write(offset);
                }

                // Tile data
                foreach (var blockData in blockDatas)
                {
                    bw.Write(blockData);
                }
            }
            finally
            {
                if (createdBitmap)
                    bitmap.Dispose();
            }
        }

        private int FindNearestColorIndex(Color color, List<RGBA> palette)
        {
            var bestIndex = 0;
            double bestDistance = double.MaxValue;

            for (var i = 0; i < palette.Count; i++)
            {
                var paletteColor = palette[i];
                var distance = ColorDistance(color, paletteColor);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestIndex = i;
                }
            }

            return bestIndex;
        }

        private double ColorDistance(Color c1, RGBA c2)
        {
            var dr = c1.R - c2.Red;
            var dg = c1.G - c2.Green;
            var db = c1.B - c2.Blue;
            var da = c1.A - c2.Alpha;
            return dr * dr + dg * dg + db * db + da * da;
        }
    }
}