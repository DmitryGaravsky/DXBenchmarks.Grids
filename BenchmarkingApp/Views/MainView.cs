using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BenchmarkingApp {
    public partial class MainForm : XtraForm, IHostService, ILogService, IClipboardService {
        public MainForm() {
            InitializeComponent();
            memoLog.Visible = false;
        }
        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            if(!mvvmContext1.IsDesignMode) {
                mvvmContext1.RegisterService(this);
                //
                var fluent = mvvmContext1.OfType<MainViewModel>();
                fluent.SetBinding(hostItemBindingSource, bs => bs.DataSource, x => x.HostItems);
                fluent.SetBinding(leHosts, le => le.EditValue, x => x.ActiveHostItem);
                fluent.SetBinding(benchmarkItemBindingSource, bs => bs.DataSource, x => x.BenchmarkItems);
                fluent.SetBinding(leBenchmarks, le => le.EditValue, x => x.ActiveBenchmarkItem);
                //
                fluent.BindCommand(btnCold, x => x.ColdRun());
                fluent.BindCommand(btnWarmup, x => x.WarmUp());
                fluent.BindCommand(btnRun, x => x.Run());
                //
                fluent.WithEvent(this, "Load")
                    .EventToCommand(x => x.Load());
                fluent.WithKey(this, Keys.Control | Keys.C)
                    .KeyToCommand(x => x.CopyToClipboard());
                fluent.WithKey(this, Keys.Control | Keys.T)
                    .KeyToCommand(x => x.Test());
                //
                fluent.SetBinding(result, l => l.Text, x => x.Result);
                fluent.SetBinding(this, f => f.Text, x => x.Title);
            }
        }
        void showLog_CheckedChanged(object sender, EventArgs e) {
            memoLog.Visible = showLog.Checked;
            result.Visible = !showLog.Checked;
            Padding = new Padding(8, 40, 8, showLog.Checked ? 120 : 8);
        }
        #region Custom Services
        void IHostService.Show(IBenchmarkHost host) {
            var hostControl = host as Control;
            hostControl.Dock = DockStyle.Fill;
            hostControl.Parent = hostPanel;
            hostControl.BringToFront();
        }
        void ILogService.Log(string message) {
            memoLog.Text += (message + Environment.NewLine);
        }
        void IClipboardService.SetResult(string result) {
            try {
                Clipboard.SetText(result, TextDataFormat.Text);
            }
            catch { }
        }
        #endregion
    }
}