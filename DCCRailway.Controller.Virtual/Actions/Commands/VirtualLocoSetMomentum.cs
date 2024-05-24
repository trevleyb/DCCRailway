using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("LocoSetMomentum", "Set the Momentum of a Loco")]
public class VirtualLocoSetMomentum : VirtualCommand, ICmdLocoSetMomentum, ICommand {
    public VirtualLocoSetMomentum() { }

    public VirtualLocoSetMomentum(int address, byte momentum) : this(new DCCAddress(address), new DCCMomentum(momentum)) { }

    public VirtualLocoSetMomentum(DCCAddress address, DCCMomentum momentum) {
        Address  = address;
        Momentum = momentum;
    }

    public DCCAddress  Address  { get; set; }
    public DCCMomentum Momentum { get; set; }

    public override string ToString() {
        return $"LOCO MOMENTUM ({Address}={Momentum}";
    }
}