using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdLocoSetSpeedSteps : ICommand,ILocoCommand {
    public IDCCAddress Address    { get; set; }
    public DCCProtocol SpeedSteps { get; set; }
}