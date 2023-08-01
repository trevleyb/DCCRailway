using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

public class NCEReadClock : Command, ICmdClockRead, ICommand {
    public static string Name => "NCE Read Fast Clock";

    public override IResult Execute(IAdapter adapter) {
        var result = SendAndReceieve(adapter, new SimpleResultValidation(2), new byte[] { 0x82 });

        if (!result.OK) return result;

        return new NCEClockReadResult(result.Data);
    }

    public override string ToString() {
        return "READ CLOCK";
    }
}