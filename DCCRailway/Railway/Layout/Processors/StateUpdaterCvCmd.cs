using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Railway.Layout.State;

namespace DCCRailway.Railway.Layout.Processors;

public class StateUpdaterCvCmd(StateManager stateManager, ICmdResult result) : StateUpdaterProcess(result), IStateUpdaterProcess {
    public override bool Process() {
        switch (Command) {
        case ICmdCVRead cmd:
            // TODO: Implement the command processing
            Event("Read a CV");
            break;
        case ICmdCVWrite cmd:
            // TODO: Implement the command processing
            Event("Write a CV");
            break;
        default:
            Error($"Command not supported.");
            throw new Exception("Unexpected type of command executed.");
        }

        return true;
    }
}