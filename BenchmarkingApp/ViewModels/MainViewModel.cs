namespace BenchmarkingApp {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
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
        int? batchIndex, batchTotal;
        public Task LoadAndRunBatch(string[] args) {
            Load();
            BenchmarkItem[] activeBenchmarks = BenchmarkItem.GetActive(benchmarks, args);
            var dispatcher = this.GetRequiredService<IDispatcherService>();
            var awaiter = this.GetService<IUIAwaiter>();
            this.batchTotal = activeBenchmarks.Length;
            return Task.Factory.StartNew(() =>
            {
                int total = 0;
                for(int i = 0; i < activeBenchmarks.Length; i++) {
                    this.batchIndex = i + 1;
                    var current = activeBenchmarks[i];
                    total = UIOperation<int>(dispatcher, () =>
                    {
                        ActiveHostItem = HostItems.FirstOrDefault(host => BenchmarkItem.IsBenchmarkFor(current.Type, host.Name));
                        if(ActiveHostItem != null)
                            ActiveBenchmarkItem = current;
                        return total + ((ActiveHostItem != null) ? 1 : 0);
                    }).ContinueWith(tSelectItem =>
                    {
                        Run().Wait();
                        return tSelectItem.Result;
                    }).Result;
                }
                this.batchTotal = null;
                this.batchIndex = null;
                dispatcher.BeginInvoke(() =>
                    OnBatchRunComplete(total));
            });
        }
        void OnBatchRunComplete(int total) {
            var messageService = this.GetRequiredService<IMessageBoxService>();
            if(messageService != null)
                messageService.ShowMessage("Total benchmarks complete:" + total.ToString());
        }
        //
        protected bool HasBenchmark {
            get { return ActiveBenchmarkItem != null; }
        }
        void UpdateCommands() {
            this.RaisePropertyChanged(x => x.Title);
            this.RaiseCanExecuteChanged(x => x.ColdRun());
            this.RaiseCanExecuteChanged(x => x.WarmUp());
            this.RaiseCanExecuteChanged(x => x.Run());
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
        public Task ColdRun() {
            var dispatcher = this.GetRequiredService<IDispatcherService>();
            var awaiter = this.GetService<IUIAwaiter>();
            var target = ActiveBenchmarkItem.Target;
            var uiControl = ActiveHostItem.Target.UIControl;
            return Prepare(dispatcher, awaiter)
                .ContinueWith(_ =>
                {
                    coldRunResult = DoCycle(dispatcher, awaiter, target, uiControl).Result;
                    cold = false;
                    dispatcher.BeginInvoke(() =>
                    {
                        this.RaisePropertyChanged(x => x.Result);
                        this.RaiseCanExecuteChanged(x => x.ColdRun());
                    });
                });
        }
        public bool CanWarmUp() {
            return HasBenchmark && running == 0;
        }
        public Task WarmUp() {
            var dispatcher = this.GetRequiredService<IDispatcherService>();
            var awaiter = this.GetService<IUIAwaiter>();
            var target = ActiveBenchmarkItem.Target;
            var uiControl = ActiveHostItem.Target.UIControl;
            int warmUpCounter = BenchmarkItem.GetWarmUpCounter(ActiveBenchmarkItem.Type);
            Task coldRun = IsCold ? ColdRun() : Prepare(dispatcher, awaiter);
            return coldRun.ContinueWith(_ =>
            {
                long result = 0;
                for(int i = -2 /*pre-warmup*/; i < warmUpCounter; i++) {
                    if(i == -1) {
                        if(result > 5000)
                            warmUpCounter = 5;
                        if(result > 15000)
                            warmUpCounter = 3;
                    }
                    if(i == 0)
                        result = 0;
                    result += DoCycle(dispatcher, awaiter, target, uiControl).Result;
                }
                warmUpResult = Math.Max(1, result / warmUpCounter);
                dispatcher.BeginInvoke(() =>
                {
                    this.RaisePropertyChanged(x => x.Result);
                    this.RaiseCanExecuteChanged(x => x.Run());
                });
            });
        }
        public bool CanRun() {
            return HasBenchmark && running == 0;
        }
        int running;
        int? runIndex, runTotal;
        public Task Run() {
            running++;
            var dispatcher = this.GetRequiredService<IDispatcherService>();
            var awaiter = this.GetService<IUIAwaiter>();
            Task warmUp = !IsWarmedUp ? WarmUp() : Prepare(dispatcher, awaiter);
            // Prepare
            var target = ActiveBenchmarkItem.Target;
            var uiControl = ActiveHostItem.Target.UIControl;
            return warmUp.ContinueWith(_ =>
            {
                int counter = BenchmarkItem.GetBenchmarkCounter(ActiveBenchmarkItem.Type, Math.Max(10, Math.Min(100, 1000 / (int)warmUpResult.Value)));
                long[] results = new long[counter];
                long low = warmUpResult.Value - Math.Min(warmUpResult.Value / 3, Math.Max(5, warmUpResult.Value / 20));
                long hi = warmUpResult.Value + Math.Min(warmUpResult.Value / 3, Math.Max(5, warmUpResult.Value / 20));
                if(low == hi) {
                    low--; hi++;
                }
                worst = 0;
                int watchDog = Benchmarks.Data.Configuration.Current.WatchDog(counter);
                runTotal = counter;
                while(counter > 0 && (0 < watchDog--)) {
                    // Run Normal
                    long current = DoCycle(dispatcher, awaiter, target, uiControl).Result;
                    worst = Math.Max(current, worst.Value);
                    if(current > low && current < hi) {
                        results[--counter] = current;
                        runIndex = runTotal - counter;
                        dispatcher.BeginInvoke(() =>
                            this.RaisePropertyChanged(x => x.Title));
                    }
                }
                int actualResultsCount = results.Where(r => r != 0).Count();
                // Check results completeness
                if(watchDog <= 0 && (results.Length - counter) >= 0) {
                    while(counter > 0) {
                        // Run Ever!
                        long current = DoCycle(dispatcher, awaiter, target, uiControl).Result;
                        worst = Math.Max(current, worst.Value);
                        if(current < warmUpResult.Value * 2) {
                            results[--counter] = current;
                            runIndex = runTotal - counter;
                            dispatcher.BeginInvoke(() =>
                                this.RaisePropertyChanged(x => x.Title));
                        }
                    }
                    actualResultsCount = results.Length;
                }
                // Calc
                result = (long)Math.Ceiling((double)results.Sum() / (double)actualResultsCount);
                dispatcher.BeginInvoke(() =>
                {
                    this.runTotal = null;
                    this.runIndex = null;
                    this.RaisePropertyChanged(x => x.Result);
                    running--;
                    UpdateCommands();
                    LogResults();
                    CopyToClipboard();
                });
            });
        }
        public bool CanTest() {
            return HasBenchmark && running == 0;
        }
        public Task Test() {
            var dispatcher = this.GetRequiredService<IDispatcherService>();
            var awaiter = this.GetService<IUIAwaiter>();
            var target = ActiveBenchmarkItem.Target;
            var uiControl = ActiveHostItem.Target.UIControl;
            return Prepare(dispatcher, awaiter)
                    .ContinueWith(_ =>
                        DoSetup(dispatcher, awaiter, target, uiControl).Wait())
                    .ContinueWith(_ =>
                        DoBehcmark(dispatcher, awaiter, target).Result)
                    .ContinueWith(run =>
                    {
                        Thread.Sleep(5000);
                        DoTearDown(dispatcher, awaiter, target, uiControl).Wait();
                        return run.Result;
                    });
        }
        // Tasks
        Task Prepare(IDispatcherService dispatcher, IUIAwaiter awaiter) {
            return UIOperation(dispatcher, () =>
            {
                UpdateCommands();
                awaiter.Prepare();
                return 0;
            });
        }
        Task<T> UIOperation<T>(IDispatcherService dispatcher, Func<T> uiOperation) {
            var completionSource = new TaskCompletionSource<T>();
            completionSource.Task.ConfigureAwait(false);
            dispatcher.BeginInvoke(() =>
                completionSource.SetResult(uiOperation()));
            return completionSource.Task;
        }
        Task<long> DoCycle(IDispatcherService dispatcher, IUIAwaiter awaiter, IBenchmarkItem target, object uiControl) {
            return DoSetup(dispatcher, awaiter, target, uiControl)
                    .ContinueWith(_ =>
                        DoBehcmark(dispatcher, awaiter, target).Result)
                    .ContinueWith(run =>
                    {
                        DoTearDown(dispatcher, awaiter, target, uiControl).Wait();
                        return run.Result;
                    });
        }
        Task DoSetup(IDispatcherService service, IUIAwaiter awaiter, IBenchmarkItem target, object uiControl) {
            return Task.Factory.StartNew(() =>
            {
                running++;
                using(var token = awaiter.BeginAwaiting(() => target.SetUp(uiControl)))
                    service.BeginInvoke(() => token.Run());
            }, TaskCreationOptions.LongRunning);
        }
        Task DoTearDown(IDispatcherService service, IUIAwaiter awaiter, IBenchmarkItem target, object uiControl) {
            return Task.Factory.StartNew(() =>
            {
                using(var token = awaiter.BeginAwaiting(() => target.TearDown(uiControl)))
                    service.BeginInvoke(() => token.Run());
                running--;
            }, TaskCreationOptions.LongRunning);
        }
        Task<long> DoBehcmark(IDispatcherService service, IUIAwaiter awaiter, IBenchmarkItem target) {
            return Task.Factory.StartNew<long>(() =>
            {
                using(var token = awaiter.BeginAwaiting(() => target.Benchmark())) {
                    service.BeginInvoke(() => token.Run());
                    return token.ElapsedMilliseconds;
                }
            }, TaskCreationOptions.LongRunning);
        }
        // Title
        const string name = "Benckmarking App for DevExpress WinForms Grids";
        public string Title {
            get {
                string runningStage = string.Empty;
                if(running > 0) {
                    runningStage = ", Running: " + ActiveBenchmarkItem.Name;
                    if(runIndex.HasValue)
                        runningStage += ", " + runIndex.Value.ToString() + "/" + runTotal.Value.ToString();
                    if(batchIndex.HasValue)
                        runningStage += ", " + batchIndex.Value.ToString() + " of " + batchTotal.Value.ToString();
                }
                return name + " (Version: " + AssemblyInfo.Version + "; Configuration: " + Benchmarks.Data.Configuration.Current.Name + runningStage + ")";
            }
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
        #region Log
        void LogResults() {
            var log = this.GetService<ILogService>();
            if(log != null) {
                string name = ActiveHostItem.Name + "(" + Benchmarks.Data.Configuration.Current.Name + ") - " + ActiveBenchmarkItem.Name;
                log.Log("[" + name + "] " + Result);
            }
        }
        #endregion Log
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
    }
}