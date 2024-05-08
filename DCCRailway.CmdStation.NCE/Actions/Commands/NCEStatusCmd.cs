using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Results.Abstract;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Commands.Validators;
using DCCRailway.CmdStation.NCE.Commands.Results;

namespace DCCRailway.CmdStation.NCE.Commands;

[Command("StatusCmd", "Get the NCE Status")]
public class NCEStatusCmd : NCECommand, ICmdStatus {
    public override ICmdResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(3), new byte[] { 0xAA });
        return result.Success ? new NCECmdResultVersion(result.Data) : CmdResult.Fail(result.Data, "Failed to get NCE Status");
    }

    public override string ToString() => "GET STATUS";
}