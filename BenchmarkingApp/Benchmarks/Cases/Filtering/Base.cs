﻿namespace BenchmarkingApp.Tree {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using DevExpress.XtraTreeList;
    using DevExpress.XtraTreeList.Nodes;

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
namespace BenchmarkingApp.PivotGrid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using DevExpress.XtraPivotGrid;

    [BenchmarkHost("Pivot")]
    public abstract class FilterBase : IBenchmarkItem {
        List<Row> dataSource;
        protected PivotGridControl pivot;
        public void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            pivot = ((PivotGridControl)uiControl);
            pivot.BeginUpdate();
            pivot.DataSource = dataSource;
            pivot.RetrieveFields(PivotArea.FilterArea, true);
            pivot.Fields["ID"].Area = PivotArea.RowArea;
            pivot.ActiveFilterCriteria = null;
            pivot.EndUpdate();
        }
        public void TearDown(object uiControl) {
            pivot.BeginUpdate();
            pivot.Fields["ID"].Area = PivotArea.FilterArea;
            pivot.ActiveFilterCriteria = null;
            pivot.EndUpdate();
            pivot = null;
        }
        public abstract void Benchmark();
    }
}

namespace BenchmarkingApp.RadGrid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using Telerik.WinControls.UI;

    [BenchmarkHost("RadGrid")]
    public abstract class FilterBase : IBenchmarkItem {
        List<Row> dataSource;
        protected RadGridView gridView;
        public void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            gridView = ((RadGridView)uiControl);
            gridView.Relations.Clear();
            gridView.FilterDescriptors.Clear();
            gridView.DataSource = dataSource;
            gridView.AllowAddNewRow = false;
            gridView.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            gridView.EnableFiltering = true;
        }
        public void TearDown(object uiControl) {
            gridView.FilterDescriptors.Clear();
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
    public abstract class FilterBase : IBenchmarkItem {
        List<HierarchicalRow> dataSource;
        protected RadGridView gridView;
        public void SetUp(object uiControl) {
            Row.EnsureHierarchicalSource(ref dataSource, Configuration.Current.Rows,Configuration.Current.Levels);
            gridView = ((RadGridView)uiControl);
            gridView.Relations.Clear();
            gridView.FilterDescriptors.Clear();
            gridView.DataSource = dataSource;
            gridView.Columns["ParentId"].IsVisible = false;
            gridView.AllowAddNewRow = false;
            gridView.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            gridView.EnableFiltering = true;
            gridView.MasterTemplate.EnableHierarchyFiltering = true;
            gridView.Relations.AddSelfReference(gridView.MasterTemplate, "ID", "ParentID");
            gridView.MasterTemplate.ExpandAll();
        }
        public void TearDown(object uiControl) {
            gridView.FilterDescriptors.Clear();
            gridView = null;
        }
        public abstract void Benchmark();
    }
}

namespace BenchmarkingApp.UltraGrid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using Infragistics.Win.UltraWinGrid;

    [BenchmarkHost("UltraGrid")]
    public abstract class FilterBase : IBenchmarkItem {
        List<Row> dataSource;
        protected UltraGrid gridView;
        public virtual void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            gridView = ((UltraGrid)uiControl);
            gridView.DataSource = dataSource;
            gridView.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
            gridView.Rows.Refresh(RefreshRow.ReloadData);
        }
        public void TearDown(object uiControl) {
            gridView.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
            gridView = null;
        }
        public abstract void Benchmark();
    }
}

namespace BenchmarkingApp.SfDataGrid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using Syncfusion.WinForms.DataGrid;

    [BenchmarkHost("SfDataGrid")]
    public abstract class FilterBase : IBenchmarkItem {
        List<Row> dataSource;
        protected SfDataGrid gridView;
        public void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            gridView = ((SfDataGrid)uiControl);
            gridView.DataSource = dataSource;
            gridView.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
            gridView.AllowFiltering = true;
        }
        public void TearDown(object uiControl) {
            foreach(GridColumn item in gridView.Columns) {
                item.FilterPredicates.Clear();
            }
            gridView = null;
        }
        public abstract void Benchmark();
    }
}