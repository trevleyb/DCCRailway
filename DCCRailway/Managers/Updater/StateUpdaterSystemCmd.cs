using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;
using DCCRailway.Managers.State;

namespace DCCRailway.Managers.Updater;

public class StateUpdaterSystemCmd(IStateManager stateManager) : IStateUpdater {
    public IResult Process(ICmdResult cmdResult) {

        if (cmdResult.Command is ISystemCmd command) {
            switch (command) {
            case ICmdStatus cmd:
                break;
            case ICmdClockRead cmd:
                if (cmdResult is ICmdResultFastClock res) {
                    stateManager.SetState("SYSTEM", StateType.Clock, res.CurrentTime);
                }
                break;
            case ICmdClockSet cmd:
                stateManager.SetState("SYSTEM", StateType.Clock, cmd.ClockTime);
                break;
            case ICmdClockStart cmd:
                stateManager.SetState("SYSTEM", StateType.ClockRunning, true);
                break;
            case ICmdClockStop cmd:
                stateManager.SetState("SYSTEM", StateType.ClockRunning, false);
                break;
            case ICmdMacroRun cmd:
                break;
            case ICmdPowerGetState cmd:
                if (cmdResult is ICmdResultPowerState powerState) {
                    stateManager.SetState("SYSTEM", StateType.Power, powerState.State);
                }
                break;
            case ICmdPowerSetOff cmd:
                stateManager.SetState("SYSTEM", StateType.Power, DCCPowerState.Off);
                break;
            case ICmdPowerSetOn cmd:
                stateManager.SetState("SYSTEM", StateType.Power, DCCPowerState.On);
                break;
            case ICmdTrackMain cmd:
                stateManager.SetState("SYSTEM", StateType.ActiveTrack, DCCActiveTrack.Main);
                break;
            case ICmdTrackProg cmd:
                stateManager.SetState("SYSTEM", StateType.ActiveTrack, DCCActiveTrack.Programming);
                break;
            case IDummyCmd cmd:
                var dummyCount = stateManager.GetState<int>("SYSTEM", StateType.Dummy);
                dummyCount++;
                stateManager.SetState("SYSTEM", StateType.Dummy, dummyCount);
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