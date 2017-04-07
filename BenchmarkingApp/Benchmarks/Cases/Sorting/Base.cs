namespace BenchmarkingApp.Tree {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using DevExpress.XtraTreeList;

    [BenchmarkHost("TreeList")]
    public abstract class SortBase : IBenchmarkItem {
        protected List<Row> dataSource;
        protected TreeList treeList;
        public virtual void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, 10000);
            treeList = ((TreeList)uiControl);
            treeList.DataSource = null;
            treeList.Columns.Clear();
            treeList.Nodes.Clear();
            treeList.OptionsBehavior.PopulateServiceColumns = true;
        }
        public virtual void TearDown(object uiControl) {
            treeList.ClearSorting();
            treeList = null;
        }
        public abstract void Benchmark();
    }
    namespace Bound {
        public abstract class SortBoundBase : SortBase {
            public override void SetUp(object uiControl) {
                base.SetUp(uiControl);
                treeList.DataSource = dataSource;
                treeList.ClearSorting();
            }
        }
    }
    namespace Unbound {
        [BenchmarkHost("TreeList")]
        public abstract class SortUnboundBase : SortBase {
            public override void SetUp(object uiControl) {
                base.SetUp(uiControl);
                // Columns
                treeList.BeginUpdate();
                var columns = Row.GetColumns();
                for(int i = 0; i < columns.Length; i++)
                    treeList.Columns.AddVisible(columns[i]);
                treeList.EndUpdate();
                // Data
                treeList.BeginUnboundLoad();
                for(int i = 0; i < dataSource.Count; i++)
                    treeList.Nodes.Add(dataSource[i].GetData());
                treeList.EndUnboundLoad();
                treeList.ClearSorting();
            }
        }
    }
}

namespace BenchmarkingApp.Grid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using DevExpress.XtraGrid;
    using DevExpress.XtraGrid.Views.Grid;

    [BenchmarkHost("Grid")]
    public abstract class SortBase : IBenchmarkItem {
        List<Row> dataSource;
        protected GridControl grid;
        protected GridView gridView;
        public void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, 10000);
            grid = ((GridControl)uiControl);
            gridView = grid.MainView as GridView;
            grid.DataSource = dataSource;
            gridView.ClearSorting();
        }
        public void TearDown(object uiControl) {
            gridView.ClearSorting();
            gridView = null;
            grid = null;
        }
        public abstract void Benchmark();
    }
}