﻿namespace BenchmarkingApp.Tree {
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

    [BenchmarkHost("Grid")]
    public abstract class LoadBase : IBenchmarkItem {
        protected GridControl grid;
        public virtual void SetUp(object uiControl) {
            grid = ((GridControl)uiControl);
            grid.DataSource = null;
        }
        public void TearDown(object uiControl) {
            grid = null;
        }
        public abstract void Benchmark();
    }
}

namespace BenchmarkingApp.RadGrid.Bound {
    using Telerik.WinControls;
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
    using Telerik.WinControls;
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