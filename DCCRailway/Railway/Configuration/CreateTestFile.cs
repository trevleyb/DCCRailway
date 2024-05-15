using DCCRailway.Common.Types;
using DCCRailway.Layout.Entities;
using DCCRailway.Railway.Configuration.Entities;
using Parameter = DCCRailway.Railway.Configuration.Entities.Parameter;
using Route = DCCRailway.Layout.Entities.Route;

namespace DCCRailway.Railway.Configuration;

public static class CreateTestFile {

    public static void Build(string? pathname = null, string? name = "DCCRailway") {

        var manager = RailwayManager.New("TestLayout", "./Configuration");

        manager.Settings.Description = "A Test configuration file for testing";
        manager.Settings.Controller.Name = "Virtual";
        manager.Settings.Controller.Adapters.Add("Virtual");
        manager.Settings.Controller.Parameters.Add("Optional", "Value");

        var locomotives = manager.Locomotives;
        locomotives.Add(new Locomotive { Id = "CN1029", Address = new DCCAddress(1029, DCCAddressType.Long), Name = "CN-1029", Description = "Canadian National (1029)" });
        locomotives.Add(new Locomotive { Id = "UP6502", Address = new DCCAddress(6502, DCCAddressType.Long), Name = "UP-6502", Description = "Union Pacific (6502)" });
        locomotives.Add(new Locomotive { Id = "NS98", Address = new DCCAddress(98, DCCAddressType.Short), Name = "NS-98", Description = "Norfolk Southern (98)" });
        locomotives.Add(new Locomotive { Id = "NS97", Address = new DCCAddress(97, DCCAddressType.Short), Name = "NS-97", Description = "Norfolk Southern (97)" });
        locomotives.Add(new Locomotive { Id = "CN1020", Address = new DCCAddress(1020, DCCAddressType.Long), Name = "CN-1020", Description = "Canadian National (1020)" });

        var sensors = manager.Sensors;
        sensors.Add(new Sensor { Id = "S01", Name = "Yard-Occupied" });
        sensors.Add(new Sensor { Id = "S02", Name = "Station-Enter" });
        sensors.Add(new Sensor { Id = "S03", Name = "Station-Exit" });
        sensors.Add(new Sensor { Id = "S04", Name = "Station-Occupied" });
        sensors.Add(new Sensor { Id = "S05", Name = "Mainline-Occupied" });

        var turnouts = manager.Turnouts;
        turnouts.Add(new Turnout { Id = "T01", Name = "Mainline-to-Station" });
        turnouts.Add(new Turnout { Id = "T02", Name = "Mainline-to-Yard" });

        var signals = manager.Signals;
        signals.Add(new Signal { Id = "SIG01", Name = "Entrance-to-Yard" });
        signals.Add(new Signal { Id = "SIG02", Name = "Exit-Yard-to-Mainline" });
        signals.Add(new Signal { Id = "SIG03", Name = "Station-to-Mainline" });

        var routes = manager.Routes;
        var route = new Route { Id = "R01", Name = "Route1", Description = "Station to Mainline" };
        route.AddRoute("T01", true);
        route.AddRoute("T02", true);
        routes.Add(route);

        route = new Route { Id = "R02", Name = "Route2", Description = "Yard to Mainline" };
        route.AddRoute("T01", false);
        route.AddRoute("T02", true);
        routes.Add(route);

        route = new Route { Id = "R03", Name = "Route3", Description = "Yard to Station" };
        route.AddRoute("T01", true);
        route.AddRoute("T02", false);
        routes.Add(route);

        route = new Route { Id = "R04", Name = "Route4", Description = "Mainline to Station" };
        route.AddRoute("T01", false);
        route.AddRoute("T02", false);
        routes.Add(route);

        var accessories = manager.Accessories;
        accessories.Add(new Accessory { Name = "ACCY1", Description = "Test Accessory Description1" });

        var blocks = manager.Blocks;
        blocks.Add(new Block { Id = "B01", Name = "Mainline-West" });
        blocks.Add(new Block { Id = "B02", Name = "Mainline-East" });

        manager.Save();
    }

}