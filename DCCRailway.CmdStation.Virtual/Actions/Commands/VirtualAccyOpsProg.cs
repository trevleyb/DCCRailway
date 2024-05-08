using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Commands.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("AccyOpsProg", "Accessory Ops Programming")]
public class VirtualAccyOpsProg : VirtualCommand, ICmdAccyOpsProg, ICommand, IAccyCmd {
    public VirtualAccyOpsProg() { }

    public VirtualAccyOpsProg(int locoAddress, DCCAddressType type, int cvAddress, byte value) {
        LocoAddress = new DCCAddress(locoAddress, type);
        CVAddress   = new DCCAddress(cvAddress, DCCAddressType.CV);
        Value       = value;
    }

    public VirtualAccyOpsProg(DCCAddress locoAddress, DCCAddress cvAddress, byte value) {
        LocoAddress = locoAddress;
        CVAddress   = cvAddress;
        Value       = value;
    }

    public new static string Name => "Virtual Accessory Programming";

    public DCCAddress Address     { get; set; }
    public DCCAddress LocoAddress { get; set; }
    public DCCAddress CVAddress   { get; set; }
    public byte        Value       { get; set; }

    public override string ToString() => $"ACCY OPS PROGRAMMING ({LocoAddress}:{CVAddress}={Value})";
}