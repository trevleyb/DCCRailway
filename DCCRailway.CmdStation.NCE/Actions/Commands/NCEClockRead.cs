using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Actions.Validators;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.NCE.Actions.Results;

namespace DCCRailway.CmdStation.NCE.Actions.Commands;

[Command("ReadClock", "Read the Clock from the NCE Controller")]
public class NCEClockRead : Command, ICmdClockRead, ICommand {
    public override ICmdResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(2), new byte[] { 0x82 });

        // Convert the resulting data into a Clock Object or just return the result raw
        return new NCECmdResultClock(result.Data);
    }

    public override string ToString() => "READ CLOCK";
}