namespace BenchmarkingApp.Benchmarks.Data {
    using System;
    using System.Linq;

    public abstract class Configuration {
        static Configuration currentConfiguration;
        public static Configuration Current {
            get {
                if(currentConfiguration == null)
                    return Configuration.Typical.Instance;
                return currentConfiguration;
            }
        }
        protected Configuration() { }
        //
        public const int Seed = 10000;
        public abstract int Rows { get; }
        public abstract int Levels { get; }
        public abstract int WatchDog(int counter);
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
            public sealed override int WatchDog(int counter) {
                return counter * 2;
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
            public sealed override int WatchDog(int counter) {
                return counter;
            }
            protected sealed override bool MatchCore(string[] items) {
                return Array.IndexOf(items, "huge") != -1;
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
            public sealed override int WatchDog(int counter) {
                return counter;
            }
            protected sealed override bool MatchCore(string[] items) {
                return Array.IndexOf(items, "deep") != -1;
            }
        }
        #endregion Configurations
        internal static string[] Parse(string[] args) {
            string cfgString = args.FirstOrDefault(a => IsConfigurationLine(a));
            if(cfgString != null) {
                cfgString = cfgString.Replace("cfg=", string.Empty);
                cfgString = cfgString.Replace("config=", string.Empty);
                cfgString = cfgString.Replace("configuration=", string.Empty);
                switch(cfgString) {
                    case "typical":
                        currentConfiguration = Typical.Instance;
                        break;
                    case "huge":
                        currentConfiguration = Huge.Instance;
                        break;
                    case "deep":
                        currentConfiguration = Deep.Instance;
                        break;
                    default:
                        currentConfiguration = null;
                        break;
                }
                args = args.Where(a => !IsConfigurationLine(a)).ToArray();
            }
            return args;
        }
        static bool IsConfigurationLine(string line) {
            return 
                line.StartsWith("cfg=") || 
                line.StartsWith("config=") || 
                line.StartsWith("configuration=");
        }
    }
}