using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("ConsistDelete", "Remove a Loco from a Consist")]
public class VirtualConsistDelete : VirtualCommand, ICmdConsistDelete, ICommand {
    public VirtualConsistDelete() { }

    public VirtualConsistDelete(DCCAddress address) => Address = address;

    public DCCAddress Address { get; set; }

    public override string ToString() => $"CONSIST DELETE ({Address})";
}