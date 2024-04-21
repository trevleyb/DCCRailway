using DCCRailway.Layout.Configuration;

namespace DCCRailway.APIEndPoints;

public static class ConfigureAPIEndPoints {
    public static void ConfigureAPIs(this WebApplication app, IRailwayConfig config) {
        BlockAPI.Configure(app,config);
        SignalAPI.Configure(app,config);
        SensorAPI.Configure(app,config);
        AccessoryAPI.Configure(app,config);
        TurnoutAPI.Configure(app,config);
        LocomotiveAPI.Configure(app,config);
        ManufacturersAPI.Configure(app,config);
    }
}