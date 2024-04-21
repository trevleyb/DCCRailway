using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

[Command("LocoSetMomentum", "Set the Momentum of a Loco")]
public class VirtualLocoSetMomentum : VirtualCommand, ICmdLocoSetMomentum, ICommand {
    public VirtualLocoSetMomentum() { }

    public VirtualLocoSetMomentum(int address, byte momentum) : this(new DCCAddress(address), new DCCMomentum(momentum)) { }

    public VirtualLocoSetMomentum(IDCCAddress address, DCCMomentum momentum) {
        Address  = address;
        Momentum = momentum;
    }

    public IDCCAddress Address  { get; set; }
    public DCCMomentum Momentum { get; set; }

    public override string ToString() => $"LOCO MOMENTUM ({Address}={Momentum}";
}