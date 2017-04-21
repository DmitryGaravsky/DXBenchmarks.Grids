namespace BenchmarkingApp.Tree.Bound {
    [BenchmarkItem("Search by Notes (Bound)")]
    public class Searching_Notes : SearchBoundBase {
        public sealed override void Benchmark() {
            treeList.ApplyFindFilter("Notes:ipsum");
        }
    }
    [BenchmarkItem("Search by all columns (Bound)", Configuration = "Huge")]
    public class Searching_All : SearchBoundBase {
        public sealed override void Benchmark() {
            treeList.ApplyFindFilter("AB 5");
        }
    }
}
namespace BenchmarkingApp.Tree.BoundHierarchy {
    [BenchmarkItem("Search by Notes (Bound Hierarchy)")]
    public class Searching_Notes : SearchBoundBase {
        public sealed override void Benchmark() {
            treeList.ApplyFindFilter("Notes:ipsum");
        }
    }
    [BenchmarkItem("Search by all columns (Bound Hierarchy)")]
    public class Searching_All : SearchBoundBase {
        public sealed override void Benchmark() {
            treeList.ApplyFindFilter("AB 5");
        }
    }
}
namespace BenchmarkingApp.Tree.Unbound {
    [BenchmarkItem("Search by Notes (Unbound)", Configuration = "Huge")]
    public class Searching_Notes : SearchUnboundBase {
        public sealed override void Benchmark() {
            treeList.ApplyFindFilter("Notes:ipsum");
        }
    }
    [BenchmarkItem("Search by all columns (Unbound)")]
    public class Searching_All : SearchUnboundBase {
        public sealed override void Benchmark() {
            treeList.ApplyFindFilter("AB 5");
        }
    }
}
namespace BenchmarkingApp.Tree.UnboundHierarchy {
    [BenchmarkItem("Search by Notes (Unbound Hierarchy)")]
    public class Searching_Notes : SearchUnboundBase {
        public sealed override void Benchmark() {
            treeList.ApplyFindFilter("Notes:ipsum");
        }
    }
    [BenchmarkItem("Search by all columns (Unbound Hierarchy)")]
    public class Searching_All : SearchUnboundBase {
        public sealed override void Benchmark() {
            treeList.ApplyFindFilter("AB 5");
        }
    }
}
namespace BenchmarkingApp.Grid.Bound {
    [BenchmarkItem("Search by Notes")]
    public class Searching_Notes : SearchBase {
        public sealed override void Benchmark() {
            gridView.ApplyFindFilter("Notes:ipsum");
        }
    }
    [BenchmarkItem("Search by all columns", Configuration = "Huge")]
    public class Searching_All : SearchBase {
        public sealed override void Benchmark() {
            gridView.ApplyFindFilter("AB 5");
        }
    }
}
namespace BenchmarkingApp.RadGrid.Bound {
    [BenchmarkItem("Search by Notes")]
    public class Searching_Notes : SearchBase {
        public override void SetUp(object uiControl) {
            base.SetUp(uiControl);
            foreach(var column in gridView.Columns)
                column.AllowSearching = false;
            gridView.Columns["Notes"].AllowSearching = true;
        }
        public sealed override void Benchmark() {
            base.Benchmark();
            gridView.MasterView.TableSearchRow.Search("ipsum");
        }
    }
    [BenchmarkItem("Search by all columns", Configuration = "Huge")]
    public class Searching_All : SearchBase {
        public override void SetUp(object uiControl) {
            base.SetUp(uiControl);
            foreach(var column in gridView.Columns)
                column.AllowSearching = true;
        }
        public sealed override void Benchmark() {
            base.Benchmark();
            gridView.MasterView.TableSearchRow.Search("AB 5");
        }
    }
}
namespace BenchmarkingApp.RadGrid.BoundHierarchy {
    [BenchmarkItem("Search by Notes (Bound Hierarchy)")]
    public class Searching_Notes : SearchBase {
        public override void SetUp(object uiControl) {
            base.SetUp(uiControl);
            foreach(var column in gridView.Columns)
                column.AllowSearching = false;
            gridView.Columns["Notes"].AllowSearching = true;
        }
        public sealed override void Benchmark() {
            base.Benchmark();
            gridView.MasterView.TableSearchRow.Search("ipsum");
        }
    }
    [BenchmarkItem("Search by all columns (Bound Hierarchy)", Configuration = "Huge")]
    public class Searching_All : SearchBase {
        public override void SetUp(object uiControl) {
            base.SetUp(uiControl);
            foreach(var column in gridView.Columns)
                column.AllowSearching = true;
        }
        public sealed override void Benchmark() {
            base.Benchmark();
            gridView.MasterView.TableSearchRow.Search("AB 5");
        }
    }
}