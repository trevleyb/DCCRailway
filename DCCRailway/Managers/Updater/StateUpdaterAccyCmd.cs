using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;
using DCCRailway.Managers.State;

namespace DCCRailway.Managers.Updater;

public class StateUpdaterAccyCmd(IStateManager stateManager) : IStateUpdater {
    public IResult Process(ICmdResult cmdResult) {
        if (cmdResult.Command is IAccyCmd accyCmd)
            switch (accyCmd) {
            case ICmdAccyOpsProg cmd: {
                break;
            }

            case ICmdAccySetState cmd: {
                stateManager.SetState(cmd.Address, StateType.Accessory, cmd.State);
                break;
            }

            default:
                return Result.Fail($"Unexpected command type {cmdResult?.Command?.AttributeInfo()?.Name}.");
            }

        return Result.Ok();
    }
}