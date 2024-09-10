using DCCRailway.Common.Configuration;
using DCCRailway.Common.Converters;
using DCCRailway.Common.Entities;
using DCCRailway.Common.Entities.Collection;
using DCCRailway.Common.Helpers;
using ILogger = Serilog.ILogger;

namespace DCCRailway.Common;

public sealed class RailwaySettings(ILogger logger) : IRailwaySettings {
    public const string DefaultConfigName   = "DCCRailway";
    public const string DefaultPathLocation = "./";

    // Helper Properties
    // -----------------------------------------------------------------------------
    public string          Name            => Settings.Name;
    public string          Description     => Settings.Description;
    public string          PathName        => Settings.PathName;
    public Controller      Controller      => Settings.Controller;
    public Controller      Analyser        => Settings.Analyser;
    public WiThrottlePrefs WiThrottlePrefs => Settings.WiThrottle;

    public int WebApiPort => Settings.WebApiPort;
    public int WebAppPort => Settings.WebAppPort;

    // Collection/Repository Properties
    // -----------------------------------------------------------------------------
    public Settings      Settings      { get; private set; }
    public Accessories   Accessories   { get; private set; } = new();
    public Blocks        Blocks        { get; private set; } = new();
    public Locomotives   Locomotives   { get; private set; } = new();
    public TrackRoutes   TrackRoutes   { get; private set; } = new();
    public Sensors       Sensors       { get; private set; } = new();
    public Signals       Signals       { get; private set; } = new();
    public Turnouts      Turnouts      { get; private set; } = new();
    public Manufacturers Manufacturers { get; private set; } = new();

    /// <summary>
    /// Re-Loads the repositories into the collections. This is done when we instantiate
    /// the Railway Manager anyway, so this is a re-load function.
    /// </summary>
    public IRailwaySettings Load(string path, string name) {
        LoadRepositories(path, name);
        Settings.Name     = name;
        Settings.PathName = path;
        return this;
    }

    /// <summary>
    /// This will clear out the existing config so that there is a set of blank items.
    /// </summary>
    public IRailwaySettings New(string path, string name) {
        CreateRepositories(path, name);
        Settings.Name     = name;
        Settings.PathName = path;
        return this;
    }

    /// <summary>
    /// This will clear out the existing config so that there is a set of blank items.
    /// </summary>
    public IRailwaySettings CreateSampleData(string path, string name) {
        CreateRepositories(path, name);
        InjectTestData.SampleData(this);
        Settings.Name     = name;
        Settings.PathName = path;
        return this;
    }

    /// <summary>
    ///     Saves the configuration of the railway manager.
    /// </summary>
    public IRailwaySettings Save() {
        SaveRepositories(PathName, Name);
        return this;
    }

    private void SaveRepositories(string path, string name) {
        JsonSerializerHelper<Settings>.SaveFile(Settings, FullName(path, name, "Settings"));
        LayoutStorage.SaveFile<Accessories, Accessory>(logger, Accessories, FullName(path, name, "Accessories"));
        LayoutStorage.SaveFile<Blocks, Block>(logger, Blocks, FullName(path, name, "Blocks"));
        LayoutStorage.SaveFile<Locomotives, Locomotive>(logger, Locomotives, FullName(path, name, "Locomotives"));
        LayoutStorage.SaveFile<TrackRoutes, TrackRoute>(logger, TrackRoutes, FullName(path, name, "Routes"));
        LayoutStorage.SaveFile<Sensors, Sensor>(logger, Sensors, FullName(path, name, "Sensors"));
        LayoutStorage.SaveFile<Signals, Signal>(logger, Signals, FullName(path, name, "Signals"));
        LayoutStorage.SaveFile<Turnouts, Turnout>(logger, Turnouts, FullName(path, name, "Turnouts"));
        LayoutStorage.SaveFile<Manufacturers, Manufacturer>(logger, Manufacturers, FullName(path, name, "Manufacturers"));
    }

    private void LoadRepositories(string path, string name) {
        Settings      = JsonSerializerHelper<Settings>.LoadFile(FullName(path, name, "Settings")) ?? new Settings();
        Accessories   = LayoutStorage.LoadFile<Accessories, Accessory>(logger, FullName(path, name, "Accessories")) ?? new Accessories();
        Blocks        = LayoutStorage.LoadFile<Blocks, Block>(logger, FullName(path, name, "Blocks")) ?? new Blocks();
        Locomotives   = LayoutStorage.LoadFile<Locomotives, Locomotive>(logger, FullName(path, name, "Locomotives")) ?? new Locomotives();
        TrackRoutes   = LayoutStorage.LoadFile<TrackRoutes, TrackRoute>(logger, FullName(path, name, "Routes")) ?? new TrackRoutes();
        Sensors       = LayoutStorage.LoadFile<Sensors, Sensor>(logger, FullName(path, name, "Sensors")) ?? new Sensors();
        Signals       = LayoutStorage.LoadFile<Signals, Signal>(logger, FullName(path, name, "Signals")) ?? new Signals();
        Turnouts      = LayoutStorage.LoadFile<Turnouts, Turnout>(logger, FullName(path, name, "Turnouts")) ?? new Turnouts();
        Manufacturers = LayoutStorage.LoadFile<Manufacturers, Manufacturer>(logger, FullName(path, name, "Manufacturers")) ?? new Manufacturers();
        if (Manufacturers.Count == 0) Manufacturers.BuildManufacturersList();
    }

    private void CreateRepositories(string path, string name) {
        Settings      = new Settings();
        Accessories   = new Accessories();
        Blocks        = new Blocks();
        Locomotives   = new Locomotives();
        TrackRoutes   = new TrackRoutes();
        Sensors       = new Sensors();
        Signals       = new Signals();
        Turnouts      = new Turnouts();
        Manufacturers = new Manufacturers();
    }

    public static string FullName(string path, string name, string entity) {
        if (!Directory.Exists(path)) {
            try {
                Directory.CreateDirectory(path);
            } catch (Exception ex) {
                throw new ApplicationException($"Unable to create the specified folder '{path}'", ex);
            }
        }

        return Path.Combine(path ?? "", $"{name}.{entity}.json");
    }
}