namespace BenchmarkingApp {
    using System;
    using System.ComponentModel.DataAnnotations;

    public class HostItem {
        readonly Lazy<IBenchmarkHost> hostCore;
        public HostItem(Type type) {
            this.Type = type;
            this.Name = GetName(type);
            this.hostCore = new Lazy<IBenchmarkHost>(() => Activator.CreateInstance(Type) as IBenchmarkHost);
        }
        public string Name {
            get;
            private set;
        }
        [Display(AutoGenerateField = false)]
        public bool IsTargetCreated {
            get { return hostCore.IsValueCreated; }
        }
        [Display(AutoGenerateField = false)]
        public IBenchmarkHost Target {
            get { return hostCore.Value; }
        }
        [Display(AutoGenerateField = false)]
        public Type Type {
            get;
            private set;
        }
        //
        static string GetName(Type type) {
            var attributes = type.GetCustomAttributes(typeof(BenchmarkHostAttribute), true);
            return (attributes.Length > 0) ? ((BenchmarkHostAttribute)attributes[0]).Name : type.Name;
        }
    }
}