namespace BenchmarkingApp.Tree {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using DevExpress.XtraTreeList;

    [BenchmarkHost("TreeList")]
    public abstract class FilterBase : IBenchmarkItem {
        protected TreeList treeList;
        public virtual void SetUp(object uiControl) {
            treeList = ((TreeList)uiControl);
            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsFilter.FilterMode = FilterMode.Extended;
            treeList.DataSource = null;
            treeList.Columns.Clear();
            treeList.Nodes.Clear();
            treeList.OptionsBehavior.PopulateServiceColumns = true;
        }
        public virtual void TearDown(object uiControl) {
            treeList.ClearColumnsFilter();
            treeList = null;
        }
        public abstract void Benchmark();
    }
    namespace Bound {
        public abstract class FilterBoundBase : FilterBase {
            protected List<Row> dataSource;
            public sealed override void SetUp(object uiControl) {
                Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
                base.SetUp(uiControl);
                treeList.DataSource = dataSource;
                treeList.ClearColumnsFilter();
            }
        }
    }
    namespace BoundHierarchy {
        public abstract class FilterBoundBase : FilterBase {
            protected List<HierarchicalRow> dataSource;
            public sealed override void SetUp(object uiControl) {
                Row.EnsureHierarchicalSource(ref dataSource, Configuration.Current.Rows, Configuration.Current.Levels);
                base.SetUp(uiControl);
                treeList.OptionsBehavior.ExpandNodesOnFiltering = true;
                treeList.DataSource = dataSource;
                treeList.ExpandAll();
                treeList.ClearColumnsFilter();
            }
        }
    }
    namespace Unbound {
        [BenchmarkHost("TreeList")]
        public abstract class FilterUnboundBase : FilterBase {
            protected List<Row> dataSource;
            public sealed override void SetUp(object uiControl) {
                Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
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
                treeList.ClearColumnsFilter();
            }
        }
    }
    namespace UnboundHierarchy {
        [BenchmarkHost("TreeList")]
        public abstract class FilterUnboundBase : FilterBase {
            protected List<HierarchicalRow> dataSource;
            public sealed override void SetUp(object uiControl) {
                Row.EnsureHierarchicalSource(ref dataSource, Configuration.Current.Rows, Configuration.Current.Levels);
                base.SetUp(uiControl);
                // Columns
                treeList.BeginUpdate();
                treeList.OptionsBehavior.ExpandNodesOnFiltering = true;
                var columns = HierarchicalRow.GetHierarchicalColumns();
                for(int i = 0; i < columns.Length; i++)
                    treeList.Columns.AddVisible(columns[i]);
                treeList.EndUpdate();
                // Data
                treeList.BeginUnboundLoad();
                for(int i = 0; i < dataSource.Count; i++) {
                    var data = dataSource[i].GetHierarchicalData();
                    treeList.AppendNode(data, dataSource[i].ParentID);
                }
                treeList.ExpandAll();
                treeList.EndUnboundLoad();
                treeList.ClearColumnsFilter();
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
    public abstract class FilterBase : IBenchmarkItem {
        List<Row> dataSource;
        protected GridControl grid;
        protected GridView gridView;
        public void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            grid = ((GridControl)uiControl);
            gridView = grid.MainView as GridView;
            grid.DataSource = dataSource;
            gridView.ClearColumnsFilter();
        }
        public void TearDown(object uiControl) {
            gridView.ClearColumnsFilter();
            gridView = null;
            grid = null;
        }
        public abstract void Benchmark();
    }
}