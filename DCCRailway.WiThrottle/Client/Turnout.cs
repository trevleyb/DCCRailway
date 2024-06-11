using DCCRailway.Common.Types;

namespace DCCRailway.WiThrottle.Client;

public class Turnout(string systemName, string userName, DCCTurnoutState stateEnum) {
    public string          Name     { get; }      = systemName;
    public string          UserName { get; }      = userName;
    public DCCTurnoutState State    { get; set; } = stateEnum;
}