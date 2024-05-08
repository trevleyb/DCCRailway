using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("LocoOpsProg", "Program a Loco in Ops Mode (POM)")]
public class VirtualLocoOpsProg : VirtualCommand, ICmdLocoOpsProg, ICommand {
    public VirtualLocoOpsProg() { }

    public VirtualLocoOpsProg(int locoAddress, DCCAddressType type, int cvAddress, byte value) {
        LocoAddress = new DCCAddress(locoAddress, type);
        CVAddress   = new DCCAddress(cvAddress, DCCAddressType.CV);
        Value       = value;
    }

    public VirtualLocoOpsProg(DCCAddress locoAddress, DCCAddress cvAddress, byte value) {
        LocoAddress = locoAddress;
        CVAddress   = cvAddress;
        Value       = value;
    }

    public DCCAddress Address     { get; set; }
    public DCCAddress LocoAddress { get; set; }
    public DCCAddress CVAddress   { get; set; }
    public byte        Value       { get; set; }

    public override string ToString() => $"LOCO OPS PROGRAMMING ({LocoAddress}:{CVAddress}={Value})";
}