using System.ComponentModel;
using Serilog;

namespace DCCRailway.Common.Helpers;

public class BackgroundWorkerOptions {
    public string Name { get; set; }
    public int FrequencyInSeconds { get; set; }
    public int DelayinMilliSeconds => FrequencyInSeconds * 1000;
}

public abstract class BackgroundWorker(BackgroundWorkerOptions options) {


    public event EventHandler WorkStarted;
    public event EventHandler WorkFinished;
    public event EventHandler WorkInProgress;

    protected readonly ILogger log = Logger.LogContext<BackgroundWorker>();
    private CancellationTokenSource? _cancellationTokenSource;

    protected abstract void DoWork();

    public void Start() {
        _cancellationTokenSource = new CancellationTokenSource();
        log.Information("Background Worker '{0}' starting up.", options.Name);
        OnWorkStarted();
        Task.Run(() => {
            try {
                while (true) {

                    DoWork();
                    OnWorkInProgress();

                    // Throws if cancellation is pending, aborting the loop.
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    // Simulate work.
                    Thread.Sleep(options.DelayinMilliSeconds);
                }
            }
            catch (OperationCanceledException) {
                log.Information("Background Worker '{0}' was cancelled.", options.Name);
            }
        }, _cancellationTokenSource.Token);
        OnWorkFinished();
        log.Information("Background Worker '{0}' finished.", options.Name);
    }

    public void Stop() {
        _cancellationTokenSource?.Cancel();
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