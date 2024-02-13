using DCCRailway.Manufacturer.NCE.Commands.Results;
using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Commands.Validators;

namespace DCCRailway.Manufacturer.NCE.Commands;

[Command("ReadClock", "Read the Clock from the NCE Controller")]
public class NCEReadClock : Command, ICmdClockRead, ICommand {
    public override ICommandResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(2), new byte[] { 0x82 });
        return result.IsFailure ? result : new NCECommandResultClock(result.Data);
    }

    public override string ToString() => "READ CLOCK";
}