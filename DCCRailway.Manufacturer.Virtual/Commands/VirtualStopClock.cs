using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("StopClock", "Stop the Virtual Clock")]
public class VirtualStopClock : VirtualCommand, ICmdClockStop, ICommand {
    public override string ToString() => "STOP CLOCK";
}