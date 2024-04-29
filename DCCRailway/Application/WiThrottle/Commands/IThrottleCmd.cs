using DCCRailway.Application.WiThrottle.Messages;

namespace DCCRailway.Application.WiThrottle.Commands;

public interface IThrottleCmd {
    IServerMsg Execute();
}