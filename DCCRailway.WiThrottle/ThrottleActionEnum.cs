namespace DCCRailway.WiThrottle;

public enum ThrottleActionEnum {
    SetLeadLocoByAddress = 'C',
    SetLeadLocoByName    = 'c',
    SetSpeed             = 'V',
    SendIdle             = 'I',
    SendEmergencyStop    = 'X',
    SetDirection         = 'R',
    SetFunctionState     = 'F',
    ForceFunctionState   = 'f',
    SetSpeedSteps        = 's',
    SetMomentaryState    = 'm',
    QueryValue           = 'q',
    UnknownAction        = '*'
}