using DCCRailway.Configuration.Entities.Base;
using DCCRailway.Layout.Types;

namespace DCCRailway.Configuration.Entities;

public class Signal : ConfigWithDecoder {
    public Signal() : base(DCCAddressType.Signal) { }
    public Controller Controller { get; set; }
}