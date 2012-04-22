using System;
using System.IO;
using System.Windows.Forms;
using iiInfinityEngine.Core;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Readers;
using iiInfinityEngine.Core.Writers;

namespace iiInfinityEngine.Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var languageFile = @"D:\MyMod\Languages\English\Main.tra";

                // Load all the resources in a game (excluding the override directory)
                Game game = new Game(@"D:\Games\ie\bg2\main");
                game.backupManager = new BackupManager(@"D:\Games\ie\bg2\main\mod_backup");
                game.LoadResources(IEFileType.Itm);

                Console.Write(String.Empty);

                //BcsDecompiler decom = new BcsDecompiler(game.Identifiers);
                //var output = decom.Decompile(@"D:\1output.baf");
                //var output = decom.Decompile(@"D:\output.bcs");
                //File.WriteAllLines(@"D:\output.baf", output.ToArray());

                //BcsCompiler compiler = new BcsCompiler(game.Identifiers);
                //var output = compiler.Compile(@"D:\Anomen.baf");
                //var output = compiler.Compile(@"D:\Games\ie\bg2\main\test.baf");
                //File.WriteAllLines(@"D:\output.baf", output.ToArray());
                
                // Update every item in the game
                foreach (var item in game.Items)
                {
                    // Set the price of every item to 500
                    item.Price = 500;

                    // Set the description to a translated string (translation files are in standard WeiDu .tra format)
                    item.IdentifiedName.Text = Translator.Text(languageFile, "@11");

                    // The filenames property is automatically loaded into the file, but contains no path element
                    var location = item.Filename;
                    var filename = Path.GetFileName(location);
                    item.Filename = String.Format("D:\\{0}", filename);
                    game.Save<ItmFile>(item);
                }
                game.Save<TlkFile>(game.Tlk);
                

                
                // Or, we can just load and edit a file directly (perhaps we don't want to read all files, or we don't have the game installed)
                var reader1 = new ItmFileBinaryReader();
                var itm1 = reader1.Read(@"D:\Games\ie\bg2\main\override\hamm09.ITM");
                itm1.Price = 200;

                var writer1 = new ItmFileBinaryWriter();
                writer1.BackupManger = new BackupManager(@"D:\back");
                writer1.Write(@"D:\out.itm", itm1);



                // If we want to load and edit a file directory, but amend the strings, we can pass the Tlk file in
                var tlkReader = new TlkFileBinaryReader();
                var tlkFile = tlkReader.Read(@"D:\Games\ie\bg2\main\dialog.tlk");

                var reader2 = new ItmFileBinaryReader();
                reader2.TlkFile = tlkFile;
                var itm2 = reader2.Read(@"D:\Games\ie\bg2\main\override\hamm09.ITM");
                itm1.Price = 200;
                itm2.IdentifiedName.Strref = Common.NewString;
                itm2.IdentifiedName.Text = "Really awesome hammer";

                var writer2 = new ItmFileBinaryWriter();
                writer2.TlkFile = tlkFile;
                writer2.Write(@"D:\out.itm", itm2);

                TlkFileBinaryWriter tlkWriter = new TlkFileBinaryWriter();
                tlkWriter.Write(@"D:\dialog.tlk", tlkFile);
                

                // Or we can just dump files in the override
                IEFileCopier copier = new IEFileCopier();
                copier.CopyFile(@"D:\test.bmp", @"E:\test.bmp");


                // Or we can dump files in the override - but with backup capability
                var bm = new BackupManager(@"D:\back");
                IEFileCopier c = new IEFileCopier();
                c.BackupManager = bm;
                c.CopyFile(@"D:\New Text Document.txt", @"E:\New Text Document.txt");
                // And we can then uninstall the changes
                bm.Uninstall();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}