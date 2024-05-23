using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;
using DCCRailway.Managers.State;

namespace DCCRailway.Managers.Updater;

public class StateUpdaterCvCmd(IStateManager stateManager) : IStateUpdater {
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