using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Entities.System;
using DCCRailway.Layout.Configuration.Helpers;
using DCCRailway.Layout.Configuration.Repository;
using DCCRailway.Layout.Configuration.Repository.Base;
using Makaretu.Dns;

namespace DCCRailway.Layout.Configuration;

/// <summary>
/// IRailwayConfig represents the data used to manage the railway. This will include Locomotices, Blocks,
/// Switches, Tracks, Signals etc. This is within a interface so that we can store the data in different
/// formats or styles (default is a .json file).
/// </summary>
public interface IRailwayConfig {
    public string           Name            { get; set;  }
    public string           Description     { get; set; }
    public string           Filename        { get; set; }
    public Manufacturers    Manufacturers   { get; }

    public IRepository<Controller>     ControllerRepository    { get; }
    public IRepository<Accessory>      AccessoryRepository     { get; }
    public IRepository<Block>          BlockRepository         { get; }
    public IRepository<Locomotive>     LocomotiveRepository    { get; }
    public IRepository<Sensor>         SensorRepository        { get; }
    public IRepository<Signal>         SignalRepository        { get; }
    public IRepository<Turnout>        TurnoutRepository       { get; }
    public IRepository<Route>          RouteRepository         { get; }

    public void             Save();
    public void             Save(string? name);

}