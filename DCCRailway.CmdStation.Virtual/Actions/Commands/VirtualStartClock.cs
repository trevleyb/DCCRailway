using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Types;

namespace DCCRailway.CmdStation.Virtual.Commands;

[Command("StartClock", "Start the Virtual Clock")]
public class VirtualStartClock : VirtualCommand, ICmdClockStart, ICommand {
    public override string ToString() => "STOP CLOCK";
}