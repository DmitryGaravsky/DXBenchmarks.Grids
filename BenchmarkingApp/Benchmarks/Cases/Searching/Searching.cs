namespace BenchmarkingApp.Tree.Bound {
    [BenchmarkItem("Search by Notes (Bound)")]
    public class Searching_Notes : SearchBoundBase {
        public sealed override void Benchmark() {
            treeList.ApplyFindFilter("Notes:ipsum");
        }
    }
    [BenchmarkItem("Search by all columns (Bound)")]
    public class Searching_All : SearchBoundBase {
        public sealed override void Benchmark() {
            treeList.ApplyFindFilter("AB 5");
        }
    }
}

namespace BenchmarkingApp.Tree.Unbound {
    [BenchmarkItem("Search by Notes (Unbound)")]
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

namespace BenchmarkingApp.Grid.Bound {
    [BenchmarkItem("Search by Notes (Unbound)")]
    public class Searching_Notes : SearchBase {
        public sealed override void Benchmark() {
            gridView.ApplyFindFilter("Notes:ipsum");
        }
    }
    [BenchmarkItem("Search by all columns")]
    public class Searching_All : SearchBase {
        public sealed override void Benchmark() {
            gridView.ApplyFindFilter("AB 5");
        }
    }
}