using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Virtual.Commands;

[Command("AccyOpsProg", "Accessory Ops Programming")]
public class VirtualAccyOpsProg : VirtualCommand, ICmdAccyOpsProg, ICommand, IAccyCmd {
    public VirtualAccyOpsProg() { }

    public VirtualAccyOpsProg(int locoAddress, DCCAddressType type, int cvAddress, byte value) {
        LocoAddress = new DCCAddress(locoAddress, type);
        CVAddress   = new DCCAddress(cvAddress, DCCAddressType.CV);
        Value       = value;
    }

    public VirtualAccyOpsProg(IDCCAddress locoAddress, IDCCAddress cvAddress, byte value) {
        LocoAddress = locoAddress;
        CVAddress   = cvAddress;
        Value       = value;
    }

    public new static string Name => "Virtual Accessory Programming";

    public IDCCAddress Address     { get; set; }
    public IDCCAddress LocoAddress { get; set; }
    public IDCCAddress CVAddress   { get; set; }
    public byte        Value       { get; set; }

    public override string ToString() => $"ACCY OPS PROGRAMMING ({LocoAddress}:{CVAddress}={Value})";
}