using DCCRailway.DCCController.Adapters;
using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Results;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.Manufacturer.NCE.Commands.Validators;

namespace DCCRailway.Manufacturer.NCE.Commands;

[Command("StopClock", "Stop the NCE Clock")]
public class NCEStopClock : NCECommand, ICmdClockStop, ICommand {
    public new string Name => "NCE Stop Clock";

    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x83 });

    public override string ToString() => "STOP CLOCK";
}