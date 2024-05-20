﻿using System.Diagnostics;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Entities.Base;

namespace DCCRailway.Layout.Entities;

[Serializable, DebuggerDisplay("TURNOUT={Id}, Name: {Name}")]
public class Turnout : LayoutEntityDecoder {
    private DCCTurnoutState _currentState;
    private DCCTurnoutState _initialState;

    private bool           _isManual;
    private bool           _isReversed;
    private bool           _resetOnPowerOn;
    private DCCTurnoutType _type;
    public Turnout(string id = "") : base(id) { }

    public bool IsManual {
        get => _isManual;
        set => SetField(ref _isManual, value);
    }

    public bool IsReversed {
        get => _isReversed;
        set => SetField(ref _isReversed, value);
    }

    public bool ResetOnPowerOn {
        get => _resetOnPowerOn;
        set => SetField(ref _resetOnPowerOn, value);
    }

    public DCCTurnoutState InitialState {
        get => _initialState;
        set => SetField(ref _initialState, value);
    }

    public DCCTurnoutState CurrentState {
        get => _currentState;
        set => SetField(ref _currentState, value);
    }

    public DCCTurnoutType Type {
        get => _type;
        set => SetField(ref _type, value);
    }
}