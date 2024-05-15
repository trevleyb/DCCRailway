using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Railway.Layout.State;

namespace DCCRailway.Railway.Layout.Processors;

public class StateUpdaterSystemCmd(IRailwayManager railwayManager, IStateManager stateManager, ICmdResult result) : StateUpdaterProcess(result), IStateUpdaterProcess {
    public override bool Process() {
        switch (Command) {
        case ICmdStatus cmd:
            Event("Getting the Status of the System");
            break;
        case ICmdClockRead cmd:
            if (result is ICmdResultFastClock res) {
                stateManager.SetState("SYSTEM", StateType.Clock, res.CurrentTime);
                Event("Read the Clock");
            }
            break;
        case ICmdClockSet cmd:
            stateManager.SetState("SYSTEM", StateType.Clock, cmd.ClockTime);
            Event("Set the Clock");
            break;
        case ICmdClockStart cmd:
            stateManager.SetState("SYSTEM", StateType.ClockRunning, true);
            Event("Start the Clock");
            break;
        case ICmdClockStop cmd:
            stateManager.SetState("SYSTEM", StateType.ClockRunning, false);
            Event("Stop the Clock");
            break;
        case ICmdMacroRun cmd:
            Event("Run a Macro");
            break;
        case ICmdPowerGetState cmd:
            if (result is ICmdResultPowerState powerState) {
                stateManager.SetState("SYSTEM", StateType.Power, powerState.State);
                Event("Get Current Power State");
            }
            break;
        case ICmdPowerSetOff cmd:
            stateManager.SetState("SYSTEM", StateType.Power, DCCPowerState.Off);
            Event("Set Power OFF");
            break;
        case ICmdPowerSetOn cmd:
            stateManager.SetState("SYSTEM", StateType.Power, DCCPowerState.On);
            Event("Set Power ON");
            break;
        case ICmdTrackMain cmd:
            stateManager.SetState("SYSTEM", StateType.ActiveTrack, DCCActiveTrack.Main);
            Event("Switch to Main Track");
            break;
        case ICmdTrackProg cmd:
            stateManager.SetState("SYSTEM", StateType.ActiveTrack, DCCActiveTrack.Programming);
            Event("Switch to Programming Track");
            break;
        case IDummyCmd cmd:
            var dummyCount = stateManager.GetState<int>("SYSTEM", StateType.Dummy);
            dummyCount++;
            stateManager.SetState("SYSTEM", StateType.Dummy, dummyCount);
            Event($"Dummy Command (called {dummyCount} times).");
            break;

        default:
            Error("Command not supported.");
            throw new Exception("Unexpected type of command executed.");
        }
        return true;
    }
}