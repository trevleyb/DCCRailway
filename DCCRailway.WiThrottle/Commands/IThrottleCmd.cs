namespace DCCRailway.WiThrottle.Commands;

public interface IThrottleCmd {
    void Execute(string commandStr);
}