using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

[Command("ReadClock", "Read the Clock from the Virtual Controller")]
public class VirtualReadClock : VirtualCommand, ICmdClockRead, ICommand {
    public override string ToString() => "READ CLOCK";
}