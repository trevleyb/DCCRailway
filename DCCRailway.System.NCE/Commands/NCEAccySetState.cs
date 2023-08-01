using DCCRailway.Core.Utilities;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

public class NCEAccySetState : NCECommand, ICmdAccySetState, ICommand {
    public NCEAccySetState() { }

    public NCEAccySetState(DCCAccessoryState state = DCCAccessoryState.Normal) {
        State = state;
    }

    public static string Name => "NCE Set Accessory State";

    public IDCCAddress Address { get; set; }
    public DCCAccessoryState State { get; set; }

    public override IResult Execute(IAdapter adapter) {
        var cmd = new byte[] { 0xAD }; // Command is 0xAD
        cmd = cmd.AddToArray(((DCCAddress)Address).AddressBytes); // Add the high and low bytes of the Address
        cmd = cmd.AddToArray((byte)(State == DCCAccessoryState.On ? 0x03 : 0x04)); // Normal=0x03, Thrown=0x04
        cmd = cmd.AddToArray(0); // Accessory always has a data of 0x00

        return SendAndReceieve(adapter, new NCEStandardValidation(), cmd);
    }

    public override string ToString() {
        return $"ACCY STATE ({Address} = {State})";
    }
}