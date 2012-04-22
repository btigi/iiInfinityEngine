using System;

namespace iiInfinityEngine.Core
{
    /// <summary>
    /// DO NOT USE
    /// </summary>
    class MosConverter
    {/*
        struct RGBACol
        {
            public char Red;
            public char Green;
            public char Blue;
            public char Alpha; // Unused
        }
        */
        public void Convert()
        {/*
            var blockSize = 64;
            System.Drawing.Bitmap b = new System.Drawing.Bitmap(@"d:\test.bmp");
            var columns = Math.Truncate(Math.Ceiling(b.Size.Width / 64d));
            var rows = Math.Truncate(Math.Ceiling(b.Size.Height / 64d));
            var paletteCount = rows * columns;

            var TestColour = new RGBACol();
            int pixelRowsToRead;
            for (int currentRow = 0; currentRow < rows - 1; currentRow++)
            {
                // For the last row, we may not read Header.BlockSize -1 pixelrows, as  we
                // may not be a full row. We need to read Header.BlockSize or Header.Height mod 64
                if (currentRow == rows - 1)
                {
                    if (rows % blockSize == 0)
                    {
                        pixelRowsToRead = blockSize - 1;
                    }
                    else
                    {
                        pixelRowsToRead = (blockSize % 1) - 1;
                    }
                }
                else
                {
                    pixelRowsToRead = blockSize - 1;
                }

                // for each pixel row in this row
                for (int i = 0; i < pixelRowsToRead; i++)
                {

                    // read the entire row, 1 pixel at a time
                    // Note that each pixel is R,G,B hence the 3's everywhere
                    var j = 0;
                    while (j < (b.Size.Width * 3) - 1)
                    {
                        //TestColour.Red = b.GetPixel((3 * b.Size.Width * blockSize * currentRow) + (3 * b.Size.Width * i) + j);
                        //TestColour.Green = b.GetPixel((3 * b.Size.Width * blockSize * currentRow) + (3 * b.Size.Width * i) + j + 1);
                        //TestColour.Blue = b.GetPixel((3 * b.Size.Width * blockSize * currentRow) + (3 * b.Size.Width * i) + j + 2);

                        // colour, row, 'column'
                        //if (!Buildpalettes(TestColour, k, j))
                        //{
                        //return;
                        //}
                        j += 3;
                    }
                }
            }*/
        }

        /*
        function Buildpalettes(Colour : RGBACol; row : integer; colbase : integer) : boolean;
        var
          col          : integer;
          i            : integer;
          FoundColour  : boolean;
          paletteIndex : integer;
          index        : integer;
        begin
          Result := TRUE;
          index := -1;
          FoundColour := FALSE;
          Colour.Alpha := #0;
          col := (colbase div 3) div Header.BlockSize;
          paletteIndex := (col) + (row * Header.columns);

          // See if we already have this colour in this blocks palette
          for i := 0 to 255 do
          begin
            if (BlockArray[paletteIndex].palette[i].Red   = Colour.Red) and
               (BlockArray[paletteIndex].palette[i].Green = Colour.Green) and
               (BlockArray[paletteIndex].palette[i].Blue  = Colour.Blue) then
            begin
              FoundColour := TRUE;
              index := i;
              break;
            end;
          end;

          // If the first colour is black, we find it, but never add it.
          // This fixed the problem
          if (BlockArray[paletteIndex].palette[i].Red   = Colour.Red)   and
             (BlockArray[paletteIndex].palette[i].Green = Colour.Green) and
             (BlockArray[paletteIndex].palette[i].Blue  = Colour.Blue)  and
             not (AddedBlack) then
           begin
            Inc(BlockArray[(col) + (row * Header.columns)].paletteCount);
            AddedBlack := TRUE;
           end;

          // We don't have this colour in this blocks palette
          if not (FoundColour) then
          begin
            // Add it, if we have room
            if (BlockArray[paletteIndex].paletteCount < 256) then
            begin
              BlockArray[paletteIndex].palette[BlockArray[paletteIndex].paletteCount {+1}].Red := Colour.Red;
              BlockArray[paletteIndex].palette[BlockArray[paletteIndex].paletteCount {+1}].Green := Colour.Green;
              BlockArray[paletteIndex].palette[BlockArray[paletteIndex].paletteCount {+1}].Blue := Colour.Blue;
              BlockArray[paletteIndex].palette[BlockArray[paletteIndex].paletteCount {+1}].Alpha := Colour.Alpha;
              index := BlockArray[(col) + (row * Header.columns)].paletteCount;
              Inc(BlockArray[(col) + (row * Header.columns)].paletteCount);
            end
            else
            begin
              Result := FALSE;
              if (ErrorShown = 0) then
              begin
                MessageDlg('ERROR: Too many unique colours in block (row ' + IntToStr(row) + ', column ' + IntToStr(col) + ')', mtError, [mbOK], 0);
                ErrorShown := 1;
              end;
            end;
          end;

          // Now, record the index into the palette, for the image data
          if (index <> -1) then
          begin
            BlockArray[paletteIndex].data[BlockArray[paletteIndex].dataCount] := index;
            Inc(BlockArray[paletteIndex].dataCount);
          end
          else
          begin
            if (ErrorShown <> 2) then
            begin
              Result := FALSE;
              MessageDlg('ERROR: Colour could not be found in constructed palette.', mtError, [mbOK], 0);
              ErrorShown := 2;
            end;
          end;
        end;*/
    }
}