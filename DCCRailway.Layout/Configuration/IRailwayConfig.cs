using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Repository;

namespace DCCRailway.Layout.Configuration;

/// <summary>
/// IRailwayConfig represents the data used to manage the railway. This will include Locomotices, Blocks,
/// Switches, Tracks, Signals etc. This is within a interface so that we can store the data in different
/// formats or styles (default is a .json file).
/// </summary>
public interface IRailwayConfig {
    public string       Name        { get; set;  }
    public string       Description { get; set; }
    public string       Filename    { get; set; }

    public SystemEntities SystemEntities { get; set; }
    public LayoutEntities LayoutEntities { get; set; }
    public PanelEntities  PanelEntities  { get; set; }

    // IRepository Access to allow Add/Delete/Update of Entities in the collection
    [JsonIgnore]public IRepository<Accessory>  Accessories  { get; }
    [JsonIgnore]public IRepository<Block>      Blocks       { get; }
    [JsonIgnore]public IRepository<Sensor>     Sensors      { get; }
    [JsonIgnore]public IRepository<Signal>     Signals      { get; }
    [JsonIgnore]public IRepository<Turnout>    Turnouts     { get; }
    [JsonIgnore]public IRepository<Locomotive> Locomotives  { get; }

    public void                    Save();
    public void                    Save(string? name);

}