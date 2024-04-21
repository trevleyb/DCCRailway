using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

[Command("StopClock", "Stop the Virtual Clock")]
public class VirtualStopClock : VirtualCommand, ICmdClockStop, ICommand {
    public override string ToString() => "STOP CLOCK";
}