using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Commands;

[Command("ConsistDelete", "Remove a Loco from a Consist")]
public class VirtualConsistDelete : VirtualCommand, ICmdConsistDelete, ICommand {
    public VirtualConsistDelete() { }

    public VirtualConsistDelete(DCCAddress address) => Address = address;

    public DCCAddress Address { get; set; }

    public override string ToString() => $"CONSIST DELETE ({Address})";
}