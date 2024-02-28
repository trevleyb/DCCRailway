using DCCRailway.DCCController.Adapters;
using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Results;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.Manufacturer.NCE.Commands.Validators;

namespace DCCRailway.Manufacturer.NCE.Commands;

[Command("StartClock", "Start the NCE Clock")]
public class NCEStartClock : NCECommand, ICmdClockStart, ICommand {
    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x84 });

    public override string ToString() => "STOP CLOCK";
}