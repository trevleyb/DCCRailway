using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Utilities;
using DCCRailway.System.NCE.Commands.Validators;
using DCCRailway.System.Types;

namespace DCCRailway.System.NCE.Commands;

[Command("ConsistKill", "Remove a whole Consist")]
public class NCEConsistKill : NCECommand, ICmdConsistKill, ICommand {
    public NCEConsistKill() { }

    public NCEConsistKill(IDCCLoco loco) : this(loco.Address) { }

    public NCEConsistKill(IDCCAddress address) {
        Address = address;
    }

    public IDCCAddress Address { get; set; }
    
    public override IResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(Address.AddressBytes);
        command = command.AddToArray(0x11);
        command = command.AddToArray(0);

        return SendAndReceieve(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() {
        return $"CONSIST KILL ({Address})";
    }
}