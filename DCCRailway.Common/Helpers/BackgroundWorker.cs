using System.ComponentModel;
using Serilog;

namespace DCCRailway.Common.Helpers;

public abstract class BackgroundWorker(string? name, TimeSpan? frequency = null) {
    public event EventHandler WorkStarted;
    public event EventHandler WorkFinished;
    public event EventHandler WorkInProgress;

    public string Name { get; set; } = name ?? "default";
    public TimeSpan Frequency { get; set; } = frequency ?? new TimeSpan(0,0,0);
    public decimal Seconds => (decimal)Frequency.TotalSeconds;
    public int Milliseconds => (int)Frequency.TotalMilliseconds;

    protected readonly ILogger Log = Logger.LogContext<BackgroundWorker>();
    private CancellationTokenSource? _cancellationTokenSource;

    protected abstract void DoWork();

    public virtual void Start() {
        _cancellationTokenSource = new CancellationTokenSource();
        Log.Information("{0}: Background Worker starting up on a frequency of '{1}'.", Name, Frequency.ToString());
        if (Milliseconds > 0) {
            OnWorkStarted();
            Task.Run(() => {
                try {
                    while (true) {
                        DoWork();
                        OnWorkInProgress();
                        _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                        Thread.Sleep(Milliseconds);
                    }
                }
                catch (OperationCanceledException) {
                    Log.Information("{0}: Background Worker Cancelled.", Name);
                }
                OnWorkFinished();
            }, _cancellationTokenSource.Token);
        }
        else {
            Log.Information("{0}: Frequency not defined so task will conclude.", Name);
        }
    }

    public virtual void Stop() {
        _cancellationTokenSource?.Cancel();
        Log.Information("{0}: Background Worker Finished.", Name);
    }

    protected virtual void OnWorkStarted() {
        WorkStarted?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnWorkFinished() {
        WorkFinished?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnWorkInProgress() {
        WorkInProgress?.Invoke(this, EventArgs.Empty);
    }
}