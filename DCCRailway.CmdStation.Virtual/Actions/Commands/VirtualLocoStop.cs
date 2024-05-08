using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

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