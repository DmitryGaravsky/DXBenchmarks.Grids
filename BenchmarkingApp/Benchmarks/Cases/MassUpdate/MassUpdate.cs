namespace BenchmarkingApp.Tree.Bound {
    [BenchmarkItem("Mass Update, SID (Bound)", Configuration = "Huge")]
    public class MassUpdate_SID : MassUpdateBoundBase {
        public sealed override void Benchmark() {
            Benchmarks.UpdateSID(treeList);
        }
    }
    [BenchmarkItem("Mass Update, Size (Bound)")]
    public class MassUpdate_Size : MassUpdateBoundBase {
        public sealed override void Benchmark() {
            Benchmarks.UpdateSize(treeList);
        }
    }
}
namespace BenchmarkingApp.Tree.BoundHierarchy {
    [BenchmarkItem("Mass Update, SID (Bound Hierarchy)", Configuration = "Deep;Huge")]
    public class MassUpdate_SID : MassUpdateBoundBase {
        public sealed override void Benchmark() {
            Benchmarks.UpdateSID(treeList);
        }
    }
    [BenchmarkItem("Mass Update, Size (Bound Hierarchy)")]
    public class MassUpdate_Size : MassUpdateBoundBase {
        public sealed override void Benchmark() {
            Benchmarks.UpdateSize(treeList);
        }
    }
}
namespace BenchmarkingApp.Tree.Unbound {
    [BenchmarkItem("Mass Update, SID (Unbound)", Configuration = "Huge")]
    public class MassUpdate_SID : MassUpdateUnboundBase {
        public sealed override void Benchmark() {
            Benchmarks.UpdateSID(treeList);
        }
    }
    [BenchmarkItem("Mass Update, Size (Unbound)")]
    public class MassUpdate_Size : MassUpdateUnboundBase {
        public sealed override void Benchmark() {
            Benchmarks.UpdateSize(treeList);
        }
    }
}
namespace BenchmarkingApp.Tree.UnboundHierarchy {
    [BenchmarkItem("Mass Update, SID (Unbound Hierarchy)", Configuration = "Deep;Huge")]
    public class MassUpdate_SID : MassUpdateUnboundBase {
        public sealed override void Benchmark() {
            Benchmarks.UpdateSID(treeList);
        }
    }
    [BenchmarkItem("Mass Update, Size (Unbound Hierarchy)")]
    public class MassUpdate_Size : MassUpdateUnboundBase {
        public sealed override void Benchmark() {
            Benchmarks.UpdateSize(treeList);
        }
    }
}

namespace BenchmarkingApp.Grid.Bound {
    [BenchmarkItem("Mass Update, SID", Configuration = "Huge")]
    public class MassUpdate_SID : MassUpdateBase {
        public sealed override void Benchmark() {
            Benchmarks.UpdateSID(gridView);
        }
    }
    [BenchmarkItem("Mass Update, Size")]
    public class MassUpdate_Size : MassUpdateBase {
        public sealed override void Benchmark() {
            Benchmarks.UpdateSize(gridView);
        }
    }
}

namespace BenchmarkingApp.RadGrid.Bound {
    [BenchmarkItem("Mass Update, SID", Configuration = "Huge")]
    public class MassUpdate_ID : MassUpdateBase {
        public sealed override void Benchmark() {
            Benchmarks.UpdateSID(gridView);
        }
    }
    [BenchmarkItem("Mass Update, Size")]
    public class MassUpdate_Size : MassUpdateBase {
        public sealed override void Benchmark() {
            Benchmarks.UpdateSize(gridView);
        }
    }
}
namespace BenchmarkingApp.RadGrid.BoundHierarchy {
    [BenchmarkItem("Mass Update, SID (Bound Hierarchy)", Configuration = "Huge")]
    public class MassUpdate_ID : MassUpdateBase {
        public sealed override void Benchmark() {
            Benchmarks.UpdateSID(gridView);
        }
    }
    [BenchmarkItem("Mass Update, Size (Bound Hierarchy)")]
    public class MassUpdate_Size : MassUpdateBase {
        public sealed override void Benchmark() {
            Benchmarks.UpdateSize(gridView);
        }
    }
}