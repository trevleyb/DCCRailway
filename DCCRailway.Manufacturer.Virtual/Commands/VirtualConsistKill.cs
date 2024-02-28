using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.DCCController.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("ConsistKill", "Remove a whole Consist")]
public class VirtualConsistKill : VirtualCommand, ICmdConsistKill, ICommand {
    public VirtualConsistKill() { }

    public VirtualConsistKill(DCCAddress address) => Address = address;

    public IDCCAddress Address { get; set; }

    public override string ToString() => $"CONSIST KILL ({Address})";
}