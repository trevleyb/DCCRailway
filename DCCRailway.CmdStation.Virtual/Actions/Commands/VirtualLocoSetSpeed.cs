using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Commands;

[Command("LocoSetSpeed", "Set the Speed of a Loco")]
public class VirtualLocoSetSpeed : VirtualCommand, ICmdLocoSetSpeed, ICommand {
    public VirtualLocoSetSpeed() { }

    public VirtualLocoSetSpeed(DCCAddress address, DCCDirection direction = DCCDirection.Forward, DCCSpeed? speed = null, DCCProtocol speedSteps = DCCProtocol.DCC128) {
        Address    = address;
        Speed      = speed ?? new DCCSpeed(0);
        Direction  = direction;
        SpeedSteps = speedSteps;
    }

    public DCCAddress  Address    { get; set; }
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public DCCSpeed     Speed      { get; set; }

    public override string ToString() => $"LOCO SPEED ({Address}={Direction}@{SpeedSteps}={Speed}";
}