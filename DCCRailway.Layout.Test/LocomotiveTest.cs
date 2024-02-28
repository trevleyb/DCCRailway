using DCCRailway.Layout.Entities;
using NUnit.Framework;

namespace DCCRailway.Test.SystemTests;

[TestFixture]
public class LocomotiveTest {

    [Test]
    public void TestLocomotive() {
        var loco = new Locomotive();
        loco.Name = "MyLoco";
        loco.Description = "My Locomotive";
        Assert.That(loco.ToString().Equals("MyLoco"));
    }
}