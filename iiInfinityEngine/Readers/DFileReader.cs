using System.Collections.Generic;
using System.IO;
using System.Text;
using iiInfinityEngine.Core.Binary;
using iiInfinityEngine.Core.Files;
using System;
using System.Linq;

namespace iiInfinityEngine.Core.Readers
{
    public class DFileReader //: IDileReader
    {
        public TlkFile TlkFile { get; set; }

        public DlgFile Read(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var f = Read(fs);
                f.Filename = Path.GetFileName(filename);
                return f;
            }
        }

        public DlgFile Read(Stream s)
        {
            using (StreamReader br = new StreamReader(s))
            {
                var dlgFile = ParseFile(br);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                dlgFile.OriginalFile = ParseFile(br);
                return dlgFile;
            }
        }

        private DlgFile ParseFile(StreamReader rdr)
        {

            /*
            <dialog name="x">
              <state weight="1">
                <triggers>
                  True()
                </triggers>
                <transitions>
                  <text ="sometext">
                  <actions>
                    Continue()
                  </actions>
                </transitions>
              </state>
            </dialog>             
            */

            var dlgFile = new DlgFile();
            dlgFile.Checksum = MD5HashGenerator.GenerateKey(dlgFile);
            return dlgFile;
        }
    }
}