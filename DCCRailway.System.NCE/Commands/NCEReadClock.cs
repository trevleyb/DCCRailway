using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Layout.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

[Command("ReadClock", "Read the Clock from the NCE Controller")]
public class NCEReadClock : Command, ICmdClockRead, ICommand {
    public override ICommandResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(2), new byte[] { 0x82 });
        return result.IsFailure ? result : new NCECommandResultClock(result.Data);
    }

    public override string ToString() => "READ CLOCK";
}