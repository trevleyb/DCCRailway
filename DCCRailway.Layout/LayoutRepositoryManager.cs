using System.Text.Json.Serialization;
using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Base;
using DCCRailway.Layout.Collection;
using DCCRailway.Layout.Entities;

namespace DCCRailway.Layout;

public sealed class LayoutRepository : JsonSerializerHelper<LayoutRepository> {
    public const string DefaultConfigFilename = "DCCRailway.Layout.json";

    private  static readonly object _lockObject = new object();
    private  static LayoutRepository? _instance = null;
    internal static LayoutRepository Instance {
        get {
            if (_instance == null) {
                lock (_lockObject) {
                    _instance ??= new LayoutRepository();
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// Instantiates a new instance of the RailwayConfig class. This is a static class so that access can be anywhere
    /// </summary>
    /// <param name="filename">The name of the file. Default will be used otherwise</param>
    /// <returns>The instance that has been created</returns>
    internal static LayoutRepository New(string filename = DefaultConfigFilename) {
        lock (_lockObject) {
            _instance = new LayoutRepository {
                Filename    = filename
            };
        }
        return Instance;
    }

    [JsonConstructor]
    internal LayoutRepository() { }

    public string Filename { get; set; } = "Railway.Layout.json";

    // List of items that are under management by yhe layout Service
    // ------------------------------------------------------------------------------------------------------------
    [JsonInclude] internal Accessories    Accessories   { get; set; } = [];
    [JsonInclude] internal Blocks         Blocks        { get; set; } = [];
    [JsonInclude] internal Locomotives    Locomotives   { get; set; } = [];
    [JsonInclude] internal Sensors        Sensors       { get; set; } = [];
    [JsonInclude] internal Signals        Signals       { get; set; } = [];
    [JsonInclude] internal Turnouts       Turnouts      { get; set; } = [];
    [JsonInclude] internal Routes         Routes        { get; set; } = [];

    public ILayoutRepository<TEntity> GetRepository<TEntity>() where TEntity: LayoutEntity {
        return typeof(TEntity) switch {
            { } t when t == typeof(Accessory)   => (ILayoutRepository<TEntity>)Accessories,
            { } t when t == typeof(Block)       => (ILayoutRepository<TEntity>)Blocks,
            { } t when t == typeof(Locomotive)  => (ILayoutRepository<TEntity>)Locomotives,
            { } t when t == typeof(Sensor)      => (ILayoutRepository<TEntity>)Sensors,
            { } t when t == typeof(Signal)      => (ILayoutRepository<TEntity>)Signals,
            { } t when t == typeof(Turnout)     => (ILayoutRepository<TEntity>)Turnouts,
            { } t when t == typeof(Turnout)     => (ILayoutRepository<TEntity>)Routes,
            _ => throw new ArgumentException($"Type {typeof(TEntity).Name} is not supported")
        };
    }


    // Access to save and load the configuration to JSON files
    // -------------------------------------------------------------------------------------------------------------
    public static LayoutRepository? Load() => LoadFile(DefaultConfigFilename);
    public void                  Save() => SaveFile(this, DefaultConfigFilename);
    public static LayoutRepository? Load(string? name) => LoadFile(name ?? DefaultConfigFilename);
    public void                  Save(string? name) => SaveFile(this, name ?? DefaultConfigFilename);
}