namespace DCCRailway.DCCController.Types;

public enum DCCTurnoutState {
    On       = 01,
    Off      = 00,
    Normal   = On,
    Reversed = Off,
    Thrown   = On,
    Closed   = Off
}