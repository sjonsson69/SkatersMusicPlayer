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
            using (formMusicPlayer FMP = new formMusicPlayer())
            {
                Application.Run(FMP);
            }
        }
    }
}
