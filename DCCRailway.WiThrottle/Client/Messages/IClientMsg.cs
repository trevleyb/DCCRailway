namespace DCCRailway.WiThrottle.Client.Messages;

public interface IClientMsg {
    void Process(string commandStr);
}