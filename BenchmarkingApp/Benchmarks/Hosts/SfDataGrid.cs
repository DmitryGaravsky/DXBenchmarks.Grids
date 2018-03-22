using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenchmarkingApp.Benchmarks.Hosts {
    [BenchmarkHost("SfDataGrid")]
    public partial class SfDataGrid : UserControl, IBenchmarkHost {
        public SfDataGrid() {
            InitializeComponent();
        }
        #region IBenchmarkHost Members
        object IBenchmarkHost.UIControl {
            get { return sfDataGrid1; }
        }
        #endregion
    }
}
