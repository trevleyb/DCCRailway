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
    public const string DefaultConfigFilename = "DCCRailway.Config.json";

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
    /// Creates a new instance of the RailwayManager.
    /// </summary>
    /// <param name="filename">The filename of the configuration file. If not provided, a default filename will be used.</param>
    /// <param name="name">The name of the railway manager. (Optional)</param>
    /// <param name="description">The description of the railway manager. (Optional)</param>
    /// <returns>An instance of IRailwayManager.</returns>
    public static IRailwayManager New(string filename = DefaultConfigFilename, string? name = null, string? description = null) {
        lock (_lockObject) {
            _instance = new RailwayManager();
            CreateRepositories(_instance);
            SetDefaults(_instance, filename, name, description);
        }
        return Instance;
    }

    /// <summary>
    /// Loads the RailwayManager instance with the specified configuration file.
    /// </summary>
    /// <param name="filename">The filename of the configuration file. If not provided, the default filename "DCCRailway.Config.json" will be used.</param>
    /// <param name="name">The name of the railway manager. (Optional)</param>
    /// <param name="description">The description of the railway manager. (Optional)</param>
    /// <returns>An instance of IRailwayManager.</returns>
    public static IRailwayManager Load(string filename = DefaultConfigFilename, string? name = null, string? description = null) {
        lock (_lockObject) {
            _instance = new RailwayManager();
            _instance.Settings.FileName = filename;
            _instance.Settings.Load();
            SetDefaults(_instance, filename, name, description);
            CreateRepositories(_instance);
        }
        return Instance;
    }

    /// <summary>
    /// Sets the default values for the railway manager instance.
    /// </summary>
    /// <param name="instance">The railway manager instance.</param>
    /// <param name="filename">The filename of the configuration file.</param>
    /// <param name="name">The name of the railway manager. (Optional)</param>
    /// <param name="description">The description of the railway manager. (Optional)</param>
    private static void SetDefaults(IRailwayManager instance, string filename, string? name, string? description) {
        instance.Settings.Name = (string.IsNullOrEmpty(name) ? string.IsNullOrEmpty(instance.Name) ? "My Railway" : _instance?.Name : name ?? " ") ?? string.Empty;
        instance.Settings.Description = string.IsNullOrEmpty(description) ? instance.Description : description;
        instance.Settings.FileName = filename;
    }

    /// <summary>
    /// Creates repository instances for each entity type in the RailwayManager.
    /// </summary>
    /// <param name="instance">The instance of the RailwayManager.</param>
    private static void CreateRepositories(IRailwayManager instance) {
        instance.Accessories    = CreateRepository<Accessories>(instance.Settings, "Accessories");
        instance.Blocks         = CreateRepository<Blocks>(instance.Settings, "Blocks");
        instance.Locomotives    = CreateRepository<Locomotives>(instance.Settings, "Locomotives");
        instance.Routes         = CreateRepository<Routes>(instance.Settings, "Routes");
        instance.Sensors        = CreateRepository<Sensors>(instance.Settings, "Sensors");
        instance.Signals        = CreateRepository<Signals>(instance.Settings, "Signals");
        instance.Turnouts       = CreateRepository<Turnouts>(instance.Settings, "Turnouts");
        instance.Manufacturers  = CreateRepository<Manufacturers>(instance.Settings, "Manufacturers");
    }

    /// <summary>
    /// Saves the configuration of the railway manager.
    /// </summary>
    public void Save() {
        Settings.Save(Settings,Settings.FullName);
        foreach (var property in this.GetType().GetProperties().Where(x => x.PropertyType == typeof(ILayoutSaveLoad) || typeof(ILayoutSaveLoad).IsAssignableFrom(x.PropertyType))) {
            if (property.GetValue(this) is ILayoutSaveLoad saveable) saveable.Save();
        }
    }

    /// <summary>
    /// Saves the configuration of the railway manager to a file with the provided filename.
    /// </summary>
    /// <param name="filename">The filename of the configuration file.</param>
    public void Save(string filename) {
        Settings.FileName = filename;
        Save();
    }

    /// <summary>
    /// Creates a repository of type T based on the provided settings and entity key.
    /// This is to alolow the filename, the prefix and path location to be overridden in the
    /// settings structure for each of the repository types.
    /// </summary>
    /// <typeparam name="T">The type of the repository to create.</typeparam>
    /// <param name="settings">The settings object containing the entity information.</param>
    /// <param name="entityKey">The key of the entity in the settings object.</param>
    /// <returns>A repository of type T.</returns>
    public static T CreateRepository<T>(Settings settings, string entityKey) {
        var prefix = settings?.Entities[entityKey]?.Prefix ?? "A";
        var fileName = settings?.Entities[entityKey]?.FileName;
        var pathName = settings?.PathName;
        var entity = (T)Activator.CreateInstance(typeof(T), prefix, fileName, pathName)!;
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