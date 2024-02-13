using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Layout.Commands.Types;

public interface ICmdSensorGetState : ICommand {
    public IDCCAddress SensorAddress { get; set; }
}