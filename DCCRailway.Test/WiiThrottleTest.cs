using DCCRailway.Application.WiThrottle;
using DCCRailway.Layout.Configuration;
using DCCRailway.Station.Controllers;
using DCCRailway.Station.Virtual;
using DCCRailway.Station.Virtual.Adapters;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class WiThrottleTest {

    [Test]
    public void TestIfWiThrottleLoads() {

        var config = TestConfig.CreateTestConfig();
        IController controller = new VirtualController {
            Adapter = new VirtualAdapter()
        };

        var wii = new WiThrottleServer(config, controller);
        Assert.That(wii,Is.Not.Null);
        Assert.That(wii.ServerActive, Is.False);
        wii.Stop();
    }

    [Test]
    //[Ignore("Ignore this test")]
    public void TestIfWiThrottleLoadsAndRuns() {

        IRailwayConfig config = TestConfig.CreateTestConfig();
        IController controller = new VirtualController {
            Adapter = new VirtualAdapter()
        };
        var wii = new WiThrottleServer(config,controller);
        Assert.That(wii,Is.Not.Null);
        Assert.That(wii.ServerActive, Is.False);
        wii.Start();
        Assert.That(wii.ServerActive, Is.True);

        Console.ReadLine();
    }
}