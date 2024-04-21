using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.NCE.Commands.Validators;

namespace DCCRailway.Station.NCE.Commands;

[Command("StopClock", "Stop the NCE Clock")]
public class NCEStopClock : NCECommand, ICmdClockStop, ICommand {
    public new string Name => "NCE Stop Clock";

    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x83 });

    public override string ToString() => "STOP CLOCK";
}