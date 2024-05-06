using DCCRailway.CmdStation.Commands.Types.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Types;

public interface ICmdLocoSetSpeedSteps : ICommand, ILocoCmd {
    public DCCProtocol SpeedSteps { get; set; }
}