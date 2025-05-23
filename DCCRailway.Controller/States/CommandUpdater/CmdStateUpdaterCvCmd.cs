using DCCRailway.Common.Result;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.States.CommandUpdater;

public class CmdStateUpdaterCvCmd(IStateTracker stateTracker) {
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