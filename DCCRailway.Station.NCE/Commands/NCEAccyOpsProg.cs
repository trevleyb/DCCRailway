using System;
using DCCRailway.Common.Types;
using DCCRailway.Common.Utilities;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Commands.Types.Base;
using DCCRailway.Station.NCE.Commands.Validators;

namespace DCCRailway.Station.NCE.Commands;

[Command("AccyOpsProg", "Accessory Ops Programming")]
public class NCEAccyOpsProg : NCECommand, ICmdAccyOpsProg, ICommand, IAccyCmd {
    public NCEAccyOpsProg() { }

    public NCEAccyOpsProg(int locoAddress, DCCAddressType type, int cvAddress, byte value) {
        LocoAddress = new DCCAddress(locoAddress, type);
        CVAddress   = new DCCAddress(cvAddress, DCCAddressType.CV);
        Value       = value;
    }

    public NCEAccyOpsProg(IDCCAddress locoAddress, IDCCAddress cvAddress, byte value) {
        LocoAddress = locoAddress;
        CVAddress   = cvAddress;
        Value       = value;
    }

    public new static string Name => "NCE Accessory Programming";

    public IDCCAddress Address     { get; set; }
    public IDCCAddress LocoAddress { get; set; }
    public IDCCAddress CVAddress   { get; set; }
    public byte        Value       { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        var cmd = new byte[] { 0xAF };
        cmd = cmd.AddToArray(LocoAddress.AddressBytes);
        cmd = cmd.AddToArray(CVAddress.AddressBytes);
        cmd = cmd.AddToArray(Value);

        return SendAndReceieve(adapter, new NCEStandardValidation(), cmd);
    }

    private ICommandResult SendAndReceieve(IAdapter adapter, NCEStandardValidation nceStandardValidation, byte[] cmd) => throw new NotImplementedException();

    public override string ToString() => $"ACCY OPS PROGRAMMING ({LocoAddress}:{CVAddress}={Value})";
}