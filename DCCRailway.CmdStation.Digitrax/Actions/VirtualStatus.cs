using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Actions.Validators;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.Common.Helpers;

namespace DCCRailway.CmdStation.Digitrax.Actions;

[Command("Status", "Status Command")]
public class VirtualStatus : Command, ICmdStatus {
    private readonly byte[] CommandData = "STATUS_COMMAND".ToByteArray();

    public override ICmdResult Execute(IAdapter adapter) => SendAndReceive(adapter, new SimpleResultValidation(2), "STATUS_COMMAND".ToByteArray());
}