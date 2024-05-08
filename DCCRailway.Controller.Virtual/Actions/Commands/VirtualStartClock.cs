using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("StartClock", "Start the Virtual Clock")]
public class VirtualStartClock : VirtualCommand, ICmdClockStart, ICommand {
    public override string ToString() => "STOP CLOCK";
}