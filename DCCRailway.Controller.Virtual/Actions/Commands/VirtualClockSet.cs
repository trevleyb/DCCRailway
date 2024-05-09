using System;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Exceptions;
using DCCRailway.Controller.Virtual.Actions.Results;
using DCCRailway.Controller.Virtual.Adapters;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("SetClock", "Set the Clock on a Virtual commandStation")]
public class VirtualClockSet : VirtualCommand, ICmdClockSet, ICommand {
    protected override ICmdResult Execute(IAdapter adapter) {
        var result = new VirtualCmdResultClock(Hour, Minute, Ratio);
        if (adapter is VirtualAdapter virtualAdapter) {
            virtualAdapter.FastClockSetTime = result.CurrentTime;
            virtualAdapter.FastClockRatio   = result.Ratio;
            virtualAdapter.FastClockState   = true;
        }
        return new VirtualCmdResultClock(Hour, Minute, Ratio);
    }

    private int  _hour = 12;
    private bool _is24Hour;
    private int  _minute;
    private int  _ratio = 1;

    public int Hour {
        set {
            if (value <= 0 || value > 24) throw new ValidationException("Hour must be in the range of 1..24");
            _hour = value;
        }
        get => _hour;
    }

    public int Minute {
        set {
            if (value < 0 || value > 59) throw new ValidationException("Minute must be in the range of 0..59");
            _minute = value;
        }
        get => _minute;
    }

    public DateTime ClockTime => new DateTime(DateTime.Now.Year,
                                              DateTime.Now.Month,
                                              DateTime.Now.Day,
                                              Hour, Minute, 0);


    public bool Is24Hour {
        set => _is24Hour = value;
    }

    public int Ratio {
        set {
            if (value <= 0 || value > 15) throw new ValidationException("Ratio must be in the range of 1..15 (1:1 ... 1:15)");
            _ratio = value;
        }
        get => _ratio;
    }

    public override string ToString() => $"SET CLOCK ({_hour:D2}:{_minute:D2}@{_ratio}:15";
}