namespace DCCRailway.WiThrottle.Server.Commands;

public interface IThrottleCmd {
    void Execute(string commandStr);
}