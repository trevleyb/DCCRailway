using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

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