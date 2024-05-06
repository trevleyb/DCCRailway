using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Commands;

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