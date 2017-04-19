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

### The resulting tables (17.1 vs v16.2, **Typical configuration, 10k data items, up to 5 levels**):

1) i7-4702HQ 2.2 GHz, 16 Gb, Win8.1x64 

Test | Grid		| Tree(Bound)	| Tree(Unbound)	| Tree(Bound Hierarchy)	| Tree(Unbound Hierarchy)
--- | ---:| ---:| ---:| ---:| ---:|
Filter by Price		| 1.25		| 1.80		| 1.30		| 30.79			| 1.54
Filter by Approved		| 1.14		| 2.05		| 1.52		| 3.28			| 1.60
Filter by Price and Approved	| 1.11		| 1.77		| 1.71		| 1.76			| 1.52
Filter by Amount and Open	| 1.10		| 1.08		| 1.35		| 1.27			| 1.30
Search by Notes		| 1.02		| 1.13		| 1.16		| 1.13			| 1.14
Search by all columns		| 1.01		| 1.42		| 1.45		| 1.39			| 1.50
Sort by ID			| 1.33		| 4.50		|4.62		|2.25			|2.20
Sort by Name			| 1.20		| 2.83		| 2.30		| 2.00			| 1.69
Sort by ID and Name		| 1.14		| 1.56		| 2.07		| 2.04			| 1.79
Sort by Age and Factor	| 1.13		| 1.61		| 2.26		| 1.89			| 1.95

2) i7-4710HQ 2.5 GHz, 16 Gb, Win8.1x64 

Test | Grid		| Tree(Bound)	| Tree(Unbound)	| Tree(Bound Hierarchy)	| Tree(Unbound Hierarchy)
--- | ---:| ---:| ---:| ---:| ---:|
Filter by Price	| 1.13	| 1.75	| 1.18	| 31.20	| 1.79
Filter by Approved	| 1.17	| 1.68	| 1.76	| 3.48	| 1.50
Filter by Price and Approved	| 1.13	| 1.85	| 2.10	| 1.92	| 1.56
Filter by Amount and Open	| 1.05	| 1.08	| 1.28	| 1.50	| 1.24
Search by Notes	| 1.02	| 1.20	| 1.13	| 1.15	| 1.16
Search by all columns	| 1.01	| 1.43	| 1.33	| 1.30	| 1.44
Sort by ID	| 1.17	| 7.08	| 4.07	| 2.50	| 2.67
Sort by Name	| 1.10	| 2.92	| 2.36	| 2.18	| 1.60
Sort by ID and Name	| 2.67	| 1.89	| 2.38	| 2.17	| 1.78
Sort by Age and Factor	| 1.14	| 1.63	| 2.16	| 2.14	| 1.95
