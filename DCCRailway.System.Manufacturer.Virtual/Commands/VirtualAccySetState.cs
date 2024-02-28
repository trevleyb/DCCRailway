using DCCRailway.Common.Types;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.System.Manufacturer.Virtual.Commands;

[Command("AccySetState", "Set the state of an Accessory")]
public class VirtualAccySetState : VirtualCommand, ICmdAccySetState, ICommand {
    public VirtualAccySetState() { }

    public VirtualAccySetState(DCCAccessoryState state = DCCAccessoryState.Normal) => State = state;

    public IDCCAddress       Address { get; set; }
    public DCCAccessoryState State   { get; set; }

    public override string ToString() => $"ACCY STATE ({Address} = {State})";
}