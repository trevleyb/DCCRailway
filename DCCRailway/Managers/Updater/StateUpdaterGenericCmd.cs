using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Managers.State;

namespace DCCRailway.Managers.Updater;

public class StateUpdaterGenericCmd(IStateManager stateManager) : IStateUpdater {
    public IResult Process(ICmdResult cmdResult) {
        return Result.Ok("Command - no matching condition. Ignored.");
    }
}