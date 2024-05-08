using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Actions.Validators;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.Common.Helpers;

namespace DCCRailway.CmdStation.Digitrax.Actions;

[Command("Dummy", "Dummy Command")]
public class VirtualDummy : Command, IDummyCmd {
    public override ICmdResult Execute(IAdapter adapter) => SendAndReceive(adapter, new SimpleResultValidation(2), "DUMMY_COMMAND".ToByteArray());
}