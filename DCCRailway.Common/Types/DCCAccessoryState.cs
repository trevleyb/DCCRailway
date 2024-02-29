namespace DCCRailway.Common.Types;

public enum DCCAccessoryState {
    On       = 0x1,
    Off      = 0x0,
    Normal   = On,
    Reversed = Off,
    Thrown   = On,
    Closed   = Off
}