using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Attributes;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("SetProgTrk", "Switch Power to the Programming Track")]
public class VirtualSetProgTrk : VirtualCommand, ICmdTrackProg {
    public override string ToString() => "PROGRAMMING TRACK";
}