using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BenchmarkingApp {
    public partial class RunnerForm : XtraForm, IHostService, ILogService, IClipboardService {
        readonly string[] benchmarkArgs;
        readonly string resultsPath = "runner.results";
        public RunnerForm()
            : this(new string[] { }, string.Empty) {
        }
        public RunnerForm(string[] benchmarkArgs, string workload) {
            this.benchmarkArgs = benchmarkArgs;
            if(!string.IsNullOrEmpty(workload))
                resultsPath = Path.GetFileName(workload) + ".results";
            if(File.Exists(resultsPath))
                File.Delete(resultsPath);
            InitializeComponent();
        }
        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            if(!mvvmContext1.IsDesignMode) {
                mvvmContext1.RegisterService(this);
                //
                var fluent = mvvmContext1.OfType<MainViewModel>();
                fluent.WithEvent(this, "Shown")
                    .EventToCommand(x => x.LoadAndRunBatch(null), (EventArgs args) => benchmarkArgs);
                fluent.SetBinding(this, f => f.Text, x => x.Title);
            }
        }
        #region Custom Services
        void IHostService.Show(IBenchmarkHost host) {
            Controls.Clear();
            var hostControl = host as Control;
            hostControl.Dock = DockStyle.Fill;
            hostControl.Parent = this;
        }
        void ILogService.Log(string message) {
            message = message.Substring(0, message.IndexOf(']') + 1);
            using(var writer = File.AppendText(resultsPath))
                writer.Write(message.PadRight(40) + '\t'.ToString());
        }
        void IClipboardService.SetResult(string result) {
            using(var writer = File.AppendText(resultsPath))
                writer.WriteLine(result);
        }
        #endregion
    }
}