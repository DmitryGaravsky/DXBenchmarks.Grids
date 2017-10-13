namespace BenchmarkingApp.Grid.Bound {
    [BenchmarkItem("Group by ID", Configuration = "Huge")]
    public class Grouping_ID : GroupBase {
        public sealed override void Benchmark() {
            gridView.Columns["ID"].GroupIndex = 0;
        }
    }
    [BenchmarkItem("Group by Name")]
    public class Grouping_Name : GroupBase {
        public sealed override void Benchmark() {
            gridView.Columns["Name"].GroupIndex = 0;
        }
    }
}

namespace BenchmarkingApp.PivotGrid.Bound {
    [BenchmarkItem("Group by ID", Configuration = "Huge")]
    public class Grouping_ID : GroupBase {
        public sealed override void Benchmark() {
            pivot.Fields["ID"].Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
        }
    }
    [BenchmarkItem("Group by Name")]
    public class Grouping_Name : GroupBase {
        public sealed override void Benchmark() {
            pivot.Fields["Name"].Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
        }
    }
}

namespace BenchmarkingApp.RadGrid.Bound {
    [BenchmarkItem("Group by ID", Configuration = "Huge")]
    public class Grouping_ID : GroupBase {
        public sealed override void Benchmark() {
            gridView.GroupDescriptors.Add("ID", System.ComponentModel.ListSortDirection.Ascending);
        }
    }
    [BenchmarkItem("Group by Name")]
    public class Grouping_Name : GroupBase {
        public sealed override void Benchmark() {
            gridView.GroupDescriptors.Add("Name", System.ComponentModel.ListSortDirection.Ascending);
        }
    }
}

namespace BenchmarkingApp.UltraGrid.Bound {
    [BenchmarkItem("Group by ID", Configuration = "Huge")]
    public class Grouping_ID : GroupBase {
        public sealed override void Benchmark() {
            gridView.DisplayLayout.Bands[0].SortedColumns.Add("ID", false, true);
        }
    }
    [BenchmarkItem("Group by Name")]
    public class Grouping_Name : GroupBase {
        public sealed override void Benchmark() {
            gridView.DisplayLayout.Bands[0].SortedColumns.Add("Name", false, true);
        }
    }
}