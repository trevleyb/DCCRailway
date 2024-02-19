using NUnit.Framework;

namespace DCCRailway.Test.SystemTests;

[TestFixture]
public class ManufacturersLoadTest {
    
    [Test]
    public void LoadManufacturersList() {
        var manufacturers = new Configuration.Manufacturers();
        Assert.That(manufacturers, Is.Not.Null, "Should have at least 1 manufacturer returned from the Manufacturers call");
        foreach (var manufacturer in manufacturers) {
            Assert.That(manufacturers.Find(manufacturer.Identifier) != null);
        }
    }
}