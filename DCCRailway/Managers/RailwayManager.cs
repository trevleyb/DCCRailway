using System.Reflection;
using DCCRailway.Common;
using DCCRailway.Controller.Analysers.NCEPacketAnalyser.Tasks;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Controller.States;
using DCCRailway.Controller.States.CommandUpdater;
using DCCRailway.Controller.States.PacketUpdater;
using DCCRailway.Controller.Tasks.Events;
using DCCRailway.WiThrottle.Server;
using DCCRailway.WiThrottle.ServiceHelper;
using ILogger = Serilog.ILogger;

namespace DCCRailway.Managers;

/// <summary>
/// Railway Manager is the main entry point for the DCCRailway (other than the WebApp). It loads the
/// data from .JSON files, starts up a controller with associated Adapters, starts the Packet Analyser
/// (if used), starts the WiThrottle Server, and starts the State Manager.
/// </summary>
/// <param name="logger"></param>
public sealed class RailwayManager(ILogger logger) {
    public IRailwaySettings Settings { get; private set; }

    public ControllerManager ControllerManager { get; private set; }
    public ControllerManager AnalyserManager   { get; private set; }
    public StateTracker      StateManager      { get; private set; }
    public Server            WiThrottle        { get; private set; }
    public ServerBroadcast   ServerBroadcast   { get; private set; }

    /// <summary>
    /// Re-Loads the repositories into the collections. This is done when we instantiate
    /// the Railway Manager anyway, so this is a re-load function.
    /// </summary>
    public RailwayManager Load(string path, string name) {
        Settings = new RailwaySettings(logger);
        Settings.Load(path, name);
        return this;
    }

    /// <summary>
    /// This will clear out the existing config so that there is a set of blank items.
    /// </summary>
    public RailwayManager New(string path, string name) {
        Settings = new RailwaySettings(logger);
        Settings.New(path, name);
        return this;
    }

    /// <summary>
    ///     Saves the configuration of the railway manager.
    /// </summary>
    public void Save() {
        Settings.Save();
    }

    public void Start(string url) {
        StateManager = new StateTracker(logger);

        // Create an instance of the Main Controller that controls the layout
        // -------------------------------------------------------------------
        if (Settings.Controller is { Name: not null }) {
            //var cmdStateUpdater = new CmdStateUpdater(Logger, StateManager);
            ControllerManager                 =  new ControllerManager(logger, Settings.Controller);
            ControllerManager.ControllerEvent += ControllerManagerOnControllerEvent;
            ControllerManager.TaskEvent       += ControllerManagerOnTaskEvent;
            ControllerManager.Start();

            if (Settings.WiThrottlePrefs.RunOnStartup) {
                WiThrottle = new Server(logger, Settings);
                WiThrottle.Start(ControllerManager.CommandStation!);
            }
        } else {
            logger.Warning("No controller has been defined in settings. Only WebApp will run.");
        }

        // If we have defined a Packet Analyser, then create an instance of the Packet Analyser
        // -------------------------------------------------------------------
        if (Settings.Analyser is { Name: not null }) {
            //var pktStateUpdater = new PacketStateUpdater(Logger, StateManager);
            AnalyserManager                 =  new ControllerManager(logger, Settings.Analyser);
            AnalyserManager.ControllerEvent += AnalyserManagerOnControllerEvent;
            AnalyserManager.TaskEvent       += AnalyserManagerOnTaskEvent;
            AnalyserManager.Start();
        }

        ServerBroadcast = new ServerBroadcast(logger);
        ServerBroadcast.Start(url);
        WriteProductDetailsToLogger();
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
        ServerBroadcast?.Stop();
        WiThrottle?.Stop();
        ControllerManager?.Stop();
        AnalyserManager?.Stop();
        logger.Information("All finished. Shutting down.");
    }

    public void WriteProductDetailsToLogger() {
        var assembly = Assembly.GetExecutingAssembly();

        // Get product name
        var productAttributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
        if (productAttributes.Length > 0) {
            var productAttribute    = (AssemblyProductAttribute)productAttributes[0];
            var productName         = productAttribute.Product;
            var productDescriptions = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            var description         = productDescriptions.Length > 0 ? ((AssemblyDescriptionAttribute)productDescriptions[0]).Description : "";
            logger.Information($"{productName ?? "DCCRailway"} - {description}");
        }

        // Get copyright
        var copyrightAttributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
        if (copyrightAttributes.Length > 0) {
            var copyrightAttribute = (AssemblyCopyrightAttribute)copyrightAttributes[0];
            var copyright          = copyrightAttribute.Copyright;
            logger.Information("Copyright " + copyright ?? "(c) Trevor Leybourne, 2024");
        }

        // Get version
        var version = assembly.GetName().Version;
        logger.Information("Version " + version ?? "0.0.0.0");
    }

    //  Validate that the name of the layout exists in the path, or if not, then we
    //  look for it in the provided path, and if it still does not exist then return
    //  a default name or set the layout to this name.
    //  ---------------------------------------------------------------------------------
    public string ValidateName(string path, string? name) {
        return name ?? FindConfigFile(path) ?? "DCCRailway";
    }

    //  Look for the config file in the specified folder.
    //  ---------------------------------------------------------------------------------
    public string? FindConfigFile(string path) {
        return Directory.GetFiles(path).Select(Path.GetFileName).FirstOrDefault(file => file != null && file.EndsWith(".Settings.json", StringComparison.InvariantCultureIgnoreCase))?.Replace(".Settings.json", "", StringComparison.InvariantCultureIgnoreCase);
    }

    //
    //  Validate that the Path provided is a Valid Path. If it does not exist
    //  then create the path if we can.
    //  -----------------------------------------------------------------------
    public string ValidatePath(string path) {
        if (Directory.Exists(path)) return path;

        try {
            var dirInfo = Directory.CreateDirectory(path);
            if (dirInfo.Exists) return path;
            throw new Exception("Failed to find configuration directory or create directory.");
        } catch (Exception ex) {
            throw new Exception("Failed to find configuration directory or create directory.", ex);
        }
    }
}