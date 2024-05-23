using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;
using DCCRailway.Managers.State;

namespace DCCRailway.Managers.Updater;

public class StateUpdaterSensorCmd(IStateManager stateManager) : IStateUpdater {
    public IResult Process(ICmdResult cmdResult) {
        if (cmdResult.Command is ISensorCmd sensorCmd)
            switch (sensorCmd) {
            case ICmdSensorGetState cmd:
                stateManager.SetState(cmd.Address, StateType.Sensor, cmdResult.Byte);
                break;
            default:
                return Result.Fail($"Unexpected command type {cmdResult?.Command?.AttributeInfo()?.Name}.");
            }

        return Result.Ok();
    }
}