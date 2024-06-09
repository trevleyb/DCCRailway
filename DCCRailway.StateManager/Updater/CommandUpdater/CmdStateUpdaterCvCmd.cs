using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.StateManager.Updater.CommandUpdater;

public class CmdStateUpdaterCvCmd(IStateManager stateManager) {
    public IResult Process(ICmdResult cmdResult) {
        switch (cmdResult.Command) {
        case ICmdCVRead cmd:
            //stateManager.SetState()
            break;
        case ICmdCVWrite cmd:
            //stateManager.SetState()
            break;
        default:
            return Result.Fail($"Unexpected command type {cmdResult?.Command?.AttributeInfo()?.Name}.");
        }

        return Result.Ok();
    }
}