using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Types;

namespace DCCRailway.CmdStation.Virtual.Commands;

[Command("StopClock", "Stop the Virtual Clock")]
public class VirtualStopClock : VirtualCommand, ICmdClockStop, ICommand {
    public override string ToString() => "STOP CLOCK";
}