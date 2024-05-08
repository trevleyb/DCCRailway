using DCCRailway.CmdStation.Actions.Commands.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Actions.Commands;

public interface ICmdLocoSetSpeedSteps : ICommand, ILocoCmd {
    public DCCProtocol SpeedSteps { get; set; }
}