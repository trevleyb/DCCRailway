using System.Text.Json.Serialization;
using DCCRailway.Layout.Layout.Entities;
using DCCRailway.Railway.Configuration.Entities;
using DCCRailway.Railway.Configuration.Helpers;
using DCCRailway.Railway.Layout;
using DCCRailway.Railway.Layout.State;
using Parameters = DCCRailway.Layout.Layout.Entities.Parameters;

namespace DCCRailway.Railway.Configuration;

public sealed class RailwayManager : RailwayConfigJsonHelper, IRailwayManager {
    public const string DefaultConfigFilename = "DCCRailway.Config.json";

    private static object _lockObject = new object();
    private static IRailwayManager? _instance = null;
    public  static IRailwayManager Instance {
        get {
            _instance ??= New();
            return _instance;
        }
    }

    /// <summary>
    /// Instantiates a new instance of the RailwayConfig class. This is a static class so that access can be anywhere
    /// </summary>
    /// <param name="name">The name of this configuration/layout</param>
    /// <param name="description">A description of the layout</param>
    /// <param name="filename">The name of the file. Default will be used otherwise</param>
    /// <returns>The instance that has been created</returns>
    public static IRailwayManager New(string name = "My Entities", string description = "", string filename = DefaultConfigFilename) {
        lock (_lockObject) {
            _instance = new RailwayManager {
                Name        = name,
                Description = description,
                Filename    = filename
            };
        }
        return Instance;
    }

    public static IRailwayManager Load(string? name = null) {
        var configuration = LoadConfigFromFile(name ?? DefaultConfigFilename);
        return configuration;
    }

    public void Start() {
        if (Controller is { Name: not null }) {
            StateManager = new StateManager();
            StateProcessor = new StateEventProcessor(StateManager);
            CommandStationManager = new CommandStationManager(Controller, StateProcessor);
            CommandStationManager.Start();
        }
    }

    public void Stop() {
        if (Controller is { Name: not null }) {
            CommandStationManager.Stop();
        }
    }

    [JsonConstructor]
    private RailwayManager() { }

    /// <summary>
    /// Public properites that
    /// </summary>

    public string         Name                  { get; set; } = "My Entities";
    public string         Description           { get; set; } = "";
    public string         Filename              { get; set; } = "Railway.Config.json";

    public DCCController  Controller            { get; set; } = new Entities.DCCController();
    public Parameters     Parameters            { get; set; } = [];
    public Manufacturers  Manufacturers         { get; }      = new Manufacturers();

    public Accessories    Accessories           { get; init; } = new();
    public Blocks         Blocks                { get; init; } = new();
    public Locomotives    Locomotives           { get; init; } = new();
    public Routes         Routes                { get; init; } = new();
    public Sensors        Sensors               { get; init; } = new();
    public Signals        Signals               { get; init; } = new();
    public Turnouts       Turnouts              { get; init; } = new();

    [JsonIgnore] public CommandStationManager   CommandStationManager  { get; protected set; }
    [JsonIgnore] public StateManager            StateManager { get; protected set; }
    [JsonIgnore] public StateEventProcessor     StateProcessor { get; set; }

    public void Save() => SaveConfigToFile(this,DefaultConfigFilename);
    public void Save(string name) => SaveConfigToFile(this,name);
}