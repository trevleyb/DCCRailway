using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Validators;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Digitrax.Actions;

[Command("Dummy", "Dummy Command")]
public class VirtualDummy : Command, IDummyCmd {
    protected override ICmdResult Execute(IAdapter adapter) => SendAndReceive(adapter, new SimpleResultValidation(2), "DUMMY_COMMAND".ToByteArray());
}