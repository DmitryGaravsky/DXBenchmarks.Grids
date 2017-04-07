using System.Windows.Forms;

namespace BenchmarkingApp.Benchmarks.Hosts {
    [BenchmarkHost("InMemory")]
    public partial class Empty : UserControl, IBenchmarkHost {
        public Empty() {
            InitializeComponent();
        }
        #region IBenchmarkHost Members
        object IBenchmarkHost.UIControl {
            get { return this; }
        }
        #endregion
    }
}