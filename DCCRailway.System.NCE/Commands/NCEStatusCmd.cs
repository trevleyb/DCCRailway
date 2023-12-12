using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.CommandType;
using DCCRailway.System.Commands.Result;
using DCCRailway.System.Commands.Validator;

namespace DCCRailway.System.NCE.Commands;

[Command("StatusCmd", "Get the NCE Status")]
public class NCEStatusCmd : NCECommand, ICmdStatus {
    public override IResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(3), new byte[] { 0xAA });

        if (!result.OK) return result;

        return new NCEStatusResult(result.Data);
    }

    public override string ToString() => "GET STATUS";
}