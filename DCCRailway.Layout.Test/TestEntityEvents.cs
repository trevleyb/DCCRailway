using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Entities.System;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class TestEntityEvents {

    [TestCase]
    public void TestThatEntityRepositoryIsEbentingChanges() {

        var propertyChanged = false;
        var propertyChanging = false;
        var config = CreateTestConfig();
        var locomotives = config.LocomotiveRepository;
        locomotives.PropertyChanged += (sender, args) => propertyChanged = true;
        locomotives.PropertyChanging += (sender, args) => propertyChanging = true;

        propertyChanged = false;
        propertyChanging = false;
        locomotives.AddAsync(new Locomotive { Name = "Train06" });
        Assert.That(propertyChanged,Is.False);  // Property Is not changed on Add/Remove
        Assert.That(propertyChanging,Is.False); // Property Is not changed on Add/Remove

        var locomotive = locomotives.GetByNameAsync("Train01").Result;

        propertyChanged = false;
        propertyChanging = false;
        locomotive.Name = "Train09";
        Assert.That(propertyChanged,Is.True);
        Assert.That(propertyChanging,Is.True);

        propertyChanged = false;
        propertyChanging = false;
        locomotive.Address = new DCCAddress(345);
        Assert.That(propertyChanged,Is.True);
        Assert.That(propertyChanging,Is.True);

        propertyChanged = false;
        propertyChanging = false;
        locomotive.Direction = DCCDirection.Reverse;
        Assert.That(propertyChanged,Is.True);
        Assert.That(propertyChanging,Is.True);

        propertyChanged = false;
        propertyChanging = false;
        locomotive.Direction = DCCDirection.Stop;
        Assert.That(propertyChanged,Is.True);
        Assert.That(propertyChanging,Is.True);

        propertyChanged = false;
        propertyChanging = false;
        locomotives.DeleteAsync(locomotive.Id);

    }

    private IRailwayConfig CreateTestConfig() {

        var config = RailwayConfig.New("Test Layout", "Test Layout");

        var locomotives = config.LocomotiveRepository;
        locomotives.AddAsync(new Locomotive { Name = "Train01" } );
        locomotives.AddAsync(new Locomotive { Name = "Train02" } );
        locomotives.AddAsync(new Locomotive { Name = "Train03" } );
        locomotives.AddAsync(new Locomotive { Name = "Train04" } );
        locomotives.AddAsync(new Locomotive { Name = "Train05" } );

        var sensors = config.SensorRepository;
        sensors.AddAsync(new Sensor { Name = "Sensor01", Address = new DCCAddress(501) });
        sensors.AddAsync(new Sensor { Name = "Sensor02", Address = new DCCAddress(502) });
        sensors.AddAsync(new Sensor { Name = "Sensor03", Address = new DCCAddress(503) });
        sensors.AddAsync(new Sensor { Name = "Sensor04", Address = new DCCAddress(504) });
        sensors.AddAsync(new Sensor { Name = "Sensor05", Address = new DCCAddress(505) });

        return config;
    }
}