using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

[Command("SetMainTrk", "Switch Power to the MainLine")]
public class VirtualSetMainTrk : VirtualCommand, ICmdTrackMain {
    public override string ToString() => "MAIN TRACK";
}