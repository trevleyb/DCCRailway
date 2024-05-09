using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Validators;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Results;

namespace DCCRailway.Controller.NCE.Actions.Commands;

[Command("ReadClock", "Read the Clock from the NCE CommandStation")]
public class NCEClockRead : Command, ICmdClockRead, ICommand {
    protected override ICmdResultFastClock Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(2), new byte[] { 0x82 });

        // Convert the resulting data into a Clock Object or just return the result raw
        return new NCECmdResultClock(result.Data);
    }

    public override string ToString() => "READ CLOCK";
}