using DCCRailway.Application.WiThrottle;
using DCCRailway.Layout.Configuration;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class WiThrottleTest {

    [TestCase]
    public void TestIfWiThrottleLoads() {

        var config = RailwayConfig.New();
        var options = new WiThrottleServerOptions(config);
        var wii = new WiThrottleServer(options);
        Assert.That(wii,Is.Not.Null);
        Assert.That(wii.ServerActive, Is.False);
        wii.Stop();
    }

    [TestCase,Ignore("Only run manually")]
    public void TestIfWiThrottleLoadsAndRuns() {

        var config = RailwayConfig.New();
        var options = new WiThrottleServerOptions(config);
        var wii = new WiThrottleServer(options);
        Assert.That(wii,Is.Not.Null);
        Assert.That(wii.ServerActive, Is.False);
        wii.Start();
        Assert.That(wii.ServerActive, Is.True);

        Console.ReadLine();
    }
}