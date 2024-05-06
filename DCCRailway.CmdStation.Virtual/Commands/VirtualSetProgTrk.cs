using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands.Types;

namespace DCCRailway.CmdStation.Virtual.Commands;

[Command("SetProgTrk", "Switch Power to the Programming Track")]
public class VirtualSetProgTrk : VirtualCommand, ICmdTrackProg {
    public override string ToString() => "PROGRAMMING TRACK";
}