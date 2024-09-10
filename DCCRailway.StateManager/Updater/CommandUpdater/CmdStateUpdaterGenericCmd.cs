using DCCRailway.Common.Result;
using DCCRailway.Controller.Actions.Results;

namespace DCCRailway.StateManager.Updater.CommandUpdater;

public class CmdStateUpdaterGenericCmd(IStateTracker stateTracker) {
    public IResult Process(ICmdResult cmdResult) {
        return Result.Ok("Command - no matching condition. Ignored.");
    }
}