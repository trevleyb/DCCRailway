using DCCRailway.Core.Utilities;

namespace DCCRailway.System.Virtual.Commands;

public class VirtualDummy : Command, IDummyCmd {
    public override IResult Execute(IAdapter adapter) {
        var result = SendAndReceieve(adapter, new SimpleResultValidation(2), "DUMMY_COMMAND".ToByteArray());

        if (!result.OK) return result;

        return new ResultOK(result.Data);
    }
}