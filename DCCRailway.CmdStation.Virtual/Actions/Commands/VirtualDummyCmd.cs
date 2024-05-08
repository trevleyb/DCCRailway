using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Attributes;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("DummyCmd", "A Dummy Command that does not do anything")]
public class VirtualDummyCmd : VirtualCommand, IDummyCmd {
    public override string ToString() => "DUMMY CMD";
}