using System.Diagnostics;
using DCCRailway.CmdStation;
using DCCRailway.Common.Helpers;
using DCCRailway.Configuration;
using DCCRailway.Layout;
using DCCRailway.Throttles.WiThrottle;
using DCCRailway.WebApp;

namespace DCCRailway;

/// <summary>
/// Railway Layout loads config, starts controllers, supports restarting and shutting down
/// and acts as the meditor between the Entities and the Controllers.
/// </summary>
public class RailwayManager(IRailwayConfig? config = null) {

    private LayoutService       _layoutService;
    private LayoutWebApp        _layoutWebApp;
    private LayoutUpdater       _layoutUpdater;
    private CmdStationManager   _layoutCmdStation;
    private WiThrottleServer    _wiThrottle;

    public IRailwayConfig Config {
        get {
            try {
                config ??= RailwayConfig.Load();
            }
            catch (Exception ex) {
                throw new Exception("Unable to load the Railway Configuration.", ex);
            }
            return config;
        }
    }

    public void Start() {
        Logger.LogContext<RailwayManager>().Information("Starting the Railway Manager.");

        _layoutService      = new LayoutService();
        _layoutWebApp       = new LayoutWebApp();
        _layoutUpdater      = new LayoutUpdater();
        _layoutCmdStation   = new CmdStationManager();
        _wiThrottle         = new WiThrottleServer();

        _layoutCmdStation.Start(Config,_layoutUpdater);
        _layoutService.Start(Config.LayoutManagerSettings);
        _wiThrottle.Start(Config,_layoutCmdStation);
        _layoutWebApp.Start(Config.WepAppManagerSettings); // Blocking Call

        // Still to resolve a couple of things:
        // 1. Start the main WebApp so have access to run via Browser
        // 2. Need to reset the system based on the settings - such as set Turnouts to default

        Logger.LogContext<RailwayManager>().Information("Railway Manager finished.");
        Stop();
    }

    public void Restart() {
        Stop();
        Start();
    }

    public void Stop() {
        Logger.LogContext<RailwayManager>().Information("Stopping the Railway Manager.");
        _wiThrottle.Stop();
        _layoutCmdStation.Stop();
        _layoutService.Stop();
        _layoutWebApp.Stop();
        config?.Save();
    }

}