using DCCRailway.Common.Result;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.States.CommandUpdater;

public class CmdStateUpdaterSystemCmd(IStateTracker stateTracker) {
    public IResult Process(ICmdResult cmdResult) {
        if (cmdResult.Command is ISystemCmd command) {
            switch (command) {
            case ICmdStatus cmd:
                break;
            case ICmdClockRead cmd:
                if (cmdResult is ICmdResultFastClock res) {
                    stateTracker.SetState("SYSTEM", StateType.Clock, res.CurrentTime);
                }

                break;
            case ICmdClockSet cmd:
                stateTracker.SetState("SYSTEM", StateType.Clock, cmd.ClockTime);
                break;
            case ICmdClockStart cmd:
                stateTracker.SetState("SYSTEM", StateType.ClockRunning, true);
                break;
            case ICmdClockStop cmd:
                stateTracker.SetState("SYSTEM", StateType.ClockRunning, false);
                break;
            case ICmdMacroRun cmd:
                break;
            case ICmdPowerGetState cmd:
                if (cmdResult is ICmdResultPowerState powerState) {
                    stateTracker.SetState("SYSTEM", StateType.Power, powerState.State);
                }

                break;
            case ICmdPowerSetOff cmd:
                stateTracker.SetState("SYSTEM", StateType.Power, DCCPowerState.Off);
                break;
            case ICmdPowerSetOn cmd:
                stateTracker.SetState("SYSTEM", StateType.Power, DCCPowerState.On);
                break;
            case ICmdTrackMain cmd:
                stateTracker.SetState("SYSTEM", StateType.ActiveTrack, DCCActiveTrack.Main);
                break;
            case ICmdTrackProg cmd:
                stateTracker.SetState("SYSTEM", StateType.ActiveTrack, DCCActiveTrack.Programming);
                break;
            case IDummyCmd cmd:
                var dummyCount = stateTracker.GetState<int>("SYSTEM", StateType.Dummy);
                dummyCount++;
                stateTracker.SetState("SYSTEM", StateType.Dummy, dummyCount);
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