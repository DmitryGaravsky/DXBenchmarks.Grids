﻿namespace BenchmarkingApp.Tree.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading (Bound)", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<Row> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            base.SetUp(uiControl);
        }
        public sealed override void Benchmark() {
            treeList.DataSource = dataSource;
        }
    }
}
namespace BenchmarkingApp.Tree.BoundHierarchy {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading (Bound Hierarchy)", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<HierarchicalRow> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureHierarchicalSource(ref dataSource, Configuration.Current.Rows, Configuration.Current.Levels);
            base.SetUp(uiControl);
        }
        public sealed override void Benchmark() {
            treeList.BeginUpdate();
            treeList.DataSource = dataSource;
            treeList.ExpandAll();
            treeList.EndUpdate();
        }
    }
}
namespace BenchmarkingApp.Tree.Unbound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading (Unbound)", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<Row> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            base.SetUp(uiControl);
            // Columns
            treeList.BeginUpdate();
            var columns = Row.GetColumns();
            for(int i = 0; i < columns.Length; i++)
                treeList.Columns.AddVisible(columns[i]);
            treeList.EndUpdate();
        }
        public sealed override void Benchmark() {
            treeList.BeginUnboundLoad();
            for(int i = 0; i < dataSource.Count; i++)
                treeList.Nodes.Add(dataSource[i].GetData());
            treeList.EndUnboundLoad();
        }
    }
}
namespace BenchmarkingApp.Tree.UnboundHierarchy {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading (Unbound Hierarchy)", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<HierarchicalRow> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureHierarchicalSource(ref dataSource, Configuration.Current.Rows, Configuration.Current.Levels);
            base.SetUp(uiControl);
            // Columns
            treeList.BeginUpdate();
            var columns = HierarchicalRow.GetHierarchicalColumns();
            for(int i = 0; i < columns.Length; i++)
                treeList.Columns.AddVisible(columns[i]);
            treeList.Columns["ParentID"].Visible = false;
            treeList.EndUpdate();
        }
        public sealed override void Benchmark() {
            treeList.BeginUnboundLoad();
            for(int i = 0; i < dataSource.Count; i++) {
                var data = dataSource[i].GetHierarchicalData();
                treeList.AppendNode(data, dataSource[i].ParentID);
            }
            treeList.ExpandAll();
            treeList.EndUnboundLoad();
        }
    }
}
namespace BenchmarkingApp.Grid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<Row> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            base.SetUp(uiControl);
        }
        public sealed override void Benchmark() {
            grid.DataSource = dataSource;
        }
    }
}

namespace BenchmarkingApp.RadGrid.Bound {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<Row> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureListSource(ref dataSource, Configuration.Current.Rows);
            base.SetUp(uiControl);
        }
        public sealed override void Benchmark() {
            gridView.DataSource = dataSource;
        }
    }
}
namespace BenchmarkingApp.RadGrid.BoundHierarchy {
    using System.Collections.Generic;
    using BenchmarkingApp.Benchmarks.Data;

    [BenchmarkItem("Loading (Bound Hierarchy)", Configuration = "Huge")]
    public class Loading : LoadBase {
        List<HierarchicalRow> dataSource;
        public sealed override void SetUp(object uiControl) {
            Row.EnsureHierarchicalSource(ref dataSource, Configuration.Current.Rows, Configuration.Current.Levels);
            base.SetUp(uiControl);
        }
        public sealed override void Benchmark() {
            gridView.BeginUpdate();
            gridView.Relations.AddSelfReference(gridView.MasterTemplate, "ID", "ParentID");
            gridView.DataSource = dataSource;
            gridView.Columns["ParentId"].IsVisible = false;
            gridView.MasterTemplate.ExpandAll();
            gridView.EndUpdate();
        }
    }
}