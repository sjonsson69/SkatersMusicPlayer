using System;
using System.Threading;
using System.Windows.Forms;

namespace SkatersMusicPlayer
{
    static class Program
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);

                Logger.Info("Startar programmet");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                using (formMusicPlayer FMP = new formMusicPlayer())
                {
                    Application.Run(FMP);
                }
            }
            finally
            {
                Logger.Info("Stänger programmet");
                NLog.LogManager.Shutdown();
            }
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            Exception ex = t.Exception;
            //Bygg felmeddelande
            String msg = ex.Message + "\n" + ex.StackTrace;
            if (ex.InnerException != null)
            {
                msg = msg + "\n\n" + ex.InnerException.Message + "\n" + ex.InnerException.StackTrace;
            }
            MessageBox.Show(msg, "Fel!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Logger.Fatal(ex, "Övergripande error fångat");

            Application.Exit();
        }
    }
}
