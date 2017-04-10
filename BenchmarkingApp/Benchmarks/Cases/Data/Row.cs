namespace BenchmarkingApp.Benchmarks.Data {
    using System;
    using System.Collections.Generic;

    public enum Gender {
        Male, Female, Other
    }
    public class Row {
        static Random random = new Random(10000);
        protected Row() { }
        //
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
        //
        readonly static IDictionary<int, List<Row>> dataSourcesCache = new Dictionary<int, List<Row>>();
        public static void EnsureListSource(ref List<Row> dataSource, int n = 10000) {
            if(dataSource == null && !dataSourcesCache.TryGetValue(n, out dataSource)) {
                dataSource = new List<Row>(n);
                dataSourcesCache.Add(n, dataSource);
            }
            int start = dataSource.Count;
            if(start < n) {
                for(int i = start; i < n; i++)
                    dataSource.Add(Row.CreateRow(i));
            }
            else dataSource.RemoveRange(n, dataSource.Count - n);
        }
        readonly static IDictionary<int, List<HierarchicalRow>> hierarchicalDataSourcesCache = new Dictionary<int, List<HierarchicalRow>>();
        public static void EnsureHierarchicalSource(ref List<HierarchicalRow> dataSource, int n = 10000, int level = 5) {
            if(dataSource == null && !hierarchicalDataSourcesCache.TryGetValue(n, out dataSource)) {
                dataSource = new List<HierarchicalRow>(n);
                hierarchicalDataSourcesCache.Add(n, dataSource);
            }
            if(dataSource.Count != n) {
                dataSource.Clear();
                int levelStep = (int)(Math.Log(n, level) + 0.5);
                int levelBegin = 0; int levelEnd = levelStep - 1;
                int parentLevelBegin = 0; int parentLevelEnd = 0;
                for(int i = 0; i < n; i++) {
                    if(i == levelEnd) {
                        parentLevelBegin = levelBegin;
                        parentLevelEnd = levelEnd;
                        levelEnd = levelBegin + (levelEnd - levelBegin) * levelStep;
                        levelBegin = i;
                    }
                    HierarchicalRow parent = null;
                    if(parentLevelBegin >= 0 && parentLevelEnd > 0) {
                        int parentLevelSize = parentLevelEnd - parentLevelBegin;
                        parent = dataSource[parentLevelBegin + (i - levelBegin) % parentLevelSize];
                    }
                    dataSource.Add(HierarchicalRow.CreateHierarchicalRow(parent, i));
                }
            }
        }
        public static Row CreateRow(int seed, Func<Row> createRow = null) {
            createRow = createRow ?? new Func<Row>(() => new Row());
            Row r = createRow();
            r.ID = seed;
            r.UID = new Guid(seed.UID());
            r.SID = random.String(6);
            //
            r.Age = random.Next(16, 100);
            r.Size = random.Next(0, int.MaxValue);
            r.Price = Math.Round(new decimal(random.NextDouble() * 10000.0), 2);
            r.Amount = Math.Round(115.0 + random.NextDouble() * 100.0, 4);
            r.Factor = (float)Math.Round(15.0 + random.NextDouble() * 10.0, 2);
            //
            r.Name = random.String(random.Next(6, 12));
            r.Notes = random.LoremIpsum();
            //
            r.IsActive = seed % 17 != 0;
            if(seed % 23 == 0)
                r.Approved = random.Next(10000) % 2 != 0;
            //
            r.Open = new DateTime(2000, 1, 1).AddDays(random.Next(1000));
            if(seed % 7 == 0)
                r.Resolved = r.Open.AddDays(random.Next(50));
            //
            r.Gender = random.Next(10000) % 2 == 0 ? Gender.Male : Gender.Female;
            if(r.Gender == Gender.Male && seed % 7 == 0)
                r.Gender = Gender.Other;
            return r;
        }
        internal static string[] GetColumns() {
            return new string[] { "ID", "UID", "SID", "Age", "Size", "Price", "Amount", "Factor", "Name", "Notes", "IsActive", "Approved", "Open", "Resolved", "Gender" };
        }
        internal object[] GetData() {
            return new object[] { ID, UID, SID, Age, Size, Price, Amount, Factor, Name, Notes, IsActive, Approved, Open, Resolved, Gender };
        }
    }
    public class HierarchicalRow : Row {
        HierarchicalRow(Row parent) {
            if(parent != null) {
                ParentID = parent.ID;
                ParentSID = parent.SID;
                ParentUID = parent.UID;
            }
            else ParentID = -1;
        }
        public int ParentID {
            get;
            private set;
        }
        public string ParentSID {
            get;
            private set;
        }
        public Guid ParentUID {
            get;
            private set;
        }
        public static HierarchicalRow CreateHierarchicalRow(HierarchicalRow parent, int seed) {
            return (HierarchicalRow)CreateRow(seed, () => new HierarchicalRow(parent));
        }
        internal static string[] GetHierarchicalColumns() {
            return new string[] { "ID", "ParentID", "UID", "ParentUID", "SID", "ParentSID", "Age", "Size", "Price", "Amount", "Factor", "Name", "Notes", "IsActive", "Approved", "Open", "Resolved", "Gender" };
        }
        internal object[] GetHierarchicalData() {
            return new object[] { ID, ParentID, UID, ParentUID, SID, ParentSID, Age, Size, Price, Amount, Factor, Name, Notes, IsActive, Approved, Open, Resolved, Gender };
        }
    }
}