namespace BenchmarkingApp.Grid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;
    using DevExpress.XtraGrid;
    using DevExpress.XtraGrid.Views.Grid;

    [BenchmarkHost("Grid")]
    public abstract class GroupBase : IBenchmarkItem {
        List<Row> dataSource;
        protected GridControl grid;
        protected GridView gridView;
        public void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            grid = ((GridControl)uiControl);
            gridView = grid.MainView as GridView;
            grid.DataSource = dataSource;
            gridView.ClearGrouping();
        }
        public void TearDown(object uiControl) {
            gridView.ClearGrouping();
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
    public abstract class GroupBase : IBenchmarkItem {
        List<Row> dataSource;
        protected PivotGridControl pivot;
        public void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            pivot = ((PivotGridControl)uiControl);
            pivot.DataSource = dataSource;
            pivot.RetrieveFields(PivotArea.FilterArea, true);
        }
        public void TearDown(object uiControl) {
            pivot.Fields["ID"].Area = PivotArea.FilterArea;
            pivot.Fields["Name"].Area = PivotArea.FilterArea;
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
    public abstract class GroupBase : IBenchmarkItem {
        List<Row> dataSource;
        protected RadGridView gridView;
        public void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            gridView = ((RadGridView)uiControl);
            gridView.Relations.Clear();
            gridView.DataSource = dataSource;
            gridView.AllowAddNewRow = false;
            gridView.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            gridView.GroupDescriptors.Clear();
        }
        public void TearDown(object uiControl) {
            gridView.GroupDescriptors.Clear();
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
    public abstract class GroupBase : IBenchmarkItem {
        List<Row> dataSource;
        protected UltraGrid gridView;
        public virtual void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            gridView = ((UltraGrid)uiControl);
            gridView.DataSource = dataSource;
            gridView.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            gridView.DisplayLayout.GroupByBox.Hidden = true;
            gridView.DisplayLayout.Bands[0].SortedColumns.Clear();
            gridView.Rows.Refresh(Infragistics.Win.UltraWinGrid.RefreshRow.ReloadData);
        }
        public void TearDown(object uiControl) {
            gridView.DisplayLayout.Bands[0].SortedColumns.Clear();
            gridView.DisplayLayout.ResetBands();
            gridView = null;
        }
        public abstract void Benchmark();
    }
}