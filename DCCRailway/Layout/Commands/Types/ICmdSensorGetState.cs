using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdSensorGetState : ICommand {
    public IDCCAddress SensorAddress { get; set; }
}