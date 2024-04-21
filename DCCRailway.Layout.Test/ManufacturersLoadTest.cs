using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.System;
using NUnit.Framework;

namespace DCCRailway.Test.SystemTests;

[TestFixture]
public class ManufacturersLoadTest {
    [Test]
    public async Task LoadManufacturersList() {
        var manufacturers = RailwayConfig.Instance.ManufacturerRepository;
        Assert.That(manufacturers, Is.Not.Null, "Should have at least 1 manufacturer returned from the Manufacturers call");
        foreach (var manufacturer in await manufacturers.GetAllAsync()) {
            Assert.That(RailwayConfig.Instance.ManufacturerRepository.Find(x => x.Id.Equals(manufacturer.Id)) != null);
        }
    }
}