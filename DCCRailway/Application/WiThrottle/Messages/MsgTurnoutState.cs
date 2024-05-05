using System.Text;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.LayoutService.Layout.Entities;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgTurnoutState(WiThrottleConnection connection, Turnout? turnout) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var sb = new StringBuilder();
            if (turnout != null) {
                /*
                 * TODO: Need to get this from the STATE service

                var stateCode = turnout.CurrentState switch {
                    DCCTurnoutState.Closed => '2',
                    DCCTurnoutState.Thrown => '4',
                    _                      => '1'
                };
                sb.AppendLine($"PTA{stateCode}{turnout.Id}");
                */
            }
            return sb.ToString();
        }
    }
    public override string ToString() => $"MSG:TurnoutState [{connection?.ToString() ?? ""}]=>{Terminators.ForDisplay(Message)}";

}