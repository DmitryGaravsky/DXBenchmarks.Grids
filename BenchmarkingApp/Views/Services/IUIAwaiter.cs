namespace BenchmarkingApp {
    using System;

    public interface IUIAwaiter {
        void Prepare();
        IUIAwaitingToken BeginAwaiting(Action action);
    }
    public interface IUIAwaitingToken : IDisposable {
        long ElapsedMilliseconds { get; }
        void Run();
    }
}