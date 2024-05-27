using DCCRailway.Common.Types;
using DCCRailway.WiThrottle.Helpers;
using DCCRailway.WiThrottle.Messages;

namespace DCCRailway.WiThrottle.Commands;

public partial class CmdMultiThrottle {
    private IThrottleMsg[] PerformLocoAction(MultiThrottleMessage data) {
        var cmdHelper = new LayoutCmdHelper(connection.CommandStation, data.Address);
        var locoData  = connection.GetLoco(data.Address);
        if (locoData is null) return [new MsgIgnore()];

        var msg = data.Action switch {
            ThrottleActionEnum.SetLeadLocoByAddress => SetLeadLocoByAddress(data, locoData, cmdHelper),
            ThrottleActionEnum.SetLeadLocoByName    => SetLeadLocoByName(data, locoData, cmdHelper),
            ThrottleActionEnum.SetSpeed             => SetSpeed(data, locoData, cmdHelper),
            ThrottleActionEnum.SendIdle             => SendIdle(data, locoData, cmdHelper),
            ThrottleActionEnum.SendEmergencyStop    => SendEmergencyStop(data, locoData, cmdHelper),
            ThrottleActionEnum.SetDirection         => SetDirection(data, locoData, cmdHelper),
            ThrottleActionEnum.SetFunctionState     => SetFunctionState(data, locoData, cmdHelper),
            ThrottleActionEnum.ForceFunctionState   => ForceFunctionState(data, locoData, cmdHelper),
            ThrottleActionEnum.SetSpeedSteps        => SetSpeedSteps(data, locoData, cmdHelper),
            ThrottleActionEnum.SetMomentaryState    => SetMomentaryState(data, locoData, cmdHelper),
            ThrottleActionEnum.QueryValue           => QueryValue(data, locoData, cmdHelper),
            _                                       => new MsgIgnore(),
        };
        return [msg];
    }

    private IThrottleMsg SetLeadLocoByAddress(MultiThrottleMessage data, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        return new MsgIgnore();
    }

    private IThrottleMsg SetLeadLocoByName(MultiThrottleMessage data, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        return new MsgIgnore();
    }

    private IThrottleMsg SetSpeed(MultiThrottleMessage data, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        byte.TryParse(data.ActionData, out var speed);
        loco.Speed = new DCCSpeed(speed);
        cmdHelper.SetSpeed(speed, loco.Direction);
        return new MsgString(data.Message);
    }

    private IThrottleMsg SendIdle(MultiThrottleMessage data, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        loco.Speed = new DCCSpeed(0);
        cmdHelper.SetSpeed(0, loco.Direction);
        return new MsgString(data.Message);
    }

    private IThrottleMsg SendEmergencyStop(MultiThrottleMessage data, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        loco.Speed = new DCCSpeed(0);
        cmdHelper.Stop();
        return new MsgString(data.Message);
    }

    private IThrottleMsg SetDirection(MultiThrottleMessage data, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        var isParseSuccessful = byte.TryParse(data.ActionData, out var direction);

        if (isParseSuccessful) {
            loco.Direction = direction == 0 ? DCCDirection.Reverse : DCCDirection.Forward;
            cmdHelper.SetSpeed(loco.Speed, loco.Direction);
            return new MsgString(data.Message);
        }
        return new MsgIgnore();
    }

    // If this is a momentary option, then we expect to send a ON followed by a OFF on a Press/Release
    // If this is latching, then we need to turn on on a Press if it is off, turn off on a press if it is on
    // but ignore the release otherwise. 
    private IThrottleMsg SetFunctionState(MultiThrottleMessage data, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        if (!string.IsNullOrEmpty(data.ActionData) && Enum.TryParse(data.ActionData[0].ToString(), out FunctionStateEnum stateEnum)) {
            if (byte.TryParse(data.ActionData[1..], out var functionNum)) {
                if (loco.GetMomentaryState(functionNum) == MomentaryStateEnum.Momentary) {
                    cmdHelper.SetFunctionState(loco.Address, functionNum, stateEnum);
                    loco.SetFunctionState(functionNum, stateEnum);
                    return new MsgFunctionState(connection, data, functionNum, stateEnum);
                } else {
                    var previousState = loco.GetFunctionState(functionNum);

                    if (previousState != stateEnum) {
                        cmdHelper.SetFunctionState(loco.Address, functionNum, stateEnum);
                        loco.SetFunctionState(functionNum, stateEnum);
                        return new MsgFunctionState(connection, data, functionNum, stateEnum);
                    }
                }
            }
        }
        return new MsgIgnore();
    }

    // Forces a function to a given state regardless of momentary or Latching. 
    // -----------------------------------------------------------------------
    private IThrottleMsg ForceFunctionState(MultiThrottleMessage data, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        if (!string.IsNullOrEmpty(data.ActionData) && Enum.TryParse(data.ActionData[0].ToString(), out FunctionStateEnum stateEnum)) {
            if (byte.TryParse(data.ActionData[1..], out var functionNum)) {
                cmdHelper.SetFunctionState(loco.Address, functionNum, stateEnum);
                loco.SetFunctionState(functionNum, stateEnum);
                return new MsgFunctionState(connection, data, functionNum, stateEnum);
            }
        }
        return new MsgIgnore();
    }

    private IThrottleMsg SetSpeedSteps(MultiThrottleMessage data, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
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
        return new MsgString(data.Message); // Just return the original message
    }

    private IThrottleMsg SetMomentaryState(MultiThrottleMessage data, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        var stateStr    = data.ActionData[0].ToString();
        var functionStr = data.ActionData[1..];
        byte.TryParse(stateStr, out var state);
        byte.TryParse(functionStr, out var function);
        loco.SetMomentaryState(function, state == 0 ? MomentaryStateEnum.Latching : MomentaryStateEnum.Momentary);
        return new MsgString(data.Message); // Just return the original message
    }

    private IThrottleMsg QueryValue(MultiThrottleMessage data, AssignedLoco loco, LayoutCmdHelper cmdHelper) {
        return data.ActionData switch {
            "V" => new MsgString($"{data.Action}{data.Group}{data.Function}{data.Address}<;>V{loco.Speed.Value}"),
            "D" => new MsgString($"{data.Action}{data.Group}{data.Function}{data.Address}<;>V{(loco.Direction == DCCDirection.Forward ? 1 : 0)}"),
            "S" => new MsgString($"{data.Action}{data.Group}{data.Function}{data.Address}<;>V{ConvertSpeedSteps(loco.SpeedSteps)}"),
            "M" => new MsgIgnore(), // Not sure what the format of this should be
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