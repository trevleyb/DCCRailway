using DCCRailway.Application.WiThrottle;
using DCCRailway.Layout.Configuration;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class WiThrottleTest {

    [Test]
    public void TestIfWiThrottleLoads() {

        var config = RailwayConfig.New();
        var options = new WiThrottleServerOptions(config, null);
        var wii = new WiThrottleServer(options);
        Assert.That(wii,Is.Not.Null);
        Assert.That(wii.ServerActive, Is.False);
        wii.Stop();
    }

    [Test,Ignore("Ignore as runs continuously")]
    public void TestIfWiThrottleLoadsAndRuns() {

        var config = RailwayConfig.New();
        var options = new WiThrottleServerOptions(config, null);
        var wii = new WiThrottleServer(options);
        Assert.That(wii,Is.Not.Null);
        Assert.That(wii.ServerActive, Is.False);
        wii.Start();
        Assert.That(wii.ServerActive, Is.True);

        Console.ReadLine();
    }
}