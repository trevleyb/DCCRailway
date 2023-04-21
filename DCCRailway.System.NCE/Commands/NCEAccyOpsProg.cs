using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Systems.Types;
using DCCRailway.Core.Utilities;
using DCCRailway.Systems.NCE.Commands.Validators;

namespace DCCRailway.Systems.NCE.Commands; 

public class NCEAccyOpsProg : NCECommand, ICmdAccyOpsProg, ICommand {
    public NCEAccyOpsProg() { }

    public NCEAccyOpsProg(int locoAddress, DCCAddressType type, int cvAddress, byte value) {
        LocoAddress = new DCCAddress(locoAddress, type);
        CVAddress = new DCCAddress(cvAddress, DCCAddressType.CV);
        Value = value;
    }

    public NCEAccyOpsProg(IDCCAddress locoAddress, IDCCAddress cvAddress, byte value) {
        LocoAddress = locoAddress;
        CVAddress = cvAddress;
        Value = value;
    }

    public static string Name => "NCE Accessory Programming";

    public IDCCAddress LocoAddress { get; set; }
    public IDCCAddress CVAddress { get; set; }
    public byte Value { get; set; }

    public override IResult Execute(IAdapter adapter) {
        var cmd = new byte[] {0xAF};
        cmd = cmd.AddToArray(LocoAddress.AddressBytes);
        cmd = cmd.AddToArray(CVAddress.AddressBytes);
        cmd = cmd.AddToArray(Value);
        return SendAndReceieve(adapter, new NCEStandardValidation(), cmd);
    }

    public override string ToString() {
        return $"ACCY OPS PROGRAMMING ({LocoAddress}:{CVAddress}={Value})";
    }
}