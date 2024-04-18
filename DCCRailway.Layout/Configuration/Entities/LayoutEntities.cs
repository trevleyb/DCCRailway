using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Entities;

[Serializable]
public class LayoutEntities {
    public Dictionary<Guid, Accessory>  Accessories { get; set; } = new();
    public Dictionary<Guid, Block>      Blocks      { get; set; } = new();
    public Dictionary<Guid, Locomotive> Locomotives { get; set; } = new();
    public Dictionary<Guid, Sensor>     Sensors     { get; set; } = new();
    public Dictionary<Guid, Signal>     Signals     { get; set; } = new();
    public Dictionary<Guid, Turnout>    Turnouts    { get; set; } = new();
}