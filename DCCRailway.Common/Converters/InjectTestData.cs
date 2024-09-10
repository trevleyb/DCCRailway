using DCCRailway.Common.Configuration;
using DCCRailway.Common.Entities;
using DCCRailway.Common.Types;

namespace DCCRailway.Common.Converters;

public static class InjectTestData {
    public static void SampleData(IRailwaySettings manager) {
        if (string.IsNullOrEmpty(manager.Settings.Name)) manager.Settings.Name = "Sample";
        manager.Settings.Description        = "A Test configuration file for testing";
        manager.Settings.Controller.Name    = "Virtual";
        manager.Settings.Controller.Adapter = new Adapter() { Name = "Virtual" };
        manager.Settings.Controller.Parameters.Add("Optional", "Value");

        var locomotives = manager.Locomotives;

        locomotives.Add(new Locomotive {
            Id = "CN1029", Address = new DCCAddress(1029), Name = "CN-1029", Description = "Canadian National (1029)"
        });

        locomotives.Add(new Locomotive {
            Id = "UP6502", Address = new DCCAddress(6502), Name = "UP-6502", Description = "Union Pacific (6502)"
        });

        locomotives.Add(new Locomotive {
            Id          = "NS98", Address = new DCCAddress(98, DCCAddressType.Short), Name = "NS-98",
            Description = "Norfolk Southern (98)"
        });

        locomotives.Add(new Locomotive {
            Id          = "NS97", Address = new DCCAddress(97, DCCAddressType.Short), Name = "NS-97",
            Description = "Norfolk Southern (97)"
        });

        locomotives.Add(new Locomotive {
            Id = "CN1020", Address = new DCCAddress(1020), Name = "CN-1020", Description = "Canadian National (1020)"
        });

        var sensors = manager.Sensors;
        sensors.Add(new Sensor { Id = "S01", Name = "Yard-Occupied" });
        sensors.Add(new Sensor { Id = "S02", Name = "Station-Enter" });
        sensors.Add(new Sensor { Id = "S03", Name = "Station-Exit" });
        sensors.Add(new Sensor { Id = "S04", Name = "Station-Occupied" });
        sensors.Add(new Sensor { Id = "S05", Name = "Mainline-Occupied" });

        var turnouts = manager.Turnouts;
        turnouts.Add(new Turnout { Id = "T01", Name = "Mainline-to-Station", Address = new DCCAddress(1) });
        turnouts.Add(new Turnout { Id = "T02", Name = "Mainline-to-Yard", Address    = new DCCAddress(2) });
        turnouts.Add(new Turnout { Id = "T03", Name = "Turnout 3", Address           = new DCCAddress(3) });
        turnouts.Add(new Turnout { Id = "T04", Name = "Turnout 4", Address           = new DCCAddress(4) });
        turnouts.Add(new Turnout { Id = "T05", Name = "Turnout 5", Address           = new DCCAddress(5) });

        var signals = manager.Signals;
        signals.Add(new Signal { Id = "SIG01", Name = "Entrance-to-Yard", Address      = new DCCAddress(11) });
        signals.Add(new Signal { Id = "SIG02", Name = "Exit-Yard-to-Mainline", Address = new DCCAddress(12) });
        signals.Add(new Signal { Id = "SIG03", Name = "Station-to-Mainline", Address   = new DCCAddress(13) });

        var routes = manager.TrackRoutes;
        var route  = new TrackRoute { Id = "R01", Name = "Route1", Description = "Station to Mainline" };
        route.AddRoute("T01", false);
        route.AddRoute("T02", false);
        route.AddRoute("T03", false);
        route.AddRoute("T04", false);
        route.AddRoute("T05", false);
        routes.Add(route);

        route = new TrackRoute { Id = "R02", Name = "Route2", Description = "Yard to Mainline" };
        route.AddRoute("T01", false);
        route.AddRoute("T02", false);
        route.AddRoute("T03", true);
        route.AddRoute("T04", true);
        route.AddRoute("T05", false);
        routes.Add(route);

        route = new TrackRoute { Id = "R03", Name = "Route3", Description = "Yard to Station" };
        route.AddRoute("T01", false);
        route.AddRoute("T02", false);
        routes.Add(route);

        route = new TrackRoute { Id = "R04", Name = "Route4", Description = "Mainline to Station" };
        route.AddRoute("T02", true);
        route.AddRoute("T03", false);
        route.AddRoute("T04", false);
        routes.Add(route);

        route = new TrackRoute { Id = "R05", Name = "Route5", Description = "Route 5" };
        route.AddRoute("T02", false);
        route.AddRoute("T03", false);
        route.AddRoute("T04", false);
        routes.Add(route);

        route = new TrackRoute { Id = "RX1", Name = "CrossOver1", Description = "CrossOver Passthrough" };
        route.AddRoute("T01", false);
        route.AddRoute("T02", false);
        routes.Add(route);

        route = new TrackRoute { Id = "RX2", Name = "CrossOver2", Description = "CrossOver CrossOver" };
        route.AddRoute("T01", true);
        route.AddRoute("T02", true);
        routes.Add(route);

        var accessories = manager.Accessories;
        accessories.Add(new Accessory { Name = "ACCY1", Description = "Test Accessory Description1" });

        var blocks = manager.Blocks;
        blocks.Add(new Block { Id = "B01", Name = "Mainline-West" });
        blocks.Add(new Block { Id = "B02", Name = "Mainline-East" });
    }
}