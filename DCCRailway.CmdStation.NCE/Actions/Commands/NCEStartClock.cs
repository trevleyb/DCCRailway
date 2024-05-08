using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.NCE.Actions.Validators;

namespace DCCRailway.CmdStation.NCE.Actions.Commands;

[Command("StartClock", "Start the NCE Clock")]
public class NCEStartClock : NCECommand, ICmdClockStart, ICommand {
    public override ICmdResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x84 });

    public override string ToString() => "STOP CLOCK";
}