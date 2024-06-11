using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Converters;
using DCCRailway.Layout.Entities;
using DCCRailway.Layout.Entities.Collection;
using ILogger = Serilog.ILogger;
using Route = DCCRailway.Layout.Entities.Route;

namespace DCCRailway.Layout;

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

    // Collection/Repository Properties
    // -----------------------------------------------------------------------------
    public Settings      Settings      { get; private set; }
    public Accessories   Accessories   { get; private set; } = [];
    public Blocks        Blocks        { get; private set; } = [];
    public Locomotives   Locomotives   { get; private set; } = [];
    public Routes        Routes        { get; private set; } = [];
    public Sensors       Sensors       { get; private set; } = [];
    public Signals       Signals       { get; private set; } = [];
    public Turnouts      Turnouts      { get; private set; } = [];
    public Manufacturers Manufacturers { get; private set; } = [];

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
    public IRailwaySettings Sample(string path, string name) {
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
        LayoutStorage.SaveFile<Routes, Route>(logger, Routes, FullName(path, name, "Routes"));
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
        Routes        = LayoutStorage.LoadFile<Routes, Route>(logger, FullName(path, name, "Routes")) ?? new Routes();
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
        Routes        = new Routes();
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