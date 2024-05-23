using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Results.Abstract;
using DCCRailway.Controller.Actions.Validators;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Results;

namespace DCCRailway.Controller.NCE.Actions.Commands;

[Command("StatusCmd", "Get the NCE Status")]
public class NCEStatusCmd : NCECommand, ICmdStatus {
    protected override ICmdResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(3), new byte[] { 0xAA });
        return result.Success
            ? new NCECmdResultVersion(result.Data)
            : CmdResult.Fail(result.Data, "Failed to get NCE Status");
    }

    public override string ToString() {
        return "GET STATUS";
    }
}