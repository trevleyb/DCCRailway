using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Railway.Configuration;
using DCCRailway.Railway.Layout.State;

namespace DCCRailway.Railway.Layout.Processors;

public class StateUpdaterSignalCmd(IRailwayManager railwayManager, IStateManager stateManager, ICmdResult result) : StateUpdaterProcess(result), IStateUpdaterProcess {
    public override bool Process() {
        if (Command is ISignalCmd signalCmd) {
            var signal = railwayManager.Signals.Find(x => x.Address == signalCmd.Address);

            switch (signalCmd) {
            case ICmdSignalSetAspect cmd:
                // TODO: Implement the command processing
                Event("Setting Signal Aspect.");
                break;
            default:
                Error($"Command not supported.");
                throw new Exception("Unexpected type of command executed.");
            }
        }
        return true;
    }
}