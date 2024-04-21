using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

[Command("ConsistKill", "Remove a whole Consist")]
public class VirtualConsistKill : VirtualCommand, ICmdConsistKill, ICommand {
    public VirtualConsistKill() { }

    public VirtualConsistKill(DCCAddress address) => Address = address;

    public IDCCAddress Address { get; set; }

    public override string ToString() => $"CONSIST KILL ({Address})";
}