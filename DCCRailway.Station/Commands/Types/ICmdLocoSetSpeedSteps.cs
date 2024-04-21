using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

public interface ICmdLocoSetSpeedSteps : ICommand, ILocoCmd {
    public DCCProtocol SpeedSteps { get; set; }
}