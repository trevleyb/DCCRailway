using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Commands.Validators;
using DCCRailway.Station.NCE.Commands.Results;

namespace DCCRailway.Station.NCE.Commands;

[Command("StatusCmd", "Get the NCE Status")]
public class NCEStatusCmd : NCECommand, ICmdStatus {
    public override ICommandResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(3), new byte[] { 0xAA });

        return result.IsOK ? new NCECommandResultVersion(result.Data) : CommandResult.Fail("Failed to get NCE Status", result.Data);
    }

    public override string ToString() => "GET STATUS";
}