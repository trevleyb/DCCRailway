using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("LocoSetSpeedSteps", "Set the number of Speed Steps for this Loco")]
public class VirtualLocoSetSpeedSteps : VirtualCommand, ICmdLocoSetSpeedSteps, ICommand {
    public VirtualLocoSetSpeedSteps() { }

    public VirtualLocoSetSpeedSteps(int address, DCCProtocol speedSteps = DCCProtocol.DCC128) : this(new DCCAddress(address), speedSteps) { }

    public VirtualLocoSetSpeedSteps(DCCAddress address, DCCProtocol speedSteps = DCCProtocol.DCC128) {
        Address    = address;
        SpeedSteps = speedSteps;
    }

    public DCCAddress Address    { get; set; }
    public DCCProtocol SpeedSteps { get; set; }

    public override string ToString() => $"LOCO SPEED STEPS ({Address}={SpeedSteps}";
}