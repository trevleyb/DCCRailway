using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Railway.Layout.State;

namespace DCCRailway.Railway.Layout.Processors;

public class StateUpdaterConsistCmd(IRailwayManager railwayManager, IStateManager stateManager, ICmdResult result) : StateUpdaterProcess(result), IStateUpdaterProcess {
    public override bool Process() {
        switch (Command) {
        case ICmdConsistCreate cmd:
            // TODO: Implement the command processing
            Event("Create a Consist");
            break;
        case ICmdConsistKill cmd:
            // TODO: Implement the command processing
            Event("Kill a Consist");
            break;
        case ICmdConsistDelete cmd:
            // TODO: Implement the command processing
            Event("Delete a Consist");
            break;
        case ICmdConsistAdd cmd:
            // TODO: Implement the command processing
            Event("Add a Consist");
            break;
        default:
            Error("Command not supported.");
            throw new Exception("Unexpected type of command executed.");
        }

        return true;
    }
}