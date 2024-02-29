using DCCRailway.Common.Types;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.System.Manufacturer.Virtual.Commands;

[Command("LocoStop", "Stop the Loco")]
public class VirtualLocoStop : VirtualCommand, ICmdLocoStop, ICommand {
    public VirtualLocoStop() { }

    public VirtualLocoStop(IDCCAddress address, DCCDirection direction = DCCDirection.Forward) {
        Address   = address;
        Direction = direction;
    }

    public DCCDirection Direction { get; set; }
    public IDCCAddress  Address   { get; set; }

    public override string ToString() => $"LOCO STOP ({Address}={Direction}";
}