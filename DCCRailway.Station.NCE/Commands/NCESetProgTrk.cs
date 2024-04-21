using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.NCE.Commands.Validators;

namespace DCCRailway.Station.NCE.Commands;

[Command("SetProgTrk", "Switch Power to the Programming Track")]
public class NCESetProgTrk : NCECommand, ICmdTrackProg {
    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), 0x9E);

    public override string ToString() => "PROGRAMMING TRACK";
}