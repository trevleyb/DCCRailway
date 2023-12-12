using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.CommandType;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Validator;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.Virtual.Commands;

[Command("Status", "Status Command")]
public class VirtualStatus : Command, ICmdStatus {
    private readonly byte[] CommandData = "STATUS_COMMAND".ToByteArray();

    public override CommandResult Execute(IAdapter adapter) {
        return SendAndReceive(adapter, new SimpleResultValidation(2), "STATUS_COMMAND".ToByteArray());
    }
}