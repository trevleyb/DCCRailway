using DCCRailway.Controller.Analysers.NCEPacketAnalyser.Tasks;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Controller.Tasks.Events;
using DCCRailway.Layout;
using DCCRailway.StateManager.Updater.CommandUpdater;
using DCCRailway.StateManager.Updater.PacketUpdater;
using DCCRailway.WiThrottle.Server;
using Serilog;

namespace DCCRailway.Managers;

public sealed class RailwayManager(ILogger logger, bool runWiServer, bool runWebServer) : IRailwayManager {
    private bool RunWiServer  { get; set; } = runWiServer;
    private bool RunWebServer { get; set; } = runWebServer;

    public ILogger          Logger   { get; init; } = logger;
    public IRailwaySettings Settings { get; private set; }

    public ControllerManager         ControllerManager { get; private set; }
    public ControllerManager         AnalyserManager   { get; private set; }
    public StateManager.StateManager StateManager      { get; private set; }
    public Server?                   WiThrottle        { get; private set; }
    public WebApi.Server?            WebApiServer      { get; private set; }
    public WebApp.Server?            WebAppServer      { get; private set; }

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
        StateManager = new StateManager.StateManager();

        // Create an instance of the Main Controller taht controls the layout
        // -------------------------------------------------------------------
        if (Settings.Controller is { Name: not null }) {
            //var cmdStateUpdater = new CmdStateUpdater(Logger, StateManager);
            ControllerManager                 =  new ControllerManager(Logger, Settings.Controller);
            ControllerManager.ControllerEvent += ControllerManagerOnControllerEvent;
            ControllerManager.TaskEvent       += ControllerManagerOnTaskEvent;
            ControllerManager.Start();

            if (RunWiServer && Settings.WiThrottlePrefs.RunOnStartup) {
                WiThrottle = new Server(Logger, Settings);
                WiThrottle.Start(ControllerManager.CommandStation!);
            }
        } else {
            Logger.Warning("No controller has been defined in settings. Only WebApp will run.");
        }

        // If we have defined a Packet Analyser, then create an instance of the Packet Analyser
        // -------------------------------------------------------------------
        if (Settings.Analyser is { Name: not null }) {
            //var pktStateUpdater = new PacketStateUpdater(Logger, StateManager);
            AnalyserManager                 =  new ControllerManager(Logger, Settings.Analyser);
            AnalyserManager.ControllerEvent += AnalyserManagerOnControllerEvent;
            AnalyserManager.TaskEvent       += AnalyserManagerOnTaskEvent;
            AnalyserManager.Start();
        }

        // Startup the WebServer so we have access to the data via the WebAPIs
        // -------------------------------------------------------------------
        if (RunWebServer) {
            Logger.Information("Starting WebAPI server");
            WebApiServer = new WebApi.Server(Logger, Settings);
            WebApiServer.Start();

            // This is blocking so will hold until the web-app finishes and then will exit the app
            // ------------------------------------------------------------------------------------
            Logger.Information("Starting WebAPP server");
            WebAppServer = new WebApp.Server(Logger, Settings);
            WebAppServer.Start();
        }

        Logger.Information("All finished. Shutting down.");
    }

    private void ControllerManagerOnTaskEvent(object? sender, ITaskEvent e) {
        // Do nothing at the moment
    }

    private void ControllerManagerOnControllerEvent(object? sender, ControllerEventArgs e) {
        CmdStateUpdater.Process(e, StateManager);
    }

    private void AnalyserManagerOnTaskEvent(object? sender, ITaskEvent e) {
        if (e is PacketTaskEvent packetEvent) {
            PacketStateUpdater.Process(packetEvent.PacketMessage, StateManager);
        }
    }

    private void AnalyserManagerOnControllerEvent(object? sender, ControllerEventArgs e) {
        throw new NotImplementedException();
    }

    public void Stop() {
        if (WiThrottle is not null) WiThrottle.Stop();
        if (Settings.Controller is { Name: not null }) ControllerManager.Stop();
        if (Settings.Analyser is { Name: not null }) AnalyserManager.Stop();
        if (WebApiServer is not null) WebApiServer.Stop();
    }
}