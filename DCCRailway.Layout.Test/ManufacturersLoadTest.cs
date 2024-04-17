using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.System;
using NUnit.Framework;

namespace DCCRailway.Test.SystemTests;

[TestFixture]
public class ManufacturersLoadTest {
    [Test]
    public void LoadManufacturersList() {
        var manufacturers = RailwayConfig.Instance.SystemEntities.Manufacturers;
        Assert.That(manufacturers, Is.Not.Null, "Should have at least 1 manufacturer returned from the Manufacturers call");
        foreach (var manufacturer in manufacturers) {
            Assert.That(RailwayConfig.Instance.SystemEntities.Manufacturers.FindByIdentifier(manufacturer.Identifier) != null);
        }
    }
}