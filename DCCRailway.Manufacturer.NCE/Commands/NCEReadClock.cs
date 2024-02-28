using DCCRailway.DCCController.Adapters;
using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Results;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.DCCController.Commands.Validators;
using DCCRailway.Manufacturer.NCE.Commands.Results;

namespace DCCRailway.Manufacturer.NCE.Commands;

[Command("ReadClock", "Read the Clock from the NCE Controller")]
public class NCEReadClock : Command, ICmdClockRead, ICommand {
    public override ICommandResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(2), new byte[] { 0x82 });
        return result.IsFailure ? result : new NCECommandResultClock(result.Data);
    }

    public override string ToString() => "READ CLOCK";
}