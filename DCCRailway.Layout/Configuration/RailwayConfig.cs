using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Entities.System;
using DCCRailway.Layout.Configuration.Helpers;
using DCCRailway.Layout.Configuration.Repository;
using DCCRailway.Layout.Configuration.Repository.Base;

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

    [JsonInclude] public Controllers     Controllers { get; set; } = [];
    [JsonInclude] public Parameters      Parameters { get; set; } = [];
    [JsonIgnore]  public Manufacturers   Manufacturers { get; } = new Manufacturers();

    [JsonInclude] [JsonPropertyName("Accessories")] private Accessories Accessories   { get; set; } = [];
    [JsonInclude] [JsonPropertyName("Blocks")]      private Blocks      Blocks        { get; set; } = [];
    [JsonInclude] [JsonPropertyName("Locomotives")] private Locomotives Locomotives   { get; set; } = [];
    [JsonInclude] [JsonPropertyName("Sensors")]     private Sensors     Sensors       { get; set; } = [];
    [JsonInclude] [JsonPropertyName("Signals")]     private Signals     Signals       { get; set; } = [];
    [JsonInclude] [JsonPropertyName("Turnouts")]    private Turnouts    Turnouts      { get; set; } = [];

    [JsonIgnore] public IRepository<Controller>    ControllerRepository    => GetRepository<Controller>()!;
    [JsonIgnore] public IRepository<Accessory>     AccessoryRepository     => GetRepository<Accessory>()!;
    [JsonIgnore] public IRepository<Block>         BlockRepository         => GetRepository<Block>()!;
    [JsonIgnore] public IRepository<Locomotive>    LocomotiveRepository    => GetRepository<Locomotive>()!;
    [JsonIgnore] public IRepository<Sensor>        SensorRepository        => GetRepository<Sensor>()!;
    [JsonIgnore] public IRepository<Signal>        SignalRepository        => GetRepository<Signal>()!;
    [JsonIgnore] public IRepository<Turnout>       TurnoutRepository       => GetRepository<Turnout>()!;

    private IRepository<TEntity>? GetRepository<TEntity>() {
        return typeof(TEntity) switch {
            { } t when t == typeof(Accessory)   => new AccessoryRepository(Accessories) as IRepository<TEntity>,
            { } t when t == typeof(Block)       => new BlockRepository(Blocks) as IRepository<TEntity>,
            { } t when t == typeof(Locomotive)  => new LocomotiveRepository(Locomotives) as IRepository<TEntity>,
            { } t when t == typeof(Sensor)      => new SensorRepository(Sensors) as IRepository<TEntity>,
            { } t when t == typeof(Signal)      => new SignalRepository(Signals) as IRepository<TEntity>,
            { } t when t == typeof(Turnout)     => new TurnoutRepository(Turnouts) as IRepository<TEntity>,
            { } t when t == typeof(Controller)  => new ControllerRepository(Controllers) as IRepository<TEntity>,
            _ => throw new ArgumentException($"Type {typeof(TEntity).Name} is not supported")
        };
    }

    public static IRailwayConfig   Load() => RailwayConfigJsonHelper<RailwayConfig>.Load(DefaultConfigFilename);
    public void                    Save() => RailwayConfigJsonHelper<RailwayConfig>.Save(this, DefaultConfigFilename);
    public static IRailwayConfig   Load(string? name) => RailwayConfigJsonHelper<RailwayConfig>.Load(name);
    public void                    Save(string? name) => RailwayConfigJsonHelper<RailwayConfig>.Save(this, name);
}