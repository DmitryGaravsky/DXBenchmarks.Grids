namespace BenchmarkingApp.Tree.Bound {
    [BenchmarkItem("Sort by ID (Bound)", Configuration = "Huge")]
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
    [BenchmarkItem("Sort by ID (Bound Hierarchy)", Configuration = "Deep;Huge")]
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
    [BenchmarkItem("Sort by ID (Unbound)", Configuration = "Huge")]
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
    [BenchmarkItem("Sort by ID (Unbound Hierarchy)", Configuration = "Deep;Huge")]
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
    [BenchmarkItem("Sort by ID", Configuration = "Huge")]
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

namespace BenchmarkingApp.PivotGrid.Bound {
    [BenchmarkItem("Sort by ID", Configuration = "Huge")]
    public class Sorting_ID : SortBase {
        public sealed override void Benchmark() {
            pivot.Fields["ID"].ChangeSortOrder();
        }
    }
    [BenchmarkItem("Sort by Name")]
    public class Sorting_Name : SortBase {
        public sealed override void Benchmark() {
            pivot.Fields["Name"].ChangeSortOrder();
        }
    }
}

namespace BenchmarkingApp.RadGrid.Bound {
    using Telerik.WinControls.Data;
    using Telerik.WinControls.UI;

    [BenchmarkItem("Sort by ID", Configuration = "Huge")]
    public class Sorting_ID : SortBase {
        public sealed override void Benchmark() {
            gridView.Columns["ID"].SortOrder = RadSortOrder.Descending;
        }
    }
    [BenchmarkItem("Sort by Name")]
    public class Sorting_Name : SortBase {
        public sealed override void Benchmark() {
            gridView.Columns["Name"].SortOrder = RadSortOrder.Descending;
        }
    }
    [BenchmarkItem("Sort by ID and Name")]
    public class Sorting_ID_and_Name : SortBase {
        public sealed override void Benchmark() {
            gridView.SortDescriptors.AddRange(
                    new SortDescriptor("ID", System.ComponentModel.ListSortDirection.Descending),
                    new SortDescriptor("Name", System.ComponentModel.ListSortDirection.Ascending)
                );
        }
    }
    [BenchmarkItem("Sort by Age and Factor")]
    public class Sorting_Age_and_Factor : SortBase {
        public sealed override void Benchmark() {
            gridView.SortDescriptors.AddRange(
                    new SortDescriptor("Age", System.ComponentModel.ListSortDirection.Descending),
                    new SortDescriptor("Factor", System.ComponentModel.ListSortDirection.Ascending)
                );
        }
    }
}
namespace BenchmarkingApp.RadGrid.BoundHierarchy {
    using Telerik.WinControls.Data;
    using Telerik.WinControls.UI;

    [BenchmarkItem("Sort by ID (Bound Hierarchy)", Configuration = "Huge")]
    public class Sorting_ID : SortBase {
        public sealed override void Benchmark() {
            gridView.Columns["ID"].SortOrder = RadSortOrder.Descending;
        }
    }
    [BenchmarkItem("Sort by Name (Bound Hierarchy)")]
    public class Sorting_Name : SortBase {
        public sealed override void Benchmark() {
            gridView.Columns["Name"].SortOrder = RadSortOrder.Descending;
        }
    }
    [BenchmarkItem("Sort by ID and Name (Bound Hierarchy)")]
    public class Sorting_ID_and_Name : SortBase {
        public sealed override void Benchmark() {
            gridView.SortDescriptors.AddRange(
                    new SortDescriptor("ID", System.ComponentModel.ListSortDirection.Descending),
                    new SortDescriptor("Name", System.ComponentModel.ListSortDirection.Ascending)
                );
        }
    }
    [BenchmarkItem("Sort by Age and Factor (Bound Hierarchy)")]
    public class Sorting_Age_and_Factor : SortBase {
        public sealed override void Benchmark() {
            gridView.SortDescriptors.AddRange(
                    new SortDescriptor("Age", System.ComponentModel.ListSortDirection.Descending),
                    new SortDescriptor("Factor", System.ComponentModel.ListSortDirection.Ascending)
                );
        }
    }
}

namespace BenchmarkingApp.UltraGrid.Bound {
    using Infragistics.Win.UltraWinGrid;

    [BenchmarkItem("Sort by ID", Configuration = "Huge")]
    public class Sorting_ID : SortBase {
        public sealed override void Benchmark() {
            gridView.DisplayLayout.Bands[0].Columns["ID"].SortIndicator = SortIndicator.Descending;
        }
    }
    [BenchmarkItem("Sort by Name")]
    public class Sorting_Name : SortBase {
        public sealed override void Benchmark() {
            gridView.DisplayLayout.Bands[0].Columns["Name"].SortIndicator = SortIndicator.Descending;
        }
    }
    [BenchmarkItem("Sort by ID and Name")]
    public class Sorting_ID_and_Name : SortBase {
        public sealed override void Benchmark() {
            gridView.BeginUpdate();
            gridView.DisplayLayout.Bands[0].SortedColumns.Add("ID", true);
            gridView.DisplayLayout.Bands[0].SortedColumns.Add("Name", false);
            gridView.EndUpdate();
        }
    }
    [BenchmarkItem("Sort by Age and Factor")]
    public class Sorting_Age_and_Factor : SortBase {
        public sealed override void Benchmark() {
            gridView.BeginUpdate();
            gridView.DisplayLayout.Bands[0].SortedColumns.Add("Age", true);
            gridView.DisplayLayout.Bands[0].SortedColumns.Add("Factor", false);
            gridView.EndUpdate();
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
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
        }
        public void TearDown(object uiControl) { }
        public abstract void Benchmark();
    }
    //
    [BenchmarkItem("Sort by ID", Configuration = "Huge")]
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