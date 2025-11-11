using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace ii.InfinityEngine
{
    public class TisConverter
    {
        public Bitmap FromTis(string filename, int width, int height)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            var signature = string.Join("", br.ReadChars(4));
            var version = string.Join("", br.ReadChars(4));
            var tileCount = br.ReadInt32();
            var dataBlockLength = br.ReadInt32();
            var tileOffset = br.ReadInt32();
            var tileSize = br.ReadInt32();

            var blockArray = new List<(byte[] palette, byte[] data)>();

            if (version == "V1  " && signature == "TIS " && dataBlockLength == 0x1400)
            {
                // read dataBlockLength bytes to read in one tile
                for (int i = 0; i < tileCount; i++)
                {
                    // Read palette
                    br.BaseStream.Seek(tileOffset + ((i * 256) * 4) + (i * (tileSize * tileSize)), SeekOrigin.Begin);
                    var palette = br.ReadBytes(256 * 4);

                    // Read image data
                    var data = br.ReadBytes(tileSize * tileSize);

                    blockArray.Add((palette, data));
                }

                var columns = width / tileSize;
                var rows = height / tileSize;
                var bytes = new byte[tileSize * tileSize * tileCount * 4];
                // Now each Block has it's palette and data. We need to get that into DST[]
                var pixelrow = 0;
                var DSTIndex = 0;
                for (int i = 0; i < rows; i++)
                {
                    pixelrow = tileSize;

                    for (var k = 0; k < pixelrow; k++)
                    {

                        for (var j = 0; j < columns; j++)
                        {

                            var blockIndex = (columns * i) + j;
                            var pixelcol = tileSize;

                            for (var m = 0; m < pixelcol; m++)
                            {
                                bytes[DSTIndex + 0] = blockArray[blockIndex].palette[blockArray[blockIndex].data[(k * pixelcol) + m] * 4 + 0]; // Blue
                                bytes[DSTIndex + 1] = blockArray[blockIndex].palette[blockArray[blockIndex].data[(k * pixelcol) + m] * 4 + 1]; // Green;
                                bytes[DSTIndex + 2] = blockArray[blockIndex].palette[blockArray[blockIndex].data[(k * pixelcol) + m] * 4 + 2]; // Red
                                bytes[DSTIndex + 3] = 255;
                                DSTIndex += 4;
                            }
                        }
                    }
                }

                var img = new Bitmap(width, height, width * 4, PixelFormat.Format32bppArgb, System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0));

                return img;
            }
            return new Bitmap(8, 8);
        }

        public void ToTis(Image image, string filename, int tileSize = 64)
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
                var width = bitmap.Width;
                var height = bitmap.Height;

                // Validate dimensions are multiples of tileSize
                if (width % tileSize != 0)
                {
                    throw new ArgumentException($"Width ({width}) must be a multiple of {tileSize}");
                }

                if (height % tileSize != 0)
                {
                    throw new ArgumentException($"Height ({height}) must be a multiple of {tileSize}");
                }

                var columns = width / tileSize;
                var rows = height / tileSize;
                var tileCount = columns * rows;

                // Initialize block arrays (one per tile)
                var blockPalettes = new List<Dictionary<uint, int>>(tileCount);
                var blockColourLists = new List<List<(byte r, byte g, byte b, byte a)>>(tileCount);
                var blockIndices = new List<List<byte>>(tileCount);

                for (var i = 0; i < tileCount; i++)
                {
                    blockPalettes.Add(new Dictionary<uint, int>());
                    blockColourLists.Add(new List<(byte r, byte g, byte b, byte a)>());
                    blockIndices.Add(new List<byte>());
                }

                // Lock bitmap to avoid SetPixel
                var rect = new Rectangle(0, 0, width, height);
                var bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                try
                {
                    var stride = bitmapData.Stride;
                    var bitmapBytes = new byte[stride * height];
                    Marshal.Copy(bitmapData.Scan0, bitmapBytes, 0, bitmapBytes.Length);

                    // Process each tile
                    for (var tileRow = 0; tileRow < rows; tileRow++)
                    {
                        for (var tileCol = 0; tileCol < columns; tileCol++)
                        {
                            var blockIndex = (columns * tileRow) + tileCol;

                            // Process each pixel in this tile
                            for (var pixelRow = 0; pixelRow < tileSize; pixelRow++)
                            {
                                var imageY = tileRow * tileSize + pixelRow;
                                for (var pixelCol = 0; pixelCol < tileSize; pixelCol++)
                                {
                                    var imageX = tileCol * tileSize + pixelCol;

                                    // Calculate pixel offset in bitmap data
                                    var offset = (imageY * stride) + (imageX * 4);
                                    var blue = bitmapBytes[offset];
                                    var green = bitmapBytes[offset + 1];
                                    var red = bitmapBytes[offset + 2];
                                    var alpha = bitmapBytes[offset + 3];

                                    // Create colour key (ignoring alpha for palette matching, as per Delphi code)
                                    var colourKey = (uint)((red << 16) | (green << 8) | blue);

                                    // Find or add colour to palette
                                    if (!blockPalettes[blockIndex].ContainsKey(colourKey))
                                    {
                                        // Add, if we have room
                                        if (blockColourLists[blockIndex].Count < 256)
                                        {
                                            var paletteIndex = blockColourLists[blockIndex].Count;
                                            blockPalettes[blockIndex][colourKey] = paletteIndex;
                                            blockColourLists[blockIndex].Add((red, green, blue, alpha));
                                            blockIndices[blockIndex].Add((byte)paletteIndex);
                                        }
                                        else
                                        {
                                            // Find closest colour if palette is full
                                            var nearestIndex = FindNearestColourIndex(red, green, blue, blockColourLists[blockIndex]);
                                            blockPalettes[blockIndex][colourKey] = nearestIndex;
                                            blockIndices[blockIndex].Add((byte)nearestIndex);
                                        }
                                    }
                                    else
                                    {
                                        blockIndices[blockIndex].Add((byte)blockPalettes[blockIndex][colourKey]);
                                    }
                                }
                            }

                            // Pad palette out to 256 colours
                            while (blockColourLists[blockIndex].Count < 256)
                            {
                                blockColourLists[blockIndex].Add((0, 0, 0, 0));
                            }
                        }
                    }
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }

                using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
                using var bw = new BinaryWriter(fs);

                bw.Write("TIS ".ToCharArray());
                bw.Write("V1  ".ToCharArray());
                bw.Write(tileCount);
                bw.Write(0x1400); // TileLength (dataBlockLength) - 5120 bytes per tile
                bw.Write(24); // TileOffset - header size
                bw.Write(tileSize);

                // Write each tile's palette and data
                for (var i = 0; i < tileCount; i++)
                {
                    for (var j = 0; j < 256; j++)
                    {
                        var colour = blockColourLists[i][j];
                        bw.Write(colour.b);
                        bw.Write(colour.g);
                        bw.Write(colour.r);
                        bw.Write(colour.a);
                    }

                    var tileData = blockIndices[i].ToArray();
                    bw.Write(tileData);
                }
            }
            finally
            {
                if (createdBitmap)
                    bitmap.Dispose();
            }
        }

        private int FindNearestColourIndex(byte r, byte g, byte b, List<(byte r, byte g, byte b, byte a)> palette)
        {
            var bestIndex = 0;
            var bestDistance = double.MaxValue;

            for (var i = 0; i < palette.Count; i++)
            {
                var paletteColour = palette[i];
                var dr = r - paletteColour.r;
                var dg = g - paletteColour.g;
                var db = b - paletteColour.b;
                var distance = dr * dr + dg * dg + db * db;

                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestIndex = i;
                }
            }

            return bestIndex;
        }
    }
}