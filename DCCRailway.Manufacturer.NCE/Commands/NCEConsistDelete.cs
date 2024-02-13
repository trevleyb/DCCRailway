using DCCRailway.Manufacturer.NCE.Commands.Validators;
using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Types;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.NCE.Commands;

[Command("ConsistDelete", "Remove a Loco from a Consist")]
public class NCEConsistDelete : NCECommand, ICmdConsistDelete, ICommand {
    public NCEConsistDelete() { }

    public NCEConsistDelete(IDCCLoco loco) : this(loco.Address) { }

    public NCEConsistDelete(IDCCAddress address) => Address = address;

    public IDCCAddress Address { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(Address.AddressBytes);
        command = command.AddToArray(0x10);
        command = command.AddToArray(0);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"CONSIST DELETE ({Address})";
}