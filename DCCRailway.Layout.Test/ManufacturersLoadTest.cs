using DCCRailway.Layout.Configuration;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class ManufacturersLoadTest {
    [Test]
    public async Task LoadManufacturersList() {
        var manufacturers = RailwayConfig.Instance.Manufacturers;
        Assert.That(manufacturers, Is.Not.Null, "Should have at least 1 manufacturer returned from the Manufacturers call");
        foreach (var manufacturer in manufacturers.Values) {
            Assert.That(RailwayConfig.Instance.Manufacturers.Find(x => x.Id.Equals(manufacturer.Id)) != null);
        }
    }
}