using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;
using DCCRailway.Managers.State;

namespace DCCRailway.Managers.Updater;

public class StateUpdaterLocoCmd(IStateManager stateManager) : IStateUpdater {
    public IResult Process(ICmdResult cmdResult) {
        // Get the Accessory from the configuration so that we can update its state
        // -----------------------------------------------------------------------------
        if (cmdResult.Command is ILocoCmd locoCmd)
            switch (locoCmd) {
            case ICmdLocoStop cmd:
                stateManager.CopyState(cmd.Address, StateType.Speed, StateType.LastSpeed, new DCCSpeed(0));
                stateManager.CopyState(cmd.Address, StateType.Direction, StateType.LastDirection, DCCDirection.Forward);
                stateManager.SetState(cmd.Address, StateType.Speed, new DCCSpeed(0));
                stateManager.SetState(cmd.Address, StateType.Direction, DCCDirection.Stop);
                break;
            case ICmdLocoSetFunctions cmd:
                // TODO: How do we manage Momentary vs toggle Functions?
                break;
            case ICmdLocoSetFunction cmd:
                // TODO: How do we manage Momentary vs toggle Functions?
                break;
            case ICmdLocoSetMomentum cmd:
                stateManager.SetState<DCCMomentum>(cmd.Address, StateType.Momentum, cmd.Momentum);
                break;
            case ICmdLocoSetSpeed cmd:
                stateManager.SetState<DCCSpeed>(cmd.Address, StateType.Speed, cmd.Speed);
                break;
            case ICmdLocoSetSpeedSteps cmd:
                stateManager.SetState<DCCProtocol>(cmd.Address, StateType.SpeedSteps, cmd.SpeedSteps);
                break;
            case ICmdLocoOpsProg cmd:
                break;
            default:
                return Result.Fail($"Unexpected command type {cmdResult?.Command?.AttributeInfo()?.Name}.");
            }
        else
            return Result.Fail($"Unexpected command type {cmdResult?.Command?.AttributeInfo()?.Name}.");

        return Result.Ok();
    }
}