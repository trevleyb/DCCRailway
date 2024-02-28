using DCCRailway.DCCController.Adapters;
using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Results;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.DCCController.Commands.Validators;
using DCCRailway.Manufacturer.NCE.Commands.Results;

namespace DCCRailway.Manufacturer.NCE.Commands;

[Command("StatusCmd", "Get the NCE Status")]
public class NCEStatusCmd : NCECommand, ICmdStatus {
    public override ICommandResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(3), new byte[] { 0xAA });
        return result.IsOK ? new NCECommandResultVersion(result.Data) : CommandResult.Fail("Failed to get NCE Status", result.Data);
    }
    public override string ToString() => "GET STATUS";
}