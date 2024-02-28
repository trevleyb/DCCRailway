using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("DummyCmd", "A Dummy Command that does not do anything")]
public class VirtualDummyCmd : VirtualCommand, IDummyCmd {
    public override string ToString() => "DUMMY CMD";
}