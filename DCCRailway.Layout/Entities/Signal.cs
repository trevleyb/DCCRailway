using DCCRailway.Common.Types;
using DCCRailway.Layout.Entities.Base;

namespace DCCRailway.Layout.Entities;

public class Signal : ConfigWithDecoder {
    public Signal() : base(DCCAddressType.Signal) { }
    public Controller Controller { get; set; }
}