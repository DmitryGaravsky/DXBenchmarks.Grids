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

Hint: use the predefined `RunTypical.cmd` script.

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

### Test categories:
 - filtering by different data types
 - searching by specific and by all columns
 - sorting by ordered/random data (single column)
 - sorting by clustered/unclustered data (multiple columns)

### The results (17.1 vs 16.2)

In the 17.1 the overall performance of data-aware operations in the Tree List Control was improved:

Sorting is **up to 8 times faster**.  
Filtering is **up to 1.75 times faster**.  
Searching is **up to 1.4 times faster**.  
Loading nodes tree is **up to 2.6 times faster**.  

The resulta above are measured by using following PC configuration:
 - Intel Core i7-4702HQ 2.2 GHz, 16 Gb, Win8.1x64)
 - Here are [the bencmarking raw data and graphs](https://goo.gl/zCM6zT)

Performance notes, or Why the Grid Control faster:  
 - the TreeList and Grid controls uses different data-model and data processing approaches.
 - the Grid controls is based on list-processing, so the Grid can use the most aggressive optimizations (for example parallel processing)
