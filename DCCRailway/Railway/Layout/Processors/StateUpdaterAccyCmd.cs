using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Railway.Configuration;
using DCCRailway.Railway.Layout.State;

namespace DCCRailway.Railway.Layout.Processors;

public class StateUpdaterAccyCmd(StateManager stateManager, ICmdResult result) : StateUpdaterProcess(result), IStateUpdaterProcess {
    public override bool Process() {

        if (Command is IAccyCmd accyCmd) {
            var accessory = RailwayManager.Instance.Accessories.Find(x => x.Address == accyCmd.Address);

            if (accessory is null) {
                Error($"No matching Accessory {accyCmd.Address.Address}.");
                return false;
            }

            switch (accyCmd) {
            case ICmdAccyOpsProg cmd: {
                // TODO: Implement the command processing
                Event("Accy Ops Prog");
                //accessory.Parameters["opsMode"].Value = cmd.Value.ToString();
                break;
            }

            case ICmdAccySetState cmd: {
                // TODO: Implement the command processing
                Event("Accy Set State");
                //accessory.Parameters["state"].Value = cmd.State.ToString();
                break;
            }

            default:
                Error($"Command not supported.");
                throw new Exception("Unexpected type of command executed.");
            }
        }
        return true;
    }
}