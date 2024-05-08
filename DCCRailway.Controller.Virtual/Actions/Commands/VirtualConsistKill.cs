using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("ConsistKill", "Remove a whole Consist")]
public class VirtualConsistKill : VirtualCommand, ICmdConsistKill, ICommand {
    public VirtualConsistKill() { }

    public VirtualConsistKill(DCCAddress address) => Address = address;

    public DCCAddress Address { get; set; }

    public override string ToString() => $"CONSIST KILL ({Address})";
}