using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.StateManagement.State;

namespace DCCRailway.StateManagement.Processors;

public class StateUpdaterGenericCmd(IStateManager stateManager) : IStateUpdater {
    public IResult Process(ICmdResult cmdResult) {
        return Result.Ok("Command - no matching condition. Ignored.");
    }
}