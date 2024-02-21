using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Layout.Commands.Validators;
using DCCRailway.Manufacturer.Virtual.Commands.Results;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("StatusCmd", "Get the Virtual Status")]
public class VirtualStatusCmd : VirtualCommand, ICmdStatus {
    private readonly byte[] CommandData = "STATUS_COMMAND".ToByteArray();

    //public override ICommandResult Execute(IAdapter adapter) {
    //    var result = SendAndReceive(adapter, new SimpleResultValidation(3), new byte[] { 0xAA });
    //    return result.IsOK ? new VirtualCommandResultVersion(result.Data) : CommandResult.Fail("Failed to get Virtual Status", result.Data);
    //}

    public override ICommandResult Execute(IAdapter adapter) {
        return SendAndReceive(adapter, new SimpleResultValidation(2), "STATUS_COMMAND".ToByteArray());
    }
    public override string ToString() => "GET STATUS";
}