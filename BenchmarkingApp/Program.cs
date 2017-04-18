using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BenchmarkingApp {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            WindowsFormsSettings.EnableFormSkins();
            WindowsFormsSettings.DefaultLookAndFeel.SetSkinStyle("Office 2016 Colorful");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // check arguments
            if(args == null || args.Length == 0) {
                Application.Run(new MainForm());
            }
            else {
                string workload = string.Empty;
                if(args.Length == 1 && System.IO.File.Exists(args[0])) {
                    workload = args[0];
                    args = System.IO.File.ReadAllLines(workload);
                }
                args = args.Where(a => !string.IsNullOrWhiteSpace(a))
                    .Select(s => s.Trim().ToLowerInvariant()).ToArray();
                Application.Run(new RunnerForm(args, workload));
            }
        }
    }
}