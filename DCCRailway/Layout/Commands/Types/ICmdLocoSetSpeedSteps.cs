using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdLocoSetSpeedSteps : ICommand,ICmdWithAddress {
    public DCCProtocol SpeedSteps { get; set; }
}