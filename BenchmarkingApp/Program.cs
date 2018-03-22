using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BenchmarkingApp {
    static class Program {
        [STAThread]
        static void Main(string[] args) {
            WindowsFormsSettings.SetDPIAware();
            // AFFINITY
            var currentProcess = Process.GetCurrentProcess();
            currentProcess.ProcessorAffinity = new System.IntPtr(1);
            //
            WindowsFormsSettings.EnableFormSkins();
            WindowsFormsSettings.DefaultLookAndFeel.SetSkinStyle("Office 2016 Colorful");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // check arguments
            if(args == null || args.Length == 0 || args.Length == 1 && string.IsNullOrWhiteSpace(args[0])) 
                Application.Run(new MainForm());
            else {
                string workload = string.Empty;
                if(args.Length == 1 && System.IO.File.Exists(args[0].Trim())) {
                    workload = args[0].Trim();
                    args = System.IO.File.ReadAllLines(workload);
                }
                args = args.Where(a => !string.IsNullOrWhiteSpace(a))
                    .Select(s => s.Trim().ToLowerInvariant()).ToArray();
                Application.Run(new RunnerForm(args, workload));
            }
        }
    }
}