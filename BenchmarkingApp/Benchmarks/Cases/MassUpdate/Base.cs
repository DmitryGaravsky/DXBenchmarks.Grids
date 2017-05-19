namespace BenchmarkingApp.Tree {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using DevExpress.XtraTreeList;
    using DevExpress.XtraTreeList.Nodes;

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
        //
        protected void UpdateSID_AddSign() {
            treeList.BeginUpdate();
            var colSID = treeList.Columns["SID"];
            treeList.NodesIterator.Do(node =>
            {
                string sid = (string)node.GetValue(colSID);
                node.SetValue(colSID, "#" + sid);
            });
            treeList.EndUpdate();
        }
        protected void UpdateSID_RemoveSign() {
            treeList.BeginUpdate();
            var colSID = treeList.Columns["SID"];
            treeList.NodesIterator.Do(node =>
            {
                string sid = (string)node.GetValue(colSID);
                node.SetValue(colSID, sid.Substring(1));
            });
            treeList.EndUpdate();
        }
        protected void UpdateSize_Scale() {
            treeList.BeginUpdate();
            var colSID = treeList.Columns["Size"];
            treeList.NodesIterator.Do(node =>
            {
                long size = (long)node.GetValue(colSID);
                node.SetValue(colSID, size + size);
            });
            treeList.EndUpdate();
        }
        protected void UpdateSize_Descale() {
            treeList.BeginUpdate();
            var colSize = treeList.Columns["Size"];
            treeList.NodesIterator.Do(node =>
            {
                long size = (long)node.GetValue(colSize);
                node.SetValue(colSize, size / 2);
            });
            treeList.EndUpdate();
        }
    }
    namespace Bound {
        public abstract class MassUpdateBoundBase : MassUpdateBase {
            protected List<Row> dataSource;
            public sealed override void SetUp(object uiControl) {
                Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
                base.SetUp(uiControl);
                treeList.DataSource = dataSource;
                treeList.ClearSorting();
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
                treeList.ClearSorting();
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

namespace BenchmarkingApp.RadGrid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using Telerik.WinControls.UI;

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
            gridView.SortDescriptors.Clear();
        }
        public void TearDown(object uiControl) {
            gridView.SortDescriptors.Clear();
            gridView = null;
        }
        public abstract void Benchmark();
    }
}
namespace BenchmarkingApp.RadGrid.BoundHierarchy {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using Telerik.WinControls.UI;

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
            gridView.SortDescriptors.Clear();
            gridView.MasterTemplate.ExpandAll();
        }
        public void TearDown(object uiControl) {
            gridView.SortDescriptors.Clear();
            gridView = null;
        }
        public abstract void Benchmark();
    }
}