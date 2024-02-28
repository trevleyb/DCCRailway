using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("StopClock", "Stop the Virtual Clock")]
public class VirtualStopClock : VirtualCommand, ICmdClockStop, ICommand {
    public override string ToString() => "STOP CLOCK";
}