using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

[Command("SetProgTrk", "Switch Power to the Programming Track")]
public class VirtualSetProgTrk : VirtualCommand, ICmdTrackProg {
    public override string ToString() => "PROGRAMMING TRACK";
}