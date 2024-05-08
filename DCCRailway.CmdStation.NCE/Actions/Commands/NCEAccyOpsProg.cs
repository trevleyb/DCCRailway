using System;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Commands.Types.Base;
using DCCRailway.CmdStation.NCE.Commands.Validators;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.NCE.Commands;

[Command("AccyOpsProg", "Accessory Ops Programming")]
public class NCEAccyOpsProg : NCECommand, ICmdAccyOpsProg, ICommand, IAccyCmd {
    public NCEAccyOpsProg() { }

    public NCEAccyOpsProg(int locoAddress, DCCAddressType type, int cvAddress, byte value) {
        LocoAddress = new DCCAddress(locoAddress, type);
        CVAddress   = new DCCAddress(cvAddress, DCCAddressType.CV);
        Value       = value;
    }

    public NCEAccyOpsProg(DCCAddress locoAddress, DCCAddress cvAddress, byte value) {
        LocoAddress = locoAddress;
        CVAddress   = cvAddress;
        Value       = value;
    }

    public new static string Name => "NCE Accessory Programming";

    public DCCAddress Address     { get; set; }
    public DCCAddress LocoAddress { get; set; }
    public DCCAddress CVAddress   { get; set; }
    public byte        Value       { get; set; }

    public override ICmdResult Execute(IAdapter adapter) {
        var cmd = new byte[] { 0xAF };
        cmd = cmd.AddToArray(LocoAddress.AddressBytes);
        cmd = cmd.AddToArray(CVAddress.AddressBytes);
        cmd = cmd.AddToArray(Value);

        return SendAndReceieve(adapter, new NCEStandardValidation(), cmd);
    }

    private ICmdResult SendAndReceieve(IAdapter adapter, NCEStandardValidation nceStandardValidation, byte[] cmd) => throw new NotImplementedException();

    public override string ToString() => $"ACCY OPS PROGRAMMING ({LocoAddress}:{CVAddress}={Value})";
}