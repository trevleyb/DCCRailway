using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Commands.Validators;
using DCCRailway.System.NCE.Commands.Results;

namespace DCCRailway.System.NCE.Commands;

[Command("StatusCmd", "Get the NCE Status")]
public class NCEStatusCmd : NCECommand, ICmdStatus {
    public override ICommandResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(3), new byte[] { 0xAA });

        return result.IsOK ? new NCECommandResultVersion(result.Data) : CommandResult.Fail("Failed to get NCE Status", result.Data);
    }

    public override string ToString() => "GET STATUS";
}