using DCCRailway.Common.Result;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.States.CommandUpdater;

public class CmdStateUpdaterLocoCmd(IStateTracker stateTracker) {
    public IResult Process(ICmdResult cmdResult) {
        // Get the Accessory from the configuration so that we can update its state
        // -----------------------------------------------------------------------------
        if (cmdResult.Command is ILocoCmd locoCmd) {
            switch (locoCmd) {
            case ICmdLocoStop cmd:
                stateTracker.CopyState(cmd.Address, StateType.Speed, StateType.LastSpeed, new DCCSpeed(0));
                stateTracker.CopyState(cmd.Address, StateType.Direction, StateType.LastDirection, DCCDirection.Forward);
                stateTracker.SetState(cmd.Address, StateType.Speed, new DCCSpeed(0));
                stateTracker.SetState(cmd.Address, StateType.Direction, DCCDirection.Stop);
                break;
            case ICmdLocoSetFunction cmd:
                var currentState = stateTracker.GetState<DCCFunctionBlocks>(cmd.Address, StateType.Functions) ?? new DCCFunctionBlocks();
                currentState.SetFunction(cmd.Function, cmd.State);
                stateTracker.SetState<DCCFunctionBlocks>(cmd.Address, StateType.Functions, currentState);
                break;
            case ICmdLocoSetMomentum cmd:
                stateTracker.SetState<DCCMomentum>(cmd.Address, StateType.Momentum, cmd.Momentum);
                break;
            case ICmdLocoSetSpeed cmd:
                stateTracker.SetState<DCCSpeed>(cmd.Address, StateType.Speed, cmd.Speed);
                break;
            case ICmdLocoSetSpeedSteps cmd:
                stateTracker.SetState<DCCProtocol>(cmd.Address, StateType.SpeedSteps, cmd.SpeedSteps);
                break;
            case ICmdLocoOpsProg cmd:
                break;
            default:
                return Result.Fail($"Unexpected command type {cmdResult?.Command?.AttributeInfo()?.Name}.");
            }
        } else {
            return Result.Fail($"Unexpected command type {cmdResult?.Command?.AttributeInfo()?.Name}.");
        }

        return Result.Ok();
    }
}