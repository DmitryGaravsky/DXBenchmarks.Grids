namespace BenchmarkingApp.Benchmarks.Data {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public enum Gender {
        Male, Female, Other
    }
    public class Row {
        static Random random = new Random(Configuration.Seed);
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
        public static void EnsureListSource(ref List<Row> dataSource, int rows) {
            if(dataSource == null && !dataSourcesCache.TryGetValue(rows, out dataSource)) {
                dataSource = new List<Row>(rows);
                dataSourcesCache.Add(rows, dataSource);
            }
            int start = dataSource.Count;
            if(start < rows) {
                for(int i = start; i < rows; i++)
                    dataSource.Add(Row.CreateRow(i));
            }
            else dataSource.RemoveRange(rows, dataSource.Count - rows);
        }
        readonly static IDictionary<int, List<HierarchicalRow>> hierarchicalDataSourcesCache = new Dictionary<int, List<HierarchicalRow>>();
        public static void EnsureHierarchicalSource(ref List<HierarchicalRow> dataSource, int rows, int levels) {
            if(dataSource == null && !hierarchicalDataSourcesCache.TryGetValue(rows, out dataSource)) {
                dataSource = new List<HierarchicalRow>(rows);
                hierarchicalDataSourcesCache.Add(rows, dataSource);
            }
            if(dataSource.Count != rows) {
                dataSource.Clear();
                int levelStep = (int)(Math.Log(rows, levels) + 0.5);
                int levelBegin = 0; int levelEnd = levelStep - 1;
                int parentLevelBegin = 0; int parentLevelEnd = 0;
                for(int i = 0; i < rows; i++) {
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
        internal static Type[] GetColumnTypes() {
            return new Type[] { 
                typeof(int), typeof(Guid), typeof(string), typeof(int), typeof(long), typeof(decimal), typeof(double), typeof(float), 
                typeof(string), typeof(string), typeof(bool), typeof(bool?), typeof(DateTime), typeof(DateTime?), typeof(Gender) };
        }
        internal object[] GetData() {
            return new object[] { ID, UID, SID, Age, Size, Price, Amount, Factor, Name, Notes, IsActive, Approved, Open, Resolved, Gender };
        }
        internal object GetData(int columnIndex) {
            switch(columnIndex) {
                case 0: return ID;
                case 1: return UID;
                case 2: return SID;
                case 3: return Age;
                case 4: return Size;
                case 5: return Price;
                case 6: return Amount;
                case 7: return Factor;
                case 8: return Name;
                case 9: return Notes;
                case 10: return IsActive;
                case 11: return Approved;
                case 12: return Open;
                case 13: return Resolved;
                case 14: return Gender;
            }
            throw new IndexOutOfRangeException();
        }
    }
    //
    public class HierarchicalRow : Row {
        HierarchicalRow(Row parent) {
            ParentID = (parent != null) ? parent.ID : -1;
        }
        [Display(Order = -1)]
        public int ParentID {
            get;
            private set;
        }
        public static HierarchicalRow CreateHierarchicalRow(HierarchicalRow parent, int seed) {
            return (HierarchicalRow)CreateRow(seed, () => new HierarchicalRow(parent));
        }
        internal static string[] GetHierarchicalColumns() {
            return new string[] { "ID", "ParentID", "UID", "SID", "Age", "Size", "Price", "Amount", "Factor", "Name", "Notes", "IsActive", "Approved", "Open", "Resolved", "Gender" };
        }
        internal object[] GetHierarchicalData() {
            return new object[] { ID, ParentID, UID, SID, Age, Size, Price, Amount, Factor, Name, Notes, IsActive, Approved, Open, Resolved, Gender };
        }
    }
}