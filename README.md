# DXBenchmarks.Grids
Benchmarking app for DevExpress Grids (WinForms)

App for measuring performance of DevExpress WinForms GridControl and TreeListControl.

### Usage:

You can run this app either in *interactive* or in *batch* mode. 

### Batch mode:

To run the app in batch mode use the command line arguments:

    BenchmarkingApp cfg=Deep

or

    BenchmarkingApp Workloads/Sorting.workload

The `*.workload` file contains bencmarking workload declaration. For example:

    Configuration=Typical

    BenchmarkingApp.InMemory.Filtering_Price
    BenchmarkingApp.Grid.Bound.Filtering_Price
    BenchmarkingApp.Tree.Bound.Filtering_Price


You can specify the bencmarking configuration (`Typical`, `Huge`, `Deep`):

    Configuration=Typical
    config=huge
    cfg=deep

and, optionally, define the specific benchmarking fixtures (you can use either fixture's
type name or fixture's description).

The results will be available in `*.results` files.

Hint: use the predefined `RunTypical.cmd` script (it requires administrative permissions due to using NGEN).

### Testing Data:

    public class Row {
        public int ID { get; set; }
        public Guid UID { get; set; }
        public string SID { get; set; }
        //
        public int Age { get; set; }
        public long Size { get; set; }
        public decimal Price { get; set; }
        public double Amount { get; set; }
        public float Factor { get; set; }
        //
        public string Name { get; set; }
        public string Notes { get; set; }
        //
        public bool IsActive { get; set; }
        public bool? Approved { get; set; }
        //
        public DateTime Open { get; set; }
        public DateTime? Resolved { get; set; }
        //
        public Gender Gender { get; set; }
    }

    public class HierarchicalRow : Row {
        public int ParentID { get; }
    }

All the data are generated randomly and looks like real.

### Test categories:

 - loading rows/tree nodes by assigning the data source(Bound) or via appending nodes in cycle(Unbound)
 - sorting by ordered/random data (single column)
 - sorting by clustered/unclustered data (multiple columns)
 - filtering by different data types with simple and complex filters
 - grouping by unique or typicaly distributed values
 - searching by specific column and by the all columns
 - some specific operations

All the above tests can be performed either on flat list or on self-referenced hierarchy(ID->ParentID).

## Measuring technique notes: 

  Some vendors use the “postponed” data-operation execution which ends immediately after initializing,  but then blocks the UI until ending.
  So our measuring technique takes into account only *the UI response time*. 
  The UI response time  is a period (in milliseconds) from operation start to the moment when the application’s UI backs into the responsive state. 
  As the indicator, we have used the message pump – we are awaiting the recovering of the normal messages processing (idle-state).

