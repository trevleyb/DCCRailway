using DCCRailway.Controller.Actions.Results;
using DCCRailway.Railway.Configuration;
using DCCRailway.Railway.Layout.State;

namespace DCCRailway.Railway.Layout.Processors;

public class StateUpdaterGenericCmd(IRailwayManager railwayManager, IStateManager stateManager, ICmdResult result) : StateUpdaterProcess(result), IStateUpdaterProcess {
    public override bool Process() {
        Event($"Command - no matching condition. Ignored.");
        return true;
    }
}