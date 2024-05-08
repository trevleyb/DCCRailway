using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Validators;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Sprog.Actions.Commands;

[Command("Status", "Status Command")]
public class VirtualStatus : Command, ICmdStatus {
    private readonly byte[] CommandData = "STATUS_COMMAND".ToByteArray();
    public override ICmdResult Execute(IAdapter adapter) => SendAndReceive(adapter, new SimpleResultValidation(2), "STATUS_COMMAND".ToByteArray());
}