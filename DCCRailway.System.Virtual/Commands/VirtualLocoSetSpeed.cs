using DCCRailway.Common.Types;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Virtual.Commands.Validators;

namespace DCCRailway.System.Virtual.Commands;

[Command("LocoSetSpeed", "Set the Speed of a Loco")]
public class VirtualLocoSetSpeed : VirtualCommand, ICmdLocoSetSpeed, ICommand {
    public VirtualLocoSetSpeed() { }

    public VirtualLocoSetSpeed(IDCCAddress address, DCCDirection direction = DCCDirection.Forward, DCCSpeed? speed = null, DCCProtocol speedSteps = DCCProtocol.DCC128) {
        Address    = address;
        Speed      = speed ?? new DCCSpeed(0);
        Direction  = direction;
        SpeedSteps = speedSteps;
    }

    public IDCCAddress  Address    { get; set; }
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public DCCSpeed     Speed      { get; set; }

    public override string ToString() => $"LOCO SPEED ({Address}={Direction}@{SpeedSteps}={Speed}";
}