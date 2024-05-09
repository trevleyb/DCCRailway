using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Railway.Configuration;
using DCCRailway.Railway.Layout.State;

namespace DCCRailway.Railway.Layout.Processors;

public class StateUpdaterLocoCmd(StateManager stateManager, ICmdResult result) : StateUpdaterProcess(result), IStateUpdaterProcess {
    public override bool Process() {

        // Get the Accessory from the configuration so that we can update its state
        // -----------------------------------------------------------------------------
        if (Command is ILocoCmd locoCmd) {
            //var locomotives = RailwayConfig.Instance.Locomotives;
            //var loco = locomotives.Find(x => x.Address.Address == locoCmd.Address.Address).Results;
            //var loco = Config.Locomotives[locoCmd.Address];
            var loco = RailwayManager.Instance.Locomotives.Find(x => x.Address.Address == locoCmd.Address.Address);

            if (loco is null) {
                Error($"Command - no matching Accessory {((IAccyCmd)Command).Address.Address}.");
                return false;
            }

            switch (Command) {
            case ICmdLocoStop cmd:
                stateManager.CopyState(cmd.Address, StateType.Speed,StateType.LastSpeed, new DCCSpeed(0));
                stateManager.CopyState(cmd.Address, StateType.Direction,StateType.LastDirection, DCCDirection.Forward);
                stateManager.SetState (cmd.Address, StateType.Speed, new DCCSpeed(0));
                stateManager.SetState (cmd.Address, StateType.Direction, DCCDirection.Stop);
                Event("Setting Loco to Stop.");
                break;
            case ICmdLocoSetFunctions cmd:
                Event("Setting Loco Functions.");
                break;
            case ICmdLocoSetFunction cmd:
                Event("Setting Loco Function");
                break;
            case ICmdLocoSetMomentum cmd:
                Event("Setting Loco Momentum.");
                //loco.Momentum = cmd.Momentum;
                break;
            case ICmdLocoSetSpeed cmd:
                Event("Setting Loco Speed.");
                //loco.Speed     = cmd.Speed;
                //loco.LastSpeed = cmd.Speed;
                break;
            case ICmdLocoSetSpeedSteps cmd:
                Event("Setting Loco Speed Steps.");
                break;
            case ICmdLocoOpsProg cmd:
                Event("Setting Loco To ops Programming.");
                break;
            default:
                Error($"Command not supported.");
                throw new Exception("Unexpected type of command executed.");
            }
        }
        else {
            Error($"Command not supported.");
        }
        return true;
    }
}