using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Validators;

namespace DCCRailway.Controller.NCE.Actions.Commands;

[Command("StopClock", "Stop the NCE Clock")]
public class NCEStopClock : NCECommand, ICmdClockStop, ICommand {
    public new string Name => "NCE Stop Clock";

    protected override ICmdResult Execute(IAdapter adapter) {
        return SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x83 });
    }

    public override string ToString() {
        return "STOP CLOCK";
    }
}