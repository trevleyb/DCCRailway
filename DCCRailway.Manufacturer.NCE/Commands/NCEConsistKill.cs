using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Layout.Types;
using DCCRailway.Manufacturer.NCE.Commands.Validators;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.NCE.Commands;

[Command("ConsistKill", "Remove a whole Consist")]
public class NCEConsistKill : NCECommand, ICmdConsistKill, ICommand {
    public NCEConsistKill() { }

    public NCEConsistKill(DCCAddress address) => Address = address;

    public IDCCAddress Address { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(Address.AddressBytes);
        command = command.AddToArray(0x11);
        command = command.AddToArray(0);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"CONSIST KILL ({Address})";
}