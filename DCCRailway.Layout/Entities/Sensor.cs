using DCCRailway.Common.Types;
using DCCRailway.Layout.Entities.Base;

namespace DCCRailway.Layout.Entities;

public class Sensor : ConfigWithDecoder {
    public Sensor() : base(DCCAddressType.Sensor) { }
    public Controller Controller { get; set; }
}