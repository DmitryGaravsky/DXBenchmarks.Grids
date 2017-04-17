namespace BenchmarkingApp {
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class BenchmarkHostAttribute : Attribute {
        readonly string hostName;
        public BenchmarkHostAttribute(string hostName) {
            this.hostName = hostName;
        }
        public string Name {
            get { return hostName; }
        }
    }
    //
    public interface IBenchmarkHost {
        string Name { get; }
        object UIControl { get; }
    }
}