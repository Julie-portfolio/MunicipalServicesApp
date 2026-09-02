using System;
using System.Windows.Forms;
using MunicipalServicesApp.Forms;

namespace MunicipalServicesApp
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
            // Load persisted theme choice (if any)
            try { ThemeManager.LoadTheme(); } catch { }
            Application.Run(new MainForm());
        }
    }
}
