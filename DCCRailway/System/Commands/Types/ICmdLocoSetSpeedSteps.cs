using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Types;

public interface ICmdLocoSetSpeedSteps : ICommand,ILocoCommand {
    public DCCProtocol SpeedSteps { get; set; }
}