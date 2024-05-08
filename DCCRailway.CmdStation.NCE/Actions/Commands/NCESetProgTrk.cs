using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.NCE.Actions.Validators;

namespace DCCRailway.CmdStation.NCE.Actions.Commands;

[Command("SetProgTrk", "Switch Power to the Programming Track")]
public class NCESetProgTrk : NCECommand, ICmdTrackProg {
    public override ICmdResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), 0x9E);

    public override string ToString() => "PROGRAMMING TRACK";
}