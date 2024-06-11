using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Controllers;

namespace DCCRailway.WiThrottle.Server;

/// <summary>
/// This is a helper class that makes calls to the Command Station 
/// </summary>
/// <param name="commandStation">Reference to the Command Station Controller</param>
/// <param name="address">Address of the item to send the mesage to (eg: Speed, or Direction)</param>
public class LayoutCmdHelper(ICommandStation commandStation, DCCAddress? address = null) {
    public DCCPowerState PowerState {
        get {
            if (GetCommandRef<ICmdPowerGetState>() is { } command) {
                if (command?.Execute() is ICmdResultPowerState res) return res.State;
            }

            return DCCPowerState.Unknown;
        }
    }

    public void Stop() {
        if (GetCommandRef<ICmdLocoStop>() is { } command) {
            command.Execute();
        } else {
            SetSpeed(0, DCCDirection.Stop);
        }
    }

    public void SetSpeed(byte speed, DCCDirection direction) {
        SetSpeed(new DCCSpeed(speed), direction);
    }

    public void SetSpeed(DCCSpeed speed, DCCDirection direction) {
        if (GetCommandRef<ICmdLocoSetSpeed>() is { } command) {
            command.Speed     = speed;
            command.Direction = direction;
            command.Execute();
        }
    }

    public void SetSpeedSteps14() {
        SetSpeedSteps(DCCProtocol.DCC14);
    }

    public void SetSpeedSteps28() {
        SetSpeedSteps(DCCProtocol.DCC28);
    }

    public void SetSpeedSteps128() {
        SetSpeedSteps(DCCProtocol.DCC128);
    }

    public void SetSpeedSteps(byte steps) {
        if (steps == 28) SetSpeedSteps28();
        if (steps == 14) SetSpeedSteps14();
        SetSpeedSteps128();
    }

    private T? GetCommandRef<T>() where T : ICommand {
        return commandStation.IsCommandSupported<T>() ? commandStation.CreateCommand<T>(address) : default;
    }

    public void SetSpeedSteps(DCCProtocol protocol) {
        if (GetCommandRef<ICmdLocoSetSpeedSteps>() is { } command) {
            command.SpeedSteps = protocol;
            command.Execute();
        }
    }

    public void Acquire() {
        if (GetCommandRef<ICmdLocoAcquire>() is { } command) {
            command.Execute();
        }
    }

    public void Release() {
        if (GetCommandRef<ICmdLocoRelease>() is { } command) {
            command.Execute();
        }
    }

    public void Dispatch() {
        if (GetCommandRef<ICmdLocoDispatch>() is { } command) {
            command.Execute();
        }
    }

    public bool IsReleaseSupported() {
        return commandStation.IsCommandSupported<ICmdLocoRelease>();
    }

    public bool IsDispatchSupported() {
        return commandStation.IsCommandSupported<ICmdLocoDispatch>();
    }

    public bool IsAcquireSupported() {
        return commandStation.IsCommandSupported<ICmdLocoAcquire>();
    }

    public bool IsPowerSupported() {
        return commandStation.IsCommandSupported<ICmdPowerSetOn>();
    }

    public void SetTurnoutState(DCCTurnoutState state) {
        if (GetCommandRef<ICmdTurnoutSet>() is { } command) {
            command.State = state;
            command.Execute();
        }
    }

    public void SetPowerState(DCCPowerState state) {
        _ = state switch {
            DCCPowerState.On  => commandStation.CreateCommand<ICmdPowerSetOn>()?.Execute(),
            DCCPowerState.Off => commandStation.CreateCommand<ICmdPowerSetOff>()?.Execute(),
            _                 => null
        };
    }

    public void SetFunctionState(DCCAddress locoAddress, byte functionNum, FunctionStateEnum stateEnum) {
        if (GetCommandRef<ICmdLocoSetFunction>() is { } command) {
            command.Function = functionNum;
            command.State    = stateEnum == FunctionStateEnum.On;
            command.Execute();
        }
    }
}