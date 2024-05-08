using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("SetMainTrk", "Switch Power to the MainLine")]
public class VirtualSetMainTrk : VirtualCommand, ICmdTrackMain {
    public override string ToString() => "MAIN TRACK";
}