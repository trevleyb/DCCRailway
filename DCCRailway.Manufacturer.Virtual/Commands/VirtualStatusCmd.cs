using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Results;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("StatusCmd", "Get the Virtual Status")]
public class VirtualStatusCmd : VirtualCommand, ICmdStatus {
    public override string ToString() => "GET STATUS";
}