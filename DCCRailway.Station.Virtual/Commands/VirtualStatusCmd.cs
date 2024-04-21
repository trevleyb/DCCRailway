using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

[Command("StatusCmd", "Get the Virtual Status")]
public class VirtualStatusCmd : VirtualCommand, ICmdStatus {
    public override string ToString() => "GET STATUS";
}