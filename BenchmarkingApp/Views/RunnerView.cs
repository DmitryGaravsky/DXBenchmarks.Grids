using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.Mvvm;
using DevExpress.XtraEditors;

namespace BenchmarkingApp {
    public partial class RunnerForm : XtraForm, IHostService, ILogService, IClipboardService, IMessageBoxService {
        readonly string[] benchmarkArgs;
        readonly string results = ".results";
        public RunnerForm()
            : this(new string[] { }, string.Empty) {
        }
        public RunnerForm(string[] benchmarkArgs, string workload) {
            this.benchmarkArgs = benchmarkArgs;
            if(!string.IsNullOrEmpty(workload))
                results = Path.GetFileName(workload.Replace(".workload", ".results"));
            if(File.Exists(results))
                File.Delete(results);
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
            using(var writer = File.AppendText(results))
                writer.Write(message.PadRight(64) + '\t'.ToString());
        }
        void IClipboardService.SetResult(string result) {
            using(var writer = File.AppendText(results))
                writer.WriteLine(result);
        }
        MessageResult IMessageBoxService.Show(string messageBoxText, string caption, MessageButton button, MessageIcon icon, MessageResult defaultResult) {
            var result = Views.Controls.AutoClosingMessageBox.Show(
                messageBoxText + Environment.NewLine + "Application is about to be closed automatically",
                caption,
                2500, MessageBoxButtons.OK, DialogResult.OK);
            if(result == DialogResult.OK)
                Application.Exit();
            return MessageResult.OK;
        }
        #endregion
    }
}