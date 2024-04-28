using DCCRailway.Common.Helpers;

namespace DCCRailway.Common.Test;

[TestFixture]
public class BackgroundWorkerTest {

    [TestCase]
    public void TestTheWorker() {

        bool itStarted = false;
        bool itStopped = false;
        int didWork = 0;

        var options = new BackgroundWorkerOptions() {
            Name = "MyTestWorker",
            FrequencyInSeconds = 1
        };

        var bw = new BackgroundWorkerTestClass(options);
        bw.WorkStarted += (sender, args) => itStarted = true;
        bw.WorkFinished += (sender, args) => itStopped = true;
        bw.WorkInProgress += (sender, args) => didWork++;

        bw.Start();
        Thread.Sleep(10000); // 10-seconds so the background worker SHOULD run at least 9 times
        bw.Stop();

        Assert.That(itStarted,Is.True);
        Assert.That(itStopped,Is.True);
        Assert.That(didWork,Is.GreaterThanOrEqualTo(9));

    }

}

public class BackgroundWorkerTestClass(BackgroundWorkerOptions options) : BackgroundWorker(options) {
    private int counter = 0;
    private DateTime? lastTime = null;

    protected override void DoWork() {
        TimeSpan? duration = new TimeSpan(0);
        if (lastTime != null) {
            duration = DateTime.Now - lastTime;
        }
        log.Information($"Doing some work: {++counter} and last execution was {duration?.ToString()}");
        lastTime = DateTime.Now;
    }
}