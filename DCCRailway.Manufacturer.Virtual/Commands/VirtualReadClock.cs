using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Results;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("ReadClock", "Read the Clock from the Virtual Controller")]
public class VirtualReadClock : VirtualCommand, ICmdClockRead, ICommand {
    public override string  ToString() => "READ CLOCK";
}