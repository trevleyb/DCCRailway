using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("ConsistKill", "Remove a whole Consist")]
public class VirtualConsistKill : VirtualCommand, ICmdConsistKill, ICommand {
    public VirtualConsistKill() { }

    public VirtualConsistKill(DCCAddress address) => Address = address;

    public DCCAddress Address { get; set; }

    public override string ToString() => $"CONSIST KILL ({Address})";
}