using System.Windows.Forms;

namespace BenchmarkingApp.Benchmarks.Hosts {
    [BenchmarkHost("TreeList")]
    public partial class TreeListHost : UserControl, IBenchmarkHost {
        public TreeListHost() {
            InitializeComponent();
        }
        #region IBenchmarkHost Members
        object IBenchmarkHost.UIControl {
            get { return treeList; }
        }
        #endregion
    }
}