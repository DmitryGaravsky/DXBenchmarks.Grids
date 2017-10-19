using System.Windows.Forms;

namespace BenchmarkingApp.Benchmarks.Hosts {
    [BenchmarkHost("RadVirtualGrid")]
    public partial class RadVirtualGrid : UserControl, IBenchmarkHost {
        public RadVirtualGrid() {
            InitializeComponent();
        }
        #region IBenchmarkHost Members
        object IBenchmarkHost.UIControl {
            get { return radVirtualGrid1; }
        }
        #endregion
    }
}