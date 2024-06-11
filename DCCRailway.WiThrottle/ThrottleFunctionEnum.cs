namespace DCCRailway.WiThrottle;

public enum ThrottleFunctionEnum {
    AcquireLoco          = '+',
    ReleaseLoco          = '-',
    StealLoco            = 'S',
    ProvideLocoFunctions = 'L',
    PerformLocoAction    = 'A',
    UnknownFunction      = '*'
}