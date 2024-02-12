using System;
using DCCRailway.System.Types;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.Simulator;

/// <summary>
///     Command Station Simulator. This is designed to attempt to act like a
///     command station might. It will respond to most commands, will track what commands
///     have been issued (to return mostly true information) and will track state as/when it
///     can. This can then be attached to a VirtualAdapter to act as a map between
///     the commands that this simulator will accept and the commands of the true
///     command station.
/// </summary>
/// TODO: Need to add checks such as READCV only on Programming Track etc.
/// TODO: While we access a LocoEntry here, this should move to be part of a global list (runtime real)
public class DCCSimulator {
    private readonly SimulatedLocoList _locoList = new();
    private          DateTime?         _clockSetAt;

    private int  _hour;
    private bool _mainTrackSelected = true;
    private int  _min;

    private DCCPowerState _powerState = DCCPowerState.Unknown;
    private bool          _progTrackSelected;
    private int           _ratio;

    #region Power On/Off and Main Track/Prog Track operations
    public object? SetPowerState(DCCPowerState state) {
        _powerState = state;

        return null;
    }

    public object? SetPowerState(bool state) {
        _powerState = state ? DCCPowerState.On : DCCPowerState.Off;

        return null;
    }

    public DCCPowerState GetPowerState() => _powerState;

    public object? SetProgTrack() {
        _mainTrackSelected = false;
        _progTrackSelected = true;

        return null;
    }

    public object? SetMainTrack() {
        _mainTrackSelected = true;
        _progTrackSelected = false;

        return null;
    }
    #endregion

    #region Consist Management Functions
    public object? CreateConsist(DCCAddress frontAddress, DCCDirection frontDirection, DCCAddress rearAddress, DCCDirection readDirection) {
        // Find the next available Consist Address
        // ----------------------------------------
        int? consistAddress = null;

        for (var i = 127; i > 3; i--) {
            if (_locoList.GetLoco(i) != null) continue;
            consistAddress = i;

            break;
        }

        if (consistAddress == null) throw new IndexOutOfRangeException("No available Consist Adresses. Cannot create a Consist.");

        //
        // Update the Loco CVs to indicate they are in a Consist and Create a Conists Entry
        // --------------------------------------------------------------------------------
        DCCAddress address = new(consistAddress!.Value, DCCAddressType.Consist);
        _locoList.GetLoco(address); // Actually will add this entry to the list
        _locoList.GetLoco(frontAddress).SetConsist(consistAddress!.Value, frontDirection);
        _locoList.GetLoco(rearAddress).SetConsist(consistAddress!.Value, readDirection);

        return null;
    }

    /// <summary>
    ///     This function will delete a Conist by removing the entry in the LocoList for the Consist but also
    ///     by removing all Locos that are part of the consist.
    /// </summary>
    /// <param name="Address">Either the Conists Address (1..127) or a Loco in the Consist</param>
    public object? KillConsist(DCCAddress Address) {
        var consistAddress = FindConsistAddress(Address);

        /*
        foreach (var entry in _locoList.Values) {
            if (entry.IsInConsist(consistAddress)) {
                entry.ClearConsist();
            }
        }
        _locoList.Remove(consistAddress);
        */
        return null;
    }

    private int FindConsistAddress(int Address) => FindConsistAddress(new DCCAddress(Address));

    private int FindConsistAddress(DCCAddress Address) {
        var entry = _locoList.GetLoco(Address.Address);

        if (entry!.Address.AddressType == DCCAddressType.Consist) return entry.Address.Address;

        return entry.ConsistAddress();
    }

    /// <summary>
    ///     Add a Loco to the specific Consist
    /// </summary>
    public object? AddConsist(DCCAddress consistAddress, DCCAddress locoAddress, DCCDirection direction) {
        _locoList.GetLoco(locoAddress).SetConsist(FindConsistAddress(consistAddress), direction);

        return null;
    }

    public object? AddConsist(int consistAddress, DCCAddress locoAddress, DCCDirection direction) {
        _locoList.GetLoco(locoAddress).SetConsist(FindConsistAddress(consistAddress), direction);

        return null;
    }

    /// <summary>
    ///     Remove the loco from the Conists
    /// </summary>
    public object? DelConsist(DCCAddress address) {
        _locoList.GetLoco(address).ClearConsist();

        return null;
    }
    #endregion

