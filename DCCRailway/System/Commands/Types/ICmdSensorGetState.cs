using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Types;

public interface ICmdSensorGetState : ICommand {
    public IDCCAddress SensorAddress { get; set; }
}