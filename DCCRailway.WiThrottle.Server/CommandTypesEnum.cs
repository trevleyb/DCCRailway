namespace DCCRailway.WiThrottle;

public enum CommandTypeEnum {
    Name          = 'N',
    Hardware      = 'H',
    MultiThrottle = 'M',
    Panel         = 'P',
    Roster        = 'R',
    Heartbeat     = '*',
    Quit          = 'Q',
    Ignore        = 'X'
}