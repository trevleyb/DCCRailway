using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType;

public interface ICmdSensorGetState : ICommand {
    public IDCCAddress SensorAddress { get; set; }
}