using DCCRailway.System.Adapters;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

public class NCEStatusCmd : NCECommand, ICmdStatus {
    public static string Name => "NCE Get Status";

    public override IResult Execute(IAdapter adapter) {
        var result = SendAndReceieve(adapter, new SimpleResultValidation(3), new byte[] { 0xAA });

        if (!result.OK) return result;

        return new NCEStatusResult(result.Data);
    }

    public override string ToString() {
        return "GET STATUS";
    }
}