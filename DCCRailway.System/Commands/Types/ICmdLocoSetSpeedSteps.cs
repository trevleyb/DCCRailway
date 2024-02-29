using DCCRailway.Common.Types;
using DCCRailway.System.Commands.Types.BaseTypes;

namespace DCCRailway.System.Commands.Types;

public interface ICmdLocoSetSpeedSteps : ICommand, ILocoCmd {
    public DCCProtocol SpeedSteps { get; set; }
}