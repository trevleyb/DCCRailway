using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Entities.System;
using DCCRailway.Layout.Configuration.Helpers;
using DCCRailway.Layout.Configuration.Repository;
using DCCRailway.Layout.Configuration.Repository.Layout;

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
    /// Public properites that
    /// </summary>

    public string         Name           { get; set; } = "My Layout";
    public string         Description    { get; set; } = "";
    public string         Filename       { get; set; } = "Railway.Config.json";
    public SystemEntities SystemEntities { get; set; } = new();
    public LayoutEntities LayoutEntities { get; set; } = new();
    public PanelEntities  PanelEntities  { get; set; } = new();

    [JsonIgnore]public IRepository<Accessory>  Accessories  => new AccessoryRepository(LayoutEntities.Accessories);
    [JsonIgnore]public IRepository<Block>      Blocks       => new BlockRepository(LayoutEntities.Blocks);
    [JsonIgnore]public IRepository<Sensor>     Sensors      => new SensorRepository(LayoutEntities.Sensors);
    [JsonIgnore]public IRepository<Signal>     Signals      => new SignalRepository(LayoutEntities.Signals);
    [JsonIgnore]public IRepository<Turnout>    Turnouts     => new TurnoutRepository(LayoutEntities.Turnouts);
    [JsonIgnore]public IRepository<Locomotive> Locomotives  => new LocomotiveRepository(LayoutEntities.Locomotives);

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

    public static IRailwayConfig   Load() => RailwayConfigJsonHelper<IRailwayConfig>.Load(DefaultConfigFilename);
    public void                    Save() => RailwayConfigJsonHelper<IRailwayConfig>.Save(this, DefaultConfigFilename);
    public static IRailwayConfig   Load(string? name) => RailwayConfigJsonHelper<IRailwayConfig>.Load(name);
    public void                    Save(string? name) => RailwayConfigJsonHelper<IRailwayConfig>.Save(this, name);
}