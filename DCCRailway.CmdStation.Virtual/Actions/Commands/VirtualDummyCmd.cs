using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands.Types;

namespace DCCRailway.CmdStation.Virtual.Commands;

[Command("DummyCmd", "A Dummy Command that does not do anything")]
public class VirtualDummyCmd : VirtualCommand, IDummyCmd {
    public override string ToString() => "DUMMY CMD";
}