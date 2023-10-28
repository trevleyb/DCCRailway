using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdLocoSetSpeedSteps : ICommand {
    public IDCCAddress Address    { get; set; }
    public DCCProtocol SpeedSteps { get; set; }
}