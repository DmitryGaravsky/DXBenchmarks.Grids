namespace BenchmarkingApp.Tree.Bound {
    [BenchmarkItem("Mass Update, SID (Bound)", Configuration = "Huge")]
    public class MassUpdate_SID : MassUpdateBoundBase {
        public sealed override void Benchmark() {
            UpdateSID_AddSign();
            UpdateSID_RemoveSign();
        }
    }
    [BenchmarkItem("Mass Update, Size (Bound)")]
    public class MassUpdate_Size : MassUpdateBoundBase {
        public sealed override void Benchmark() {
            UpdateSize_Scale();
            UpdateSize_Descale();
        }
    }
}

namespace BenchmarkingApp.Tree.BoundHierarchy {
    [BenchmarkItem("Mass Update, SID (Bound Hierarchy)", Configuration = "Deep;Huge")]
    public class MassUpdate_SID : MassUpdateBoundBase {
        public sealed override void Benchmark() {
            UpdateSID_AddSign();
            UpdateSID_RemoveSign();
        }
    }
    [BenchmarkItem("Mass Update, Size (Bound Hierarchy)")]
    public class MassUpdate_Size : MassUpdateBoundBase {
        public sealed override void Benchmark() {
            UpdateSize_Scale();
            UpdateSize_Descale();
        }
    }
}

namespace BenchmarkingApp.Tree.Unbound {
    [BenchmarkItem("Mass Update, SID (Unbound)", Configuration = "Huge")]
    public class MassUpdate_SID : MassUpdateUnboundBase {
        public sealed override void Benchmark() {
            UpdateSID_AddSign();
            UpdateSID_RemoveSign();
        }
    }
    [BenchmarkItem("Mass Update, Size (Unbound)")]
    public class MassUpdate_Size : MassUpdateUnboundBase {
        public sealed override void Benchmark() {
            UpdateSize_Scale();
            UpdateSize_Descale();
        }
    }
}

namespace BenchmarkingApp.Tree.UnboundHierarchy {
    [BenchmarkItem("Mass Update, SID (Unbound Hierarchy)", Configuration = "Deep;Huge")]
    public class MassUpdate_SID : MassUpdateUnboundBase {
        public sealed override void Benchmark() {
            UpdateSID_AddSign();
            UpdateSID_RemoveSign();
        }
    }
    [BenchmarkItem("Mass Update, Size (Unbound Hierarchy)")]
    public class MassUpdate_Size : MassUpdateUnboundBase {
        public sealed override void Benchmark() {
            UpdateSize_Scale();
            UpdateSize_Descale();
        }
    }
}

namespace BenchmarkingApp.Grid.Bound {
    [BenchmarkItem("Mass Update, SID", Configuration = "Huge")]
    public class MassUpdate_SID : MassUpdateBase {
        public sealed override void Benchmark() {
            var colSID = gridView.Columns["SID"];
            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                string sid = (string)gridView.GetRowCellValue(i, colSID);
                gridView.SetRowCellValue(i, colSID, "#" + sid);
            }
            gridView.EndUpdate();

            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                string sid = (string)gridView.GetRowCellValue(i, colSID);
                gridView.SetRowCellValue(i, colSID, sid.Substring(1));
            }
            gridView.EndUpdate();
        }
    }
    [BenchmarkItem("Mass Update, Size")]
    public class MassUpdate_Size : MassUpdateBase {
        public sealed override void Benchmark() {
            var colSize = gridView.Columns["Size"];
            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                long size = (long)gridView.GetRowCellValue(i, colSize);
                gridView.SetRowCellValue(i, colSize, size + size);
            }
            gridView.EndUpdate();

            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                long size = (long)gridView.GetRowCellValue(i, colSize);
                gridView.SetRowCellValue(i, colSize, size / 2);
            }
            gridView.EndUpdate();
        }
    }
}

namespace BenchmarkingApp.RadGrid.Bound {
    [BenchmarkItem("Mass Update, SID", Configuration = "Huge")]
    public class MassUpdate_ID : MassUpdateBase {
        public sealed override void Benchmark() {
            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                string sid = (string)gridView.Rows[i].Cells["SID"].Value;
                gridView.Rows[i].Cells["SID"].Value = "#" + sid;
            }
            gridView.EndUpdate();
            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                string sid = (string)gridView.Rows[i].Cells["SID"].Value;
                gridView.Rows[i].Cells["SID"].Value = sid.Substring(1);
            }
            gridView.EndUpdate();
        }
    }
    [BenchmarkItem("Mass Update, Size")]
    public class MassUpdate_Size : MassUpdateBase {
        public sealed override void Benchmark() {
            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                long size = (long)gridView.Rows[i].Cells["Size"].Value;
                gridView.Rows[i].Cells["Size"].Value = size + size;
            }
            gridView.EndUpdate();
            gridView.BeginUpdate();
            for(int i = 0; i < gridView.RowCount; i++) {
                long size = (long)gridView.Rows[i].Cells["Size"].Value;
                gridView.Rows[i].Cells["Size"].Value = size / 2;
            }
            gridView.EndUpdate();
        }
    }
}