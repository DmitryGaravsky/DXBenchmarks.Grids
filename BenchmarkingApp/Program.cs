using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BenchmarkingApp {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            WindowsFormsSettings.EnableFormSkins();
            WindowsFormsSettings.DefaultLookAndFeel.SetSkinStyle("Office 2016 Colorful");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
