using DCCRailway.Common.Types;

namespace DCCRailway.WiThrottle;

public class AssignedLoco {
    public Connection Connection { get; set; }
    public char       Group      { get; set; }
    public DCCAddress Address    { get; set; }
}