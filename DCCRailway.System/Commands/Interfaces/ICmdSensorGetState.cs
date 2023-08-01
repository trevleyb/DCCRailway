using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdSensorGetState : ICommand {
    public IDCCAddress SensorAddress { get; set; }
}