using DCCRailway.Common.Result;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.StateManager.Updater.CommandUpdater;

public class CmdStateUpdaterAccyCmd(IStateTracker stateTracker) {
    public IResult Process(ICmdResult cmdResult) {
        if (cmdResult.Command is IAccyCmd accyCmd) {
            switch (accyCmd) {
            case ICmdAccyOpsProg cmd: {
                break;
            }

            case ICmdAccySetState cmd: {
                stateTracker.SetState(cmd.Address, StateType.Accessory, cmd.State);
                break;
            }

            default:
                return Result.Fail($"Unexpected command type {cmdResult?.Command?.AttributeInfo()?.Name}.");
            }
        }

        return Result.Ok();
    }
}