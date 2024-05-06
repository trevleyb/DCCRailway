using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands.Types;

namespace DCCRailway.CmdStation.Virtual.Commands;

[Command("StatusCmd", "Get the Virtual Status")]
public class VirtualStatusCmd : VirtualCommand, ICmdStatus {
    public override string ToString() => "GET STATUS";
}