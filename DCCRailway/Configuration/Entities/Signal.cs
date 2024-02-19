using DCCRailway.Configuration.Base;
using DCCRailway.Layout.Types;

namespace DCCRailway.Configuration.Entities;

public class Signal : ConfigWithDecoder {
    public Signal() : base(DCCAddressType.Signal) { }
    public Controller Controller { get; set; }
}