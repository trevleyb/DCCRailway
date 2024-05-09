using DCCRailway.Railway.Configuration;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class ManufacturersLoadTest {
    [Test]
    public void LoadManufacturersList() {
        var manufacturers = RailwayManager.Instance.Manufacturers;
        Assert.That(manufacturers, Is.Not.Null, "Should have at least 1 manufacturer returned from the Manufacturers call");
        foreach (var manufacturer in manufacturers.Values) {
            Assert.That(RailwayManager.Instance.Manufacturers.Find(x => x.Id.Equals(manufacturer.Id)) != null);
        }
    }
}