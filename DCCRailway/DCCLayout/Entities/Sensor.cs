using DCCRailway.DCCController.Types;
using DCCRailway.DCCLayout.Entities.Base;

namespace DCCRailway.DCCLayout.Entities;

public class Sensor : ConfigWithDecoder {
    public Sensor() : base(DCCAddressType.Sensor) { }
    public Controller Controller { get; set; }
}