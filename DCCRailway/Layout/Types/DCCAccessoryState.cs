namespace DCCRailway.System.Layout.Types;

public enum DCCAccessoryState {
    On       = 01,
    Off      = 00,
    Normal   = On,
    Reversed = Off,
    Thrown   = On,
    Closed   = Off
}