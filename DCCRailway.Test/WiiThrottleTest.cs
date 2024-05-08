using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class WiThrottleTest {

    [Test]
    public void TestIfWiThrottleLoads() {

        /*
        var config = TestConfig.CreateTestConfig();
        ICommandStation commandStation = new VirtualCommandStation {
            Adapter = new VirtualAdapter()
        };

        var wii = new WiThrottleServer(config, commandStation);
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
        ICommandStation commandStation = new VirtualCommandStation {
            Adapter = new VirtualAdapter()
        };
        var wii = new WiThrottleServer(config,commandStation);
        Assert.That(wii,Is.Not.Null);
        Assert.That(wii.ActiveClients, Is.EqualTo(0));
        wii.Start();
        Console.ReadLine();
        wii.Stop();
        */
    }
}