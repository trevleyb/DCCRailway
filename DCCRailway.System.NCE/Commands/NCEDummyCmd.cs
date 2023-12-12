using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.CommandType;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

[Command("DummyCmd", "A Dummy Command that does not do anything")]
public class NCEDummyCmd : NCECommand, IDummyCmd {
    protected byte[] CommandData => new byte[] { 0x80 };

    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), CommandData);

    public override string ToString() => "DUMMY CMD";
}