namespace BenchmarkingApp.Tree {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using DevExpress.XtraTreeList;
    using DevExpress.XtraTreeList.Nodes;

    [BenchmarkHost("TreeList")]
    public abstract class SearchBase : IBenchmarkItem {
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
            treeList.ApplyFindFilter(null);
            treeList = null;
        }
        public abstract void Benchmark();
    }
    namespace Bound {
        public abstract class SearchBoundBase : SearchBase {
            protected List<Row> dataSource;
            public override void SetUp(object uiControl) {
                Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
                base.SetUp(uiControl);
                treeList.DataSource = dataSource;
                treeList.ApplyFindFilter(null);
            }
        }
    }
    namespace BoundHierarchy {
        public abstract class SearchBoundBase : SearchBase {
            protected List<HierarchicalRow> dataSource;
            public sealed override void SetUp(object uiControl) {
                Row.EnsureHierarchicalSource(ref dataSource, Configuration.Current.Rows, Configuration.Current.Levels);
                base.SetUp(uiControl);
                treeList.DataSource = dataSource;
                treeList.ExpandAll();
                treeList.ApplyFindFilter(null);
            }
        }
    }
    namespace Unbound {
        public abstract class SearchUnboundBase : SearchBase {
            protected List<Row> dataSource;
            public override void SetUp(object uiControl) {
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
                treeList.ApplyFindFilter(null);
            }
        }
    }
    namespace UnboundHierarchy {
        public abstract class SearchUnboundBase : SearchBase {
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
                treeList.ApplyFindFilter(null);
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
    public abstract class SearchBase : IBenchmarkItem {
        List<Row> dataSource;
        protected GridControl grid;
        protected GridView gridView;
        public void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            grid = ((GridControl)uiControl);
            gridView = grid.MainView as GridView;
            grid.DataSource = dataSource;
            gridView.ApplyFindFilter(null);
        }
        public void TearDown(object uiControl) {
            gridView.ApplyFindFilter(null);
            gridView = null;
            grid = null;
        }
        public abstract void Benchmark();
    }
}
namespace BenchmarkingApp.RadGrid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using Telerik.WinControls;
    using Telerik.WinControls.UI;

    [BenchmarkHost("RadGrid")]
    public abstract class SearchBase : IBenchmarkItem {
        List<Row> dataSource;
        protected RadGridView gridView;
        public virtual void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            gridView = ((RadGridView)uiControl);
            gridView.AllowAddNewRow = false;
            gridView.AllowSearchRow = true;
            gridView.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            gridView.EnableFiltering = true;
            gridView.MasterView.TableSearchRow.HighlightResults = true;
            gridView.MasterView.TableSearchRow.CaseSensitive = false;
            gridView.Relations.Clear();
            gridView.DataSource = dataSource;
        }
        public void TearDown(object uiControl) {
            gridView.MasterView.TableSearchRow.Search(null);
            gridView = null;
        }
        public abstract void Benchmark();
    }
}
namespace BenchmarkingApp.RadGrid.BoundHierarchy {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using Telerik.WinControls;
    using Telerik.WinControls.UI;

    [BenchmarkHost("RadGrid")]
    public abstract class SearchBase : IBenchmarkItem {
        List<HierarchicalRow> dataSource;
        protected RadGridView gridView;
        public virtual void SetUp(object uiControl) {
            Row.EnsureHierarchicalSource(ref dataSource, Configuration.Current.Rows,Configuration.Current.Levels);
            gridView = ((RadGridView)uiControl);
            gridView.DataSource = dataSource;
            gridView.Columns["ParentId"].IsVisible = false;
            gridView.AllowAddNewRow = false;
            gridView.AllowSearchRow = true;
            gridView.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            gridView.EnableFiltering = true;
            gridView.Relations.Clear();
            gridView.Relations.AddSelfReference(gridView.MasterTemplate, "ID", "ParentID");
            gridView.MasterView.TableSearchRow.HighlightResults = true;
            gridView.MasterView.TableSearchRow.CaseSensitive = false;
            gridView.MasterTemplate.ExpandAll();
        }
        public void TearDown(object uiControl) {
            gridView.MasterView.TableSearchRow.Search(null);
            gridView = null;
        }
        public abstract void Benchmark();
    }
}