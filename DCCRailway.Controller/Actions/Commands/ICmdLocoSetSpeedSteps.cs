using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdLocoSetSpeedSteps : ICommand, ILocoCmd {
    public DCCProtocol SpeedSteps { get; set; }
}