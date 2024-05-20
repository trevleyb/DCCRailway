using DCCRailway.Layout;
using DCCRailway.StateManagement;
using DCCRailway.StateManagement.State;
using DCCRailway.WebApp;
using DCCRailway.WiThrottle;
using Serilog;

namespace DCCRailway;

public sealed class RailwayManager(ILogger logger) : IRailwayManager {

    public ILogger          Logger   { get; init; } = logger;
    public IRailwaySettings Settings { get; private set; }

    public CommandStationManager CommandStationManager { get; private set; }
    public StateManager          StateManager          { get; private set; }
    public StateUpdater          StateProcessor        { get; private set; }
    public WiThrottleServer?     WiThrottle            { get; private set; }

    /// <summary>
    /// Re-Loads the repositories into the collections. This is done when we instantiate
    /// the Railway Manager anyway, so this is a re-load function.
    /// </summary>
    public IRailwayManager Load(string path, string name) {
        Settings = new RailwaySettings(Logger);
        Settings.Load(path: path, name: name);
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
            StateManager          = new StateManager();
            StateProcessor        = new StateUpdater(Logger, StateManager);
            CommandStationManager = new CommandStationManager(Logger); //Controller, StateProcessor);

            //CommandStationManager.Start();

            if (Settings.WiThrottlePrefs.RunOnStartup) {
                WiThrottle = new WiThrottleServer(Logger, Settings); // TODO: Needs a reference to talk to a command station????
                WiThrottle.Start();
            }
        } else {
            Logger.Warning("No controller has been defined in settings. Only WebApp will run.");
        }

        // This is blocking so will hold until the web-app finishes and then will exit the app
        // ------------------------------------------------------------------------------------
        var webApp = new RailwayWebApp();
        webApp.Start(new string[] { });
    }

    public void Stop() {
        if (WiThrottle is not null) WiThrottle.Stop();
        if (Settings.Controller is { Name: not null }) CommandStationManager.Stop();
    }
}