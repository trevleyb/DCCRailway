using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Commands.Validators;
using DCCRailway.Common.Helpers;

namespace DCCRailway.CmdStation.Sprog.Commands;

[Command("Dummy", "Dummy Command")]
public class VirtualDummy : Command, IDummyCmd {
    public override ICmdResult Execute(IAdapter adapter) => SendAndReceive(adapter, new SimpleResultValidation(2), "DUMMY_COMMAND".ToByteArray());
}