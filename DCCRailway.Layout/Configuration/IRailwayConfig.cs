using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Entities.System;
using DCCRailway.Layout.Configuration.Repository;
using Makaretu.Dns;

namespace DCCRailway.Layout.Configuration;

/// <summary>
/// IRailwayConfig represents the data used to manage the railway. This will include Locomotices, Blocks,
/// Switches, Tracks, Signals etc. This is within a interface so that we can store the data in different
/// formats or styles (default is a .json file).
/// </summary>
public interface IRailwayConfig {
    public string           Name        { get; set;  }
    public string           Description { get; set; }
    public string           Filename    { get; set; }

    public IRepository<Guid,Accessory>      AccessoryRepository     { get; }
    public IRepository<Guid,Block>          BlockRepository         { get; }
    public IRepository<Guid,Locomotive>     LocomotiveRepository    { get; }
    public IRepository<Guid,Sensor>         SensorRepository        { get; }
    public IRepository<Guid,Signal>         SignalRepository        { get; }
    public IRepository<Guid,Turnout>        TurnoutRepository       { get; }
    public IRepository<Guid,Controller>     ControllerRepository    { get; }
    public IRepository<Guid,Manufacturer>   ManufacturerRepository  { get; }
    public IRepository<Guid,Parameter>      ParameterRepository     { get; }
    public IRepository<Guid,Adapter>        AdapterRepository       { get; }

    public void             Save();
    public void             Save(string? name);

}