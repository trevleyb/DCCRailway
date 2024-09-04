using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using DCCRailway.Common.Entities.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.Common.Entities;

[Serializable]
[DebuggerDisplay("TURNOUT={Id}, Name: {Name}")]
public partial class Turnout(string id = "") : LayoutEntityDecoder(id, DCCAddressType.Turnout) {
    [ObservableProperty] private DCCTurnoutState _currentState;
    [ObservableProperty] private DCCTurnoutState _initialState;
    [ObservableProperty] private bool            _isManual;
    [ObservableProperty] private bool            _isReversed;
    [ObservableProperty] private bool            _resetOnPowerOn;
    [ObservableProperty] private DCCTurnoutType  _type;
}