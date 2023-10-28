using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.NCE.Commands.Validators;
using DCCRailway.System.Types;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.NCE.Commands;

[Command("ConsistDelete", "Remove a Loco from a Consist")]
public class NCEConsistDelete : NCECommand, ICmdConsistDelete, ICommand {
    public NCEConsistDelete() { }

    public NCEConsistDelete(IDCCLoco loco) : this(loco.Address) { }

    public NCEConsistDelete(IDCCAddress address) => Address = address;

    public IDCCAddress Address { get; set; }

    public override IResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(Address.AddressBytes);
        command = command.AddToArray(0x10);
        command = command.AddToArray(0);

        return SendAndReceieve(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"CONSIST DELETE ({Address})";
}