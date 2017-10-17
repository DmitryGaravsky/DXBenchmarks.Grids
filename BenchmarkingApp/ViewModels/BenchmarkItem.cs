namespace BenchmarkingApp {
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class BenchmarkItem {
        readonly Lazy<IBenchmarkItem> itemCore;
        public BenchmarkItem(Type type) {
            this.Type = type;
            this.Name = GetName(type);
            this.itemCore = new Lazy<IBenchmarkItem>(() => Activator.CreateInstance(Type) as IBenchmarkItem);
        }
        public string Name {
            get;
            private set;
        }
        [Display(AutoGenerateField = false)]
        public Type Type {
            get;
            private set;
        }
        [Display(AutoGenerateField = false)]
        public bool IsTargetCreated {
            get { return itemCore.IsValueCreated; }
        }
        [Display(AutoGenerateField = false)]
        public IBenchmarkItem Target {
            get { return itemCore.Value; }
        }
        //
        static string GetName(Type type) {
            var attributes = type.GetCustomAttributes(typeof(BenchmarkItemAttribute), true);
            return (attributes.Length > 0) ? ((BenchmarkItemAttribute)attributes[0]).Name : type.Name;
        }
        public static bool IsBenchmarkFor(Type type, string hostName) {
            var attributes = type.GetCustomAttributes(typeof(BenchmarkHostAttribute), true);
            return attributes.Length > 0 && ((BenchmarkHostAttribute)attributes[0]).Name == hostName;
        }
        public static int GetWarmUpCounter(Type type, int defaultValue = 10) {
            var attributes = type.GetCustomAttributes(typeof(BenchmarkItemAttribute), true);
            return (attributes.Length > 0) ? ((BenchmarkItemAttribute)attributes[0]).WarmUp.GetValueOrDefault(defaultValue) : defaultValue;
        }
        public static int GetBenchmarkCounter(Type type, int defaultValue = 10) {
            var attributes = type.GetCustomAttributes(typeof(BenchmarkItemAttribute), true);
            return (attributes.Length > 0) ? ((BenchmarkItemAttribute)attributes[0]).Benchmark.GetValueOrDefault(defaultValue) : defaultValue;
        }
        //
        public static bool Match(Type type, Benchmarks.Data.Configuration configuration) {
            return typeof(IBenchmarkItem).IsAssignableFrom(type) && MatchCore(type, configuration);
        }
        static bool MatchCore(System.Type type, Benchmarks.Data.Configuration configuration) {
            var attributes = type.GetCustomAttributes(typeof(BenchmarkItemAttribute), true);
            return (attributes.Length > 0) ? configuration.Match(((BenchmarkItemAttribute)attributes[0]).Configuration) : true;
        }
        //
        public static BenchmarkItem[] GetActive(BenchmarkItem[] benchmarks, string[] args) {
            args = Benchmarks.Data.Configuration.Parse(args);
            if(args != null && args.Length > 0) {
                return benchmarks.Where(b => IsSpecificBenchmark(b, args))
                    .OrderBy(b => GetBenchmarkIndex(b, args))
                    .ToArray();
            }
            return benchmarks;
        }
        static bool IsSpecificBenchmark(BenchmarkItem current, string[] args) {
            return
                Array.IndexOf(args, current.Type.FullName.ToLowerInvariant()) != -1 ||
                Array.IndexOf(args, current.Name.ToLowerInvariant()) != -1;
        }
        static int GetBenchmarkIndex(BenchmarkItem current, string[] args) {
            int typeNameIndex = Array.IndexOf(args, current.Type.FullName.ToLowerInvariant());
            return (typeNameIndex != -1) ? typeNameIndex : Array.IndexOf(args, current.Name.ToLowerInvariant());
        }
    }
}