using DCCRailway.Common.Types;

namespace DCCRailway.WiThrottle;

public class WiThrottleAssignedLoco {
    public WiThrottleConnection Connection { get; set; }
    public char Group { get; set; }
    public DCCAddress Address { get; set; }
}