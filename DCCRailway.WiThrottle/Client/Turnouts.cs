using DCCRailway.Common.Types;

namespace DCCRailway.WiThrottle.Client;

public class Turnouts : List<Turnout> {
    public Turnout Find(string systemName) {
        if (string.IsNullOrEmpty(systemName)) throw new ArgumentNullException(nameof(systemName));
        if (!Exists(t => t.Name == systemName)) Add(systemName, "Unknown", "1");
        return this.First(t => t.Name == systemName);
    }

    public void Add(string systemName, string userName, string state) {
        //State is represented by 1 (unknown), 2 (closed), or 4 (thrown).
        var stateEnum = state switch {
            "1" => DCCTurnoutState.Unknown,
            "2" => DCCTurnoutState.Closed,
            "4" => DCCTurnoutState.Thrown,
            _   => DCCTurnoutState.Unknown
        };

        Add(new Turnout(systemName, userName, stateEnum));
    }
}