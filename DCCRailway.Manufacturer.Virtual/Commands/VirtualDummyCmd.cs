using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("DummyCmd", "A Dummy Command that does not do anything")]
public class VirtualDummyCmd : VirtualCommand, IDummyCmd {
    protected byte[] CommandData => new byte[] { 0x80 };

    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new VirtualStandardValidation(), CommandData);

    public override string ToString() => "DUMMY CMD";
}