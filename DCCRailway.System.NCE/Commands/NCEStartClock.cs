using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

[Command("StartClock", "Start the NCE Clock")]
public class NCEStartClock : NCECommand, ICmdClockStart, ICommand {
    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x84 });

    public override string ToString() => "STOP CLOCK";
}