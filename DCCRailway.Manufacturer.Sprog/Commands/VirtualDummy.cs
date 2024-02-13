using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Commands.Validators;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.Sprog.Commands;

[Command("Dummy", "Dummy Command")]
public class VirtualDummy : Command, IDummyCmd {
    public override ICommandResult Execute(IAdapter adapter) {
        return SendAndReceive(adapter, new SimpleResultValidation(2), "DUMMY_COMMAND".ToByteArray());
    }
}