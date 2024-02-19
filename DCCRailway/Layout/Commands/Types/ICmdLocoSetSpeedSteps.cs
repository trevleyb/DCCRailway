using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdLocoSetSpeedSteps : ICommand,ILocoCommand {
    public DCCProtocol SpeedSteps { get; set; }
}