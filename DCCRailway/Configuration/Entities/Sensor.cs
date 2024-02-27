using DCCRailway.Configuration.Entities.Base;
using DCCRailway.Layout.Types;

namespace DCCRailway.Configuration.Entities;

public class Sensor : ConfigWithDecoder {
    public Sensor() : base(DCCAddressType.Sensor) { }
    public Controller Controller { get; set; }
}