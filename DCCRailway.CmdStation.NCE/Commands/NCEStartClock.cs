using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.NCE.Commands.Validators;

namespace DCCRailway.CmdStation.NCE.Commands;

[Command("StartClock", "Start the NCE Clock")]
public class NCEStartClock : NCECommand, ICmdClockStart, ICommand {
    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x84 });

    public override string ToString() => "STOP CLOCK";
}