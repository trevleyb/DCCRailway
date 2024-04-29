using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Entities.System;
using DCCRailway.Layout.Configuration.Helpers;

namespace DCCRailway.Layout.Configuration;

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
    public static IRailwayConfig New(string name = "My Layout", string description = "", string filename = DefaultConfigFilename) {
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

    public string         Name           { get; set; } = "My Layout";
    public string         Description    { get; set; } = "";
    public string         Filename       { get; set; } = "Railway.Config.json";

    [JsonInclude] public Controllers    Controllers { get; set; } = [];
    [JsonInclude] public Parameters     Parameters { get; set; } = [];
    [JsonIgnore]  public Manufacturers  Manufacturers { get; } = new Manufacturers();

    [JsonInclude] public Accessories    Accessories   { get; set; } = [];
    [JsonInclude] public Blocks         Blocks        { get; set; } = [];
    [JsonInclude] public Locomotives    Locomotives   { get; set; } = [];
    [JsonInclude] public Sensors        Sensors       { get; set; } = [];
    [JsonInclude] public Signals        Signals       { get; set; } = [];
    [JsonInclude] public Turnouts       Turnouts      { get; set; } = [];
    [JsonInclude] public Routes         Routes        { get; set; } = [];

    public static IRailwayConfig   Load() => RailwayConfigJsonHelper<RailwayConfig>.Load(DefaultConfigFilename);
    public void                    Save() => RailwayConfigJsonHelper<RailwayConfig>.Save(this, DefaultConfigFilename);
    public static IRailwayConfig   Load(string? name) => RailwayConfigJsonHelper<RailwayConfig>.Load(name);
    public void                    Save(string? name) => RailwayConfigJsonHelper<RailwayConfig>.Save(this, name);
}