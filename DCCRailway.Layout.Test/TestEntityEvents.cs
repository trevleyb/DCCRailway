using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Entities.System;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class TestEntityEvents {

    [TestCase]
    public void TestThatEntityRepositoryIsEventingChanges() {

        var propertyChanged = false;
        var repositoryChanged = false;
        var config = CreateTestConfig();
        var locomotives = config.Locomotives;
        locomotives.RepositoryChanged += (sender, args) => repositoryChanged= true;

        propertyChanged = false;
        var addedLoco = locomotives.AddAsync(new Locomotive { Name = "Train06" }).Result;
        Assert.That(repositoryChanged,Is.True);  // Property Is not changed on Add/Remove
        Assert.That(propertyChanged,Is.False);  // Property Is not changed on Add/Remove

        var locomotive = locomotives.GetByNameAsync("Train01").Result;
        Assert.That(locomotive,Is.Not.Null);
        locomotive.PropertyChanged += (sender, args) => propertyChanged = true;

        propertyChanged = false;
        locomotive.Name = "Train09";
        Assert.That(propertyChanged,Is.True);

        propertyChanged = false;
        locomotive.Address = new DCCAddress(345);
        Assert.That(propertyChanged,Is.True);

        propertyChanged = false;
        locomotive.Direction = DCCDirection.Reverse;
        Assert.That(propertyChanged,Is.True);

        propertyChanged = false;
        locomotive.Direction = DCCDirection.Stop;
        Assert.That(propertyChanged,Is.True);

        propertyChanged = false;
        locomotives.DeleteAsync(locomotive.Id);

    }

    private IRailwayConfig CreateTestConfig() {

        var config = RailwayConfig.New("Test Layout", "Test Layout");

        var locomotives = config.Locomotives;
        locomotives.Add(new Locomotive { Name = "Train01" } );
        locomotives.Add(new Locomotive { Name = "Train02" } );
        locomotives.Add(new Locomotive { Name = "Train03" } );
        locomotives.Add(new Locomotive { Name = "Train04" } );
        locomotives.Add(new Locomotive { Name = "Train05" } );

        var sensors = config.Sensors;
        sensors.Add(new Sensor { Name = "Sensor01", Address = new DCCAddress(501) });
        sensors.Add(new Sensor { Name = "Sensor02", Address = new DCCAddress(502) });
        sensors.Add(new Sensor { Name = "Sensor03", Address = new DCCAddress(503) });
        sensors.Add(new Sensor { Name = "Sensor04", Address = new DCCAddress(504) });
        sensors.Add(new Sensor { Name = "Sensor05", Address = new DCCAddress(505) });

        return config;
    }
}