using Serilog;

namespace DCCRailway.Common.Helpers;

public abstract class BackgroundWorker(ILogger logger, string? name, TimeSpan? frequency = null) {
    private CancellationTokenSource? _cancellationTokenSource;

    public string             Name         { get; set; } = name ?? "default";
    public TimeSpan           Frequency    { get; set; } = frequency ?? new TimeSpan(0, 0, 0);
    public decimal            Seconds      => (decimal)Frequency.TotalSeconds;
    public int                Milliseconds => (int)Frequency.TotalMilliseconds;
    public event EventHandler WorkStarted;
    public event EventHandler WorkFinished;
    public event EventHandler WorkInProgress;

    protected abstract void DoWork();

    public virtual void Start() {
        _cancellationTokenSource = new CancellationTokenSource();
        logger.Information("{0}: Background Worker starting up on a frequency of '{1}'.", Name, Frequency.ToString());
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
                    logger.Information("{0}: Background Worker Cancelled.", Name);
                }

                OnWorkFinished();
            }, _cancellationTokenSource.Token);
        }
        else {
            logger.Information("{0}: Frequency not defined so task will conclude.", Name);
        }
    }

    public virtual void Stop() {
        _cancellationTokenSource?.Cancel();
        logger.Information("{0}: Background Worker Finished.", Name);
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