using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Test;

public static class TestConfig {

    public static IRailwayConfig CreateTestConfig(string name = "Test Config", string desc = "My Test Railway", string file = "TestConfig.json") {

        var config = RailwayConfig.New(name,desc,file);

        var locomotives = config.Locomotives;
        locomotives.Add(new Locomotive { Id = "", Name = "TestLocomotive1", Description = "Test Locomotive Description1" });

        var sensors = config.Sensors;
        sensors.Add(new Sensor { Id = "SEN01", Name="Yard-Occupied" });
        sensors.Add(new Sensor { Id = "SEN02", Name="Station-Enter" });
        sensors.Add(new Sensor { Id = "SEN03", Name="Station-Exit" });
        sensors.Add(new Sensor { Id = "SEN04", Name="Station-Occupied" });
        sensors.Add(new Sensor { Id = "SEN05", Name="Mainline-Occupied" });

        var turnouts = config.Turnouts;
        turnouts.Add(new Turnout { Id="TRN01", Name = "Mainline-to-Station"});
        turnouts.Add(new Turnout { Id="TRN01", Name = "Mainline-to-Yard"});

        var signals = config.Signals;
        signals.Add(new Signal { Id = "SIG01", Name = "Entrance-to-Yard"});
        signals.Add(new Signal { Id = "SIG02", Name = "Exit-Yard-to-Mainline"});
        signals.Add(new Signal { Id = "SIG03", Name = "Station-to-Mainline"});

        var routes = config.Routes;
        routes.Add(new Route { Name = "TestTurnout1", Description = "Test Turnout Description1" });

        var accessories = config.Accessories;
        accessories.Add(new Accessory { Name = "TestAccessory1", Description = "Test Accessory Description1" });

        var blocks = config.Blocks;
        blocks.Add(new Block { Id="BLK01", Name = "Mainline-West" });
        blocks.Add(new Block { Id="BLK02", Name = "Mainline-East" });

        return config;
    }

}