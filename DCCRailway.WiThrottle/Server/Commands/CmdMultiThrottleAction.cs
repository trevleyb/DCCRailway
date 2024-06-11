using DCCRailway.Common.Types;
using DCCRailway.WiThrottle.Server.Messages;

namespace DCCRailway.WiThrottle.Server.Commands;

public partial class CmdMultiThrottle {
    private IThrottleMsg[] PerformLocoAction(CmdMultiThrottleHelper data) {
        var messages = new List<IThrottleMsg>();
        foreach (var address in data.Addresses) {
            var cmdHelper = new LayoutCmdHelper(connection.CommandStation, address);
            var locoData  = connection.GetLoco(address);
            if (locoData is not null) {
                try {
                    var msg = data.Action switch {
                        ThrottleActionEnum.SetLeadLocoByAddress => SetLeadLocoByAddress(data, address, locoData, cmdHelper),
                        ThrottleActionEnum.SetLeadLocoByName    => SetLeadLocoByName(data, address, locoData, cmdHelper),
                        ThrottleActionEnum.SetSpeed             => SetSpeed(data, address, locoData, cmdHelper),
                        ThrottleActionEnum.SendIdle             => SendIdle(data, address, locoData, cmdHelper),
                        ThrottleActionEnum.SendEmergencyStop    => SendEmergencyStop(data, address, locoData, cmdHelper),
                        ThrottleActionEnum.SetDirection         => SetDirection(data, address, locoData, cmdHelper),
                        ThrottleActionEnum.SetFunctionState     => SetFunctionState(data, address, locoData, cmdHelper),
                        ThrottleActionEnum.ForceFunctionState   => ForceFunctionState(data, address, locoData, cmdHelper),
                        ThrottleActionEnum.SetSpeedSteps        => SetSpeedSteps(data, address, locoData, cmdHelper),
                        ThrottleActionEnum.SetMomentaryState    => SetMomentaryState(data, address, locoData, cmdHelper),
                        ThrottleActionEnum.QueryValue           => QueryValue(data, address, locoData, cmdHelper),
                        _                                       => new MsgIgnore(),
                    };

                    messages.Add(msg);

                    // If we have set in the preferences to reset the speed to zero if we change directions, then 
                    // send the idle command after the direction command to reset the loco to stop on the new direction. 
                    // ---------------------------------------------------------------------------------------------------
                    if (data.Action == ThrottleActionEnum.SetDirection && connection.RailwaySettings.WiThrottlePrefs.ZeroSpeedOnDirection) {
                        messages.Add(SendIdle(data, address, locoData, cmdHelper));
                    }
                } catch (Exception ex) {
                    logger.Error("Unable to process a MultiThrottle Action Message due to {0}", ex.Message);
                }
            }
        }

        return messages.ToArray();
    }

    private IThrottleMsg SetLeadLocoByAddress(CmdMultiThrottleHelper data, DCCAddress address, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        return new MsgIgnore();
    }

    private IThrottleMsg SetLeadLocoByName(CmdMultiThrottleHelper data, DCCAddress address, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        return new MsgIgnore();
    }

    private IThrottleMsg SetSpeed(CmdMultiThrottleHelper data, DCCAddress address, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        byte.TryParse(data.ActionData, out var speed);
        loco.Speed = new DCCSpeed(speed);
        if (loco.Speed.Value > 0 && loco.Direction == DCCDirection.Stop) loco.Direction = DCCDirection.Forward;
        cmdHelper.SetSpeed(speed, loco.Direction);

        // Don't send back the speed as it confuses the controller 
        return new MsgIgnore(); // new MsgQueryValue(connection, address, data.Group, (char)data.Function, 'V', loco.Speed.Value.ToString());
    }

    private IThrottleMsg SendIdle(CmdMultiThrottleHelper data, DCCAddress address, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        loco.Speed     = new DCCSpeed(0);
        loco.Direction = DCCDirection.Stop;
        cmdHelper.SetSpeed(0, loco.Direction);
        return new MsgQueryValue(connection, address, data.Group, (char)data.Function, 'V', loco.Speed.Value.ToString());
    }

    private IThrottleMsg SendEmergencyStop(CmdMultiThrottleHelper data, DCCAddress address, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        loco.Speed     = new DCCSpeed(0);
        loco.Direction = DCCDirection.Stop;
        cmdHelper.Stop();
        return new MsgQueryValue(connection, address, data.Group, (char)data.Function, 'V', loco.Speed.Value.ToString());
    }

    private IThrottleMsg SetDirection(CmdMultiThrottleHelper data, DCCAddress address, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        var isParseSuccessful = byte.TryParse(data.ActionData, out var direction);
        if (isParseSuccessful) {
            loco.Direction = direction == 0 ? DCCDirection.Reverse : DCCDirection.Forward;
            cmdHelper.SetSpeed(loco.Speed, loco.Direction);
            return new MsgQueryValue(connection, address, data.Group, (char)data.Function, 'R', loco.Direction == DCCDirection.Forward ? "1" : "0");
        }

        return new MsgIgnore();
    }

