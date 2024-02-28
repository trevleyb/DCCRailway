using DCCRailway.DCCController.Types;
using DCCRailway.DCCLayout.Entities.Base;

namespace DCCRailway.DCCLayout.Entities;

public class Signal : ConfigWithDecoder {
    public Signal() : base(DCCAddressType.Signal) { }
    public Controller Controller { get; set; }
}