using DCCRailway.Common.Types;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Virtual.Commands.Validators;

namespace DCCRailway.System.Virtual.Commands;

[Command("LocoSetSpeedSteps", "Set the number of Speed Steps for this Loco")]
public class VirtualLocoSetSpeedSteps : VirtualCommand, ICmdLocoSetSpeedSteps, ICommand {
    public VirtualLocoSetSpeedSteps() { }

    public VirtualLocoSetSpeedSteps(int address, DCCProtocol speedSteps = DCCProtocol.DCC128) : this(new DCCAddress(address), speedSteps) { }

    public VirtualLocoSetSpeedSteps(IDCCAddress address, DCCProtocol speedSteps = DCCProtocol.DCC128) {
        Address    = address;
        SpeedSteps = speedSteps;
    }

    public IDCCAddress Address    { get; set; }
    public DCCProtocol SpeedSteps { get; set; }

    public override string ToString() => $"LOCO SPEED STEPS ({Address}={SpeedSteps}";
}