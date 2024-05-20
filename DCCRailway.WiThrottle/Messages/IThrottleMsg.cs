namespace DCCRailway.WiThrottle.Messages;

public interface IThrottleMsg {
    bool   IsValid { get; }
    string Message { get; }
}