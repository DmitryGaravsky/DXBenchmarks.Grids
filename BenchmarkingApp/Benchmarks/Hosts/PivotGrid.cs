using System.Windows.Forms;

namespace BenchmarkingApp.Benchmarks.Hosts {
    [BenchmarkHost("Pivot")]
    public partial class PivotGrid : UserControl, IBenchmarkHost {
        public PivotGrid() {
            InitializeComponent();
        }
        #region IBenchmarkHost Members
        object IBenchmarkHost.UIControl {
            get { return pivotGridControl1; }
        }
        #endregion
    }
}