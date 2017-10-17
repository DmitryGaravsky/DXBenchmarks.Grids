namespace BenchmarkingApp {
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    sealed class UIAwaitingToken : IUIAwaitingToken {
        readonly TaskCompletionSource<long> source = new TaskCompletionSource<long>();
        readonly IntPtr handle;
        readonly Action action;
        public UIAwaitingToken(IntPtr handle, Action action) {
            this.action = action;
            this.handle = handle;
        }
        void IDisposable.Dispose() {
            source.Task.Wait();
            GC.SuppressFinalize(this);
        }
        readonly static Stopwatch stopwatch = new Stopwatch();
        void IUIAwaitingToken.Run() {
            Application.Idle += Application_Idle;
            DevExpress.Utils.Drawing.Helpers.NativeMethods.PostMessage(handle, 0, IntPtr.Zero, IntPtr.Zero);
            stopwatch.Restart();
            action();
        }
        const int MAGIC_IDLE_COUNTER = 10;
        int counter = 0;
        void Application_Idle(object sender, EventArgs e) {
            if(++counter > MAGIC_IDLE_COUNTER) {
                stopwatch.Stop();
                Application.Idle -= Application_Idle;
                source.SetResult(stopwatch.ElapsedMilliseconds);
            }
            else DevExpress.Utils.Drawing.Helpers.NativeMethods.PostMessage(handle, 0, IntPtr.Zero, IntPtr.Zero);
        }
        long? result;
        long IUIAwaitingToken.ElapsedMilliseconds {
            get {
                if(!result.HasValue)
                    result = source.Task.Result;
                return result.Value;
            }
        }
    }
}