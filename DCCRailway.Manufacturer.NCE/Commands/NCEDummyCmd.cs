using DCCRailway.DCCController.Adapters;
using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Results;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.Manufacturer.NCE.Commands.Validators;

namespace DCCRailway.Manufacturer.NCE.Commands;

[Command("DummyCmd", "A Dummy Command that does not do anything")]
public class NCEDummyCmd : NCECommand, IDummyCmd {
    protected byte[] CommandData => new byte[] { 0x80 };

    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), CommandData);

    public override string ToString() => "DUMMY CMD";
}