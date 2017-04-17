namespace BenchmarkingApp {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.POCO;

    public class MainViewModel {
        public virtual HostItem ActiveHostItem {
            get;
            set;
        }
        public virtual List<HostItem> HostItems {
            get;
            protected set;
        }
        protected virtual void OnHostItemsChanged() {
            ActiveHostItem = HostItems.FirstOrDefault();
        }
        protected virtual void OnActiveHostItemChanged() {
            var hostService = this.GetService<IHostService>();
            if(hostService != null)
                hostService.Show(ActiveHostItem.Target);
            //
            if(ActiveHostItem == null)
                BenchmarkItems = new List<BenchmarkItem>();
            else {
                BenchmarkItems = benchmarks
                    .Where(x => BenchmarkItem.IsBenchmarkFor(x.Type, ActiveHostItem.Name))
                    .ToList();
            }
        }
        //
        public virtual BenchmarkItem ActiveBenchmarkItem {
            get;
            set;
        }
        public virtual List<BenchmarkItem> BenchmarkItems {
            get;
            protected set;
        }
        protected virtual void OnBenchmarkItemsChanged() {
            ActiveBenchmarkItem = BenchmarkItems.FirstOrDefault();
        }
        protected virtual void OnActiveBenchmarkItemChanged() {
            this.cold = null;
            this.warmUpResult = null;
            this.coldRunResult = null;
            this.result = this.worst = null;
            this.RaisePropertyChanged(x => x.Result);
            UpdateCommands();
        }
        Type[] types;
        BenchmarkItem[] benchmarks;
        public void Load() {
            this.types = typeof(MainViewModel).Assembly.GetTypes();
            this.benchmarks = types
                .Where(t => t.IsPublic && !t.IsAbstract && !t.IsInterface && BenchmarkItem.Match(t, Benchmarks.Data.Configuration.Current))
                .Select(x => new BenchmarkItem(x)).ToArray();
            HostItems = types
                .Where(t => t.IsPublic && !t.IsAbstract && typeof(IBenchmarkHost).IsAssignableFrom(t) && !t.IsInterface)
                .Select(x => new HostItem(x)).ToList();
        }
        //
        public void LoadAndRunBatch(string[] args) {
            args = Benchmarks.Data.Configuration.Parse(args);
            Load();
            BenchmarkItem[] activeBenchmarks = benchmarks;
            if(args != null && args.Length > 0) {
                activeBenchmarks = benchmarks.Where(b => IsSpecificBenchmark(b, args))
                    .OrderBy(b => GetBenchmarkIndex(b, args))
                    .ToArray();
            }
            int total = 0;
            for(int i = 0; i < activeBenchmarks.Length; i++) {
                var current = activeBenchmarks[i];
                ActiveHostItem = HostItems.FirstOrDefault(host => BenchmarkItem.IsBenchmarkFor(current.Type, host.Name));
                if(ActiveHostItem != null) {
                    ActiveBenchmarkItem = current;
                    Run();
                    total++;
                }
            }
            OnBatchRunComplete(total);
        }
        int GetBenchmarkIndex(BenchmarkItem current, string[] args) {
            int typeNameIndex = Array.IndexOf(args, current.Type.FullName.ToLowerInvariant());
            return (typeNameIndex != -1) ? typeNameIndex : Array.IndexOf(args, current.Name.ToLowerInvariant());
        }
        bool IsSpecificBenchmark(BenchmarkItem current, string[] args) {
            return
                Array.IndexOf(args, current.Type.FullName.ToLowerInvariant()) != -1 ||
                Array.IndexOf(args, current.Name.ToLowerInvariant()) != -1;
        }
        void OnBatchRunComplete(int total) {
            var messageService = this.GetRequiredService<IMessageBoxService>();
            if(messageService != null)
                messageService.ShowMessage("Total benchmarks complete:" + total.ToString());
        }
        //
        readonly Stopwatch stopwatch = new Stopwatch();
        protected bool HasBenchmark {
            get { return ActiveBenchmarkItem != null; }
        }
        void UpdateCommands() {
            this.RaiseCanExecuteChanged(x => x.ColdRun());
            this.RaiseCanExecuteChanged(x => x.WarmUp());
            this.RaiseCanExecuteChanged(x => x.Run());
            System.Windows.Forms.Application.DoEvents();
        }
        //
        long? warmUpResult;
        protected bool IsWarmedUp {
            get { return warmUpResult.HasValue; }
        }
        long? coldRunResult;
        protected bool IsCold {
            get { return !coldRunResult.HasValue; }
        }
        bool? cold;
        public bool CanColdRun() {
            return HasBenchmark && cold.GetValueOrDefault(true) && running == 0;
        }
        public void ColdRun() {
            var target = ActiveBenchmarkItem.Target;
            var uiControl = ActiveHostItem.Target.UIControl;
            target.SetUp(uiControl);
            try {
                stopwatch.Restart();
                target.Benchmark();
                stopwatch.Stop();
                coldRunResult = stopwatch.ElapsedMilliseconds;
                cold = false;
            }
            finally { target.TearDown(uiControl); }
            this.RaisePropertyChanged(x => x.Result);
            this.RaiseCanExecuteChanged(x => x.ColdRun());
        }
        public bool CanWarmUp() {
            return HasBenchmark && running == 0;
        }
        public void WarmUp() {
            if(IsCold) ColdRun();
            int warmUpCounter = BenchmarkItem.GetWarmUpCounter(ActiveBenchmarkItem.Type);
            var target = ActiveBenchmarkItem.Target;
            var uiControl = ActiveHostItem.Target.UIControl;
            stopwatch.Reset();
            for(int i = 0; i < warmUpCounter; i++) {
                target.SetUp(uiControl);
                try {
                    stopwatch.Start();
                    target.Benchmark();
                    stopwatch.Stop();
                }
                finally { target.TearDown(uiControl); }
            }
            warmUpResult = Math.Max(1, stopwatch.ElapsedMilliseconds / warmUpCounter);
            this.RaisePropertyChanged(x => x.Result);
            this.RaiseCanExecuteChanged(x => x.Run());
        }
        public bool CanRun() {
            return HasBenchmark && running == 0;
        }
        int running;
        public void Run() {
            running++;
            PrepareRun();
            UpdateCommands();
            if(!IsWarmedUp) WarmUp();
            // Prepare
            var target = ActiveBenchmarkItem.Target;
            var uiControl = ActiveHostItem.Target.UIControl;
            int counter = BenchmarkItem.GetBenchmarkCounter(ActiveBenchmarkItem.Type, Math.Max(10, 1000 / (int)warmUpResult.Value));
            long[] results = new long[counter];
            long low = warmUpResult.Value - Math.Min(warmUpResult.Value / 3, Math.Max(5, warmUpResult.Value / 20));
            long hi = warmUpResult.Value + Math.Min(warmUpResult.Value / 3, Math.Max(5, warmUpResult.Value / 20));
            if(low == hi) {
                low--;
                hi++;
            }
            worst = 0;
            int watchDog = Math.Max(counter * 2, 50);
            while(counter > 0 && (0 < watchDog--)) {
                // Run
                target.SetUp(uiControl);
                try {
                    stopwatch.Restart();
                    target.Benchmark();
                    stopwatch.Stop();
                }
                finally { target.TearDown(uiControl); }
                // Continue
                long current = stopwatch.ElapsedMilliseconds;
                worst = Math.Max(current, worst.Value);
                if(current > low && current < hi)
                    results[--counter] = current;
            }
            int actualResultsCount = results.Where(r => r != 0).Count();
            // Check results completeness
            if(watchDog <= 0 && actualResultsCount < results.Length / 2) {
                while(counter > 0) {
                    // Run Ever!
                    target.SetUp(uiControl);
                    try {
                        stopwatch.Restart();
                        target.Benchmark();
                        stopwatch.Stop();
                    }
                    finally { target.TearDown(uiControl); }
                    long current = stopwatch.ElapsedMilliseconds;
                    worst = Math.Max(current, worst.Value);
                    results[--counter] = current;
                }
                actualResultsCount = results.Length;
            }
            // Calc
            result = (long)Math.Ceiling((double)results.Sum() / (double)actualResultsCount);
            this.RaisePropertyChanged(x => x.Result);
            running--;
            UpdateCommands();
            LogResults();
            CopyToClipboard();
        }
        static void PrepareRun() {
            System.Windows.Forms.Application.DoEvents();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        void LogResults() {
            var log = this.GetService<ILogService>();
            if(log != null) {
                string name = ActiveHostItem.Name + "(" + Benchmarks.Data.Configuration.Current.Name + ") - " + ActiveBenchmarkItem.Name;
                log.Log("[" + name + "] " + Result);
            }
        }
        // Title
        const string name = "Benckmarking App for DevExpress WinForms Grids";
        public string Title {
            get { return name + " (" + Benchmarks.Data.Configuration.Current.Name + " Configuration)"; }
        }
        // Result
        long? result, worst;
        public string Result {
            get {
                if(!IsCold) {
                    string resultStr = "Cold Run(ms): " + coldRunResult.Value.ToString();
                    if(warmUpResult.HasValue)
                        resultStr += " Warm Up(AVG,ms): " + warmUpResult.Value.ToString();
                    if(result.HasValue)
                        resultStr += " Result(AVG,ms): " + result.Value.ToString();
                    if(worst.HasValue)
                        resultStr += " Worst(ms): " + worst.Value.ToString();
                    return resultStr;
                }
                return string.Empty;
            }
        }
        #region Clipboard
        protected string TabulatedResult {
            get {
                if(!IsCold) {
                    string tab = '\t'.ToString();
                    string tabulated = coldRunResult.Value.ToString();
                    if(warmUpResult.HasValue)
                        tabulated += tab + warmUpResult.Value.ToString();
                    if(result.HasValue)
                        tabulated += tab + result.Value.ToString();
                    if(worst.HasValue)
                        tabulated += tab + worst.Value.ToString();
                    return tabulated;
                }
                return string.Empty;
            }
        }
        public bool CanCopyToClipboard() {
            return HasBenchmark && running == 0;
        }
        public void CopyToClipboard() {
            var clipboard = this.GetService<IClipboardService>();
            if(clipboard != null)
                clipboard.SetResult(TabulatedResult);
        }
        #endregion Clipboard
        #region Test
        public bool CanTest() {
            return HasBenchmark && running == 0;
        }
        public Task Test() {
            var dispatcher = this.GetRequiredService<IDispatcherService>();
            var target = ActiveBenchmarkItem.Target;
            var uiControl = ActiveHostItem.Target.UIControl;
            return Task.Factory.StartNew(() =>
                dispatcher.BeginInvoke(() =>
                {
                    running++;
                    target.SetUp(uiControl);
                })
            ).ContinueWith(setup =>
                dispatcher.BeginInvoke(() => target.Benchmark())
            ).ContinueWith(b =>
            {
                System.Threading.Thread.Sleep(5000);
                dispatcher.BeginInvoke(() =>
                {
                    target.TearDown(uiControl);
                    running--;
                });
            });
        }
        #endregion Test
    }
}