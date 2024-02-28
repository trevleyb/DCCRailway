using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.System.Manufacturer.Virtual.Commands;

[Command("DummyCmd", "A Dummy Command that does not do anything")]
public class VirtualDummyCmd : VirtualCommand, IDummyCmd {
    public override string ToString() => "DUMMY CMD";
}