using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

[Command("DummyCmd", "A Dummy Command that does not do anything")]
public class VirtualDummyCmd : VirtualCommand, IDummyCmd {
    public override string ToString() => "DUMMY CMD";
}