using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("StatusCmd", "Get the Virtual Status")]
public class VirtualStatusCmd : VirtualCommand, ICmdStatus {
    public override string ToString() => "GET STATUS";
}