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

 - sorting by ordered/random data (single column)
 - sorting by clustered/unclustered data (multiple columns)
 - filtering by different data types with simple and complex filters
 - searching by specific column and by the all columns
 - loading tree nodes by assigning the data source(Bound) or via appending nodes in cycle(Unbound)

All the above tests can be performed on flat list or on self-referenced hierarchy(ID->ParentID).
 
### The results (17.1 vs 16.2)

In the 17.1 the overall performance of data-aware operations in the Tree List Control was improved:

Sorting is **up to 8 times faster**.  
Filtering is **up to 1.75 times faster**.  
Searching is **up to 1.4 times faster**.  
Loading nodes tree is **up to 2.6 times faster**.  

### Benchmarking notes

1. The results above are measured by using the following PC configuration:  
   Intel Core i7-4702HQ 2.2 GHz, 16 Gb, Win8.1x64)
2. Here are [the bencmarking raw data and graphs](https://goo.gl/zCM6zT)

Why the Grid Control faster?
 - the TreeList and Grid controls uses different data-model and data processing approaches.
 - the Grid control is based on List' processing, so the Grid can use the most aggressive optimizations (for example parallel processing or indexed data-access)
