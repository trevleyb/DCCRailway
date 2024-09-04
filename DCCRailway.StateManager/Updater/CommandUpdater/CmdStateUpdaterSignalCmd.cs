using DCCRailway.Common.Result;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.StateManager.Updater.CommandUpdater;

public class CmdStateUpdaterSignalCmd(IStateManager stateManager) {
    public IResult Process(ICmdResult cmdResult) {
        if (cmdResult.Command is ISignalCmd signalCmd) {
            switch (signalCmd) {
            case ICmdSignalSetAspect cmd:
                stateManager.SetState(cmd.Address, StateType.Signal, cmd.Aspect);
                break;
            default:
                return Result.Fail($"Unexpected command type {cmdResult?.Command?.AttributeInfo()?.Name}.");
            }
        }

        return Result.Ok();
    }
}