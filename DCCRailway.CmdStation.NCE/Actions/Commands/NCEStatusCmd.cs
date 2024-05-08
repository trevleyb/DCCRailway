using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Actions.Results.Abstract;
using DCCRailway.CmdStation.Actions.Validators;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.NCE.Actions.Results;

namespace DCCRailway.CmdStation.NCE.Actions.Commands;

[Command("StatusCmd", "Get the NCE Status")]
public class NCEStatusCmd : NCECommand, ICmdStatus {
    public override ICmdResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(3), new byte[] { 0xAA });
        return result.Success ? new NCECmdResultVersion(result.Data) : CmdResult.Fail(result.Data, "Failed to get NCE Status");
    }

    public override string ToString() => "GET STATUS";
}