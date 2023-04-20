using DCCRailway.Core.Systems.Types;

namespace DCCRailway.Core.Systems.Commands.Interfaces; 

public interface ICmdLocoSetSpeedSteps : ICommand {
    public IDCCAddress Address { get; set; }
    public DCCProtocol SpeedSteps { get; set; }
}