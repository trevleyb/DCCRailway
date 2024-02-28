using DCCRailway.DCCController.Adapters;
using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Results;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.Manufacturer.NCE.Commands.Validators;

namespace DCCRailway.Manufacturer.NCE.Commands;

[Command("SetMainTrk", "Switch Power to the MainLine")]
public class NCESetMainTrk : NCECommand, ICmdTrackMain {
    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), 0x9F);

    public override string ToString() => "MAIN TRACK";
}