
using DCCRailway.Configuration.Base;
using DCCRailway.Layout.Types;

namespace DCCRailway.Configuration.Entities;
public class Locomotive() : ConfigWithDecoder(DCCAddressType.Long) {
    public string Type { get; set; }
    public string RoadName { get; set; }
    public string RoadNumber { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }

    public new string ToString() => $"{Name}";
}