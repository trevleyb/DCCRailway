using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Virtual.Commands.Results;

namespace DCCRailway.System.Virtual.Commands;

[Command("ReadClock", "Read the Clock from the Virtual Controller")]
public class VirtualReadClock : VirtualCommand, ICmdClockRead, ICommand {
    public override string ToString() => "READ CLOCK";
}