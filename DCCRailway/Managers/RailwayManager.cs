using DCCRailway.Layout;
using DCCRailway.Managers.Controller;
using DCCRailway.Managers.State;
using Serilog;
using Server = DCCRailway.WebApp.Server;

namespace DCCRailway.Managers;

public sealed class RailwayManager(ILogger logger) : IRailwayManager {
    public ILogger          Logger   { get; init; } = logger;
    public IRailwaySettings Settings { get; private set; }

    public ControllerManager  ControllerManager { get; private set; }
    public StateManager       StateManager      { get; private set; }
    public WiThrottle.Server? WiThrottle        { get; private set; }

    /// <summary>
    /// Re-Loads the repositories into the collections. This is done when we instantiate
    /// the Railway Manager anyway, so this is a re-load function.
    /// </summary>
    public IRailwayManager Load(string path, string name) {
        Settings = new RailwaySettings(Logger);
        Settings.Load(path, name);
        return this;
    }

    /// <summary>
    /// This will clear out the existing config so that there is a set of blank items.
    /// </summary>
    public IRailwayManager New(string path, string name) {
        Settings = new RailwaySettings(Logger);
        Settings.New(path, name);
        return this;
    }

    /// <summary>
    ///     Saves the configuration of the railway manager.
    /// </summary>
    public void Save() {
        Settings.Save();
    }

    public void Start() {
        if (Settings.Controller is { Name: not null }) {
            StateManager      = new StateManager();
            ControllerManager = new ControllerManager(Logger, StateManager, Settings.Controller);
            ControllerManager.Start();

            if (Settings.WiThrottlePrefs.RunOnStartup) {
                WiThrottle = new WiThrottle.Server(Logger, Settings);
                WiThrottle.Start(ControllerManager.CommandStation);
            }
        } else {
            Logger.Warning("No controller has been defined in settings. Only WebApp will run.");
        }

        // This is blocking so will hold until the web-app finishes and then will exit the app
        // ------------------------------------------------------------------------------------
        var webApp = new Server();
        webApp.Start(new string[] { });
    }

    public void Stop() {
        if (WiThrottle is not null) WiThrottle.Stop();
        if (Settings.Controller is { Name: not null }) ControllerManager.Stop();
    }
}