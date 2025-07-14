using System;
using System.Threading;
using System.Windows.Forms;

namespace Proje_Hastane
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ApplicationExit += new EventHandler(OnApplicationExit);

            using (SplashScreen splash = new SplashScreen())
            {
                splash.Show();
                Application.DoEvents();
                Thread.Sleep(3000);
            }

            using (FrmLogins loginForm = new FrmLogins())
            {
                Application.Run(loginForm);
            }

            Environment.Exit(0);
        }

        static void OnApplicationExit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
