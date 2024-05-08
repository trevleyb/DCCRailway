using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Attributes;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("StatusCmd", "Get the Virtual Status")]
public class VirtualStatusCmd : VirtualCommand, ICmdStatus {
    public override string ToString() => "GET STATUS";
}