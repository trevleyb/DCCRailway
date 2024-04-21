using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

[Command("ConsistDelete", "Remove a Loco from a Consist")]
public class VirtualConsistDelete : VirtualCommand, ICmdConsistDelete, ICommand {
    public VirtualConsistDelete() { }

    public VirtualConsistDelete(DCCAddress address) => Address = address;

    public IDCCAddress Address { get; set; }

    public override string ToString() => $"CONSIST DELETE ({Address})";
}