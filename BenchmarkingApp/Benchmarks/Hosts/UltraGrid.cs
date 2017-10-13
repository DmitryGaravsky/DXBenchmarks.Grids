using System.Windows.Forms;

namespace BenchmarkingApp.Benchmarks.Hosts {
    [BenchmarkHost("UltraGrid")]
    public partial class UltraGrid : UserControl, IBenchmarkHost {
        public UltraGrid() {
            InitializeComponent();
        }
        #region IBenchmarkHost Members
        object IBenchmarkHost.UIControl {
            get { return ultraGrid1; }
        }
        #endregion
    }
}