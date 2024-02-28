using DCCRailway.DCCController.Commands.Types.BaseTypes;
using DCCRailway.DCCController.Types;

namespace DCCRailway.DCCController.Commands.Types;

public interface ICmdLocoSetSpeedSteps : ICommand,ILocoCmd {
    public DCCProtocol SpeedSteps { get; set; }
}