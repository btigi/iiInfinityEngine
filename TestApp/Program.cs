using System;
using Forms = System.Windows.Forms;

namespace iiInfinityEngine.Application
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Forms.Application.EnableVisualStyles();
            Forms.Application.SetCompatibleTextRenderingDefault(false);
            Forms.Application.Run(new Form1());
        }
    }
}