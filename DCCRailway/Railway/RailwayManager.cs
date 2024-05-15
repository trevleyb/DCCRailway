using System.ComponentModel.Design;
using System.Text.Json.Serialization;
using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Base;
using DCCRailway.Layout.Collection;
using DCCRailway.Layout.Entities;
using DCCRailway.Layout.LayoutSDK;
using DCCRailway.Railway.Configuration.Entities;
using DCCRailway.Railway.Configuration.Helpers;
using DCCRailway.Railway.Layout;
using DCCRailway.Railway.Layout.State;
using DCCRailway.Railway.Throttles.WiThrottle;
using Microsoft.VisualBasic;
using Parameters = DCCRailway.Layout.Entities.Parameters;

namespace DCCRailway.Railway.Configuration;

public sealed class RailwayManager : IRailwayManager {

    public const string DefaultConfigName = "DCCRailway";
    public const string DefaultPathLocation = "./";

    public string Name          => Settings.Name;
    public string Description   => Settings.Description;
    public string Filename      => Settings.FullName;

    public Settings Settings { get; init; } = new();

    public Accessories             Accessories             { get; set; }
    public Blocks                  Blocks                  { get; set; }
    public Locomotives             Locomotives             { get; set; }
    public Routes                  Routes                  { get; set; }
    public Sensors                 Sensors                 { get; set; }
    public Signals                 Signals                 { get; set; }
    public Turnouts                Turnouts                { get; set; }
    public Manufacturers           Manufacturers           { get; set; }

    public CommandStationManager   CommandStationManager   { get; private set; }
    public StateManager            StateManager            { get; private set; }
    public StateEventProcessor     StateProcessor          { get; private set; }

    public WiThrottlePreferences WiThrottlePreferences => Settings.WiThrottle;

    private static readonly object _lockObject = new object();
    private static IRailwayManager? _instance = null;
    public  static IRailwayManager Instance {
        get {
            lock (_lockObject) {
                _instance ??= Load();
            }
            return _instance;
        }
    }

    /// <summary>
    /// Creates a new instance of the RailwayManager with the specified name and pathname.
    /// If no name and pathname are provided, default values are used.
    /// </summary>
    /// <param name="name">The name of the RailwayManager instance. Defaults to null.</param>
    /// <param name="pathname">The pathname of the RailwayManager instance. Defaults to null.</param>
    /// <returns>The new instance of the RailwayManager.</returns>
    public static IRailwayManager New(string? name = null, string? pathname = null) {
        lock (_lockObject) {
            _instance = new RailwayManager();
            _instance.Settings.Name = name ?? DefaultConfigName;
            _instance.Settings.PathName = pathname ?? DefaultPathLocation;
            CreateRepositories(_instance, false);
        }
        return Instance;
    }

    /// <summary>
    /// Loads the RailwayManager instance with the specified name and pathname. If no name and pathname are provided, default values are used.
    /// </summary>
    /// <param name="name">The name of the RailwayManager instance. Defaults to null.</param>
    /// <param name="pathname">The pathname of the RailwayManager instance. Defaults to null.</param>
    /// <returns>The loaded instance of the RailwayManager.</returns>
    public static IRailwayManager Load(string? name = null, string? pathname = null) {
        lock (_lockObject) {
            _instance = new RailwayManager();
            _instance.Settings.Name = name ?? DefaultConfigName;
            _instance.Settings.PathName = pathname ?? DefaultPathLocation;
            _instance.Settings.Load();
            _instance.Settings.Name = name ?? DefaultConfigName;
            _instance.Settings.PathName = pathname ?? DefaultPathLocation;
            CreateRepositories(_instance,true);
        }
        return Instance;
    }


    /// <summary>
    /// Creates repository instances for each entity type in the RailwayManager.
    /// </summary>
    /// <param name="instance">The instance of the RailwayManager.</param>
    /// <param name="load">Indicates if the instantiation should load existing data.</param>
    private static void CreateRepositories(IRailwayManager instance, bool load = false) {
        // TODO: Should add that the name of the system is incorporated into the file names
        // eg: MyTestRailway.Settings.Json, MyTestRailway.Accessories.Json etc.
        instance.Accessories    = CreateRepository<Accessories>(instance.Settings, "Accessories", "A",load);
        instance.Blocks         = CreateRepository<Blocks>(instance.Settings, "Blocks", "B", load);
        instance.Locomotives    = CreateRepository<Locomotives>(instance.Settings, "Locomotives", "L", load);
        instance.Routes         = CreateRepository<Routes>(instance.Settings, "Routes", "R", load);
        instance.Sensors        = CreateRepository<Sensors>(instance.Settings, "Sensors", "S", load);
        instance.Signals        = CreateRepository<Signals>(instance.Settings, "Signals", "G", load);
        instance.Turnouts       = CreateRepository<Turnouts>(instance.Settings, "Turnouts", "T", load);
        instance.Manufacturers  = CreateRepository<Manufacturers>(instance.Settings, "Manufacturers", "M", load);
    }

    /// <summary>
    /// Saves the configuration of the railway manager.
    /// </summary>
    public void Save() {
        try {
            if (!Directory.Exists(Settings.PathName)) Directory.CreateDirectory(Settings.PathName);
        } catch (Exception ex) {
            throw new ApplicationException($"Unable to create or access the specified path for the config files '{Settings.PathName}'", ex);
        }

        try {
            Settings.Save(Settings.FullName);
        } catch (Exception ex) {
            throw new ApplicationException($"Unable to save the main Configuration settings file. '{Settings.FullName}'", ex);
        }

        try {
            foreach (var property in this.GetType().GetProperties().Where(x => x.PropertyType == typeof(ILayoutSaveLoad) || typeof(ILayoutSaveLoad).IsAssignableFrom(x.PropertyType))) {
                if (property.GetValue(this) is ILayoutSaveLoad saveable) saveable.Save();
            }
        } catch (Exception ex) {
            throw new ApplicationException($"Unable to save one of the configuration files.", ex);
        }
    }

    /// <summary>
    /// Creates a repository of type T based on the provided settings and entity key.
    /// This is to alolow the filename, the prefix and path location to be overridden in the
    /// settings structure for each of the repository types.
    /// </summary>
    /// <typeparam name="T">The type of the repository to create.</typeparam>
    /// <param name="settings">The settings object containing the entity information.</param>
    /// <param name="entityKey">The key of the entity in the settings object.</param>
    /// <param name="defautPrefix">The prefix to be used when auto-generting the unique identifier</param>
    /// <param name="load">Indicates if the instantiation should load existing data.</param>
    /// <returns>A repository of type T.</returns>
    public static T CreateRepository<T>(Settings settings, string entityKey, string defautPrefix, bool load = false) {
        var prefix = settings.Entities[entityKey]?.Prefix ?? defautPrefix;
        var entity = (T)Activator.CreateInstance(typeof(T), prefix, settings.Name, settings.PathName)!;
        if (entity is ILayoutSaveLoad loadable) if (load) loadable.Load();
        return entity;
    }

    public void Start() {
        if (Settings.Controller is { Name: not null }) {
            StateManager = new StateManager();
            StateProcessor = new StateEventProcessor(StateManager);
            //CommandStationManager = new CommandStationManager(Controller, StateProcessor);
            //CommandStationManager.Start();
        }
    }

    public void Stop() {
        if (Settings.Controller is { Name: not null }) {
            CommandStationManager.Stop();
        }
    }
}