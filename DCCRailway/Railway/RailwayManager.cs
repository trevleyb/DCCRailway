using System.Reflection;
using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Collection;
using DCCRailway.Layout.Entities;
using DCCRailway.Railway.Configuration;
using DCCRailway.Railway.Layout;
using DCCRailway.Railway.Layout.State;
using DCCRailway.Railway.Throttles.WiThrottle;
using DCCRailway.WebApp;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace DCCRailway.Railway;

public sealed class RailwayManager : IRailwayManager {
    public const string DefaultConfigName   = "DCCRailway";
    public const string DefaultPathLocation = "./";

    public string Name {
        get => Settings.Name;
        set => Settings.Name = value;
    }

    public string Description {
        get => Settings.Description;
        set => Settings.Description = value;
    }

    public string PathName {
        get => Settings.PathName;
        set {
            Settings.PathName = value;
            foreach (var property in GetType().GetProperties().Where(x => x.PropertyType == typeof(ILayoutSaveLoad) || typeof(ILayoutSaveLoad).IsAssignableFrom(x.PropertyType))) {
                if (property.GetValue(this) is ILayoutSaveLoad saveable) saveable.PathName = value;
            }
        }
    }

    public ILogger       Logger        { get; init; }
    public Settings      Settings      { get; init; } = new();

    public Accessories   Accessories   { get; set; }
    public Blocks        Blocks        { get; set; }
    public Locomotives   Locomotives   { get; set; }
    public Routes        Routes        { get; set; }
    public Sensors       Sensors       { get; set; }
    public Signals       Signals       { get; set; }
    public Turnouts      Turnouts      { get; set; }
    public Manufacturers Manufacturers { get; set; }

    public CommandStationManager CommandStationManager { get; set; }
    public StateManager          StateManager          { get; private set; }
    public StateEventProcessor   StateProcessor        { get; private set; }
    public WiThrottleServer?     WiThrottle            { get; private set; }

    public RailwayManager (ILogger logger, string? path, string? name = null, bool? load = null, bool? runWiThrottle = null) {
        Logger   = logger;
        Name     = name ?? DefaultConfigName;
        PathName = path ?? DefaultPathLocation;
        if (runWiThrottle != null) Settings.WiThrottle.RunOnStartup = (bool)runWiThrottle;
        CreateRepositories();
        if (load is not null && (bool) load) Load();
    }

    public RailwayManager () : this(LoggerHelper.ConsoleLogger, DefaultPathLocation, DefaultConfigName, false, false) { }
    public RailwayManager (bool load) : this(LoggerHelper.ConsoleLogger, DefaultPathLocation, DefaultConfigName, load, false) { }
    public RailwayManager (string? name = null) : this(LoggerHelper.ConsoleLogger, DefaultPathLocation, name, false, false) { }
    public RailwayManager (string? path, string? name = null, bool? load = null, bool? runWiThrottle = null) : this(LoggerHelper.ConsoleLogger, path, name, load, runWiThrottle) { }
    public RailwayManager (string? path, string? name = null, bool load = false) : this(LoggerHelper.ConsoleLogger, path, name, load, false) { }
    public RailwayManager (string? path, string? name = null) : this(LoggerHelper.ConsoleLogger, path, name, false, false) { }

    /// <summary>
    /// Re-Loads the repositories into the collections. This is done when we instantiate
    /// the Railway Manager anyway, so this is a re-load function.
    /// </summary>
    public void Load() {
        Settings.Load(Settings.FullName);
        foreach (var property in GetType().GetProperties().Where(x => x.PropertyType == typeof(ILayoutSaveLoad) || typeof(ILayoutSaveLoad).IsAssignableFrom(x.PropertyType))) {
            if (property.GetValue(this) is ILayoutSaveLoad saveable) saveable.Load();
        }
    }

