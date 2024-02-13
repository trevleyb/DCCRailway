using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

[Command("StopClock", "Stop the NCE Clock")]
public class NCEStopClock : NCECommand, ICmdClockStop, ICommand {
    public string Name => "NCE Stop Clock";

    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x83 });

    public override string ToString() => "STOP CLOCK";
}