using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("SetProgTrk", "Switch Power to the Programming Track")]
public class VirtualSetProgTrk : VirtualCommand, ICmdTrackProg {
    public override string ToString() => "PROGRAMMING TRACK";
}