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

    [JsonConstructor]
    private RailwayConfig() { }

    /// <summary>
    /// Public properites that
    /// </summary>

    public string         Name           { get; set; } = "My Layout";
    public string         Description    { get; set; } = "";
    public string         Filename       { get; set; } = "Railway.Config.json";

    public Controllers  Controllers { get; set; } = [];
    public Parameters   Parameters { get; set; } = [];

    [JsonIgnore]  internal  Manufacturers Manufacturers { get; set; } = [];
    [JsonInclude] internal  IEntityCollection<Accessory>     Accessories { get; set; } = new EntityCollection<Guid,Accessory>();
    [JsonInclude] internal  IEntityCollection<Block>         Blocks      { get; set; } = new EntityCollection<Guid,Block>();
    [JsonInclude] internal  IEntityCollection<Locomotive>    Locomotives { get; set; } = new EntityCollection<Guid,Locomotive>();
    [JsonInclude] internal  IEntityCollection<Sensor>        Sensors     { get; set; } = new EntityCollection<Guid,Sensor>();
    [JsonInclude] internal  IEntityCollection<Signal>        Signals     { get; set; } = new EntityCollection<Guid,Signal>();
    [JsonInclude] internal  IEntityCollection<Turnout>       Turnouts    { get; set; } = new EntityCollection<Guid,Turnout>();

    public IRepository<Guid,Accessory>      AccessoryRepository     => GetRepository<Guid,Accessory>()!;
    public IRepository<Guid,Block>          BlockRepository         => GetRepository<Guid,Block>()!;
    public IRepository<Guid,Locomotive>     LocomotiveRepository    => GetRepository<Guid,Locomotive>()!;
    public IRepository<Guid,Sensor>         SensorRepository        => GetRepository<Guid,Sensor>()!;
    public IRepository<Guid,Signal>         SignalRepository        => GetRepository<Guid,Signal>()!;
    public IRepository<Guid,Turnout>        TurnoutRepository       => GetRepository<Guid,Turnout>()!;
    public IRepository<Guid,Controller>     ControllerRepository    => GetRepository<Guid,Controller>()!;
    public IRepository<Guid,Manufacturer>   ManufacturerRepository  => GetRepository<Guid,Manufacturer>()!;
    public IRepository<Guid,Parameter>      ParameterRepository     => GetRepository<Guid,Parameter>()!;
    public IRepository<Guid,Adapter>        AdapterRepository       => GetRepository<Guid,Adapter>()!;

    public IRepository<TKey,TEntity>? GetRepository<TKey,TEntity>() {
        return typeof(TEntity) switch {
            { } t when t == typeof(Accessory)   => new AccessoryRepository(Accessories) as IRepository<TKey,TEntity>,
            { } t when t == typeof(Block)       => new BlockRepository(Blocks) as IRepository<TKey,TEntity>,
            { } t when t == typeof(Locomotive)  => new LocomotiveRepository(Locomotives) as IRepository<TKey,TEntity>,
            { } t when t == typeof(Sensor)      => new SensorRepository(Sensors) as IRepository<TKey,TEntity>,
            { } t when t == typeof(Signal)      => new SignalRepository(Signals) as IRepository<TKey,TEntity>,
            { } t when t == typeof(Turnout)     => new TurnoutRepository(Turnouts) as IRepository<TKey,TEntity>,
            { } t when t == typeof(Controller)  => new ControllerRepository(Controllers) as IRepository<TKey,TEntity>,
            { } t when t == typeof(Manufacturer) => new ManufacturerRepository(Manufacturers) as IRepository<TKey,TEntity>,
            _ => throw new ArgumentException($"Type {typeof(TEntity).Name} is not supported")
        };
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

    public static IRailwayConfig   Load() => RailwayConfigJsonHelper<IRailwayConfig>.Load(DefaultConfigFilename);
    public void                    Save() => RailwayConfigJsonHelper<IRailwayConfig>.Save(this, DefaultConfigFilename);
    public static IRailwayConfig   Load(string? name) => RailwayConfigJsonHelper<IRailwayConfig>.Load(name);
    public void                    Save(string? name) => RailwayConfigJsonHelper<IRailwayConfig>.Save(this, name);
}