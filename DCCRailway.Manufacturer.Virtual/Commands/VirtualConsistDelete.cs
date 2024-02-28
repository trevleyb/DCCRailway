using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.DCCController.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("ConsistDelete", "Remove a Loco from a Consist")]
public class VirtualConsistDelete : VirtualCommand, ICmdConsistDelete, ICommand {
    public VirtualConsistDelete() { }

    public VirtualConsistDelete(DCCAddress address) => Address = address;

    public IDCCAddress Address { get; set; }

    public override string ToString() => $"CONSIST DELETE ({Address})";
}