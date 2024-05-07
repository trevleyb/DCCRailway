using DCCRailway.Railway.Configuration;

namespace DCCRailway.Railway.EndPoints;

public static class AddEndPointsHelper {
    public static void AddEndPoints(this WebApplication app, IRailwayConfig config) {
        BlockAPI.Configure(app,config.Blocks);
        SignalAPI.Configure(app,config.Signals);
        SensorAPI.Configure(app,config.Sensors);
        AccessoryApi.Configure(app,config.Accessories);
        TurnoutAPI.Configure(app,config.Turnouts);
        LocomotiveAPI.Configure(app,config.Locomotives);
        RouteAPI.Configure(app,config.Routes);
        StateAPI.Configure(app, config.States);
    }
}