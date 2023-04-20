using DCCRailway.Core.Systems.Types;

namespace DCCRailway.Core.Systems.Commands.Interfaces; 

public interface ICmdSensorGetState : ICommand {
    public IDCCAddress SensorAddress { get; set; }
}