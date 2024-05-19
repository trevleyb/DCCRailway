using System.Reflection;
using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Base;
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

public sealed class RailwayManager(ILogger logger) : IRailwayManager {

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
        set => Settings.PathName = value;
    }

    public ILogger       Logger        { get; init; } = logger;
    public Settings      Settings      { get; private set; }

    public Accessories   Accessories   { get; private set; } = [];
    public Blocks        Blocks        { get; private set; } = [];
    public Locomotives   Locomotives   { get; private set; } = [];
    public Routes        Routes        { get; private set; } = [];
    public Sensors       Sensors       { get; private set; } = [];
    public Signals       Signals       { get; private set; } = [];
    public Turnouts      Turnouts      { get; private set; } = [];
    public Manufacturers Manufacturers { get; private set; } = [];

    public CommandStationManager CommandStationManager { get; private set; }
    public StateManager          StateManager          { get; private set; }
    public StateUpdater   StateProcessor        { get; private set; }
    public WiThrottleServer?     WiThrottle            { get; private set; }

    /// <summary>
    /// Re-Loads the repositories into the collections. This is done when we instantiate
    /// the Railway Manager anyway, so this is a re-load function.
    /// </summary>
    public IRailwayManager Load(string path, string name) {
        LoadRepositories(path,name);
        Settings.Name     = name;
        Settings.PathName = path;
        return this;
    }

    /// <summary>
    /// This will clear out the existing config so that there is a set of blank items.
    /// </summary>
    public IRailwayManager New(string path, string name) {
        CreateRepositories(path,name);
        Settings.Name = name;
        Settings.PathName = path;
        return this;
    }

    /// <summary>
    ///     Saves the configuration of the railway manager.
    /// </summary>
    public void Save() {
        SaveRepositories(PathName, Name);
    }

    public void Start() {
        if (Settings.Controller is { Name: not null }) {
            StateManager   = new StateManager();
            StateProcessor = new StateUpdater(Logger, StateManager);
            CommandStationManager = new CommandStationManager(Logger); //Controller, StateProcessor);

            //CommandStationManager.Start();

            if (Settings.WiThrottle.RunOnStartup) {
                WiThrottle = new WiThrottleServer(Logger, this, Settings.WiThrottle, CommandStationManager);
                WiThrottle.Start();
            }
        } else {
            Logger.Warning("No controller has been defined in settings. Only WebApp will run.");
        }

        // This is blocking so will hold until the web-app finishes and then will exit the app
        // ------------------------------------------------------------------------------------
        var webApp = new RailwayWebApp();
        webApp.Start(new string[]{});
    }

    public void Stop() {
        if (WiThrottle is not null) WiThrottle.Stop();
        if (Settings.Controller is { Name: not null }) CommandStationManager.Stop();
    }

    private void SaveRepositories(string path, string name) {
        JsonSerializerHelper<Settings>.SaveFile(Settings,FullName(path, name, "Settings"));
        LayoutStorage.SaveFile<Accessories, Accessory>(Logger, Accessories, FullName(path, name, "Accessories"));
        LayoutStorage.SaveFile<Blocks, Block>(Logger, Blocks,FullName(path, name, "Blocks"));
        LayoutStorage.SaveFile<Locomotives, Locomotive>(Logger, Locomotives,FullName(path, name, "Locomotives"));
        LayoutStorage.SaveFile<Routes, Route>(Logger, Routes,FullName(path, name, "Routes"));
        LayoutStorage.SaveFile<Sensors, Sensor>(Logger, Sensors,FullName(path, name, "Sensors"));
        LayoutStorage.SaveFile<Signals, Signal>(Logger, Signals,FullName(path, name, "Signals"));
        LayoutStorage.SaveFile<Turnouts, Turnout>(Logger, Turnouts,FullName(path, name, "Turnouts"));
        LayoutStorage.SaveFile<Manufacturers, Manufacturer>(Logger, Manufacturers,FullName(path, name, "Manufacturers"));
    }

    private void LoadRepositories(string path, string name) {
        Settings    = JsonSerializerHelper<Settings>.LoadFile(FullName(path, name, "Settings")) ?? new Settings();
        Accessories = LayoutStorage.LoadFile<Accessories, Accessory>(Logger, FullName(path, name, "Accessories")) ?? new Accessories();
        Blocks      = LayoutStorage.LoadFile<Blocks, Block>(Logger, FullName(path, name, "Blocks")) ?? new Blocks();
        Locomotives = LayoutStorage.LoadFile<Locomotives, Locomotive>(Logger, FullName(path, name, "Locomotives")) ?? new Locomotives();
        Routes      = LayoutStorage.LoadFile<Routes, Route>(Logger, FullName(path, name, "Routes")) ?? new Routes();
        Sensors     = LayoutStorage.LoadFile<Sensors, Sensor>(Logger, FullName(path, name, "Sensors")) ?? new Sensors();
        Signals     = LayoutStorage.LoadFile<Signals, Signal>(Logger, FullName(path, name, "Signals")) ?? new Signals();
        Turnouts    = LayoutStorage.LoadFile<Turnouts, Turnout>(Logger, FullName(path, name, "Turnouts")) ?? new Turnouts();
        Manufacturers = LayoutStorage.LoadFile<Manufacturers, Manufacturer>(Logger, FullName(path, name, "Manufacturers")) ?? new Manufacturers();
        if (Manufacturers.Count == 0) Manufacturers.BuildManufacturersList();
    }

    private void CreateRepositories(string path, string name) {
        Settings        = new Settings();
        Accessories     = new Accessories();
        Blocks          = new Blocks();
        Locomotives     = new Locomotives();
        Routes          = new Routes();
        Sensors         = new Sensors();
        Signals         = new Signals();
        Turnouts        = new Turnouts();
        Manufacturers   = new Manufacturers();

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