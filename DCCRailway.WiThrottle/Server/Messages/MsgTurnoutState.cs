using System.Text;
using DCCRailway.Common.Entities;
using DCCRailway.Common.Types;

namespace DCCRailway.WiThrottle.Server.Messages;

public class MsgTurnoutState(Connection connection, string id, DCCTurnoutState state) : ThrottleMsg, IThrottleMsg {
    public MsgTurnoutState(Connection connection, Turnout turnout) : this(connection, turnout.Id, turnout.CurrentState) { }

    // Protocol is not 100% clear on if we send a 2/4/1 or a T/C (example shows T/C, document say 2/1)
    public override string Message {
        get {
            var sb = new StringBuilder();
            var stateCode = state switch {
                DCCTurnoutState.Closed => '2',
                DCCTurnoutState.Thrown => '4',
                _                      => '1'
            };

            sb.AppendLine($"PTA{stateCode}{id}");
            return sb.ToString();
        }
    }

    public override string ToString() {
        return $"MSG:TurnoutState [{connection?.ToString() ?? ""}]";
    }
}