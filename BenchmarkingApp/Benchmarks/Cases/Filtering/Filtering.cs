﻿namespace BenchmarkingApp.Tree.Bound {
    using DevExpress.Data.Filtering;

    [BenchmarkItem("Filter by Price (Bound)")]
    public class Filtering_Price : FilterBoundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Price>5000");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Approved (Bound)")]
    public class Filtering_Approved : FilterBoundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Approved Is Not Null");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Price and Approved (Bound)")]
    public class Filtering_Price_and_Approved : FilterBoundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Price<5000 AND Approved Is Not Null");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Amount and Open (Bound)")]
    public class Filtering_Amount_and_Open : FilterBoundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("[Amount] Between('150','175') AND ([Open]>='1/1/2001' AND [Open]<'1/1/2002')");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
}
namespace BenchmarkingApp.Tree.BoundHierarchy {
    using DevExpress.Data.Filtering;

    [BenchmarkItem("Filter by Price (Bound Hierarchy)")]
    public class Filtering_Price : FilterBoundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Price>5000");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Approved (Bound Hierarchy)")]
    public class Filtering_Approved : FilterBoundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Approved Is Not Null");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Price and Approved (Bound Hierarchy)")]
    public class Filtering_Price_and_Approved : FilterBoundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Price<5000 AND Approved Is Not Null");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Amount and Open (Bound Hierarchy)")]
    public class Filtering_Amount_and_Open : FilterBoundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("[Amount] Between('150','175') AND ([Open]>='1/1/2001' AND [Open]<'1/1/2002')");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
}
namespace BenchmarkingApp.Tree.Unbound {
    using DevExpress.Data.Filtering;

    [BenchmarkItem("Filter by Price (Unbound)")]
    public class Filtering_Price : FilterUnboundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Price>5000");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Approved (Unbound)")]
    public class Filtering_Approved : FilterUnboundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Approved Is Not Null");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Price and Approved (Unbound)")]
    public class Filtering_Price_and_Approved : FilterUnboundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Price<5000 AND Approved Is Not Null");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Amount and Open (Unbound)")]
    public class Filtering_Amount_and_Open : FilterUnboundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("[Amount] Between('150','175') AND ([Open]>='1/1/2001' AND [Open]<'1/1/2002')");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
}
namespace BenchmarkingApp.Tree.UnboundHierarchy {
    using DevExpress.Data.Filtering;

    [BenchmarkItem("Filter by Price (Unbound Hierarchy)")]
    public class Filtering_Price : FilterUnboundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Price>5000");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Approved (Unbound Hierarchy)")]
    public class Filtering_Approved : FilterUnboundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Approved Is Not Null");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Price and Approved (Unbound Hierarchy)")]
    public class Filtering_Price_and_Approved : FilterUnboundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Price<5000 AND Approved Is Not Null");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Amount and Open (Unbound Hierarchy)")]
    public class Filtering_Amount_and_Open : FilterUnboundBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("[Amount] Between('150','175') AND ([Open]>='1/1/2001' AND [Open]<'1/1/2002')");
        public sealed override void Benchmark() {
            treeList.ActiveFilterCriteria = criteria;
        }
    }
}

namespace BenchmarkingApp.Grid.Bound {
    using DevExpress.Data.Filtering;

    [BenchmarkItem("Filter by Price")]
    public class Filtering_Price : FilterBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Price>5000");
        public sealed override void Benchmark() {
            gridView.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Approved")]
    public class Filtering_Approved : FilterBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Approved Is Not Null");
        public sealed override void Benchmark() {
            gridView.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Price and Approved")]
    public class Filtering_Price_and_Approved : FilterBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("Price<5000 AND Approved Is Not Null");
        public sealed override void Benchmark() {
            gridView.ActiveFilterCriteria = criteria;
        }
    }
    [BenchmarkItem("Filter by Amount and Open")]
    public class Filtering_Amount_and_Open : FilterBase {
        readonly CriteriaOperator criteria = CriteriaOperator.Parse("[Amount] Between('150','175') AND ([Open]>='1/1/2001' AND [Open]<'1/1/2002')");
        public sealed override void Benchmark() {
            gridView.ActiveFilterCriteria = criteria;
        }
    }
}

namespace BenchmarkingApp.InMemory {
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkHost("InMemory")]
    public abstract class FilterBase : IBenchmarkItem {
        protected List<Row> dataSource;
        protected Row[] filtered;
        public void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, 10000);
        }
        public void TearDown(object uiControl) { }
        public abstract void Benchmark();
    }
    //
    [BenchmarkItem("Filter by Price")]
    public class Filtering_Price : FilterBase {
        public sealed override void Benchmark() {
            filtered = dataSource
                .Where(r => r.Price > 5000)
                .ToArray();
        }
    }
    [BenchmarkItem("Filter by Approved")]
    public class Filtering_Approved : FilterBase {
        public sealed override void Benchmark() {
            filtered = dataSource
                .Where(r => r.Approved.HasValue)
                .ToArray();
        }
    }
    [BenchmarkItem("Filter by Price and Approved")]
    public class Filtering_Price_and_Approved : FilterBase {
        public sealed override void Benchmark() {
            filtered = dataSource
                .Where(r => r.Price > 5000m && r.Approved.HasValue)
                .ToArray();
        }
    }
    [BenchmarkItem("Filter by Amount and Open")]
    public class Filtering_Amount_and_Open : FilterBase {
        public sealed override void Benchmark() {
            System.DateTime before = new System.DateTime(2002, 1, 1);
            System.DateTime after = new System.DateTime(2001, 1, 1);
            filtered = dataSource
                .Where(r => (r.Amount >= 150.0 && r.Amount <= 170.0) && (r.Open >= after && r.Open < before))
                .ToArray();
        }
    }
}