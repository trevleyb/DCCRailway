using DCCRailway.Manufacturer.NCE.Commands.Validators;
using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;

namespace DCCRailway.Manufacturer.NCE.Commands;

[Command("StartClock", "Start the NCE Clock")]
public class NCEStartClock : NCECommand, ICmdClockStart, ICommand {
    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x84 });

    public override string ToString() => "STOP CLOCK";
}