    #region Clock Start/Stop and Set Functions for a Fast Clock
    public object? SetClock(int hour, int min, int ratio = 1) {
        _hour  = hour;
        _min   = min;
        _ratio = ratio;

        return null;
    }

    public string ReadClock() {
        if (_clockSetAt == null) return "12:00";

        return $"{CalculateTimeDifference().hour:D2}:{CalculateTimeDifference().min:D2}";
    }

    public object? StartClock() {
        _clockSetAt = DateTime.Now;

        return null;
    }

    public object? StopClock() {
        _hour       = CalculateTimeDifference().hour;
        _min        = CalculateTimeDifference().min;
        _clockSetAt = null;

        return null;
    }

    private (int hour, int min) CalculateTimeDifference() {
        if (_clockSetAt != null) {
            //TimeSpan duration = new(_hour, _min, 0);
            var newTime = _clockSetAt!.Value.AddSeconds((DateTime.Now - _clockSetAt!.Value).TotalSeconds * _ratio);

            return (newTime.Hour, newTime.Second);
        }

        return (12, 0);
    }
    #endregion

    #region Read and Write a CV
    public object? WriteCV(DCCAddress address, int cv, byte value) {
        if (!_progTrackSelected || _mainTrackSelected) throw new InvalidOperationException("Cannot Read/Write CV unless in Programming Mode.");
        _locoList.GetLoco(address)[cv] = value;

        return null;
    }

    public object? WriteCV(int cv, byte value) {
        if (!_progTrackSelected || _mainTrackSelected) throw new InvalidOperationException("Cannot Read/Write CV unless in Programming Mode.");
        var loco                   = _locoList.GetRandomLoco();
        if (loco != null) loco[cv] = value;

        return null;
    }

    public byte ReadCV(int cv) {
        if (!_progTrackSelected || _mainTrackSelected) throw new InvalidOperationException("Cannot Read/Write CV unless in Programming Mode.");
        var loco = _locoList.GetRandomLoco();

        if (loco != null) return loco[cv];

        return 0;
    }

    public byte ReadCV(DCCAddress address, int cv) {
        if (!_progTrackSelected || _mainTrackSelected) throw new InvalidOperationException("Cannot Read/Write CV unless in Programming Mode.");

        return _locoList.GetLoco(address)[cv];
    }
    #endregion

    #region Programming Functions
    public object? AccyOpsProgramming(DCCAddress address, int cv, byte value) {
        var entry = _locoList.GetLoco(address);
        entry[cv] = value;

        return null;
    }

    public object? LocoOpsProgramming(DCCAddress address, int cv, byte value) {
        var entry = _locoList.GetLoco(address);
        entry[cv] = value;

        return null;
    }

    public object? SetSignalAspect(DCCAddress address, byte aspect) {
        var entry = _locoList.GetLoco(address);
        entry.SignalAspect = aspect;

        return null;
    }

    public object? SetAccyState(DCCAddress address, DCCAccessoryState state) {
        var entry = _locoList.GetLoco(address);
        entry.State = state;

        return null;
    }
    #endregion

    #region Misc Functions
    public object? DoNothing() => null;

    public string GetStatus() => "1.0.0";

    public object? RunMacro(int macro) => macro > 0 ? null : null;
    #endregion

    #region Control the Locomotive Direction and Speed
    public object? StopLoco(DCCAddress address) {
        var entry = _locoList.GetLoco(address);
        entry.Speed = 0;

        return null;
    }

    public object? SetLocoFunctions(DCCAddress address, DCCFunctionBlocks functionBlock) {
        var entry = _locoList.GetLoco(address);
        entry.Functions = new DCCFunctionBlocks(functionBlock);

        return null;
    }

    public object? SetLocoMomentum(DCCAddress address, byte momentum) {
        var entry = _locoList.GetLoco(address);
        entry[3] = momentum;
        entry[4] = momentum;

        return null;
    }

    public object? SetLocoSpeed(DCCAddress address, byte speed, DCCDirection direction) {
        var entry = _locoList.GetLoco(address);
        entry.Speed     = speed;
        entry.Direction = direction;

        return null;
    }

    public object? SetLocoSpeedSteps(DCCAddress address, DCCProtocol steps) {
        var entry = _locoList.GetLoco(address);

        if (steps == DCCProtocol.DCC14) {
            entry[29] = entry[29].SetBit(2, false);
        } else {
            entry[29] = entry[29].SetBit(2, true);
        }

        return null;
    }
    #endregion
}