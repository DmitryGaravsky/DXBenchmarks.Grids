namespace BenchmarkingApp {
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class BenchmarkItemAttribute : Attribute {
        readonly string name;
        readonly int? warmUp;
        public BenchmarkItemAttribute(string name) {
            this.name = name;
        }
        public BenchmarkItemAttribute(string name, int warmUp)
            : this(name) {
            this.warmUp = warmUp;
        }
        public string Name {
            get { return name; }
        }
        public int? WarmUp {
            get { return warmUp; }
        }
        public int? Benchmark {
            get;
            set;
        }
    }
    //
    public interface IBenchmarkItem {
        void SetUp(object uiControl);
        void TearDown(object uiControl);
        void Benchmark();
    }
}