using DCCRailway.Common.Helpers;

namespace DCCRailway.Common.Test;

[TestFixture]
public class BackgroundWorkerTest {
    [Test]
    public void TestTheWorker() {
        var itStarted = false;
        var itStopped = false;
        var didWork   = 0;

        var bw = new BackgroundWorkerTestClass("test", new TimeSpan(0, 0, 1));
        bw.WorkStarted    += (sender, args) => itStarted = true;
        bw.WorkFinished   += (sender, args) => itStopped = true;
        bw.WorkInProgress += (sender, args) => didWork++;

        bw.Start();
        Thread.Sleep(10000); // 10-seconds so the background worker SHOULD run at least 9 times
        bw.Stop();
        Thread.Sleep(1000); // 1 second delay to make sure we get the event processed

        Assert.That(itStarted, Is.True);
        Assert.That(itStopped, Is.True);
        Assert.That(didWork, Is.GreaterThanOrEqualTo(9));
    }
}

public class BackgroundWorkerTestClass(string name, TimeSpan freq) : BackgroundWorker(name, freq) {
    private int _counter = 0;
    private DateTime? _lastTime;

    protected override void DoWork() {
        TimeSpan? duration  = new TimeSpan(0);
        _counter++;
        if (_lastTime != null) duration = DateTime.Now - _lastTime;
        _lastTime = DateTime.Now;
    }
}