using System.Text.Json.Serialization;
using DCCRailway.Layout.Layout.Entities;
using DCCRailway.Railway.CmdStation;
using DCCRailway.Railway.Configuration.Entities;
using DCCRailway.Railway.Configuration.Helpers;
using DCCRailway.Railway.Layout.State;
using Parameters = DCCRailway.Railway.Configuration.Entities.Parameters;

namespace DCCRailway.Railway.Configuration;

public sealed class RailwayConfig : IRailwayConfig {
    public const string DefaultConfigFilename = "Railway.Config.json";

    private static object _lockObject = new object();
    private static IRailwayConfig? _instance = null;
    public  static IRailwayConfig Instance {
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
    public static IRailwayConfig New(string name = "My Entities", string description = "", string filename = DefaultConfigFilename) {
        lock (_lockObject) {
            _instance = new RailwayConfig {
                Name        = name,
                Description = description,
                Filename    = filename
            };
        }
        return Instance;
    }

    [JsonConstructor]
    private RailwayConfig() { }

    /// <summary>
    /// Public properites that
    /// </summary>

    public string         Name                  { get; set; } = "My Entities";
    public string         Description           { get; set; } = "";
    public string         Filename              { get; set; } = "Railway.Config.json";

    public Controller     Controller            { get; set; } = new Controller();
    public Parameters     Parameters            { get; set; } = [];
    public Manufacturers  Manufacturers         { get; }      = new Manufacturers();

    public Accessories    Accessories           { get; init; } = new();
    public Blocks         Blocks                { get; init; } = new();
    public Locomotives    Locomotives           { get; init; } = new();
    public Routes         Routes                { get; init; } = new();
    public Sensors        Sensors               { get; init; } = new();
    public Signals        Signals               { get; init; } = new();
    public Turnouts       Turnouts              { get; init; } = new();

    [JsonIgnore] public CmdStationManager   CmdStation  { get; set; } = new();
    [JsonIgnore] public StateManager        States { get; set; } = new();

    public static IRailwayConfig   Load() => RailwayConfigJsonHelper<RailwayConfig>.Load(DefaultConfigFilename);
    public void                    Save() => RailwayConfigJsonHelper<RailwayConfig>.Save(this, DefaultConfigFilename);
    public static IRailwayConfig   Load(string? name) => RailwayConfigJsonHelper<RailwayConfig>.Load(name);
    public void                    Save(string? name) => RailwayConfigJsonHelper<RailwayConfig>.Save(this, name);
}