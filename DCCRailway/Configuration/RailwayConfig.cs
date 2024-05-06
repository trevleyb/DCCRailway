using System.Text.Json.Serialization;
using DCCRailway.Common.Configuration;
using DCCRailway.Configuration.Entities;
using DCCRailway.Configuration.Helpers;
using DCCRailway.Layout.Layout.LayoutSDK;

namespace DCCRailway.Configuration;

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

    public ServiceSetting WiThrottleSettings    { get; set; } = new ServiceSetting("WiThrottle", 12090, "DCCRailway.Withrottle.json");
    public ServiceSetting LayoutManagerSettings { get; set; } = new ServiceSetting("LayoutService", 13010, "DCCRailway.Layout.json");
    public ServiceSetting WepAppManagerSettings { get; set; } = new ServiceSetting("WebApp", 13010, "DCCRailway.WebApp.json");

    public Controller     Controller            { get; set; } = new Controller();
    public Parameters     Parameters            { get; set; } = [];
    public Manufacturers  Manufacturers         { get; } = new Manufacturers();

    [JsonIgnore] public Accessories Accessories => new Accessories(LayoutManagerSettings.ServiceURL);
    [JsonIgnore] public Blocks Blocks => new Blocks(LayoutManagerSettings.ServiceURL);
    [JsonIgnore] public Locomotives Locomotives => new Locomotives(LayoutManagerSettings.ServiceURL);
    [JsonIgnore] public Routes Routes => new Routes(LayoutManagerSettings.ServiceURL);
    [JsonIgnore] public Sensors Sensors => new Sensors(LayoutManagerSettings.ServiceURL);
    [JsonIgnore] public Signals Signals => new Signals(LayoutManagerSettings.ServiceURL);
    [JsonIgnore] public Turnouts Turnouts => new Turnouts(LayoutManagerSettings.ServiceURL);

    public static IRailwayConfig   Load() => RailwayConfigJsonHelper<RailwayConfig>.Load(DefaultConfigFilename);
    public void                    Save() => RailwayConfigJsonHelper<RailwayConfig>.Save(this, DefaultConfigFilename);
    public static IRailwayConfig   Load(string? name) => RailwayConfigJsonHelper<RailwayConfig>.Load(name);
    public void                    Save(string? name) => RailwayConfigJsonHelper<RailwayConfig>.Save(this, name);
}