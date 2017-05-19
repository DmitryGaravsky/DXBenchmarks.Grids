namespace BenchmarkingApp.Tree {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using DevExpress.XtraTreeList;
    using DevExpress.XtraTreeList.Nodes;

    static class Benchmarks {
        internal static void UpdateSID(TreeList treeList) {
            var colSID = treeList.Columns["SID"];
            treeList.BeginUpdate();
            treeList.NodesIterator.Do(node =>
            {
                string sid = (string)node.GetValue(colSID);
                node.SetValue(colSID, "#" + sid);
            });
            treeList.EndUpdate();
            treeList.BeginUpdate();
            treeList.NodesIterator.Do(node =>
            {
                string sid = (string)node.GetValue(colSID);
                node.SetValue(colSID, sid.Substring(1));
            });
            treeList.EndUpdate();
        }
        internal static void UpdateSize(TreeList treeList) {
            var colSize = treeList.Columns["Size"];
            treeList.BeginUpdate();
            treeList.NodesIterator.Do(node =>
            {
                long size = (long)node.GetValue(colSize);
                node.SetValue(colSize, size + size);
            });
            treeList.EndUpdate();
            treeList.BeginUpdate();
            treeList.NodesIterator.Do(node =>
            {
                long size = (long)node.GetValue(colSize);
                node.SetValue(colSize, size % 2);
            });
            treeList.EndUpdate();
        }
    }
    [BenchmarkHost("TreeList")]
    public abstract class MassUpdateBase : IBenchmarkItem {
        protected TreeList treeList;
        public virtual void SetUp(object uiControl) {
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
        public abstract class MassUpdateBoundBase : MassUpdateBase {
            protected List<Row> dataSource;
            public sealed override void SetUp(object uiControl) {
                Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
                base.SetUp(uiControl);
                treeList.DataSource = dataSource;
            }
        }
    }
    namespace BoundHierarchy {
        public abstract class MassUpdateBoundBase : MassUpdateBase {
            protected List<HierarchicalRow> dataSource;
            public sealed override void SetUp(object uiControl) {
                Row.EnsureHierarchicalSource(ref dataSource, Configuration.Current.Rows, Configuration.Current.Levels);
                base.SetUp(uiControl);
                treeList.DataSource = dataSource;
                treeList.ExpandAll();
            }
        }
    }
    namespace Unbound {
        public abstract class MassUpdateUnboundBase : MassUpdateBase {
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
            }
        }
    }
    namespace UnboundHierarchy {
        public abstract class MassUpdateUnboundBase : MassUpdateBase {
            protected List<HierarchicalRow> dataSource;
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
                // Data
                treeList.BeginUnboundLoad();
                IDictionary<int, TreeListNode> nodes = new Dictionary<int, TreeListNode>();
                for(int i = 0; i < dataSource.Count; i++) {
                    var data = dataSource[i].GetHierarchicalData();
                    int parentID = dataSource[i].ParentID;
                    TreeListNode parentNode, node;
                    if(!nodes.TryGetValue(parentID, out parentNode))
                        node = treeList.AppendNode(data, dataSource[i].ParentID);
                    else
                        node = treeList.AppendNode(data, parentNode);
                    nodes.Add(node.Id, node);
                }
                treeList.ExpandAll();
                treeList.EndUnboundLoad();
            }
        }
    }
}

namespace BenchmarkingApp.Grid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using DevExpress.XtraGrid;
    using DevExpress.XtraGrid.Views.Grid;

    static class Benchmarks {
        internal static void UpdateSID(GridView gridView) {
            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                string sid = (string)gridView.GetRowCellValue(i, "SID");
                gridView.SetRowCellValue(i, "SID", "#" + sid);
            }
            gridView.EndUpdate();
            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                string sid = (string)gridView.GetRowCellValue(i, "SID");
                gridView.SetRowCellValue(i, "SID", sid.Substring(1));
            }
            gridView.EndUpdate();
        }
        internal static void UpdateSize(GridView gridView) {
            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                long size = (long)gridView.GetRowCellValue(i, "Size");
                gridView.SetRowCellValue(i, "Size", size + size);
            }
            gridView.EndUpdate();
            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                long size = (long)gridView.GetRowCellValue(i, "Size");
                gridView.SetRowCellValue(i, "Size", size / 2);
            }
            gridView.EndUpdate();
        }
    }
    [BenchmarkHost("Grid")]
    public abstract class MassUpdateBase : IBenchmarkItem {
        List<Row> dataSource;
        protected GridControl grid;
        protected GridView gridView;
        public void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            grid = ((GridControl)uiControl);
            gridView = grid.MainView as GridView;
            grid.DataSource = dataSource;
        }
        public void TearDown(object uiControl) {
            gridView = null;
            grid = null;
        }
        public abstract void Benchmark();
    }
}

namespace BenchmarkingApp.RadGrid {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using Telerik.WinControls.UI;

    static class Benchmarks {
        internal static void UpdateSID(RadGridView gridView) {
            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                string sid = (string)gridView.Rows[i].Cells["SID"].Value;
                gridView.Rows[i].Cells["SID"].Value = "#" + sid;
            }
            gridView.EndUpdate();
            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                string sid = (string)gridView.Rows[i].Cells["SID"].Value;
                gridView.Rows[i].Cells["SID"].Value = sid.Substring(1);
            }
            gridView.EndUpdate();
        }
        internal static void UpdateSize(RadGridView gridView) {
            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                long size = (long)gridView.Rows[i].Cells["Size"].Value;
                gridView.Rows[i].Cells["Size"].Value = size + size;
            }
            gridView.EndUpdate();
            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                long size = (long)gridView.Rows[i].Cells["Size"].Value;
                gridView.Rows[i].Cells["Size"].Value = size / 2;
            }
            gridView.EndUpdate();
        }
    }
    namespace Bound {
        [BenchmarkHost("RadGrid")]
        public abstract class MassUpdateBase : IBenchmarkItem {
            List<Row> dataSource;
            protected RadGridView gridView;
            public void SetUp(object uiControl) {
                Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
                gridView = ((RadGridView)uiControl);
                gridView.Relations.Clear();
                gridView.DataSource = dataSource;
                gridView.AllowAddNewRow = false;
                gridView.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            }
            public void TearDown(object uiControl) {
                gridView = null;
            }
            public abstract void Benchmark();
        }
    }
    namespace BoundHierarchy {
        [BenchmarkHost("RadGrid")]
        public abstract class MassUpdateBase : IBenchmarkItem {
            List<HierarchicalRow> dataSource;
            protected RadGridView gridView;
            public void SetUp(object uiControl) {
                Row.EnsureHierarchicalSource(ref dataSource, Configuration.Current.Rows, Configuration.Current.Levels);
                gridView = ((RadGridView)uiControl);
                gridView.DataSource = dataSource;
                gridView.Columns["ParentId"].IsVisible = false;
                gridView.AllowAddNewRow = false;
                gridView.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                gridView.Relations.Clear();
                gridView.Relations.AddSelfReference(gridView.MasterTemplate, "ID", "ParentID");
                gridView.MasterTemplate.ExpandAll();
            }
            public void TearDown(object uiControl) {
                gridView = null;
            }
            public abstract void Benchmark();
        }
    }
}