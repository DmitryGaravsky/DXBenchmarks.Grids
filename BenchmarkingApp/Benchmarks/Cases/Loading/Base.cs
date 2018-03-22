namespace BenchmarkingApp.Tree {
    using DevExpress.XtraTreeList;

    [BenchmarkHost("TreeList")]
    public abstract class LoadBase : IBenchmarkItem {
        protected TreeList treeList;
        public virtual void SetUp(object uiControl) {
            treeList = ((TreeList)uiControl);
            treeList.DataSource = null;
            treeList.Columns.Clear();
            treeList.Nodes.Clear();
            treeList.OptionsBehavior.PopulateServiceColumns = true;
        }
        public virtual void TearDown(object uiControl) {
            treeList = null;
        }
        public abstract void Benchmark();
    }
}
namespace BenchmarkingApp.Grid.Bound {
    using DevExpress.XtraGrid;
    using DevExpress.XtraGrid.Views.Base;

    [BenchmarkHost("Grid")]
    public abstract class LoadBase : IBenchmarkItem {
        protected GridControl grid;
        public virtual void SetUp(object uiControl) {
            grid = ((GridControl)uiControl);
            grid.DataSource = null;
            ((ColumnView)grid.MainView).Columns.Clear();
        }
        public void TearDown(object uiControl) {
            grid = null;
        }
        public abstract void Benchmark();
    }
}
namespace BenchmarkingApp.Grid.Virtual {
    using System;
    using System.Linq;
    using BenchmarkingApp.Benchmarks.Data;
    using DevExpress.Data;
    using DevExpress.XtraGrid;
    using DevExpress.XtraGrid.Views.Base;

    [BenchmarkHost("Grid")]
    public abstract class LoadBase : IBenchmarkItem {
        protected GridControl grid;
        protected UnboundSource unboundSource;
        public virtual void SetUp(object uiControl) {
            grid = ((GridControl)uiControl);
            grid.DataSource = null;
            unboundSource = new UnboundSource();
            string[] names = Row.GetColumns();
            Type[] types = Row.GetColumnTypes();
            unboundSource.Properties.AddRange(names.Select(
                (name, index) => new UnboundSourceProperty(name, types[index]) { UserTag = index }));
            ((ColumnView)grid.MainView).Columns.Clear();
        }
        public virtual void TearDown(object uiControl) {
            grid = null;
        }
        public abstract void Benchmark();
    }
}

namespace BenchmarkingApp.RadGrid.Bound {
    using Telerik.WinControls.UI;

    [BenchmarkHost("RadGrid")]
    public abstract class LoadBase : IBenchmarkItem {
        protected RadGridView gridView;
        public virtual void SetUp(object uiControl) {
            gridView = ((RadGridView)uiControl);
            gridView.AllowAddNewRow = false;
            gridView.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            gridView.EnableFiltering = true;
            gridView.Relations.Clear();
            gridView.DataSource = null;
        }
        public void TearDown(object uiControl) {
            gridView = null;
        }
        public abstract void Benchmark();
    }
}
namespace BenchmarkingApp.RadGrid.BoundHierarchy {
    using Telerik.WinControls.UI;

    [BenchmarkHost("RadGrid")]
    public abstract class LoadBase : IBenchmarkItem {
        protected RadGridView gridView;
        public virtual void SetUp(object uiControl) {
            gridView = ((RadGridView)uiControl);
            gridView.AllowAddNewRow = false;
            gridView.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            gridView.EnableFiltering = true;
            gridView.Relations.Clear();
            gridView.DataSource = null;
        }
        public void TearDown(object uiControl) {
            gridView = null;
        }
        public abstract void Benchmark();
    }
}
namespace BenchmarkingApp.RadGrid.Virtual {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using Telerik.WinControls.UI;

    [BenchmarkHost("RadVirtualGrid")]
    public abstract class LoadBase : IBenchmarkItem {
        protected List<Row> dataSource;
        protected readonly string[] columnNames = Row.GetColumns();
        protected RadVirtualGrid gridView;
        public void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            gridView = ((RadVirtualGrid)uiControl);
        }
        public virtual void TearDown(object uiControl) {
            gridView = null;
        }
        public abstract void Benchmark();
    }
}

namespace BenchmarkingApp.UltraGrid.Bound {
    using Infragistics.Win.UltraWinGrid;

    [BenchmarkHost("UltraGrid")]
    public abstract class LoadBase : IBenchmarkItem {
        protected UltraGrid gridView;
        public virtual void SetUp(object uiControl) {
            gridView = ((UltraGrid)uiControl);
            gridView.DataSource = null;
            gridView.DisplayLayout.ResetBands();
            gridView.Rows.Refresh(RefreshRow.ReloadData);
        }
        public void TearDown(object uiControl) {
            gridView = null;
        }
        public abstract void Benchmark();
    }
}
namespace BenchmarkingApp.SfDataGrid.Bound {
    using Syncfusion.WinForms.DataGrid;

    [BenchmarkHost("SfDataGrid")]
    public abstract class LoadBase : IBenchmarkItem {
        protected SfDataGrid gridView;
        public virtual void SetUp(object uiControl) {
            gridView = ((SfDataGrid)uiControl);
            gridView.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
            gridView.DataSource = null;
        }
        public void TearDown(object uiControl) {
            gridView = null;
        }
        public abstract void Benchmark();
    }
}