using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType;

public interface ICmdLocoSetSpeedSteps : ICommand,ILocoCommand {
    public DCCProtocol SpeedSteps { get; set; }
}