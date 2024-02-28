using DCCRailway.DCCController.Adapters;
using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Results;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.DCCController.Commands.Validators;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.Digitrax.Commands;

[Command("Status", "Status Command")]
public class VirtualStatus : Command, ICmdStatus {
    private readonly byte[] CommandData = "STATUS_COMMAND".ToByteArray();

    public override ICommandResult Execute(IAdapter adapter) {
        return SendAndReceive(adapter, new SimpleResultValidation(2), "STATUS_COMMAND".ToByteArray());
    }
}