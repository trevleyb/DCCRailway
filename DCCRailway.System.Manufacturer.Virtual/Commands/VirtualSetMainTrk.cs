using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.System.Manufacturer.Virtual.Commands;

[Command("SetMainTrk", "Switch Power to the MainLine")]
public class VirtualSetMainTrk : VirtualCommand, ICmdTrackMain {
    public override string ToString() => "MAIN TRACK";
}