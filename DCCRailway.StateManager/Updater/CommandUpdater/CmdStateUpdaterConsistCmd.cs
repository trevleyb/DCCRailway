using DCCRailway.Common.Result;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.StateManager.Updater.CommandUpdater;

public class CmdStateUpdaterConsistCmd(IStateManager stateManager) {
    public IResult Process(ICmdResult cmdResult) {
        switch (cmdResult.Command) {
        case ICmdConsistCreate cmd:
            // TODO: Implement the command processing
            break;
        case ICmdConsistKill cmd:
            // TODO: Implement the command processing
            break;
        case ICmdConsistDelete cmd:
            // TODO: Implement the command processing
            break;
        case ICmdConsistAdd cmd:
            // TODO: Implement the command processing
            break;
        default:
            return Result.Fail($"Unexpected command type {cmdResult?.Command?.AttributeInfo()?.Name}.");
        }

        return Result.Ok();
    }
}