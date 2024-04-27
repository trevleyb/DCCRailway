using DCCRailway.Application.WiThrottle;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class WiThrottleTest {

    [TestCase]
    public void TestIfWiThrottleLoads() {

        var wii = new WiThrottleServer();
        Assert.That(wii,Is.Not.Null);
        Assert.That(wii.ServerActive, Is.True);


    }
}