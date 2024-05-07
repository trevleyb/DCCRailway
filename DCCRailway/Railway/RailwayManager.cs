using DCCRailway.Common.Helpers;
using DCCRailway.Railway.Configuration;

namespace DCCRailway.Railway;

/// <summary>
/// Railway Layout loads config, starts controllers, supports restarting and shutting down
/// and acts as the meditor between the Entities and the Controllers.
/// </summary>
public class RailwayManager(IRailwayConfig? config = null) {

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
    }

    public void Restart() {
        Stop();
        Start();
    }

    public void Stop() {
        Logger.LogContext<RailwayManager>().Information("Stopping the Railway Manager.");
        config?.Save();
    }

}