using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Commands;

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