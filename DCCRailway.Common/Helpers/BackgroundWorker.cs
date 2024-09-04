using Serilog;

namespace DCCRailway.Common.Helpers;

public abstract class BackgroundWorker(ILogger logger, string? name, TimeSpan? frequency = null) {
    private CancellationTokenSource? _cancellationTokenSource;

    public string   Name      { get; set; } = name ?? "default";
    public TimeSpan Frequency { get; set; } = frequency ?? new TimeSpan(0, 0, 0);

    public int Seconds {
        get => (int)Frequency.TotalSeconds;
        set => Frequency = new TimeSpan(0, 0, 0, value);
    }

    public int Milliseconds => (int)Frequency.TotalMilliseconds;

    public event EventHandler WorkStarted;
    public event EventHandler WorkFinished;
    public event EventHandler WorkInProgress;

    protected abstract void DoWork();

    public virtual void Start() {
        _cancellationTokenSource = new CancellationTokenSource();
        logger.Information("{0}: Background Worker starting up on a frequency of '{1}'.", Name, Frequency.ToString());

        OnWorkStarted();
        Task.Run(() => {
            try {
                while (!_cancellationTokenSource.IsCancellationRequested) {
                    DoWork();
                    OnWorkInProgress();
                    if (Milliseconds > 0) Thread.Sleep(Milliseconds);
                }
            } catch (OperationCanceledException) {
                logger.Information("{0}: Background Worker Cancelled.", Name);
            }

            OnWorkFinished();
        }, _cancellationTokenSource.Token);
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