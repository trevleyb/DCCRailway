using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("SetMainTrk", "Switch Power to the MainLine")]
public class VirtualSetMainTrk : VirtualCommand, ICmdTrackMain {
    public override string ToString() => "MAIN TRACK";
}