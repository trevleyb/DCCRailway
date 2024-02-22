using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("StartClock", "Start the Virtual Clock")]
public class VirtualStartClock : VirtualCommand, ICmdClockStart, ICommand {
    public override string ToString() => "STOP CLOCK";
}