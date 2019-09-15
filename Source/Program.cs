using System;
using System.Windows.Forms;

namespace SkatersMusicPlayer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (FormMusicPlayer FMP = new FormMusicPlayer())
            {
                Application.Run(FMP);
            }
        }
    }
}
