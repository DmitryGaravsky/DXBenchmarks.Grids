namespace BenchmarkingApp.Tree.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading (Bound)", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<Row> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            base.SetUp(uiControl);
        }
        public sealed override void Benchmark() {
            treeList.DataSource = dataSource;
        }
    }
}
namespace BenchmarkingApp.Tree.BoundHierarchy {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading (Bound Hierarchy)", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<HierarchicalRow> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureHierarchicalSource(ref dataSource, Configuration.Current.Rows, Configuration.Current.Levels);
            base.SetUp(uiControl);
        }
        public sealed override void Benchmark() {
            treeList.BeginUpdate();
            treeList.DataSource = dataSource;
            treeList.ExpandAll();
            treeList.EndUpdate();
        }
    }
}
namespace BenchmarkingApp.Tree.Unbound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading (Unbound)", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<Row> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            base.SetUp(uiControl);
            // Columns
            treeList.BeginUpdate();
            var columns = Row.GetColumns();
            for(int i = 0; i < columns.Length; i++)
                treeList.Columns.AddVisible(columns[i]);
            treeList.EndUpdate();
        }
        public sealed override void Benchmark() {
            treeList.BeginUnboundLoad();
            for(int i = 0; i < dataSource.Count; i++)
                treeList.Nodes.Add(dataSource[i].GetData());
            treeList.EndUnboundLoad();
        }
    }
}
namespace BenchmarkingApp.Tree.UnboundHierarchy {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading (Unbound Hierarchy)", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<HierarchicalRow> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureHierarchicalSource(ref dataSource, Configuration.Current.Rows, Configuration.Current.Levels);
            base.SetUp(uiControl);
            // Columns
            treeList.BeginUpdate();
            var columns = HierarchicalRow.GetHierarchicalColumns();
            for(int i = 0; i < columns.Length; i++)
                treeList.Columns.AddVisible(columns[i]);
            treeList.Columns["ParentID"].Visible = false;
            treeList.EndUpdate();
        }
        public sealed override void Benchmark() {
            treeList.BeginUnboundLoad();
            for(int i = 0; i < dataSource.Count; i++) {
                var data = dataSource[i].GetHierarchicalData();
                treeList.AppendNode(data, dataSource[i].ParentID);
            }
            treeList.ExpandAll();
            treeList.EndUnboundLoad();
        }
    }
}
namespace BenchmarkingApp.Grid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<Row> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            base.SetUp(uiControl);
        }
        public sealed override void Benchmark() {
            grid.DataSource = dataSource;
        }
    }
}
namespace BenchmarkingApp.Grid.Virtual {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using DevExpress.Data;

    [BenchmarkItem("LoadingVirtual", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<Row> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            base.SetUp(uiControl);
        }
        public override void TearDown(object uiControl) {
            grid.BeginUpdate();
            unboundSource.Count = 0;
            unboundSource.ValueNeeded -= unboundSource_ValueNeeded;
            grid.EndUpdate();
            base.TearDown(uiControl);
        }
        public sealed override void Benchmark() {
            grid.BeginUpdate();
            unboundSource.ValueNeeded += unboundSource_ValueNeeded;
            unboundSource.Count = dataSource.Count;
            grid.DataSource = unboundSource;
            grid.EndUpdate();
        }
        void unboundSource_ValueNeeded(object sender, UnboundSourceValueNeededEventArgs e) {
            e.Value = dataSource[e.RowIndex].GetData((int)e.Tag/* column index */);
        }
    }
}

namespace BenchmarkingApp.RadGrid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<Row> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            base.SetUp(uiControl);
        }
        public sealed override void Benchmark() {
            gridView.DataSource = dataSource;
        }
    }
}
namespace BenchmarkingApp.RadGrid.BoundHierarchy {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading (Bound Hierarchy)", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<HierarchicalRow> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureHierarchicalSource(ref dataSource, Configuration.Current.Rows, Configuration.Current.Levels);
            base.SetUp(uiControl);
        }
        public sealed override void Benchmark() {
            gridView.BeginUpdate();
            gridView.Relations.AddSelfReference(gridView.MasterTemplate, "ID", "ParentID");
            gridView.DataSource = dataSource;
            gridView.Columns["ParentId"].IsVisible = false;
            gridView.MasterTemplate.ExpandAll();
            gridView.EndUpdate();
        }
    }
}
namespace BenchmarkingApp.RadGrid.Virtual {
    using Telerik.WinControls.UI;

    [BenchmarkItem("Loading", Configuration = "Huge")]
    public class Loading : LoadBase {
        public override void TearDown(object uiControl) {
            gridView.BeginUpdate();
            gridView.ColumnCount = gridView.RowCount = 0;
            gridView.CellValueNeeded -= gridView_CellValueNeeded;
            gridView.EndUpdate();
            base.TearDown(uiControl);
        }
        public sealed override void Benchmark() {
            gridView.BeginUpdate();
            gridView.CellValueNeeded += gridView_CellValueNeeded;
            gridView.RowCount = dataSource.Count;
            gridView.ColumnCount = columnNames.Length;
            gridView.EndUpdate();
        }
        void gridView_CellValueNeeded(object sender, VirtualGridCellValueNeededEventArgs e) {
            if(e.ColumnIndex < 0)
                return;
            if(e.RowIndex == RadVirtualGrid.HeaderRowIndex)
                e.Value = columnNames[e.ColumnIndex];
            if(e.RowIndex >= 0 && e.RowIndex < dataSource.Count)
                e.Value = dataSource[e.RowIndex].GetData(e.ColumnIndex);
        }
    }
}

namespace BenchmarkingApp.UltraGrid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<Row> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            base.SetUp(uiControl);
        }
        public sealed override void Benchmark() {
            gridView.DataSource = dataSource;
        }
    }
}
namespace BenchmarkingApp.SfDataGrid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<Row> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            base.SetUp(uiControl);
        }
        public sealed override void Benchmark() {
            gridView.DataSource = dataSource;
        }
    }
}