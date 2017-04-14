namespace BenchmarkingApp.Benchmarks.Data {
    public abstract class Configuration {
        public static readonly Configuration Current = Huge.Instance;
        protected Configuration() { }
        //
        public const int Seed = 10000;
        public abstract int Rows { get; }
        public abstract int Levels { get; }
        public abstract string Name { get; }
        //
        #region Configurations
        readonly char[] splitChars = new char[] { ';', ',' };
        internal bool Match(string configuration) {
            var items = !string.IsNullOrEmpty(configuration) ?
                configuration.ToLowerInvariant().Split(splitChars, System.StringSplitOptions.RemoveEmptyEntries) : new string[] { };
            return MatchCore(items);
        }
        protected virtual bool MatchCore(string[] items) {
            return true;
        }
        sealed class Typical : Configuration {
            readonly internal static Configuration Instance = new Typical();
            public sealed override string Name {
                get { return "Typical"; }
            }
            public sealed override int Rows {
                get { return 10000; }
            }
            public sealed override int Levels {
                get { return 5; }
            }
        }
        sealed class Huge : Configuration {
            readonly internal static Configuration Instance = new Huge();
            public sealed override string Name {
                get { return "Huge"; }
            }
            public sealed override int Rows {
                get { return 500000; }
            }
            public sealed override int Levels {
                get { return 5; }
            }
            protected sealed override bool MatchCore(string[] items) {
                return System.Array.IndexOf(items, "huge") != -1;
            }
        }
        sealed class Deep : Configuration {
            readonly internal static Configuration Instance = new Deep();
            public sealed override string Name {
                get { return "Deep"; }
            }
            public sealed override int Rows {
                get { return 100000; }
            }
            public sealed override int Levels {
                get { return 100; }
            }
            protected sealed override bool MatchCore(string[] items) {
                return System.Array.IndexOf(items, "deep") != -1;
            }
        }
        #endregion Configurations
    }
}