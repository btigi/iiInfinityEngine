using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ii.InfinityEngine
{
    public class TisConverter
    {
        public Bitmap Convert(string filename, int width, int height)
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
    }
}