using DCCRailway.Common.Types;
using DCCRailway.Common.Utilities;
using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Manufacturer.NCE.Commands.Validators;

namespace DCCRailway.System.Manufacturer.NCE.Commands;

[Command("ConsistDelete", "Remove a Loco from a Consist")]
public class NCEConsistDelete : NCECommand, ICmdConsistDelete, ICommand {
    public NCEConsistDelete() { }

    public NCEConsistDelete(DCCAddress address) => Address = address;

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