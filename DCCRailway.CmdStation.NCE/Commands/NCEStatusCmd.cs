using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Commands.Validators;
using DCCRailway.CmdStation.NCE.Commands.Results;

namespace DCCRailway.CmdStation.NCE.Commands;

[Command("StatusCmd", "Get the NCE Status")]
public class NCEStatusCmd : NCECommand, ICmdStatus {
    public override ICommandResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(3), new byte[] { 0xAA });

        return result.IsOK ? new NCECommandResultVersion(result.Data) : CommandResult.Fail("Failed to get NCE Status", result.Data);
    }

    public override string ToString() => "GET STATUS";
}