using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Systems.Commands.Validators;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Systems.Virtual.Commands; 

public class VirtualDummy : CommandBase, IDummyCmd {
    public static string Name => "Virtual Dummy Command";

    public override IResult Execute(IAdapter adapter) {
        var result = SendAndReceieve(adapter, new SimpleResultValidation(2), "DUMMY_COMMAND".ToByteArray());
        if (!result.OK) return result;
        return new ResultOK(result.Data);
    }
}