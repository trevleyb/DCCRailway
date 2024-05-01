namespace DCCRailway.Application.WiThrottle.Messages;

public interface IThrottleMsg {
    bool IsValid { get; }
    string Message { get; }
}