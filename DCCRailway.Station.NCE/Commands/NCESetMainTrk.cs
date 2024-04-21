using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.NCE.Commands.Validators;

namespace DCCRailway.Station.NCE.Commands;

[Command("SetMainTrk", "Switch Power to the MainLine")]
public class NCESetMainTrk : NCECommand, ICmdTrackMain {
    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), 0x9F);

    public override string ToString() => "MAIN TRACK";
}