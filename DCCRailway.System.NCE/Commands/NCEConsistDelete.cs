using DCCRailway.Core.Utilities;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

public class NCEConsistDelete : NCECommand, ICmdConsistDelete, ICommand {
    public NCEConsistDelete() { }

    public NCEConsistDelete(IDCCLoco loco) : this(loco.Address) { }

    public NCEConsistDelete(IDCCAddress address) {
        Address = address;
    }

    public IDCCAddress Address { get; set; }

    public static string Name => "NCE Consist Delete";

    public override IResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(Address.AddressBytes);
        command = command.AddToArray(0x10);
        command = command.AddToArray(0);

        return SendAndReceieve(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() {
        return $"CONSIST DELETE ({Address})";
    }
}