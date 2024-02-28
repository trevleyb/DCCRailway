using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Manufacturer.Virtual.Commands.Results;

namespace DCCRailway.System.Manufacturer.Virtual.Commands;

[Command("StatusCmd", "Get the Virtual Status")]
public class VirtualStatusCmd : VirtualCommand, ICmdStatus {
    public override string ToString() => "GET STATUS";
}