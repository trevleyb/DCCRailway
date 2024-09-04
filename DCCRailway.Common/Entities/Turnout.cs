using System.Diagnostics;
using DCCRailway.Common.Entities.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.Common.Entities;

[Serializable]
[DebuggerDisplay("TURNOUT={Id}, Name: {Name}")]
public class Turnout(string id = "") : LayoutEntityDecoder(id) {
    private DCCTurnoutState _currentState;
    private DCCTurnoutState _initialState;

    private bool           _isManual;
    private bool           _isReversed;
    private bool           _resetOnPowerOn;
    private DCCTurnoutType _type;

    public new DCCAddress Address {
        get => base.Address;
        set {
            base.Address             = value;
            base.Address.AddressType = DCCAddressType.Accessory;
        }
    }

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