using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Commands.Validators;
using DCCRailway.CmdStation.NCE.Commands.Results;

namespace DCCRailway.CmdStation.NCE.Commands;

[Command("ReadClock", "Read the Clock from the NCE Controller")]
public class NCEClockRead : Command, ICmdClockRead, ICommand {
    public override ICommandResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(2), new byte[] { 0x82 });

        return result.IsOK ? result : new NCECommandResultClock(result.Data);
    }

    public override string ToString() => "READ CLOCK";
}