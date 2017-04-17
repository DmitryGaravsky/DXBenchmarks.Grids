using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BenchmarkingApp {
    public partial class RunnerForm : XtraForm, IHostService, ILogService, IClipboardService {
        readonly string[] benchmarkArgs;
        const string logPath = "runner.log";
        public RunnerForm()
            : this(new string[] { }) {
        }
        public RunnerForm(string[] benchmarkArgs) {
            this.benchmarkArgs = benchmarkArgs;
            InitializeComponent();
            if(System.IO.File.Exists(logPath))
                System.IO.File.Delete(logPath);
        }
        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            if(!mvvmContext1.IsDesignMode) {
                mvvmContext1.RegisterService(this);
                //
                var fluent = mvvmContext1.OfType<MainViewModel>();
                fluent.WithEvent(this, "Shown")
                    .EventToCommand(x => x.LoadAndRunBatch(null), (EventArgs args) => benchmarkArgs);
                //
                fluent.SetBinding(this, f => f.Text, x => x.Title);
            }
        }
        protected override void OnHandleDestroyed(EventArgs e) {
            Cleanup();
            base.OnHandleDestroyed(e);
        }
        #region Custom Services
        void IHostService.Show(IBenchmarkHost host) {
            Cleanup();
            var hostControl = host as Control;
            hostControl.Dock = DockStyle.Fill;
            hostControl.Parent = this;
        }
        void ILogService.Log(string message) {
            using(var writer = System.IO.File.AppendText(logPath))
                writer.Write(message.Substring(0, message.IndexOf(']') + 1) + '\t'.ToString());
        }
        void IClipboardService.SetResult(string result) {
            using(var writer = System.IO.File.AppendText(logPath))
                writer.WriteLine(result);
        }
        void Cleanup() {
            while(Controls.Count > 0)
                Controls[0].Dispose();
        }
        #endregion
    }
}