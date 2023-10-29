using System;
using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Exceptions;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

[Command("SetClock", "Set the Clock on a NCE controller")]
public class NCESetClock : NCECommand, ICmdClockSet, ICommand {
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

    public override IResult Execute(IAdapter adapter) {
        if (adapter == null) throw new ArgumentNullException("adapter", "The adapter connot be null.");

        // Tell the NCE System to set the Clock to 24 hours mode
        // ; 0x85 xx xx    Set clock hr / min(1)
        // ; 0x86 xx Set clock 12 / 24(1) 0 = 12 hr 1 = 24 hr
        // ; 0x87 xx Set clock ratio(1) 
        // -----------------------------------------------------------------------------------------
        IResult result;

        if ((result = SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x86, (byte)(_is24Hour ? 00 : 01) })).OK) {
            if ((result = SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x85, (byte)_hour, (byte)_minute })).OK) {
                if ((result = SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x87, (byte)_ratio })).OK) {
                    return new ResultOK();
                }
            }
        }

        return result;
    }

    public override string ToString() => $"SET CLOCK ({_hour:D2}:{_minute:D2}@{_ratio}:15";
}