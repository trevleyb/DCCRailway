using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Test;

public static class TestConfig {

    public static IRailwayConfig CreateTestConfig(string name = "Test Config", string desc = "My Test Railway", string file = "TestConfig.json") {

        var config = RailwayConfig.New(name,desc,file);

        var locomotives = config.Locomotives;
        locomotives.Add(new Locomotive { Address = new DCCAddress(1029,DCCAddressType.Long), Name = "CN-1029", Description = "Canadian National (1029)" });
        locomotives.Add(new Locomotive { Address = new DCCAddress(6502,DCCAddressType.Long), Name = "UP-6502", Description = "Union Pacific (6502)" });
        locomotives.Add(new Locomotive { Address = new DCCAddress(98,DCCAddressType.Short),  Name = "NS-98", Description = "Norfolk Southern (98)" });
        locomotives.Add(new Locomotive { Address = new DCCAddress(97,DCCAddressType.Short),  Name = "NS-97", Description = "Norfolk Southern (97)" });
        locomotives.Add(new Locomotive { Address = new DCCAddress(1020,DCCAddressType.Long), Name = "CN-1020", Description = "Canadian National (1020)" });

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
        routes.Add(new Route { Id = "RTE01", Name = "Route1", Description = "Station to Mainline" });
        routes.Add(new Route { Id = "RTE02", Name = "Route2", Description = "Station to Yard" });
        routes.Add(new Route { Id = "RTE03", Name = "Route3", Description = "Mainline to Yard" });

        var accessories = config.Accessories;
        accessories.Add(new Accessory { Name = "TestAccessory1", Description = "Test Accessory Description1" });

        var blocks = config.Blocks;
        blocks.Add(new Block { Id="BLK01", Name = "Mainline-West" });
        blocks.Add(new Block { Id="BLK02", Name = "Mainline-East" });

        return config;
    }

}