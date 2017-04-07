using System.Windows.Forms;

namespace BenchmarkingApp.Benchmarks.Hosts {
    [BenchmarkHost("Grid")]
    public partial class Grid : UserControl, IBenchmarkHost {
        public Grid() {
            InitializeComponent();
        }
        #region IBenchmarkHost Members
        object IBenchmarkHost.UIControl {
            get { return gridControl; }
        }
        #endregion
    }
}