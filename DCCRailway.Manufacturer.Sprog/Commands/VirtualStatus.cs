using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Commands.Validators;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.Sprog.Commands;

[Command("Status", "Status Command")]
public class VirtualStatus : Command, ICmdStatus {
    private readonly byte[] CommandData = "STATUS_COMMAND".ToByteArray();

    public override ICommandResult Execute(IAdapter adapter) {
        return SendAndReceive(adapter, new SimpleResultValidation(2), "STATUS_COMMAND".ToByteArray());
    }
}