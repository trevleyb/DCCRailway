using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Manufacturer.Virtual.Commands.Results;

namespace DCCRailway.System.Manufacturer.Virtual.Commands;

[Command("ReadClock", "Read the Clock from the Virtual Controller")]
public class VirtualReadClock : VirtualCommand, ICmdClockRead, ICommand {
    public override string  ToString() => "READ CLOCK";
}