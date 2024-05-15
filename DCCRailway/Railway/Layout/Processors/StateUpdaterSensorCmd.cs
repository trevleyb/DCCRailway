using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Railway.Layout.State;

namespace DCCRailway.Railway.Layout.Processors;

public class StateUpdaterSensorCmd(IRailwayManager railwayManager, IStateManager stateManager, ICmdResult result) : StateUpdaterProcess(result), IStateUpdaterProcess {
    public override bool Process() {
        if (Command is ISensorCmd sensorCmd) {
            var sensor = railwayManager.Sensors.Find(x => x.Address == sensorCmd.Address);
            switch (sensorCmd) {
            case ICmdSensorGetState cmd:
                Event("Getting the State of a Sensor");
                break;
            default:
                Error("Command not supported.");
                throw new Exception("Unexpected type of command executed.");
            }
        }
        return true;
    }
}