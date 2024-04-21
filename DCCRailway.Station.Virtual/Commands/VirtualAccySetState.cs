using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

[Command("AccySetState", "Set the state of an Accessory")]
public class VirtualAccySetState : VirtualCommand, ICmdAccySetState, ICommand {
    public VirtualAccySetState() { }

    public VirtualAccySetState(DCCAccessoryState state = DCCAccessoryState.Normal) => State = state;

    public IDCCAddress       Address { get; set; }
    public DCCAccessoryState State   { get; set; }

    public override string ToString() => $"ACCY STATE ({Address} = {State})";
}