using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.DCCController.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("AccySetState", "Set the state of an Accessory")]
public class VirtualAccySetState : VirtualCommand, ICmdAccySetState, ICommand {
    public VirtualAccySetState() { }

    public VirtualAccySetState(DCCAccessoryState state = DCCAccessoryState.Normal) => State = state;

    public IDCCAddress       Address { get; set; }
    public DCCAccessoryState State   { get; set; }

    public override string ToString() => $"ACCY STATE ({Address} = {State})";
}