    // If this is a momentary option, then we expect to send a ON followed by a OFF on a Press/Release
    // If this is latching, then we need to turn on on a Press if it is off, turn off on a press if it is on
    // but ignore the release otherwise. 
    private IThrottleMsg SetFunctionState(CmdMultiThrottleHelper data, DCCAddress address, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        if (!string.IsNullOrEmpty(data.ActionData) && Enum.TryParse(data.ActionData[0].ToString(), out FunctionStateEnum stateEnum)) {
            if (byte.TryParse(data.ActionData[1..], out var functionNum)) {
                if (loco.GetMomentaryState(functionNum) == MomentaryStateEnum.Momentary) {
                    cmdHelper.SetFunctionState(loco.Address, functionNum, stateEnum);
                    loco.SetFunctionState(functionNum, stateEnum);
                    return new MsgFunctionState(connection, address, data.Group, (char)data.Function, functionNum, stateEnum);
                } else {
                    if (stateEnum == FunctionStateEnum.On) { // The button has been pressed
                        var previousState = loco.GetFunctionState(functionNum);
                        var changeState   = previousState == FunctionStateEnum.On ? FunctionStateEnum.Off : FunctionStateEnum.On;
                        cmdHelper.SetFunctionState(loco.Address, functionNum, changeState);
                        loco.SetFunctionState(functionNum, changeState);
                        return new MsgFunctionState(connection, address, data.Group, (char)data.Function, functionNum, changeState);
                    } else {
                        // We need to ignore the OFF or button released state of the message as we just toggle the state
                        return new MsgIgnore();
                    }
                }
            }
        }

        return new MsgIgnore();
    }

    // Forces a function to a given state regardless of momentary or Latching. 
    // -----------------------------------------------------------------------
    private IThrottleMsg ForceFunctionState(CmdMultiThrottleHelper data, DCCAddress address, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        if (!string.IsNullOrEmpty(data.ActionData) && Enum.TryParse(data.ActionData[0].ToString(), out FunctionStateEnum stateEnum)) {
            if (byte.TryParse(data.ActionData[1..], out var functionNum)) {
                cmdHelper.SetFunctionState(loco.Address, functionNum, stateEnum);
                loco.SetFunctionState(functionNum, stateEnum);
                return new MsgFunctionState(connection, address, data.Group, (char)data.Function, functionNum, stateEnum);
            }
        }

        return new MsgIgnore();
    }

    private IThrottleMsg SetSpeedSteps(CmdMultiThrottleHelper data, DCCAddress address, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        byte.TryParse(data.ActionData, out var speedSteps);

        var protocol = speedSteps switch {
            1 => DCCProtocol.DCC128,
            2 => DCCProtocol.DCC28,
            4 => DCCProtocol.DCC27,
            8 => DCCProtocol.DCC14,
            _ => DCCProtocol.DCC128
        };

        loco.SpeedSteps = protocol;
        cmdHelper.SetSpeedSteps(protocol);
        return new MsgQueryValue(connection, address, data.Group, (char)data.Function, 's', ConvertSpeedSteps(loco.SpeedSteps).ToString());
    }

    private IThrottleMsg SetMomentaryState(CmdMultiThrottleHelper data, DCCAddress address, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        var stateStr    = data.ActionData[0].ToString();
        var functionStr = data.ActionData[1..];
        byte.TryParse(stateStr, out var state);
        byte.TryParse(functionStr, out var function);
        loco.SetMomentaryState(function, state == 0 ? MomentaryStateEnum.Latching : MomentaryStateEnum.Momentary);
        return new MsgString(data.Message); // Just return the original message
    }

    private IThrottleMsg QueryValue(CmdMultiThrottleHelper data, DCCAddress address, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        return data.ActionData switch {
            "V" => new MsgQueryValue(connection, address, data.Group, (char)data.Function, 'V', loco.Speed.Value.ToString()),
            "D" => new MsgQueryValue(connection, address, data.Group, (char)data.Function, 'R', loco.Direction == DCCDirection.Forward ? "1" : "0"),
            "s" => new MsgQueryValue(connection, address, data.Group, (char)data.Function, 's', ConvertSpeedSteps(loco.SpeedSteps).ToString()),
            "m" => new MsgQueryValue(connection, address, data.Group, (char)data.Function, 'm', loco.MomentaryStates),
            _   => new MsgIgnore()
        };
    }

    public override string ToString() {
        return "CMD:MultiThrottle";
    }

    public byte ConvertSpeedSteps(DCCProtocol protocol) {
        return protocol switch {
            DCCProtocol.DCC14  => 8,
            DCCProtocol.DCC27  => 4,
            DCCProtocol.DCC28  => 2,
            DCCProtocol.DCC128 => 1,
            _                  => 1
        };
    }

    public DCCProtocol ConvertSpeedSteps(byte speedSteps) {
        return speedSteps switch {
            8 => DCCProtocol.DCC14,
            4 => DCCProtocol.DCC27,
            2 => DCCProtocol.DCC28,
            1 => DCCProtocol.DCC128,
            _ => DCCProtocol.DCC128
        };
    }
}