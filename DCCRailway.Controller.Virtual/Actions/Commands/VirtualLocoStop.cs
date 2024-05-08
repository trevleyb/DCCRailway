using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("LocoStop", "Stop the Loco")]
public class VirtualLocoStop : VirtualCommand, ICmdLocoStop, ICommand {
    public VirtualLocoStop() { }

    public VirtualLocoStop(DCCAddress address, DCCDirection direction = DCCDirection.Forward) {
        Address   = address;
        Direction = direction;
    }

    public DCCDirection Direction { get; set; }
    public DCCAddress  Address   { get; set; }

    public override string ToString() => $"LOCO STOP ({Address}={Direction}";
}