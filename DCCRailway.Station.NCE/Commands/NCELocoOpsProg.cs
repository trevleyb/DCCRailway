using DCCRailway.Common.Types;
using DCCRailway.Common.Utilities;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.NCE.Commands.Validators;

namespace DCCRailway.Station.NCE.Commands;

[Command("LocoOpsProg", "Program a Loco in Ops Mode (POM)")]
public class NCELocoOpsProg : NCECommand, ICmdLocoOpsProg, ICommand {
    public NCELocoOpsProg() { }

    public NCELocoOpsProg(int locoAddress, DCCAddressType type, int cvAddress, byte value) {
        LocoAddress = new DCCAddress(locoAddress, type);
        CVAddress   = new DCCAddress(cvAddress, DCCAddressType.CV);
        Value       = value;
    }

    public NCELocoOpsProg(IDCCAddress locoAddress, IDCCAddress cvAddress, byte value) {
        LocoAddress = locoAddress;
        CVAddress   = cvAddress;
        Value       = value;
    }

    public IDCCAddress Address     { get; set; }
    public IDCCAddress LocoAddress { get; set; }
    public IDCCAddress CVAddress   { get; set; }
    public byte        Value       { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        var cmd = new byte[] { 0xAE };
        cmd = cmd.AddToArray(LocoAddress.AddressBytes);
        cmd = cmd.AddToArray(CVAddress.AddressBytes);
        cmd = cmd.AddToArray(Value);

        return SendAndReceive(adapter, new NCEStandardValidation(), cmd);
    }

    public override string ToString() => $"LOCO OPS PROGRAMMING ({LocoAddress}:{CVAddress}={Value})";
}