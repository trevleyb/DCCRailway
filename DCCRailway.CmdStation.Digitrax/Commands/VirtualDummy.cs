using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Commands.Validators;
using DCCRailway.Common.Helpers;

namespace DCCRailway.CmdStation.Digitrax.Commands;

[Command("Dummy", "Dummy Command")]
public class VirtualDummy : Command, IDummyCmd {
    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new SimpleResultValidation(2), "DUMMY_COMMAND".ToByteArray());
}