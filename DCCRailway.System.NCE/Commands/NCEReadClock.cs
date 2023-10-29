using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

[Command("ReadClock", "Read the Clock from the NCE Controller")]
public class NCEReadClock : Command, ICmdClockRead, ICommand {
    public override IResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(2), new byte[] { 0x82 });

        if (!result.OK) return result;

        return new NCEClockReadResult(result.Data);
    }

    public override string ToString() => "READ CLOCK";
}