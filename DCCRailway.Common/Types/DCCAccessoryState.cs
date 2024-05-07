namespace DCCRailway.Common.Types;

public enum DCCAccessoryState {
    On       = 0x1,
    Off      = 0x0,
    Active   = On,
    Inactive = Off,
    Normal   = Off,
    Reversed = On,
    Thrown   = On,      // For Turnout: Is Diverging Route
    Closed   = Off      // For Turnout: Is Straight Route
}