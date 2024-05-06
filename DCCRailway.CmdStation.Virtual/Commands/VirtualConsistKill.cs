using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Commands;

[Command("ConsistKill", "Remove a whole Consist")]
public class VirtualConsistKill : VirtualCommand, ICmdConsistKill, ICommand {
    public VirtualConsistKill() { }

    public VirtualConsistKill(DCCAddress address) => Address = address;

    public IDCCAddress Address { get; set; }

    public override string ToString() => $"CONSIST KILL ({Address})";
}