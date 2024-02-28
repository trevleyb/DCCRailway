using DCCRailway.Common.Types;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.System.Manufacturer.Virtual.Commands;

[Command("LocoSetSpeed", "Set the Speed of a Loco")]
public class VirtualLocoSetSpeed : VirtualCommand, ICmdLocoSetSpeed, ICommand {
    public VirtualLocoSetSpeed() { }

    public VirtualLocoSetSpeed(IDCCAddress address, DCCDirection direction = DCCDirection.Forward, byte speed = 0, DCCProtocol speedSteps = DCCProtocol.DCC128) {
        Address    = address;
        Speed      = speed;
        Direction  = direction;
        SpeedSteps = speedSteps;
    }

    public IDCCAddress  Address    { get; set; }
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public byte         Speed      { get; set; }

    public override string ToString() => $"LOCO SPEED ({Address}={Direction}@{SpeedSteps}={Speed}";
}