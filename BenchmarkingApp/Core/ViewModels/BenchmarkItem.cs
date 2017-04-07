namespace BenchmarkingApp {
    using System;
    using System.ComponentModel.DataAnnotations;

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
    }
}