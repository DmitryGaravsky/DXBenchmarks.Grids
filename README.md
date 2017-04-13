# DXBenchmarks.Grids
Benchmarking app for DevExpress Grids (WinForms)

App for measuring performance of DevExpress WinForms GridControl and TreeListControl.


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

### The resulting tables (17.1 vs v16.2, **10k data items**):

1) i7-4702HQ 2.2 GHz, 16 Gb, Win8.1x64 

	| Grid		| Tree(Bound)	| Tree(Unbound)	| Tree(Bound Hierarchy)	| Tree(Unbound Hierarchy)
--- | ---:| ---:| ---:| ---:| ---:|
Filter by Price		| 1.25		| 1.80		| 1.30		| 30.79			| 1.54
Filter by Approved		| 1.14		| 2.05		| 1.52		| 3.28			| 1.60
Filter by Price and Approved	| 1.11		| 1.77		| 1.71		| 1.76			| 1.52
Search by Notes		| 1.02		| 1.13		| 1.16		| 1.13			| 1.14
Search by all columns		| 1.01		| 1.42		| 1.45		| 1.39			| 1.50
Sort by ID			| 1.33		| 4.50		|4.62		|2.25			|2.20
Sort by Name			| 1.20		| 2.83		| 2.30		| 2.00			| 1.69
Sort by ID and Name		| 1.14		| 1.56		| 2.07		| 2.04			| 1.79
Sort by Age and Factor	| 1.13		| 1.61		| 2.26		| 1.89			| 1.95



