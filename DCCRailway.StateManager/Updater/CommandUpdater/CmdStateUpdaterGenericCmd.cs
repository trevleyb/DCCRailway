using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.StateManager.State;

namespace DCCRailway.StateManager.Updater.CommandUpdater;

public class CmdStateUpdaterGenericCmd(IStateManager stateManager) {
    public IResult Process(ICmdResult cmdResult) {
        return Result.Ok("Command - no matching condition. Ignored.");
    }
}