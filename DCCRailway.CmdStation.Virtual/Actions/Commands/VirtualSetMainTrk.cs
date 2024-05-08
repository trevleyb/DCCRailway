using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Attributes;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("SetMainTrk", "Switch Power to the MainLine")]
public class VirtualSetMainTrk : VirtualCommand, ICmdTrackMain {
    public override string ToString() => "MAIN TRACK";
}