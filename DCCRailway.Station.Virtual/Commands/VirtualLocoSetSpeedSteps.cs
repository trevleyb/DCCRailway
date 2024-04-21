using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

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