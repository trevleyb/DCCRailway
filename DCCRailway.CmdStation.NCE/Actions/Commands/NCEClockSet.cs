using System;
using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Exceptions;
using DCCRailway.CmdStation.NCE.Actions.Validators;

namespace DCCRailway.CmdStation.NCE.Actions.Commands;

[Command("SetClock", "Set the Clock on a NCE controller")]
public class NCEClockSet : NCECommand, ICmdClockSet, ICommand {
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

    public override ICmdResult Execute(IAdapter adapter) {
        if (adapter == null) throw new ArgumentNullException("adapter", "The adapter connot be null.");

        // Tell the NCE Controller to set the Clock to 24 hours mode
        // ; 0x85 xx xx    Set clock hr / min(1)
        // ; 0x86 xx Set clock 12 / 24(1) 0 = 12 hr 1 = 24 hr
        // ; 0x87 xx Set clock ratio(1) 
        // -----------------------------------------------------------------------------------------
        ICmdResult result;

        if ((result = SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x86, (byte)(_is24Hour ? 00 : 01) })).Success)
            if ((result = SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x85, (byte)_hour, (byte)_minute })).Success)
                if ((result = SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0x87, (byte)_ratio })).Success)
                    return result;

        return result;
    }

    public override string ToString() => $"SET CLOCK ({_hour:D2}:{_minute:D2}@{_ratio}:15";
}