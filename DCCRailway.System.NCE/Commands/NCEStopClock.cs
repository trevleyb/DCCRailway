using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.CommandType;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

[Command("StopClock", "Stop the NCE Clock")]
public class NCEStopClock : NCECommand, ICmdClockStop, ICommand {
    public string Name => "NCE Stop Clock";

    public override IResultOld Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x83 });

    public override string ToString() => "STOP CLOCK";
}