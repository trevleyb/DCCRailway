using System.Text;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Entities;

namespace DCCRailway.WiThrottle.Messages;

public class MsgTurnoutState : ThrottleMsg, IThrottleMsg {
    private Connection      _connection;
    private string          _id;
    private DCCTurnoutState _state;
    private Turnout?        _turnout;

    public MsgTurnoutState(Connection connection, Turnout turnout) {
        _connection = connection;
        _id         = turnout.Id;
        _state      = turnout.CurrentState;
    }

    public MsgTurnoutState(Connection connection, string id, DCCTurnoutState state) {
        _connection = connection;
        _id         = id;
        _state      = state;
        _turnout    = null;
    }

    // Protocol is not 100% clear on if we send a 2/4/1 or a T/C (example shows T/C, document say 2/1)
    public override string Message {
        get {
            var sb = new StringBuilder();
            var stateCode = _state switch {
                DCCTurnoutState.Closed => '2',
                DCCTurnoutState.Thrown => '4',
                _                      => '1'
            };

            sb.AppendLine($"PTA{stateCode}{_id}");
            return sb.ToString();
        }
    }

    public override string ToString() {
        return $"MSG:TurnoutState [{_connection?.ToString() ?? ""}]";
    }
}