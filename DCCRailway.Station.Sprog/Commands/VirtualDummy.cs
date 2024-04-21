using DCCRailway.Common.Utilities;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Commands.Validators;

namespace DCCRailway.Station.Sprog.Commands;

[Command("Dummy", "Dummy Command")]
public class VirtualDummy : Command, IDummyCmd {
    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new SimpleResultValidation(2), "DUMMY_COMMAND".ToByteArray());
}