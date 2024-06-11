using DCCRailway.Common.Helpers;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class ManufacturersLoadTest {
    [Test]
    public void LoadManufacturersList() {
        var manager       = new RailwaySettings(LoggerHelper.DebugLogger).New("xxx", "./");
        var manufacturers = manager.Manufacturers;
        Assert.That(manufacturers, Is.Not.Null, "Should have at least 1 manufacturer returned from the Manufacturers call");

        foreach (var manufacturer in manufacturers.Values) {
            Assert.That(manager.Manufacturers.Find(x => x.Id.Equals(manufacturer.Id)) != null);
        }
    }
}