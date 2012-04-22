using System;
using System.IO;
using iiInfinityEngine.Core;
using iiInfinityEngine.Core.Files;
using iiInfinityEngine.Core.Readers;
using iiInfinityEngine.Core.Writers;

namespace iiInfinityEngine.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Sample sample = new Sample();
            sample.CompileScript(@"D:\games\bg2\override\anomen.baf", @"D:\games\bg2\override\anomen.bcs");
            sample.DecompileScript(@"D:\games\bg2\override\anomen.bcs", @"D:\games\bg2\override\anomen.baf");
            sample.FileCopier();
            sample.FileCopierWithBackup();
            sample.ProcesItems();
            sample.ProcessItem();
            sample.ProcessItemWithStrings();
        }
    }


    public class Sample
    {
        Game game;
        string languageFile = @".\MyMod\Languages\English\Main.tra";

        
        public Sample()
        {
            Console.WriteLine("Loading game resources");
            game = new Game();
            game.backupManager = new BackupManager(@".\MyMod\Backup");
        }


        public void CompileScript(string inputScript, string outputScript)
        {
            BcsCompiler compiler = new BcsCompiler(game.Identifiers);
            var output = compiler.Compile(inputScript);
            File.WriteAllLines(outputScript, output.ToArray());
        }


        public void DecompileScript(string inputScript, string outputScript)
        {
            BcsDecompiler decom = new BcsDecompiler(game.Identifiers);
            var output = decom.Decompile(inputScript);
            File.WriteAllLines(outputScript, output.ToArray());
        }


        public void ProcesItems()
        {
            // Update every item in the game
            Console.WriteLine("Processing items");
            foreach (var item in game.Items)
            {
                Console.WriteLine(String.Format("Item {0}", item.Filename));
                // Set the price of every item to 500
                item.Price = 500;

                // Set the description to a translated string (translation files are in standard WeiDu .tra format)
                item.IdentifiedName.Text = Translator.Text(languageFile, "@1");

                // The filenames property is automatically loaded into the file, but contains no path element
                var location = item.Filename;
                var filename = Path.GetFileName(location);
                item.Filename = String.Format("D:\\games\\bg2\\modified_items\\{0}", filename);
                game.Save<ItmFile>(item);
            }
            game.Save<TlkFile>(game.Tlk);
        }

        public void ProcessItem()
        {
            // We can just load and edit a file directly (perhaps we don't want to read all files, or we don't have the game installed)
            var reader1 = new ItmFileBinaryReader();
            var itm1 = reader1.Read(@"D:\games\bg2\override\hamm09.ITM");
            itm1.Price = 200;

            var writer1 = new ItmFileBinaryWriter();
            writer1.BackupManger = new BackupManager(@"D:\games\bg2\backup");
            writer1.Write(@"D:\games\bg2\modified_items.itm", itm1);
        }


        public void ProcessItemWithStrings()
        {
            // If we want to load and edit a file directory, but amend the strings, we can pass the Tlk file in
            var tlkReader = new TlkFileBinaryReader();
            var tlkFile = tlkReader.Read(@"D:\games\bg2\dialog.tlk");

            var reader2 = new ItmFileBinaryReader();
            reader2.TlkFile = tlkFile;
            var itm2 = reader2.Read(@"D:\games\bg2\override\hamm09.ITM");
            itm2.IdentifiedName.Strref = Common.NewString;
            itm2.IdentifiedName.Text = "Really awesome hammer";

            var writer2 = new ItmFileBinaryWriter();
            writer2.TlkFile = tlkFile;
            writer2.Write(@"D:\games\bg2\modified_items.itm", itm2);

            TlkFileBinaryWriter tlkWriter = new TlkFileBinaryWriter();
            tlkWriter.Write(@"D:\games\bg2\dialog.tlk", tlkFile);
        }


        public void FileCopier()
        {
            // We can just dump files in the override
            IEFileCopier copier = new IEFileCopier();
            copier.CopyFile(@"D:\games\bg2\mymod\newitem.itm", @"D:\games\bg2\mymod\override\newitem.itm");
        }


        public void FileCopierWithBackup()
        {
            // We can dump files in the override - but with backup capability
            var bm = new BackupManager(@"D:\back");
            IEFileCopier c = new IEFileCopier();
            c.BackupManager = bm;
            c.CopyFile(@"D:\games\bg2\mymod\newitem.itm", @"D:\games\bg2\mymod\override\newitem.itm");
            // And we can then uninstall the changes
            bm.Uninstall();
        }
    }
}