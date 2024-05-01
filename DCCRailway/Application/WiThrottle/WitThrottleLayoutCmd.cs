using DCCRailway.Common.Types;
using DCCRailway.Station.Controllers;

namespace DCCRailway.Application.WiThrottle;

public class WitThrottleLayoutCmd {

    public WitThrottleLayoutCmd(IController controller, DCCAddress address) {}

    public void Stop() { }
    public void Release() { }
    public void Dispatch() { }
}