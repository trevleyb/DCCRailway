using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities.System;
using DCCRailway.Layout.Configuration.Helpers;
using DCCRailway.LayoutService.Layout.LayoutSDK;

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

    [JsonInclude] internal ServiceSetting WiiThrottle       { get; set; } = new ServiceSetting("WiThrottle", 12090);
    [JsonInclude] internal ServiceSetting LayoutRepository  { get; set; } = new ServiceSetting("LayoutService", 13010);
    [JsonInclude] internal ServiceSetting LayoutState       { get; set; } = new ServiceSetting("StateService", 13020);

    [JsonInclude] public Controller     Controller          { get; set; } = new Controller();
    [JsonInclude] public Parameters     Parameters          { get; set; } = [];
    [JsonIgnore]  public Manufacturers  Manufacturers       { get; } = new Manufacturers();

    public Accessories Accessories => new Accessories(LayoutRepository.ServiceURL);
    public Blocks Blocks => new Blocks(LayoutRepository.ServiceURL);
    public Locomotives Locomotives => new Locomotives(LayoutRepository.ServiceURL);
    public Routes Routes => new Routes(LayoutRepository.ServiceURL);
    public Sensors Sensors => new Sensors(LayoutRepository.ServiceURL);
    public Signals Signals => new Signals(LayoutRepository.ServiceURL);
    public Turnouts Turnouts => new Turnouts(LayoutRepository.ServiceURL);

    public static IRailwayConfig   Load() => RailwayConfigJsonHelper<RailwayConfig>.Load(DefaultConfigFilename);
    public void                    Save() => RailwayConfigJsonHelper<RailwayConfig>.Save(this, DefaultConfigFilename);
    public static IRailwayConfig   Load(string? name) => RailwayConfigJsonHelper<RailwayConfig>.Load(name);
    public void                    Save(string? name) => RailwayConfigJsonHelper<RailwayConfig>.Save(this, name);
}