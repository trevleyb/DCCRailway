﻿using System.Diagnostics;
using System.Text.Json.Serialization;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration.Entities.Base;
using DCCRailway.Layout.Configuration.Entities.System;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
[DebuggerDisplay("TURNOUT={Id}, Name: {Name}")]
public class Turnout(string id = "") : BaseEntityDecoder(id, DCCAddressType.Turnout) {

    private bool            _isManual;
    private bool            _isReversed;
    private bool            _resetOnPowerOn;
    private DCCTurnoutState _initialState;
    private DCCTurnoutState _currentState;
    private DCCTurnoutType  _type;

    public bool IsManual         { get => _isManual;            set => SetField(ref _isManual, value); }
    public bool IsReversed       { get => _isReversed;          set => SetField(ref _isReversed, value); }
    public bool ResetOnPowerOn   { get => _resetOnPowerOn;      set => SetField(ref _resetOnPowerOn, value); }

    public DCCTurnoutState InitialState { get => _initialState; set => SetField(ref _initialState, value); }
    public DCCTurnoutState CurrentState { get => _currentState; set => SetField(ref _currentState, value); }
    public DCCTurnoutType Type          { get => _type;         set => SetField(ref _type, value); }
}