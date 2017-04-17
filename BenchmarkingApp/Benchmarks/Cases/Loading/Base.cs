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
namespace BenchmarkingApp.Grid {
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