using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.DCCController.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("LocoSetMomentum", "Set the Momentum of a Loco")]
public class VirtualLocoSetMomentum : VirtualCommand, ICmdLocoSetMomentum, ICommand {
    public VirtualLocoSetMomentum() { }

    public VirtualLocoSetMomentum(int address, byte momentum) : this(new DCCAddress(address), momentum) { }

    public VirtualLocoSetMomentum(IDCCAddress address, byte momentum) {
        Address  = address;
        Momentum = momentum;
    }

    public IDCCAddress Address  { get; set; }
    public byte        Momentum { get; set; }

    public override string ToString() => $"LOCO MOMENTUM ({Address}={Momentum}";
}