using DCCRailway.CmdStation.Controllers;
using DCCRailway.CmdStation.Virtual;
using DCCRailway.CmdStation.Virtual.Adapters;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class WiThrottleTest {

    [Test]
    public void TestIfWiThrottleLoads() {

        /*
        var config = TestConfig.CreateTestConfig();
        IController controller = new VirtualController {
            Adapter = new VirtualAdapter()
        };

        var wii = new WiThrottleServer(config, controller);
        wii.Start();
        Assert.That(wii,Is.Not.Null);
        wii.Stop();
        */
    }

    [Test]
    //[Ignore("Ignore this test")]
    public void TestIfWiThrottleLoadsAndRuns() {

        /*
        IRailwayConfig config = TestConfig.CreateTestConfig();
        IController controller = new VirtualController {
            Adapter = new VirtualAdapter()
        };
        var wii = new WiThrottleServer(config,controller);
        Assert.That(wii,Is.Not.Null);
        Assert.That(wii.ActiveClients, Is.EqualTo(0));
        wii.Start();
        Console.ReadLine();
        wii.Stop();
        */
    }
}