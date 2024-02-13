using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Layout.Commands.Types;

public interface ICmdLocoSetSpeedSteps : ICommand,ILocoCommand {
    public DCCProtocol SpeedSteps { get; set; }
}