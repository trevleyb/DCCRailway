using DCCRailway.Layout.Commands.Types.BaseTypes;
using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdLocoSetSpeedSteps : ICommand,ILocoCmd {
    public DCCProtocol SpeedSteps { get; set; }
}