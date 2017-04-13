namespace BenchmarkingApp.Tree.Bound {
    [BenchmarkItem("Sort by ID (Bound)")]
    public class Sorting_ID : SortBoundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["ID"].SortOrder = System.Windows.Forms.SortOrder.Descending;
            treeList.EndSort();
        }
    }
    [BenchmarkItem("Sort by Name (Bound)")]
    public class Sorting_Name : SortBoundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["Name"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
            treeList.EndSort();
        }
    }
    [BenchmarkItem("Sort by ID and Name (Bound)")]
    public class Sorting_ID_and_Name : SortBoundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["ID"].SortOrder = System.Windows.Forms.SortOrder.Descending;
            treeList.Columns["Name"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
            treeList.EndSort();
        }
    }
    [BenchmarkItem("Sort by Age and Factor (Bound)")]
    public class Sorting_Age_and_Factor : SortBoundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["Age"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
            treeList.Columns["Factor"].SortOrder = System.Windows.Forms.SortOrder.Descending;
            treeList.EndSort();
        }
    }
}
namespace BenchmarkingApp.Tree.BoundHierarchy {
    [BenchmarkItem("Sort by ID (Bound Hierarchy)")]
    public class Sorting_ID : SortBoundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["ID"].SortOrder = System.Windows.Forms.SortOrder.Descending;
            treeList.EndSort();
        }
    }
    [BenchmarkItem("Sort by Name (Bound Hierarchy)")]
    public class Sorting_Name : SortBoundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["Name"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
            treeList.EndSort();
        }
    }
    [BenchmarkItem("Sort by ID and Name (Bound Hierarchy)")]
    public class Sorting_ID_and_Name : SortBoundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["ID"].SortOrder = System.Windows.Forms.SortOrder.Descending;
            treeList.Columns["Name"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
            treeList.EndSort();
        }
    }
    [BenchmarkItem("Sort by Age and Factor (Bound Hierarchy)")]
    public class Sorting_Age_and_Factor : SortBoundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["Age"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
            treeList.Columns["Factor"].SortOrder = System.Windows.Forms.SortOrder.Descending;
            treeList.EndSort();
        }
    }
}

namespace BenchmarkingApp.Tree.Unbound {
    [BenchmarkItem("Sort by ID (Unbound)")]
    public class Sorting_ID : SortUnboundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["ID"].SortOrder = System.Windows.Forms.SortOrder.Descending;
            treeList.EndSort();
        }
    }
    [BenchmarkItem("Sort by Name (Unbound)")]
    public class Sorting_Name : SortUnboundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["Name"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
            treeList.EndSort();
        }
    }
    [BenchmarkItem("Sort by ID and Name (Unbound)")]
    public class Sorting_ID_and_Name : SortUnboundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["ID"].SortOrder = System.Windows.Forms.SortOrder.Descending;
            treeList.Columns["Name"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
            treeList.EndSort();
        }
    }
    [BenchmarkItem("Sort by Age and Factor (Unbound)")]
    public class Sorting_Age_and_Factor : SortUnboundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["Age"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
            treeList.Columns["Factor"].SortOrder = System.Windows.Forms.SortOrder.Descending;
            treeList.EndSort();
        }
    }
}

namespace BenchmarkingApp.Tree.UnboundHierarchy {
    [BenchmarkItem("Sort by ID (Unbound Hierarchy)")]
    public class Sorting_ID : SortUnboundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["ID"].SortOrder = System.Windows.Forms.SortOrder.Descending;
            treeList.EndSort();
        }
    }
    [BenchmarkItem("Sort by Name (Unbound Hierarchy)")]
    public class Sorting_Name : SortUnboundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["Name"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
            treeList.EndSort();
        }
    }
    [BenchmarkItem("Sort by ID and Name (Unbound Hierarchy)")]
    public class Sorting_ID_and_Name : SortUnboundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["ID"].SortOrder = System.Windows.Forms.SortOrder.Descending;
            treeList.Columns["Name"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
            treeList.EndSort();
        }
    }
    [BenchmarkItem("Sort by Age and Factor (Unbound Hierarchy)")]
    public class Sorting_Age_and_Factor : SortUnboundBase {
        public sealed override void Benchmark() {
            treeList.BeginSort();
            treeList.Columns["Age"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
            treeList.Columns["Factor"].SortOrder = System.Windows.Forms.SortOrder.Descending;
            treeList.EndSort();
        }
    }
}

namespace BenchmarkingApp.Grid.Bound {
    [BenchmarkItem("Sort by ID")]
    public class Sorting_ID : SortBase {
        public sealed override void Benchmark() {
            gridView.BeginSort();
            gridView.Columns["ID"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            gridView.EndSort();
        }
    }
    [BenchmarkItem("Sort by Name")]
    public class Sorting_Name : SortBase {
        public sealed override void Benchmark() {
            gridView.BeginSort();
            gridView.Columns["Name"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            gridView.EndSort();
        }
    }
    [BenchmarkItem("Sort by ID and Name")]
    public class Sorting_ID_and_Name : SortBase {
        public sealed override void Benchmark() {
            gridView.BeginSort();
            gridView.Columns["ID"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            gridView.Columns["Name"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            gridView.EndSort();
        }
    }
    [BenchmarkItem("Sort by Age and Factor")]
    public class Sorting_Age_and_Factor : SortBase {
        public sealed override void Benchmark() {
            gridView.BeginSort();
            gridView.Columns["Age"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            gridView.Columns["Factor"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            gridView.EndSort();
        }
    }
}

namespace BenchmarkingApp.InMemory {
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkHost("InMemory")]
    public abstract class SortBase : IBenchmarkItem {
        protected List<Row> dataSource;
        protected Row[] sorted;
        public void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, 10000);
        }
        public void TearDown(object uiControl) { }
        public abstract void Benchmark();
    }
    //
    [BenchmarkItem("Sort by ID")]
    public class Sorting_ID : SortBase {
        public sealed override void Benchmark() {
            sorted = dataSource
                .OrderByDescending(r => r.ID)
                .ToArray();
        }
    }
    [BenchmarkItem("Sort by Name")]
    public class Sorting_Name : SortBase {
        public sealed override void Benchmark() {
            sorted = dataSource
                .OrderBy(r => r.Name)
                .ToArray();
        }
    }
    [BenchmarkItem("Sort by ID and Name")]
    public class Sorting_ID_and_Name : SortBase {
        public sealed override void Benchmark() {
            sorted = dataSource
                .OrderByDescending(r => r.ID).ThenBy(r => r.Name)
                .ToArray();
        }
    }
    [BenchmarkItem("Sort by Age and Factor")]
    public class Sorting_Age_and_Factor : SortBase {
        public sealed override void Benchmark() {
            sorted = dataSource
                .OrderBy(r => r.Age).ThenByDescending(r => r.Factor)
                .ToArray();
        }
    }
}