using System;
using System.Windows.Forms;

namespace BenchmarkingApp {
    public partial class MainForm : Form, IHostService {
        public MainForm() {
            InitializeComponent();
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
                fluent.SetBinding(result, l => l.Text, x => x.Result);
            }
        }
        #region IHostService Members
        void IHostService.Show(IBenchmarkHost host) {
            var hostControl = host as Control;
            hostControl.Dock = DockStyle.Fill;
            hostControl.Parent = hostPanel;
            hostControl.BringToFront();
        }
        #endregion
    }
}