using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("SetProgTrk", "Switch Power to the Programming Track")]
public class VirtualSetProgTrk : VirtualCommand, ICmdTrackProg {
    public override string ToString() => "PROGRAMMING TRACK";
}