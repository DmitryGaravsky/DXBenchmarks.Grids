using System.Windows.Forms;

namespace BenchmarkingApp.Benchmarks.Hosts {
    [BenchmarkHost("RadGrid")]
    public partial class RadGrid : UserControl, IBenchmarkHost {
        public RadGrid() {
            InitializeComponent();
        }
        #region IBenchmarkHost Members
        object IBenchmarkHost.UIControl {
            get { return radGridView1; }
        }
        #endregion
    }
}