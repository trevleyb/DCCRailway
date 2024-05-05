using DCCRailway.Layout.LayoutRepository.Entities;

namespace DCCRailway.Layout.Test;

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