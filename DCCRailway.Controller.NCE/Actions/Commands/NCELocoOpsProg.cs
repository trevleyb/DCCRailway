using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Validators;

namespace DCCRailway.Controller.NCE.Actions.Commands;

[Command("LocoOpsProg", "Program a Loco in Ops Mode (POM)")]
public class NCELocoOpsProg : NCECommand, ICmdLocoOpsProg, ICommand {
    public NCELocoOpsProg() { }

    public NCELocoOpsProg(int locoAddress, DCCAddressType type, int cvAddress, byte value) {
        LocoAddress = new DCCAddress(locoAddress, type);
        CVAddress   = new DCCAddress(cvAddress, DCCAddressType.CV);
        Value       = value;
    }

    public NCELocoOpsProg(DCCAddress locoAddress, DCCAddress cvAddress, byte value) {
        LocoAddress = locoAddress;
        CVAddress   = cvAddress;
        Value       = value;
    }

    public DCCAddress Address     { get; set; }
    public DCCAddress LocoAddress { get; set; }
    public DCCAddress CVAddress   { get; set; }
    public byte       Value       { get; set; }

    public override ICmdResult Execute(IAdapter adapter) {
        var cmd = new byte[] { 0xAE };
        cmd = cmd.AddToArray(LocoAddress.AddressBytes);
        cmd = cmd.AddToArray(CVAddress.AddressBytes);
        cmd = cmd.AddToArray(Value);

        return SendAndReceive(adapter, new NCEStandardValidation(), cmd);
    }

    public override string ToString() => $"LOCO OPS PROGRAMMING ({LocoAddress}:{CVAddress}={Value})";
}