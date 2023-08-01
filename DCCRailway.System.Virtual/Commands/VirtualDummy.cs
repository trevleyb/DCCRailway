using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Validators;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.Virtual.Commands;

public class VirtualDummy : Command, IDummyCmd {
    public override IResult Execute(IAdapter adapter) {
        var result = SendAndReceieve(adapter, new SimpleResultValidation(2), "DUMMY_COMMAND".ToByteArray());

        if (!result.OK) return result;

        return new ResultOK(result.Data);
    }
}