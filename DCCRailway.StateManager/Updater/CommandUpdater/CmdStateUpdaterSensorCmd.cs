using DCCRailway.Common.Result;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.StateManager.Updater.CommandUpdater;

public class CmdStateUpdaterSensorCmd(IStateManager stateManager) {
    public IResult Process(ICmdResult cmdResult) {
        if (cmdResult.Command is ISensorCmd sensorCmd) {
            switch (sensorCmd) {
            case ICmdSensorGetState cmd:
                stateManager.SetState(cmd.Address, StateType.Sensor, cmdResult.Byte);
                break;
            default:
                return Result.Fail($"Unexpected command type {cmdResult?.Command?.AttributeInfo()?.Name}.");
            }
        }

        return Result.Ok();
    }
}