using DCCRailway.Common.Entities;

namespace DCCRailway.Common.Test;

[TestFixture]
public class LocomotiveTest {
    [Test]
    public void TestLocomotive() {
        var loco = new Locomotive();
        loco.Name        = "MyLoco";
        loco.Description = "My Locomotive";
        Assert.That(loco.ToString().Equals("MyLoco"));
    }
}