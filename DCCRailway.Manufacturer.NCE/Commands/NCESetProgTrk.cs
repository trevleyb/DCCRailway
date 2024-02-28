using DCCRailway.DCCController.Adapters;
using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Results;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.Manufacturer.NCE.Commands.Validators;

namespace DCCRailway.Manufacturer.NCE.Commands;

[Command("SetProgTrk", "Switch Power to the Programming Track")]
public class NCESetProgTrk : NCECommand, ICmdTrackProg {
    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), 0x9E);

    public override string ToString() => "PROGRAMMING TRACK";
}