    /// <summary>
    /// This will clear out the existing config so that there is a set of blank items.
    /// </summary>
    public void New() {
        CreateRepositories();
    }

    /// <summary>
    ///     Saves the configuration of the railway manager.
    /// </summary>
    public void Save() {
        try {
            if (!Directory.Exists(PathName)) Directory.CreateDirectory(PathName);
        } catch (Exception ex) {
            throw new ApplicationException($"Unable to create or access the specified path for the config files '{Settings.PathName}'", ex);
        }

        try {
            Settings.Save(Settings.FullName);
        } catch (Exception ex) {
            throw new ApplicationException($"Unable to save the main Configuration settings file. '{Settings.FullName}'", ex);
        }

        foreach (var property in GetType().GetProperties().Where(x => x.PropertyType == typeof(ILayoutSaveLoad) || typeof(ILayoutSaveLoad).IsAssignableFrom(x.PropertyType))) {
            try {
                if (property.GetValue(this) is ILayoutSaveLoad saveable) saveable.Save();
            } catch (Exception ex) {
                throw new ApplicationException($"Unable to save one of the configuration files: '{property.Name}'.", ex);
            }
        }
    }

    public void Start() {
        if (Settings.Controller is { Name: not null }) {
            StateManager   = new StateManager();
            StateProcessor = new StateEventProcessor(Logger, this, StateManager);

            CommandStationManager = new CommandStationManager(Logger);//Controller, StateProcessor);
            //CommandStationManager.Start();

            if (Settings.WiThrottle.RunOnStartup) {
                WiThrottle = new WiThrottleServer(Logger, this, Settings.WiThrottle, CommandStationManager);
                WiThrottle.Start();
            }

            // This is blocking so will hold until the web-app finishes and then will exit the app
            // ------------------------------------------------------------------------------------
            var webApp = new RailwayWebApp();
            webApp.Start(new string[]{});
        }
    }

    public void Stop() {
        if (WiThrottle is not null) WiThrottle.Stop();
        if (Settings.Controller is { Name: not null }) CommandStationManager.Stop();
    }

    /// <summary>
    ///     Creates repository instances for each entity type in the RailwayManager.
    /// </summary>
    /// <param name="load">Indicates if the instantiation should load existing data.</param>
    private void CreateRepositories() {
        Accessories   = CreateRepository<Accessories>(Settings, "Accessories", "A");
        Blocks        = CreateRepository<Blocks>(Settings, "Blocks", "B");
        Locomotives   = CreateRepository<Locomotives>(Settings, "Locomotives", "L");
        Routes        = CreateRepository<Routes>(Settings, "Routes", "R");
        Sensors       = CreateRepository<Sensors>(Settings, "Sensors", "S");
        Signals       = CreateRepository<Signals>(Settings, "Signals", "G");
        Turnouts      = CreateRepository<Turnouts>(Settings, "Turnouts", "T");
        Manufacturers = CreateRepository<Manufacturers>(Settings, "Manufacturers", "M");
    }

    /// <summary>
    ///     Creates a repository of type T based on the provided settings and entity key.
    ///     This is to alolow the filename, the prefix and path location to be overridden in the
    ///     settings structure for each of the repository types.
    /// </summary>
    /// <typeparam name="T">The type of the repository to create.</typeparam>
    /// <param name="settings">The settings object containing the entity information.</param>
    /// <param name="entityKey">The key of the entity in the settings object.</param>
    /// <param name="defautPrefix">The prefix to be used when auto-generting the unique identifier</param>
    /// <param name="load">Indicates if the instantiation should load existing data.</param>
    /// <returns>A repository of type T.</returns>
    public static T CreateRepository<T>(Settings settings, string entityKey, string defautPrefix) {
        var prefix = settings.Entities[entityKey]?.Prefix ?? defautPrefix;
        var entity = (T)Activator.CreateInstance(typeof(T), prefix, settings.Name, settings.PathName)!;
        return entity;
    }
}